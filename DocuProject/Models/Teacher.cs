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
        string password; //??

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

    }
}