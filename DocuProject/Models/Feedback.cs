using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class Feedback
    {
        

        string contents;// תוכן תגובה
        string nameLike; // לייק
        string video; //העלת תגובה בוידיאו
        string groupStudent; // קבוצת התלמידים המגישים
        string profession;
        string numOfTask;


        public Feedback() { }

        public string Contents { get => contents; set => contents = value; }
        public string NameLike { get => nameLike; set => nameLike = value; }
        public string Video { get => video; set => video = value; }
        public string GroupStudent { get => groupStudent; set => groupStudent = value; }
        public string Profession { get => profession; set => profession = value; }
        public string NumOfTask { get => numOfTask; set => numOfTask = value; }
    }
}