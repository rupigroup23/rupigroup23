using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Text;

namespace DocuProject.Models
{
    public class Student
    {
        public Student() { }
        // להוסיף שם משתמש וסיסמא??

        int id_row;
        string fName;
        string lName;
        string phoneNum;
        string email;
        string city;
        int id;
        DateTime bday;
        string address;
        string className;
        int classNum;
        string password;
       
        public int Id_row { get => id_row; set => id_row = value; }
        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Email { get => email; set => email = value; }
        public string City { get => city; set => city = value; }
        public int Id { get => id; set => id = value; }
        public DateTime Bday { get => bday; set => bday = value; }
        public string Address { get => address; set => address = value; }
        public string ClassName { get => className; set => className = value; }
        public int ClassNum { get => classNum; set => classNum = value; }
        public string Password { get => password; set => password = value; }

        public List<Student> ReadSt()
        {
            DBservices dbs = new DBservices();
            return dbs.getFromDBST();
        }

        public int insertS(List<Student> stdentsArr) 
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertS(stdentsArr);
            return numAffected; //מחזיר את מספר השורות
        }

        internal DataTable GetStudents()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Students(ClassName, ClassNum);
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable deleteS(int id)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Students(ClassName, ClassNum);
            dbs.Delete("Student", id);
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public int insertS2(Student StudentObj) // הכנסת תלמיד ספציפי
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertS2(StudentObj);
            return numAffected; //מחזיר את מספר השורות
        }

        public DataTable PutS(int id, Student student)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Students(student.className, student.ClassNum);
            dbs.dt = checkTbl(id, student, dbs.dt);
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable checkTbl(int id, Student student, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ////////// שינוי בתוך הטבלה עצמה
                if (id == Convert.ToInt32(dr["id"]))
                {
                    dr["FName"] = student.FName;
                    dr["LName"] = student.LName;
                    dr["PhoneNum"] = student.PhoneNum;
                    dr["Email"] = student.Email;
                    dr["City"] = student.city;
                    dr["Street"] = student.Address;
                    dr["Id_"] = student.Id;
                    dr["Bday"] = student.Bday;
                    dr["ClassName"] = student.ClassName;
                    dr["ClassNum"] = student.ClassNum;
                    dr["Password_"] = student.Password;
                }
            }
            return dt;
        }
    }
}