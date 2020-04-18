using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Text;
namespace DocuProject.Models
{
    public class Admin
    {
        string fname;
        string lname;
        int id;
        string email;
        string city;
        string street;
        DateTime bday;
        string phoneNum;
        string profession;
        string password;

        public Admin() { }

        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public DateTime Bday { get => bday; set => bday = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Profession { get => profession; set => profession = value; }
        public string Password { get => password; set => password = value; }

        public Admin CheckUser(Admin admin)
        {
            DBservices dbs = new DBservices();
            return dbs.check_User(admin);

        }
            public DataTable GetDetails(int ID)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Details(ID,"admin");
            return dbs.dt;//מעביר רק את הטבלה 
        }
        public DataTable PutA(Admin admin)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Details(admin.Id,"admin");
            dbs.dt = checkTbl(admin, admin.Id, dbs.dt);
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }
        public DataTable checkTbl(Admin admin, int id, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                {
                    dr["FName"] = admin.Fname;
                    dr["LName"] = admin.Lname;
                    dr["Id_"] = admin.Id;
                    dr["Email"] = admin.Email;
                    dr["City"] = admin.City;
                    dr["Street"] = admin.Street;
                    dr["Bday"] = admin.Bday;
                    dr["PhoneNum"] = admin.PhoneNum;
                    dr["Profession"] = admin.Profession;
                    dr["Password_"] = admin.Password;
                }
            }
            return dt;
        }
        public int PostAdmin(Admin admin) // הכנסת תלמיד ספציפי
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.Post_Admin(admin);
            return numAffected; //מחזיר את מספר השורות
        }

    }



}
