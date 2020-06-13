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
        int idRows;
        string className; 
        int classNum; 
        string proffesion; 
        string deadline;
        int idTask;
        int idTeacher;
        int groupNum;
        int grade;
        string feedback;
        int status;
        string video;
        string comment;
        string group;

        public string ClassName { get => className; set => className = value; }
        public int ClassNum { get => classNum; set => classNum = value; }
        public string Proffesion { get => proffesion; set => proffesion = value; }
        public string Deadline { get => deadline; set => deadline = value; }
        public int IdTask { get => idTask; set => idTask = value; }
        public int IdTeacher { get => idTeacher; set => idTeacher = value; }
        public int GroupNum { get => groupNum; set => groupNum = value; }
        public int Grade { get => grade; set => grade = value; }
        public string Feedback { get => feedback; set => feedback = value; }
        public int Status { get => status; set => status = value; }
        public string Video { get => video; set => video = value; }
        public int IdRows { get => idRows; set => idRows = value; }
        public string Comment { get => comment; set => comment = value; }
        public string Group { get => group; set => group = value; }

        public Group_Feedback() { }


        //public List<Group_Feedback> ReadDT(string name, int num, DateTime date1)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.getDTFromDB(name, num, date1);
        //}


        internal DataTable GetTeamData() // מביאים תלמידים לפי כיתה ומס כיתה
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_TeamData(ClassName, ClassNum, Proffesion);
            return dbs.dt;//מעביר רק את הטבלה 
        }
        public DataTable PutVC(int id, Group_Feedback videoTeam)// לעדכן תלמיד ספציפי בטבלה של הדטא טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_videoTeam(videoTeam.ClassName, videoTeam.ClassNum, videoTeam.Proffesion); //להביא את כל הסטודנטים בכיתה ז2
            dbs.dt = checkTbl1(id, videoTeam, dbs.dt); // רוצים לעדכן סטודנט אחד ולכן נשתמש בפונקציה הזו 
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable checkTbl1(int id, Group_Feedback VT, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ////////// שינוי בתוך הטבלה עצמה
                if (id == Convert.ToInt32(dr["id"]))
                {
                    dr["ClassName"] = VT.ClassName;
                    dr["ClassNum"] = VT.ClassNum;
                    dr["Proffesion"] = VT.Proffesion;
                    dr["Grade"] = VT.Grade;
                    dr["Feedback"] = VT.Feedback;
                    dr["Status_"] = VT.Status;
                    dr["Video"] = VT.Video;
                    dr["Comment"] = VT.Comment;
                }
            }
            return dt; // מחזיק עכשיו טבלה חדשה שיש בה שינוי
        }

        public List<Group_Feedback> getVideo(string ClassName, string ClassNum, string Professtion, int taskNum)
        {
            DBservices dbs = new DBservices();
            return dbs.get_Videos(ClassName, ClassNum, Professtion, taskNum);
        }

        public int insert(Group_Feedback StudentObj)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertGroup(StudentObj);
            return numAffected;
        }

        public DataTable editTaskGroup(string class1, int numClass, string sub, int num, Group_Feedback taskGroup)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.getSpecificTaskGroup(class1, numClass, sub, num);
            dbs.dt = checkTblT3(taskGroup, dbs.dt);
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }
        public DataTable checkTblT3(Group_Feedback taskGroup, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                {
                    dr["Deadline"] = taskGroup.Deadline;
                }
            }
            return dt;
        }



    }

}