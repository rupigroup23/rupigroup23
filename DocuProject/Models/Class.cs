using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Text;


namespace DocuProject.Models
{
    public class Class
    {
        
        // מותאם למסך כיתה

        string name;
        int number;
        string year; // שנה עברית
        int numOfStudents;
        string teacherName;
        string classType;
 
        public Class () { }

        public string Name { get => name; set => name = value; }
        public int Number { get => number; set => number = value; }
        public string Year { get => year; set => year = value; }
        public int NumOfStudents { get => numOfStudents; set => numOfStudents = value; }
        public string TeacherName { get => teacherName; set => teacherName = value; }
        public string ClassType { get => classType; set => classType = value; }


        public int insert(Class classObj) 
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insert(classObj);
            return numAffected; 
        }

        public DataTable GetNumClass()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Nums();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public List<Class> ReadClass()
        {
            DBservices dbs = new DBservices();
            return dbs.getFromDBClass();
        }
    }
}