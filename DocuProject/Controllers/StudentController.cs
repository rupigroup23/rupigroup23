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

        [HttpPost] // ניהול תלמידים - קבלת טבלת תלמידים
        [Route("api/Student/GetStudents")]
        public DataTable GetS([FromBody]Student studentObj)
        {
            return studentObj.GetStudents();
        }


        [HttpPost] // אלגוריתם בנים בנות - נוי 
        [Route("api/Student/GetStudentsAlgoritem/{radioChoose}")]
        public List<string> Get_S([FromBody]Student studentObj, string radioChoose)
        {
            return studentObj.GetStudentsAlgoritem(radioChoose);
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
        [Route("api/Student/GetClassSubj/{name}/{num}")] // ���� �� �������� �� ����� ��� �����
        public List<ClassSubjects> GetCS(string name, string num)
        {
            ClassSubjects CS = new ClassSubjects();
            return CS.ReadCS(name, num); // Read from Models/Counrty
        }

        [HttpPost] // ����� ������� - ���� ���� �������
        [Route("api/Student/GetTeamData")]
        public DataTable GetTeamData([FromBody]Group_Feedback teamObj)
        {
            return teamObj.GetTeamData();
        }

        [HttpPut]
        [Route("api/Student/updateVStudent/{id}")]
        public DataTable PutVC(int id, [FromBody] Group_Feedback videoTeam)
        {
            Group_Feedback VC = new Group_Feedback();
            return VC.PutVC(id, videoTeam);
        }

        [HttpPost] //���1- ���� ���� ������ �������
        [Route("Api/StudentUpload/uploadtask")]
        public HttpResponseMessage Post()
        {
            List<string> taskLinks = new List<string>(); // ������ ����� ������� �� ��������
            var httpContext = HttpContext.Current; // ������ ���� ������ �� ����� 

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0) // ����� �� ���� ����� ���� ���������,������ ����� ��� ����� 
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++) // ����� ����� ��� ����� 
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i]; // ������ ���� �������� ���� �� ����� ������ ����� 

                    // this is an example of how you can extract addional values from the Ajax call
                    string name = httpContext.Request.Form["name"]; // �� ���� �� �����

                    if (httpPostedFile != null) // ������ �� ����� ����
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);
                        string fname = httpPostedFile.FileName.Split('\\').Last(); // ���� �� ��� �� �����
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), fname); // ����� �� ����� ���� ������ ���������� ������
                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath); // ���� �����
                        taskLinks.Add("uploadedFiles/" + fname); // ����� �� ������ ������ ������ ������ ������,
                    }
                }
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, taskLinks); // ���� �� ������ ����� ��������� ������ ��� �������, 
        }

        //���
        [HttpPost] /// �� �����- ����� �����
        [Route("api/Student/checkUsers")]
        public Student Post([FromBody] Student student) // ����� ���� �� ���������- �� ������
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

        [HttpPost] ///  ��� 2- ����� ���� �� ����
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

        [HttpPost] /// ����� �������
        [Route("api/Student/PostFeedback")]
        public void Post([FromBody] Feedback feedbackObj)
        {
            Feedback F = new Feedback();
            F.insertFeed(feedbackObj);
        }
        [HttpGet] /// ���� �������
        [Route("api/student/getdata/{groupStudent}/{numOfTask}/{profession}")]
        public List<Feedback> getData(string groupStudent, string numOfTask, string profession)
        {
            Feedback F = new Feedback();
            return F.getData(groupStudent, numOfTask, profession);
        }

        [HttpGet]
        [Route("api/student/getvideo/{ClassName}/{ClassNum}/{Professtion}/{taskNum}")]
        public List<Group_Feedback> getVideos_(string ClassName, string ClassNum, string Professtion, int taskNum)
        {
            Group_Feedback G = new Group_Feedback();
            List<Group_Feedback> videos = G.getVideo(ClassName, ClassNum, Professtion, taskNum);
            return videos;
        }

        //���
        [HttpGet] /// �� �����- ����� �����
        [Route("api/Student/GetDetails2/{className}/{classNum}")]
        public Class GetDetails2(string className, int classNum) // ����� ���� �� ���������- �� ������
        {
            Class C = new Class();
            return C.Get_Details2(className,classNum);
        }
    }


}





