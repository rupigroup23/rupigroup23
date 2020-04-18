using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Text;

namespace DocuProject.Models
{
    public class Teacher
    {
        string fName;
        string lName;
        string email;
        string city;
        string street;
        int id;
        DateTime bday;
        string phoneNum;
        string profession;
        string password;
        string ClassroomTeacher;
        public Teacher() { }

        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string Email { get => email; set => email = value; }
        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public int Id { get => id; set => id = value; }
        public DateTime Bday { get => bday; set => bday = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Profession { get => profession; set => profession = value; }
        public string Password { get => password; set => password = value; }
        public string ClassroomTeacher1 { get => ClassroomTeacher; set => ClassroomTeacher = value; }

        public int insertT(Teacher TeacherObj) 
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertT(TeacherObj);
            return numAffected; //מחזיר את מספר השורות
        }

        public List<Teacher> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.getFromDBT(); 
        }

        public int insertT2(List<Teacher> teachersArr)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertT2(teachersArr);
            return numAffected; //מחזיר את מספר השורות
        }
          public DataTable GetTechers()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Techers();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable deleteT(int rowID)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Techers(); //get a teachers table 
            dbs.Delete("Teacher", rowID); // check the spcific teacher
            return dbs.dt;
        }

        public DataTable PutT(int id, Teacher teacher)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Techers();
            dbs.dt = checkTbl(id, teacher, dbs.dt);
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable checkTbl(int id, Teacher teacher, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (id == Convert.ToInt32(dr["id"]))
                {
                    dr["FName"] = teacher.FName;
                    dr["LName"] = teacher.LName;
                    dr["PhoneNum"] = teacher.PhoneNum;
                    dr["Email"] = teacher.Email;
                    dr["City"] = teacher.city;
                    dr["Street"] = teacher.Street;
                    dr["Id_"] = teacher.Id;
                    dr["Bday"] = teacher.Bday;
                    dr["Profession"] = teacher.Profession;
                    dr["Password_"] = teacher.Password;
                }
            }
            return dt;
        }
        public Teacher CheckUser2(Teacher teacher)
        {
            DBservices dbs = new DBservices();
            return dbs.check_User2(teacher);

        }

        public DataTable GetDetails(int ID)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Details(ID,"teacher");
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable PutT(Teacher teacher)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Details(teacher.Id, "teacher");
            dbs.dt = checkTbl(teacher, teacher.Id, dbs.dt);
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }
        public DataTable checkTbl(Teacher teacher, int id, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                {
                    dr["FName"] = teacher.FName;
                    dr["LName"] = teacher.LName;
                    dr["Id_"] = teacher.Id;
                    dr["Email"] = teacher.Email;
                    dr["City"] = teacher.City;
                    dr["Street"] = teacher.Street;
                    dr["Bday"] = teacher.Bday;
                    dr["PhoneNum"] = teacher.PhoneNum;
                    dr["Profession"] = teacher.Profession;
                    dr["Password_"] = teacher.Password;
                }
            }
            return dt;
        }
    }


}