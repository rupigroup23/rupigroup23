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
        [Route("api/Student/GetClassSubj/{name}/{num}")] // מביא את המקצועות של הכיתה לדף הראשי
        public List<ClassSubjects> GetCS(string name, string num)
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.ReadCS(name, num); // Read from Models/Counrty
        }

        [HttpPost] // ניהול תלמידים - קבלת טבלת תלמידים
        [Route("api/Student/GetTeamData")]
        public DataTable GetTeamData([FromBody]Group_Feedback teamObj)
        {
            return teamObj.GetTeamData();
        }

        [HttpPut]
        [Route("api/Student/updateStudent/{id}")]
        public DataTable PutVC(int id, [FromBody] Group_Feedback videoTeam)
        {
            Group_Feedback VC = new Group_Feedback();
            return VC.PutVC(id, videoTeam);
        }

        [HttpPost] //שלב1- העלת קובץ ושמירה בתיקייה
        [Route("Api/StudentUpload/uploadtask")]
        public HttpResponseMessage Post()
        {
            List<string> taskLinks = new List<string>(); // יוצרים רשימה שמחזיקה את הניתובים
            var httpContext = HttpContext.Current; // יוצרים איבר שמחזיק את הבקשה 

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0) // בודקת אם הגיע הקובץ בכלל לקונטרולר,יכולים להגיע כמה קבצים 
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++) // במידה ושלחו כמה קבצים 
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i]; // יוצרים קובץ ומכניסים אליו את הקובץ הראשון במערך 

                    // this is an example of how you can extract addional values from the Ajax call
                    string name = httpContext.Request.Form["name"]; // מי העלה את הקובץ

                    if (httpPostedFile != null) // בודקים אם הקובץ נכנס
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);
                        string fname = httpPostedFile.FileName.Split('\\').Last(); // שומר את השם של הקובץ
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), fname); // ישמור את הקובץ בתוך התיקיה אפלודפיילס שיצרנו
                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath); // שומר בתקיה
                        taskLinks.Add("uploadedFiles/" + fname); // מכניס את הניתוב לרשימת לינקים שיצרנו בהתחלה,
                    }
                }
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, taskLinks); // שולח את הניתוב בחזרה לפונקציית ההצלחה בדף האינדקס, 
        }

        //נוי
        [HttpPost] /// דף כניסה- במידה ומנהל
        [Route("api/Student/checkUsers")]
        public Student Post([FromBody] Student student) // מקבלת מערך של אובייקטים- מה שהכנתי
        {
            Student S = new Student();
            return S.CheckUser(student);
        }


        //Setting
        [HttpGet]
        [Route("api/Student/GetDetailsS/{Id}")]
        public DataTable GetDIS(int Id)
        {
            Student S = new Student();
            return S.GetDetails(Id);
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

        
    }

}
      
