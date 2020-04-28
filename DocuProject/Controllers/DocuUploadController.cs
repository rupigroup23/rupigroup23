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
    public class DocuUploadController : ApiController
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

        [HttpPost] //שלב1- העלת קובץ ושמירה בתיקייה
        [Route("Api/DocuUpload/uploadtask")]
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

      
    }
}