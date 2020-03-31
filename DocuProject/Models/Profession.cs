using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocuProject.Models
{
    public class Profession
    {
        string name;

        public Profession() { }
        public string Name { get => name; set => name = value; }

        public List<Profession> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.getFromDB();

        }

        public int insertP(Profession proffObj) //// שלב 1 - נעביר את כל המערך לדטה בייס
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertP(proffObj);
            return numAffected; //מחזיר את מספר השורות
        }


    }
}