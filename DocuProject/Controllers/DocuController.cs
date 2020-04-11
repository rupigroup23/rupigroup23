using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Hosting;
using DocuProject.Models;
using System.Web.Configuration;
using System.Data;
using System.Text;

namespace DocuProject.Controllers
{
    public class DocuController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}


        [HttpPost] /// דף כיתה
        [Route("api/Docu/postClass")]
        public void Post([FromBody] Class classObj)
        {
            Class C = new Class();
            C.insert(classObj);
        }
        //shir
        [HttpPost] /// דף כיתה
        [Route("api/Docu/postClassSub")]
        public void Post([FromBody] List<ClassSubjects> classSUbObj)
        {
            ClassSubjects C = new ClassSubjects();
            C.insertClassSub(classSUbObj);
        }
        //shirend

        [HttpPost] /// דף מורה
        [Route("api/Docu/postTeach")]
        public void Post([FromBody] Teacher TeacherObj)
        {
            Teacher T = new Teacher();
            T.insertT(TeacherObj);
        }

        [HttpGet] /// דף מורה - שדה מקצוע
        [Route("api/Docu/GetP")]
        public List<Profession> GetP()
        {
            Profession P = new Profession();
            return P.Read();
        }

        [HttpPost] /// דף מורה 
        [Route("api/Docu/addProff")]
        public void Post([FromBody] Profession proffObj)
        {
            Profession P = new Profession();
            P.insertP(proffObj);
        }

        [HttpPost] /// דף כיתה- אקסל תלמידים
        [Route("api/Docu/addStudents")]
        public void Post([FromBody]List<Student> studentsArr)
        {
            Student S = new Student();
            S.insertS(studentsArr);
        }

        [HttpPost] /// דף מורה - אקסל מורים
        [Route("api/Docu/postTeach2")]
        public void Post([FromBody]List<Teacher> teachersArr)
        {
            Teacher T = new Teacher();
            T.insertT2(teachersArr);
        }
        [HttpGet]  /// דף כיתה 
        [Route("api/Docu/GetT")]
        public List<Teacher> GetT()
        {
            Teacher T = new Teacher();
            return T.Read();
        }

        [HttpPost] //// דף כניסה למערכת
        [Route("api/Docu/checkUsers")]
        public Admin Post([FromBody] Admin admin) // מקבלת מערך של אובייקטים- מה שהכנתי
        {
            Admin A = new Admin();
            return A.CheckUser(admin);
        }

        [HttpGet]
        [Route("api/Docu/getNumClass")]
        public DataTable GetClass()
        {
            Class C = new Class();
            return C.GetNumClass();
        }


        [HttpPost] // ניהול תלמידים - קבלת טבלת תלמידים
        [Route("api/Docu/GetStudents")]
        public DataTable GetS([FromBody]Student studentObj)
        {
            return studentObj.GetStudents();
        }
       
        [HttpGet]
        [Route("api/Docu/getTeachers")]
        public DataTable getTeachers()
        {
            Teacher T = new Teacher();
            return T.GetTechers();
        }

        //[HttpGet]  /// דף כיתה 
        //[Route("api/Docu/GetClassSubj")]
        //public List<ClassSubjects> GetCS([FromBody]ClassSubjects CSObj)
        //{
        //    return CSObj.ReadCS();
        //}
        // GET api/Docu/GetClassSubj/ז/2/
        [HttpGet]
        [Route("api/Docu/GetClassSubj/{name}/{num}")]
        public List<ClassSubjects> GetCS(string name, string num)
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.ReadCS(name, num); // Read from Models/Counrty
        }
        public DataTable Delete(int id)
        {
            Student S = new Student();
            return S.deleteS(id);
        }

        [HttpPost] ///  הכנסת תלמיד ספציפי
        [Route("api/Docu/postStudent")]
        public void Post([FromBody] Student StudentObj)
        {
            Student S2 = new Student();
            S2.insertS2(StudentObj);
        }

        [HttpPost] //שלב1- העלת התמונה לתוכנה
        [Route("api/Docu/uploadimage")]
        public HttpResponseMessage Post()
        {
            List<string> imageLinks = new List<string>();
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];

                    // this is an example of how you can extract addional values from the Ajax call
                    string name = httpContext.Request.Form["name"];

                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);
                        string fname = httpPostedFile.FileName.Split('\\').Last(); // שומר את השם של התמונה
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), fname); // ישמור את התמונה בתוך התיקיה אפלודפיילס שיצרנו
                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath);
                        imageLinks.Add("uploadedFiles/" + fname);
                    }
                }
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, imageLinks); // שולח את הניתוב בחזרה לפונקציית ההצלחה בדף האינדקס, לשלב 2
        }

        [HttpPost] ///  שלב 2- הכנסה לדטא
        [Route("api/Docu/uploadUrlImg")]
        public void Post([FromBody] ImgStudent StudentImage)
        {
            ImgStudent pic = new ImgStudent();
            pic.insertPic(StudentImage);
        }

    }
}