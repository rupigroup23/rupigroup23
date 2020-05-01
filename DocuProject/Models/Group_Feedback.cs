using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Text;


namespace DocuProject.Models
{
    public class Group_Feedback
    {
        // מותאם למסך כיתה

        string className; 
        int classNum; 
        string proffesion; 
        DateTime deadline;
        int idTask;
        int idTeacher;
        int groupNum;
        string group_students;
        int grade;
        string feedback;
        int status;
        string video;

        public string ClassName { get => className; set => className = value; }
        public int ClassNum { get => classNum; set => classNum = value; }
        public string Proffesion { get => proffesion; set => proffesion = value; }
        public DateTime Deadline { get => deadline; set => deadline = value; }
        public int IdTask { get => idTask; set => idTask = value; }
        public int IdTeacher { get => idTeacher; set => idTeacher = value; }
        public int GroupNum { get => groupNum; set => groupNum = value; }
        public string Group_students { get => group_students; set => group_students = value; }
        public int Grade { get => grade; set => grade = value; }
        public string Feedback { get => feedback; set => feedback = value; }
        public int Status { get => status; set => status = value; }
        public string Video { get => video; set => video = value; }

        public Group_Feedback() { }


        public List<Group_Feedback> ReadDT(string name, int num, DateTime date)
        {
            DBservices dbs = new DBservices();
            return dbs.getDTFromDB(name, num, date);
        }

    }

}