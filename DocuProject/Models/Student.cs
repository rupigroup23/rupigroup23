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
        string gender;
        int gpa;


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
        public string Gender { get => gender; set => gender = value; }
        public int Gpa { get => gpa; set => gpa = value; }

        public List<Student> ReadSt()
        {
            DBservices dbs = new DBservices();
            return dbs.getFromDBST();
        }

        public int insertS(List<Student> stdentsArr) //אקסל
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertS(stdentsArr);
            return numAffected; //מחזיר את מספר השורות
        }

        internal DataTable GetStudents() // מביאים תלמידים לפי כיתה ומס כיתה
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

        public DataTable PutS(int id, Student student)// לעדכן תלמיד ספציפי בטבלה של הדטא טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Students(student.className, student.ClassNum); //להביא את כל הסטודנטים בכיתה ז2
            dbs.dt = checkTbl(id, student, dbs.dt); // רוצים לעדכן סטודנט אחד ולכן נשתמש בפונקציה הזו 
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
                    dr["GPA"] = student.Gpa;
                }
            }
            return dt; // מחזיק עכשיו טבלה חדשה שיש בה שינוי
        }
        public Student CheckUser(Student student) //כניסת תלמיד
        {
            DBservices dbs = new DBservices();
            return dbs.check_Student(student);
        }

        public DataTable GetDetails(int ID)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Details(ID, "student");
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable Put_S(int id, Student student)// הפונקציה מחזירה דאטה טייבל ולכן מסוג דאטה טייבל
        {
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Details(id,"student");
            dbs.dt = check_Tbl(id,student, dbs.dt); //חוזר לכאן טבלה אחרי העידכונים עדיין לא בדאטהבייס
            dbs.update();
            return dbs.dt;//מעביר רק את הטבלה 
        }

        public DataTable check_Tbl(int id, Student student, DataTable dt) // פונקציית עזר של PUtT
        {
            foreach (DataRow dr in dt.Rows) //עובר על כל הטבלה שורה שורה
            {
                if (id == Convert.ToInt32(dr["Id_"]))
                {
                    dr["PhoneNum"] = student.PhoneNum;
                    dr["Password_"] = student.Password;
                }
            }
            return dt;
        }

        public List<string> GetSbyGrade1(string name, int nun, string prof, string grade)
        {
            DBservices dbs = new DBservices();

            List<Student> listbyStrong = dbs.GetSbyGradeDB(name, nun, prof, grade, "S");
            List<Student> listbyWeak = dbs.GetSbyGradeDB(name, nun, prof, grade, "W");

            List<string> listbyMix = new List<string>();

            int cntrStudent = listbyStrong.Count() + listbyWeak.Count();
            double gorupNum;
            int x;

            if (cntrStudent % 3 == 0) 
            {
                gorupNum = cntrStudent / 3;
            }
            else
            { 
                x = cntrStudent / 3;
                gorupNum = Math.Round(x + 0.5);
            }
            int cntr;
            for (int i = 0; i < gorupNum; i++)
            {
                cntr = 0;
                string nameTeam = "";
                //Strong - one student
                if(cntr <= listbyStrong.Count && listbyStrong.Count != 0)
                {
                    nameTeam = Convert.ToString(listbyStrong[cntr].Id)  + "," ;
                    listbyStrong.RemoveAt(cntr);
                }
                else
                {
                    nameTeam = Convert.ToString(listbyWeak[cntr].Id) + ",";
                    listbyWeak.RemoveAt(cntr);
                }
                //Weak - one student
                 if (cntr <= listbyWeak.Count && listbyWeak.Count != 0)
                {
                    nameTeam += Convert.ToString(listbyWeak[cntr].Id);
                    listbyWeak.RemoveAt(cntr);
                }
                else
                {
                    nameTeam = Convert.ToString(listbyStrong[cntr].Id) + ",";
                    listbyStrong.RemoveAt(cntr);
                }
                listbyMix.Add(nameTeam);
            }
            //
            for (int i = 0; i < listbyMix.Count; i++)
            {
                cntr = 0;
                if (listbyStrong.Count > listbyWeak.Count) 
                {
                    listbyMix[i] += "," + listbyStrong[cntr].Id; ;
                    listbyStrong.RemoveAt(cntr);
                }
                else
                {
                    listbyMix[i] += "," + listbyWeak[cntr].Id;
                    listbyWeak.RemoveAt(cntr);
                }
            }
            return listbyMix; //מחזיר מערך ממויין
        }
        //אלגוריתם בנים בנות - נוי 
        internal List<string> GetStudentsAlgoritem(string radioChoose)
        {
            List<string> X = new List<string>();
            DBservices dbs = new DBservices();
            dbs = dbs.Get_Students(ClassName, ClassNum);

            if (radioChoose == "shuffle") // צוותים מעורבים
            {
                X = MakeGroupsShuffle(dbs.dt);
            }
            if (radioChoose == "equals") // צוותים מאותו מין
            {
                X = MakeGroupsEquals(dbs.dt);
            }

            if (radioChoose == "random")// רנדומלי 
            {

            }
            return X;
        }

        //אלגוריתם בנים בנות - נוי        
        public List<string> MakeGroupsEquals(DataTable studentsArr) // מאותו מין

        {
            List<Student> BoysArr = new List<Student>();
            List<Student> GirlsArr = new List<Student>();
            foreach (DataRow dr in studentsArr.Rows)
            {
                if (dr["Gender"].ToString() == "זכר")
                {
                    Student S = new Student();
                    S.FName = (string)dr["FName"];
                    S.LName = (string)dr["LName"];
                    S.Id = (int)dr["Id_"];
                    S.Gender = (string)dr["Gender"];
                    BoysArr.Add(S);
                }
                else // נקבה
                {
                    Student S = new Student();
                    S.FName = (string)dr["FName"];
                    S.LName = (string)dr["LName"];
                    S.Id = (int)dr["Id_"];
                    S.Gender = (string)dr["Gender"];
                    GirlsArr.Add(S);
                }
            }

            int studentsCount = BoysArr.Count + GirlsArr.Count; //כמות התלמידים בכיתה
            double gorupNum;
            int x;
            bool Divideby3;

            if (studentsCount % 3 == 0) // כמות הצוותים - במידה והכמות תתחלק ב3
            {
                gorupNum = studentsCount / 3;
                Divideby3 = true;

            }
            else
            {
                x = studentsCount / 3;
                gorupNum = Math.Round(x + 0.5);
                Divideby3 = false;
            }

            List<string> groupsStudents = new List<string>();

            for (int i = 0; i < gorupNum; i++)
            {
                int cntr = 0;
                string str = "";

                for (int j = 0; i < BoysArr.Count; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (BoysArr.Count != 0)
                        {
                            if (BoysArr.Count == 4)
                            {
                                for (int z = 0; z < 2; z++)
                                {
                                    cntr = 0;
                                    str += BoysArr[cntr].Id + ",";
                                    //str += BoysArr[cntr].FName + " " + BoysArr[cntr].LName + ",";
                                    BoysArr.RemoveAt(cntr);
                                    cntr++;
                                }
                                groupsStudents.Add(str);
                                str = "";
                                break;

                            }
                            cntr = 0;
                            str += BoysArr[cntr].Id + ",";
                            //str += BoysArr[cntr].FName + " " + BoysArr[cntr].LName + ",";
                            BoysArr.RemoveAt(cntr);
                            cntr++;
                        }
                    }
                    if (str != "")
                    {
                        groupsStudents.Add(str);
                        str = "";
                    }

                }

                for (int j = 0; i < GirlsArr.Count; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (GirlsArr.Count != 0) // בנים
                        {
                            if (BoysArr.Count == 4)
                            {
                                for (int z = 0; z < 2; z++)
                                {
                                    cntr = 0;
                                    str += BoysArr[cntr].Id + ",";
                                    //str += BoysArr[cntr].FName + " " + BoysArr[cntr].LName + ",";
                                    BoysArr.RemoveAt(cntr);
                                    cntr++;
                                }
                                groupsStudents.Add(str);
                                str = "";
                                break;
                            }
                            cntr = 0;
                            str += GirlsArr[cntr].Id + ",";
                            //str += GirlsArr[cntr].FName + " " + GirlsArr[cntr].LName + ",";
                            GirlsArr.RemoveAt(cntr);
                            cntr++;
                        }
                    }
                    if (str != "")
                    {
                        groupsStudents.Add(str);
                        str = "";
                    }
                }
            }


            return groupsStudents;
        }
        //אלגוריתם בנים בנות - נוי   
        public List<string> MakeGroupsShuffle(DataTable studentsArr) // צוותים מעורבים
        {
            List<Student> BoysArr = new List<Student>();
            List<Student> GirlsArr = new List<Student>();
            foreach (DataRow dr in studentsArr.Rows)
            {
                if (dr["Gender"].ToString() == "זכר")
                {
                    Student S = new Student();
                    S.FName = (string)dr["FName"];
                    S.LName = (string)dr["LName"];
                    S.Id = (int)dr["Id_"];
                    S.Gender = (string)dr["Gender"];
                    BoysArr.Add(S);
                }
                else // נקבה
                {
                    Student S = new Student();
                    S.FName = (string)dr["FName"];
                    S.LName = (string)dr["LName"];
                    S.Id = (int)dr["Id_"];
                    S.Gender = (string)dr["Gender"];
                    GirlsArr.Add(S);
                }
            }
            int studentsCount = BoysArr.Count + GirlsArr.Count; //כמות התלמידים בכיתה
            double gorupNum;
            int x;

            if (studentsCount % 3 == 0) // כמות הצוותים - במידה והכמות תתחלק ב3
            {
                gorupNum = studentsCount / 3;
            }
            else
            {
                x = studentsCount / 3;
                gorupNum = Math.Round(x + 0.5);
            }

            List<string> groupsStudents = new List<string>();
            string str = "";
            int cntr;

            for (int i = 0; i < gorupNum; i++)
            {
                cntr = 0;
                str = BoysArr[cntr].Id + "," + GirlsArr[cntr].Id + ",";
                //str = BoysArr[cntr].FName + " " + BoysArr[cntr].LName + "," + GirlsArr[cntr].FName + " " + GirlsArr[cntr].LName + ",";
                BoysArr.RemoveAt(cntr);
                GirlsArr.RemoveAt(cntr);
                cntr++;
                groupsStudents.Add(str);
            }
            for (int i = 0; i < groupsStudents.Count; i++)
            {
                cntr = 0;
                if (BoysArr.Count > GirlsArr.Count) //סתם רנדומלי
                {
                    if (BoysArr.Count != 0)
                    {
                        groupsStudents[i] += BoysArr[cntr].Id;
                        //groupsStudents[i] += BoysArr[cntr].FName + BoysArr[cntr].LName;
                        BoysArr.RemoveAt(cntr);
                    }
                }
                else
                {
                    if (GirlsArr.Count != 0)
                    {
                        groupsStudents[i] += GirlsArr[cntr].Id;
                        //groupsStudents[i] += GirlsArr[cntr].FName + " " + GirlsArr[cntr].LName;
                        GirlsArr.RemoveAt(cntr);
                    }

                }
            }
            return groupsStudents; //מחזיר מערך ממויין
        }
        // עד כאן אלמנט חכם 
    }
}