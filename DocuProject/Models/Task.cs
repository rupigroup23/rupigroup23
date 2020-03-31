using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class Task
    {
        DateTime deadline; //תאריך הגשת המטלה
        string topic; //נושא המטלה
        string assignation; // שיבוץ תלמידים
        string description; // תיאור המטלה

        public Task() { }

        public DateTime Deadline { get => deadline; set => deadline = value; }
        public string Topic { get => topic; set => topic = value; }
        public string Assignation { get => assignation; set => assignation = value; }
        public string Description { get => description; set => description = value; }
    }
}