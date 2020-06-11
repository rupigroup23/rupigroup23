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
    public class TeacherController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}

        [HttpPost] //login teacher
        [Route("api/Teacher/checkUsersTeacher")]
        public Teacher Post2([FromBody] Teacher teacher) // מקבלת מערך של אובייקטים- מה שהכנתי
        {
            Teacher T = new Teacher();
            return T.CheckUser2(teacher);
        }

        [HttpGet]         // קבלת הכיתות הספציפיות שהמורה מלמד
        [Route("api/Teacher/teacherClass/{Id}")]
        public DataTable GetClass(int id)
        {
            ClassSubjects C = new ClassSubjects();
            return C.tacherClass(id);
        }
        //Setting teacher
        [HttpGet]
        [Route("api/Teacher/GetDetailsT/{Id}")]
        public DataTable GetT(int Id)
        {
            Teacher T = new Teacher();
            return T.GetDetails(Id);
        }
        //[HttpGet] /// דף מורה - שדה מקצוע
        //[Route("api/Teacher/GetP")]
        //public List<Profession> GetP()
        //{
        //    Profession P = new Profession();
        //    return P.Read();
        //}

        [HttpPut]
        [Route("api/Teacher/updatetTeacher")]
        public DataTable PutA([FromBody] Teacher teacher)
        {
            Teacher T = new Teacher();
            return T.PutT(teacher);
        }

        [HttpPost] //שלב1- העלת התמונה לתוכנה
        [Route("api/Teacher/uploadimage")]
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

        [HttpPost] ///  שלב 2- הכנסה לדטא דף מורה
        [Route("api/Teacher/uploadUrlImg2")]
        public void Post([FromBody] ImgTeacher img)
        {
            ImgTeacher pic = new ImgTeacher();
            pic.insertPic2(img);
        }

        [HttpGet]
        [Route("api/Teacher/getavatar/{Id}")]

        public string Get(string Id)
        {
            ImgTeacher i = new ImgTeacher();
            return i.getAvatarImage_(Id);
        }

        [HttpPost] /// דף מקצועות מורה
        [Route("api/Teacher/postClassSub")]
        public void Post([FromBody] List<ClassSubjects> classSUbObj)
        {
            ClassSubjects C = new ClassSubjects();
            C.insertClassSub(classSUbObj);
        }
        [HttpGet] //  קבלת המקצועות הספציפיים של המורה 
        [Route("api/Teacher/GetClassSubj_/{name}/{num}/{teachrID}")]
        public List<ClassSubjects> GetCS(string name, string num, int teachrID)
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.ReadCSTeacher(name, num, teachrID); // Read from Models/Counrty
        }

        [HttpPost] // ניהול תלמידים - קבלת טבלת תלמידים
        [Route("api/Teacher/GetStudents")]
        public DataTable GetS([FromBody]Student studentObj)
        {
            return studentObj.GetStudents();
        }

        [HttpPut] // מחיקת תגובה בדף הפידבקים
        [Route("api/Teacher/deleteComment/{selectedRow}/{type}")]
        public void delete(int selectedRow, string type) //מורה 
        {
            Feedback F = new Feedback();
            F.deleteComment(selectedRow, type);
        }

        [HttpGet] //  קבלת המקצועות הספציפיים של המורה 
        [Route("api/Teacher/gatProffDB/{teachrID}")]
        public List<ClassSubjects> get_proff(int teachrID)
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.ReadCS_Teacher(teachrID); // Read from Models/Counrty
        }
        [HttpPut]
        [Route("api/Teacher/updateFBG/{id}")]
        public DataTable PutVC(int id, [FromBody] Group_Feedback videoTeam)
        {
            Group_Feedback VC = new Group_Feedback();
            return VC.PutVC(id, videoTeam);
        }

        [HttpPut]
        [Route("api/Teacher/updateStudent/{id}")]
        public DataTable PutDIS(int id, [FromBody] Student student)
        {
            Student S = new Student();
            return S.PutS(id, student);
        }

        [HttpGet]
        [Route("api/Teacher/specifictask/{class1}/{numClass}/{sub}/{topic}")]
        public string getSpecificTask(string class1, string numClass, string sub, string topic)
        {
            Task task = new Task();
            return task.getSpecificTask(class1, numClass, sub, topic);
        }

        [HttpDelete]
        [Route("api/Teacher/deleteTask/{class1}/{numClass}/{sub}/{topic}")]
        public int deleteTask(string class1, string numClass, string sub, string topic) //מורה 
        {
            Task T = new Task();
            return T.deleteTask(class1, numClass, sub, topic);
        }
    }


}