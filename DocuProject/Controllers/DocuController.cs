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

        // DELETE api/values/5
        public void Delete(int id)
        {
        }


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

    }
}
