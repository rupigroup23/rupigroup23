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
    public class StudentController : ApiController
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

        [HttpPost] // ׳ ׳™׳”׳•׳ ׳×׳׳׳™׳“׳™׳ - ׳§׳‘׳׳× ׳˜׳‘׳׳× ׳×׳׳׳™׳“׳™׳
        [Route("api/Student/GetStudents")]
        public DataTable GetS([FromBody]Student studentObj)
        {
            return studentObj.GetStudents();
        }

        [HttpGet]
        [Route("api/Student/GetTasks/{name}/{num}/{prof}")]
        public List<Task> GetTask(string name, string num, string prof)
        {
            Task T = new Task();
            return T.ReadTask(name, num, prof); // Read from Models/Counrty
        }

        [HttpGet]
        [Route("api/Student/specifictask/{class1}/{numClass}/{sub}/{topic}")]
        public string getSpecificTask(string class1, string numClass, string sub, string topic)
        {
            Task task = new Task();
            return task.getSpecificTask(class1, numClass, sub, topic);
        }

        [HttpGet]
        [Route("api/Student/GetDataTask/{name}/{num}/{IdTask}")]
        public List<Group_Feedback> GetDT(string name, int num, int IdTask)
        {
            Group_Feedback DT = new Group_Feedback();
            return DT.ReadDT(name, num, IdTask); // Read from Models/Counrty
        }
        
        //נוי
        [HttpPost] /// דף כניסה- במידה ומנהל
        [Route("api/Student/checkUsers")]
        public Student Post([FromBody] Student student) // מקבלת מערך של אובייקטים- מה שהכנתי
        {
            Student S = new Student();
            return S.CheckUser(student);
        }


        [HttpGet] //דף הבית
        [Route("api/Student/GetClassSubj/{name}/{num}")]
        public List<ClassSubjects> GetCS(string name, string num)
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.ReadCS(name, num); // Read from Models/Counrty
        }

        //Setting
        [HttpGet]
        [Route("api/Student/GetDetailsS/{Id}")]
        public DataTable GetDIS(int Id)
        {
            Student S = new Student();
            return S.GetDetails(Id);
        }

        //pic
        [HttpPost] //שלב1- העלת התמונה לתוכנה
        [Route("api/Student/uploadimage")]
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
        [Route("api/Student/uploadUrlImg2")]
        public void Post([FromBody] ImgStudent img)
        {
            ImgStudent pic = new ImgStudent();
            pic.insertPic(img);
        }


        [HttpGet] //get pic
        [Route("api/Student/getavatar/{Id}")]
        public string Get(string Id)
        {
            ImgStudent i = new ImgStudent();
            return i.getAvatarImage(Id);
        }

        [HttpPut] //update setting
        [Route("api/Student/updateStudent")]
        public DataTable PutA([FromBody] Student student)
        {
            Student S = new Student();
            return S.Put_S(student.Id, student);
        }

    }

}
      
