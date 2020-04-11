using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class ImgStudent
    {
        string email;
        string img;

        public string Img { get => img; set => img = value; }
        public string Email { get => email; set => email = value; }


        public int insertPic(ImgStudent StudentImage) // הכנסת התמונה משלב2
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertPic(StudentImage);
            return numAffected; //מחזיר את מספר השורות
        }

       
    }
}