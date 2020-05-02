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
        DateTime deadline;
        int idTask;
        int idTeacher;
        int groupNum;
        string group_students;
        int grade;
        string feedback;
        int status;
        string video;
        string comment;

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
        public int IdRows { get => idRows; set => idRows = value; }
        public string Comment { get => comment; set => comment = value; }

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
            dbs = dbs.Get_videoTeam(videoTeam.ClassName, videoTeam.ClassNum); //להביא את כל הסטודנטים בכיתה ז2
            dbs.dt = checkTbl(id, videoTeam, dbs.dt); // רוצים לעדכן סטודנט אחד ולכן נשתמש בפונקציה הזו 
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable checkTbl(int id, Group_Feedback videoTeam, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ////////// שינוי בתוך הטבלה עצמה
                if (id == Convert.ToInt32(dr["id"]))
                {
                    dr["ClassName"] = videoTeam.ClassName;
                    dr["ClassNum"] = videoTeam.ClassNum;
                    dr["Deadline"] = videoTeam.Deadline;
                    dr["Feedback"] = videoTeam.Feedback;
                    dr["Grade"] = videoTeam.Grade;
                    dr["GroupNum"] = videoTeam.GroupNum;
                    dr["Group_students"] = videoTeam.Group_students;
                    dr["IdTask"] = videoTeam.IdTask;
                    dr["IdTeacher"] = videoTeam.IdTeacher;
                    dr["Proffesion"] = videoTeam.Proffesion;
                    dr["Status_"] = videoTeam.Status;
                    dr["Video"] = videoTeam.Video;
                    dr["comment"] = videoTeam.Comment;
                }
            }
            return dt; // מחזיק עכשיו טבלה חדשה שיש בה שינוי
        }

    }

}