using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Text;
namespace DocuProject.Models
{
    public class Task
    {
        string deadline; //תאריך הגשת המטלה
        string topic; //נושא המטלה
        string assignation; // שיבוץ תלמידים
        string description; // תיאור המטלה
        string className;
        string classNum;
        string profession;
        string video;
        int idRow;
        int taskNum;
        int idTeacher;
        public Task() { }

        public string Deadline { get => deadline; set => deadline = value; }
        public string Topic { get => topic; set => topic = value; }
        public string Assignation { get => assignation; set => assignation = value; }
        public string Description { get => description; set => description = value; }
        public string ClassName { get => className; set => className = value; }
        public string ClassNum { get => classNum; set => classNum = value; }
        public string Profession { get => profession; set => profession = value; }
        public string Video { get => video; set => video = value; }
        public int IdRow { get => idRow; set => idRow = value; }
        public int TaskNum { get => taskNum; set => taskNum = value; }
        public int IdTeacher { get => idTeacher; set => idTeacher = value; }

        public int insertTask1(Task taskObj)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertTask2(taskObj);
            return numAffected;
        }
        public List<Task> ReadTask(string name, string num, string prof)
        {
            DBservices dbs = new DBservices();
            return dbs.getTaskFromDB(name, num, prof);
        }

        public string getSpecificTask(string class1, string numClass, string sub,string  topic)
        {
            DBservices dbs = new DBservices();
            return dbs.getSpecificTask(class1, numClass, sub, topic);
        }

        public List<Task> getVideos(string ClassName, string ClassNum, string Professtion, string Topic, string Deadline)
        {
            DBservices dbs = new DBservices();
            return dbs.getVideos(ClassName, ClassNum, Professtion, Topic, Deadline);
        }

        public DataTable putT(string class1, string numClass, string sub, string topic, Task taskNew)
        {       
            DBservices dbs = new DBservices();                 
            dbs = dbs.getSpecificTask2(class1, numClass, sub, topic);
            dbs.dt = checkTblT2(taskNew , dbs.dt);
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable checkTblT2(Task taskNew,DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                {
                    dr["Deadline"] = taskNew.Deadline;
                    dr["Topic"] = taskNew.Topic;
                    dr["Description_"] = taskNew.Description;
                }
            }
            return dt;
        }

        public int deleteTask(string class1, string numClass, string sub, string topic)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.delete_task(class1, numClass, sub, topic);
            return numAffected;
        }

    }
}