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
        public void Delete(int id)
        {
        }
        //Setting teacher
        [HttpGet]
        [Route("api/Teacher/GetDetailsT/{Id}")]
        public DataTable GetT(int Id)
        {
            Teacher T = new Teacher();
            return T.GetDetails(Id);
        }

        [HttpPut]
        [Route("api/Teacher/updatetTeacher")]
        public DataTable PutA([FromBody] Teacher teacher)
        {
            Teacher T = new Teacher();
            return T.PutT(teacher);
        }

        [HttpPost] ///  שלב 2- הכנסה לדטא דף אדמין
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
    }
}