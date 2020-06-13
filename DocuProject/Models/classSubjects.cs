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
        string profession; //מקצוע למשל מתמטיקה
        int id_teacher; //מקצוע למשל מתמטיקה
        string teacher_name; //מקצוע למשל מתמטיקה
        string year_;
        public ClassSubjects() { }
        public string Name { get => name; set => name = value; }
        public string Number { get => number; set => number = value; }
        public string Profession { get => profession; set => profession = value; }
        public int Id_teacher { get => id_teacher; set => id_teacher = value; }
        public string Teacher_name { get => teacher_name; set => teacher_name = value; }
        public string Year_ { get => year_; set => year_ = value; }

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

        //  קבלת המקצועות הספציפיים של המורה 
        public List<ClassSubjects> ReadCSTeacher(string name, string num, int teachrID)
        {
            DBservices dbs = new DBservices();
            return dbs.getCSFromDB_Teachers(name, num, teachrID);
        }

        // קבלת הכיתות הספציפיות שהמורה מלמד
        public DataTable tacherClass(int id)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.teacherNumClass(id);
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable deleteT_CS(int TeacherId, string TeacherSubj)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_TechersCS(); //get a teachers table 
            dbs.DeleteCS(TeacherId, TeacherSubj); // check the spcific teacher
            return dbs.dt;
        }
            public List<ClassSubjects> ReadCS_Teacher( int teachrID)
        {
            DBservices dbs = new DBservices();
            return dbs.getCSFromDB(teachrID);
        }

        public int deleteTaskGroup(string class1, string numClass, string sub, int num)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.deleteTaskGroup(class1, numClass, sub, num);
            return numAffected;
        }
     
    }
}