using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DocuProject.Models
{
    public class Feedback
    {

        int id;
        string contents;// תוכן תגובה
        string nameLike; // לייק
        string video; //העלת תגובה בוידיאו
        string groupStudent; // קבוצת התלמידים המגישים
        string profession;
        string numOfTask;
        string userName;
        int idRow;


        public Feedback() { }

        public int Id { get=>id; set=>id=value; }
        public string Contents { get => contents; set => contents = value; }
        public string NameLike { get => nameLike; set => nameLike = value; }
        public string Video { get => video; set => video = value; }
        public string GroupStudent { get => groupStudent; set => groupStudent = value; }
        public string Profession { get => profession; set => profession = value; }
        public string NumOfTask { get => numOfTask; set => numOfTask = value; }
        public string UserName { get => userName; set => userName = value; }
        public int IdRow { get => idRow; set => idRow = value; }

        public int insertFeed(Feedback feedbackObj)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.inserFeedback1(feedbackObj);
            return numAffected;
        }

        public List<Feedback> getData (int idRow, string numOfTask, string profession)
        {
            DBservices dbs = new DBservices();
            return dbs.getData(idRow, numOfTask , profession);
        }

        public void deleteComment(int selectedRow,string type)
        {
            DBservices dbs = new DBservices();
            dbs.deleteComment(selectedRow,type);
        }

        public DataTable checkTblT1(Feedback NewFeedback, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                {
                    dr["groupStudent"] = NewFeedback.GroupStudent;
                    dr["numOfTask"] = NewFeedback.NumOfTask;
                    dr["profession"] = NewFeedback.Profession;
                    dr["contents"] = NewFeedback.Contents;
                    dr["nameLike"] = NewFeedback.NameLike;
                    dr["video"] = NewFeedback.Video;
                    dr["userName"] = NewFeedback.UserName;
                    dr["idRow"] = NewFeedback.IdRow;
                }
            }
            return dt;
        }
    }
}