using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class ImgAdmin
    {
        string email;
        string img;

        public string Img { get => img; set => img = value; }
        public string Email { get => email; set => email = value; }


        public int insertPic2(ImgAdmin AdminImage) // הכנסת התמונה משלב2
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertPic2(AdminImage);
            return numAffected; //מחזיר את מספר השורות
        }

        public string getAvatarImage(string  Id)
        {
            DBservices dbs = new DBservices();
            return dbs.getAvatarImage(Id,"admin");
        }

    }
}