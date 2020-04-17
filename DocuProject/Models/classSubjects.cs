using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Text;


namespace DocuProject.Models
{
    public class ClassSubjects
    {
        // מותאם למסך כיתה

        string name; //שם כיתה למשל ח
        string number; //מספר כיתה למשל 2
        string classType; //סוג כיתה למשל רגילה
        string profession; //מקצוע למשל מתמטיקה
        string id_teacher; //מקצוע למשל מתמטיקה
        string teacher_name; //מקצוע למשל מתמטיקה

        public ClassSubjects() { }

        public string Name { get => name; set => name = value; }
        public string Number { get => number; set => number = value; }
        public string ClassType { get => classType; set => classType = value; }
        public string Profession { get => profession; set => profession = value; }
        public string Id_teacher { get => id_teacher; set => id_teacher = value; }
        public string Teacher_name { get => teacher_name; set => teacher_name = value; }

        public int insertClassSub(List<ClassSubjects> classSUbObj)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertClassSub(classSUbObj);
            return numAffected;
        }

        public DataTable GetNumClass()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Nums();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public List<ClassSubjects> ReadCS(string name, string num)
        {
            DBservices dbs = new DBservices();
            return dbs.getCSFromDB(name, num);
        }




    }

}