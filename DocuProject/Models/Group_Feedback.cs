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
                    //dr["Deadline"] = VT.Deadline;
                    //dr["IdTask"] = VT.IdTask;
                    //dr["IdTeacher"] = VT.IdTeacher;
                    //dr["GroupNum"] = VT.GroupNum;
                    //dr["Group_students"] = VT.Group_students;
                    dr["Grade"] = VT.Grade;
                    dr["Feedback"] = VT.Feedback;
                    dr["Status_"] = VT.Status;
                    dr["Video"] = VT.Video;
                    dr["Comment"] = VT.Comment;
                }
            }
            return dt; // מחזיק עכשיו טבלה חדשה שיש בה שינוי
        }

    }

}