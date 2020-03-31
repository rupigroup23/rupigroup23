using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class Feedback
    {
        //אין עוד מסך

        string contents;// תוכן תגובה
        DateTime uploadDate; // ת. העלאת התגובה

        public Feedback() { }
        public string Contents { get => contents; set => contents = value; }
        public DateTime UploadDate { get => uploadDate; set => uploadDate = value; }
    }
}