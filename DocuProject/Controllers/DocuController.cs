using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        [Route("api/Docu/GetDetails/{ID}")]
        public DataTable GetDIS(int ID) // מקבלת שם של טבלה, מחזירה טבלה מהדטה בייס
        {
            Admin A = new Admin();
            return A.GetDetails(ID);
        }

        [HttpPut]
        [Route("api/Docu/updatetAdmin")]
        public DataTable PutA([FromBody] Admin admin)
        {
            Admin A = new Admin();
            return A.PutA(admin);
        }


    }
}