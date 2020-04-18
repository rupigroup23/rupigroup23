using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public Task() { }

        public string Deadline { get => deadline; set => deadline = value; }
        public string Topic { get => topic; set => topic = value; }
        public string Assignation { get => assignation; set => assignation = value; }
        public string Description { get => description; set => description = value; }
        public string ClassName { get => className; set => className = value; }
        public string ClassNum { get => classNum; set => classNum = value; }
        public string Profession { get => profession; set => profession = value; }
        public string Video { get => video; set => video = value; }

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

        //public DataTable PutTask(int id, Task task)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        //{
        //    DBservices dbs = new DBservices();
        //    dbs = dbs.Get_task(task.className, task.ClassNum); //Get_Students
        //    dbs.dt = checkTbtask(id, task, dbs.dt); //checkTbl
        //    dbs.update();
        //    return dbs.dt;//מעביר רק את הטבלה 
        //}
        //public DataTable checkTbtask(int id, Task task, DataTable dt)
        //{
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ////////// שינוי בתוך הטבלה עצמה
        //        if (id == Convert.ToInt32(dr["id"]))
        //        {
        //            dr["ClassName"] = task.ClassName;
        //            dr["ClassNum"] = task.ClassNum;
        //            dr["Profession"] = task.Profession;
        //            dr["Deadline"] = task.Deadline;
        //            dr["Topic"] = task.Topic;
        //            dr["Assignation"] = task.Assignation;
        //            dr["Description"] = task.Description;
        //        }
        //    }
        //    return dt;
        //}

        public string getSpecificTask(string class1, string numClass, string sub, string topic)
        {
            DBservices dbs = new DBservices();
            return dbs.getSpecificTask( class1,  numClass,  sub,  topic);
        }

        public List<Task> getVideos(string ClassName, string ClassNum, string Professtion, string Topic, string Deadline)
        {
            DBservices dbs = new DBservices();
            return dbs.getVideos(ClassName, ClassNum, Professtion, Topic, Deadline);
        }



        //public DataTable GetDetailsT(int ID)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        //{
        //    DBservices dbs = new DBservices();
        //    dbs = dbs.Get_DetailsT(ID);
        //    return dbs.dt;//מעביר רק את הטבלה 
        //}
        //public DataTable PutT(Task task)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        //{
        //    DBservices dbs = new DBservices();
        //    dbs = dbs.Get_DetailsT(task.Id);
        //    dbs.dt = checkTblT(task, task.Id, dbs.dt);
        //    dbs.update1();
        //    return dbs.dt;//מעביר רק את הטבלה 
        //}
        //public DataTable checkTblT(Task task, int id, DataTable dt)
        //{
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        {
        //            dr["ClassName"] = task.ClassName;
        //            dr["ClassNum"] = task.ClassNum;
        //            dr["Profession"] = task.Profession;
        //            dr["Deadline"] = task.Deadline;
        //            dr["Topic"] = task.Topic;
        //            dr["Assignation"] = task.Assignation;
        //            dr["Description_"] = task.Description;
        //        }
        //    }
        //    return dt;
        //}

    }
}