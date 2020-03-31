using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class Admin
    {
        string fname;
        string lname;
        int id;
        string password;

        public Admin() { }

        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public int Id { get => id; set => id = value; }
        public string Password { get => password; set => password = value; }


        public Admin CheckUser(Admin admin)
        {
            DBservices dbs = new DBservices();
            return dbs.check_User(admin);

        }


    }
}