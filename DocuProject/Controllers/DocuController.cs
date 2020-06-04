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

        [HttpPost] /// דף כיתה
        [Route("api/Docu/postGroup")]
        public void postGroup([FromBody] Group_Feedback StudentObj)
        {
            Group_Feedback C = new Group_Feedback();
            C.insert(StudentObj);
        }

        [HttpPost] /// דף כיתה
        [Route("api/Docu/postClassSub")]
        public void Post([FromBody] List<ClassSubjects> classSUbObj)
        {
            ClassSubjects C = new ClassSubjects();
            C.insertClassSub(classSUbObj);
        }

        [HttpPost]
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

        [HttpGet] /// דף מורה - שדה מקצוע
        [Route("api/Docu/GetClasses")]
        public List<Class> GetClasses()
        {
            Class P = new Class();
            return P.ReadClass();
        }
        [HttpPost]
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
        [HttpGet]
        [Route("api/Docu/GetT")]
        public List<Teacher> GetT()
        {
            Teacher T = new Teacher();
            return T.Read();
        }

        [HttpGet]
        [Route("api/Docu/GetST")]
        public List<Student> GetST()
        {
            Student S = new Student();
                return S.ReadSt();
        }

        [HttpPost] /// דף כניסה- במידה ומנהל
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

       
        // GET api/Docu/GetClassSubj/ז/2/
        [HttpGet]
        [Route("api/Docu/GetClassSubj/{name}/{num}")]
        public List<ClassSubjects> GetCS(string name, string num)
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.ReadCS(name, num); // Read from Models/Counrty
        }
        public DataTable Delete(int id) //תלמיד
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

                    if (httpPostedFile != null) //אם קיים קובץ
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

        [HttpPost] ///  שלב 2- הכנסה לדטא דף יצירת תלמיד ספציפי
        [Route("api/Docu/uploadUrlImg")]
        public void Post([FromBody] ImgStudent StudentImage)
        {
            ImgStudent pic = new ImgStudent();
            pic.insertPic(StudentImage);
        }

        [HttpPost] ///  שלב 2- הכנסה לדטא דף אדמין
        [Route("api/Docu/uploadUrlImg2")]
        public void Post([FromBody] ImgAdmin AdminImage)
        {
            ImgAdmin pic = new ImgAdmin();
            pic.insertPic2(AdminImage);
        }

        [HttpPost] /// דף מטלה ראשי
        [Route("api/Docu/Tasks")]
        public void Post([FromBody] Task taskObj)
        {
            Task T = new Task();
            T.insertTask1(taskObj);
        }
        /// דף מטלה ראשי
        // GET api/Docu/GetClassSubj/ז/2/פיזיקה
        [HttpGet]
        [Route("api/Docu/GetTasks/{name}/{num}/{prof}")]
        public List<Task> GetTask(string name, string num, string prof)
        {
            Task T = new Task();
            return T.ReadTask(name, num, prof); // Read from Models/Counrty
        }

        [HttpPut]
        [Route("api/Docu/updateStudent/{id}")]
        public DataTable PutDIS(int id, [FromBody] Student student)
        {
            Student S = new Student();
            return S.PutS(id, student);
        }

        //Setting
        [HttpGet]
        [Route("api/Docu/GetDetails/{Id}")]
        public DataTable GetDIS(int Id)
        {
            Admin A = new Admin();
            return A.GetDetails(Id);
        }

        [HttpPut]
        [Route("api/Docu/updatetAdmin")]
        public DataTable PutA([FromBody] Admin admin)
        {
            Admin A = new Admin();
            return A.PutA(admin);
        }

        [HttpGet]
        [Route("api/Docu/getavatar/{Id}")]

        public string Get(string Id)
        {
            ImgAdmin i = new ImgAdmin();
            return i.getAvatarImage(Id);
        }

        [HttpPost] /// דף מטלה ראשי
        [Route("api/Docu/PostAdmin")]
        public void PostAdmin([FromBody] Admin admin)
        {
            Admin A = new Admin();
            A.PostAdmin(admin);
        }

        [HttpGet]
        [Route("api/Docu/specifictask/{class1}/{numClass}/{sub}/{topic}")]
        public string getSpecificTask(string class1, string numClass, string sub, string topic)
        {
            Task task = new Task();
            return task.getSpecificTask(class1, numClass, sub, topic);
        }


        [HttpPost] /// דף מטלה ראשי
        [Route("api/Docu/DeleteT/{rowID}")]
        public DataTable Delete_T(int rowID) //מורה 
        {
            Teacher T = new Teacher();
            return T.deleteT(rowID);
        }

        [HttpPost] /// דף מטלה ראשי
        [Route("api/Docu/DeleteTeachFromCS/{TeacherId}/{TeacherSubj}")]
        public DataTable DeleteTeachFromCS(int TeacherId, string TeacherSubj) //מורה 
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.deleteT_CS(TeacherId, TeacherSubj);
        }


        [HttpPut] //teacher
        [Route("api/Docu/updateTeacher/{id}")]
        public DataTable PutT(int id, [FromBody] Teacher teacher)
        {
            Teacher T = new Teacher();
            return T.PutT(id, teacher);
        }

        [HttpGet]
        [Route("api/Docu/getvideo/{ClassName}/{ClassNum}/{Professtion}/{Topic}/{Deadline}")]
        public List<Task> getVideos(string ClassName, string ClassNum, string Professtion, string Topic, string Deadline)
        {
            Task t = new Task();
            List<Task> videos = t.getVideos(ClassName, ClassNum, Professtion, Topic, Deadline);
            return videos;
        }

        //submission page
        [HttpPost]
        [Route("api/Docu/GetTeamData")]
        public DataTable GetTeamData([FromBody]Group_Feedback teamObj)
        {
            return teamObj.GetTeamData();
        }


        [HttpPut]
        [Route("api/Docu/updateFBG/{id}")]
        public DataTable PutVC(int id, [FromBody] Group_Feedback videoTeam)
        {
            Group_Feedback VC = new Group_Feedback();
            return VC.PutVC(id, videoTeam);
        }

        [HttpGet]
        [Route("api/Docu/GetSudentsbyGrade/{name}/{num}/{prof}/{grade}")]
        public List<string> GetSudentsbyGrade(string name, int num, string prof, string grade)
        {
            Student G = new Student();
            return G.GetSbyGrade1(name, num, prof, grade); // Read from Models/Counrty
        }
        [HttpPost] //אלגוריתם חכם 
        [Route("api/Docu/GetStudentsAlgoritem/{radioChoose}")]
        public List<string> Get_S([FromBody]Student studentObj, string radioChoose)
        {
            return studentObj.GetStudentsAlgoritem(radioChoose);
        }

        // עריכת מטלה - נוי 
        [HttpPut]
        [Route("api/Docu/updateTask/{class1}/{numClass}/{sub}/{topic}")]
        public DataTable PutTask(string class1, string numClass, string sub, string topic, [FromBody] Task taskNew)
        {
            Task T = new Task();
            return T.putT(class1, numClass, sub, topic, taskNew);
        }

        //מחיקת מטלה - נוי 
        [HttpDelete] 
        [Route("api/Docu/deleteTask/{class1}/{numClass}/{sub}/{topic}")]
        public int deleteTask(string class1, string numClass, string sub, string topic) //מורה 
        {
            Task T = new Task();
            return T.deleteTask(class1, numClass, sub, topic);
        }

    }
}