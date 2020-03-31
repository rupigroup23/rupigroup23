using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class Group
    {
        //אין עדיין מסך מטלה בצד תלמיד, נחכה להמשך לראות איזה שדות מוסיפים

        DateTime dateOfUpdate;
        double grade;


        public Group() { }
        public DateTime DateOfUpdate { get => dateOfUpdate; set => dateOfUpdate = value; }
        public double Grade { get => grade; set => grade = value; }
    }
}