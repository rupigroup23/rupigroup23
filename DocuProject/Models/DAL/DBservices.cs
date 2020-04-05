using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using DocuProject.Models;

///Dr = שורה
// dt = טבלה 

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString) // קבוע
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a car to the cars table 
    //--------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con) /////קבוע
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    ////////////////////////////////שמירת כיתות///////////////////////////////////////////////
    public int insert(Class classObj)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DBConnectionString"); // ניצור את הקשר עם הדטה בייס - השם שיופיע פה יופיע בWEBCONFINGS

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            int numEffected = 0;
            string cStr = BuildInsertCommand(classObj);      // לא קבוע - נשנה לפי הערכים בטבלה, 
                                                             //בניית פקודת דחיפה - הכנסה לדאטהבייס
            cmd = CreateCommand(cStr, con);  ///// קבועה - לא לגעת
            numEffected += cmd.ExecuteNonQuery(); // קבועה - לא לגעת , מבצעת את הפקודה 
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommand(Class classObj) // שלב 1 - נעביר את כל המערך לדטה בייס
                                                      //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}',{1},'{2}',{3},'{4}','{5}')", classObj.Name, classObj.Number, classObj.Year, classObj.NumOfStudents, classObj.TeacherName, classObj.ClassType); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO Class_" + "(Name_,Number,year_,numOfStudents,teacherName,classType)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }

    /// <summary>
    /// //////////////////////////שמירת מורים////////////////////////////////////////

    public int insertT(Teacher TeacherObj)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DBConnectionString"); // ניצור את הקשר עם הדטה בייס - השם שיופיע פה יופיע בWEBCONFINGS

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            int numEffected = 0;
            string cStr = BuildInsertCommandT(TeacherObj);      // לא קבוע - נשנה לפי הערכים בטבלה, 
                                                                //בניית פקודת דחיפה - הכנסה לדאטהבייס
            cmd = CreateCommand(cStr, con);  ///// קבועה - לא לגעת
            numEffected += cmd.ExecuteNonQuery(); // קבועה - לא לגעת , מבצעת את הפקודה 
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandT(Teacher TeacherObj) // שלב 1 - נעביר את כל המערך לדטה בייס
                                                           //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}')", TeacherObj.FName, TeacherObj.LName, TeacherObj.Email, TeacherObj.City, TeacherObj.Street, TeacherObj.Id, TeacherObj.Bday, TeacherObj.PhoneNum, TeacherObj.Profession, TeacherObj.Password); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO Teacher__" + "(FName,LName,Email,City,Street,Id_,Bday,PhoneNum,Profession,Password_)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }

    public List<Profession> getFromDB()
    {
        List<Profession> listProfession = new List<Profession>(); // ניצור רשימה לשמירת המידע
        SqlConnection con = null;

        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT DISTINCT nameP FROM Profession_"; // נכתוב שאילתה להוצאת הטבלה 
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                Profession P = new Profession();
                P.Name = (string)dr["nameP"];

                listProfession.Add(P);
            }

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return listProfession; // מחזיר מערך 

    }
    /////////////////////
    public int insertP(Profession proffObj)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DBConnectionString");

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            int numEffected = 0;
            string cStr = BuildInsertCommandP(proffObj);      // לא קבוע - נשנה לפי הערכים בטבלה, 
                                                              //בניית פקודת דחיפה - הכנסה לדאטהבייס
            cmd = CreateCommand(cStr, con);  ///// קבועה - לא לגעת
            numEffected += cmd.ExecuteNonQuery(); // קבועה - לא לגעת , מבצעת את הפקודה 
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandP(Profession proffObj) // שלב 1 - נעביר את כל המערך לדטה בייס
                                                            //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}')", proffObj.Name); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO Profession_" + "(nameP)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }


    ///////////////////
    ///
    public int insertS(List<Student> studentsArr)
    {

        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("DBConnectionString"); // ניצור את הקשר עם הדטה בייס - השם שיופיע פה יופיע בWEBCONFINGS

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            String cStr = "";
            int numEffected = 0;

            foreach (var row in studentsArr)

            {
                cStr = BuildInsertCommandS(row);
                cmd = CreateCommand(cStr, con);
                numEffected += cmd.ExecuteNonQuery();

            }
            return numEffected;

        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandS(Student student)
    //POST                                                
    {

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}','{1}' ,'{2}','{3}','{4}','{5}' ,{6},'{7}','{8}',{9},'{10}')", student.FName, student.LName, student.PhoneNum, student.Email, student.City, student.Address, student.Id, student.Bday, student.ClassName , student.ClassNum, student.Password); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO Student" + "(FName,LName,PhoneNum,Email,City,Street, Id_,Bday,ClassName,ClassNum,Password_) "; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }

    //////////קריאת מורים לטופס כיתה////////////
    public List<Teacher> getFromDBT()
    {
        List<Teacher> listProfession = new List<Teacher>();
        SqlConnection con = null;

        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select distinct FName,Lname from Teacher__"; // נכתוב שאילתה להוצאת הטבלה 
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                Teacher T = new Teacher();
                T.FName = (string)dr["FName"];
                T.LName = (string)dr["LName"];

                listProfession.Add(T);
            }

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return listProfession; // מחזיר מערך 

    }

    /////////////////// הכנסת קובץ אקסל מורים 
    public int insertT2(List<Teacher> tachersArr)
    {

        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("DBConnectionString"); // ניצור את הקשר עם הדטה בייס - השם שיופיע פה יופיע בWEBCONFINGS

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            String cStr = "";
            int numEffected = 0;

            foreach (var row in tachersArr)

            {
                cStr = BuildInsertCommandT2(row);
                cmd = CreateCommand(cStr, con);
                numEffected += cmd.ExecuteNonQuery();

            }
            return numEffected;

        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandT2(Teacher teacher)
    //POST                                                
    {

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}','{4}',{5} ,'{6}','{7}','{8}','{9}')", teacher.FName, teacher.LName, teacher.Email, teacher.City, teacher.Street, teacher.Id, teacher.Bday, teacher.PhoneNum, teacher.Profession, teacher.Password); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO Teacher__" + "(FName,LName,Email,City,Street,Id_,Bday,PhoneNum,Profession,Password_) "; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }


    public Admin check_User(Admin adminInput)
    {
        Admin A = new Admin();

        // שומרת את המשתנים שהוכנסו בפועל
        int id_ = adminInput.Id;
        string pass_ = adminInput.Password;

        SqlConnection con = null;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = $@"SELECT *
                               FROM Admin_
                               where Id_ = '{id_}' and Password_='{pass_}'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                // שומרת את הנתונים מהדטה בייס לתוך אובייקט שיצרתי
                A.Id = (int)dr["Id_"];
                A.Password = (string)dr["Password_"];
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return A;
    }

    public SqlDataAdapter da;
    public DataTable dt;
    public DBservices Get_Nums() // מחזיר איבר מסוג DBSERVICES
    {
        SqlConnection con = null;
        string str = "";
        try /////// בדיקה - איזה טבלה לקחת
        {
            con = connect("DBConnectionString");
       
              str = "select distinct Number,Name_ from Class_ where Name_ <> ' '";
            da = new SqlDataAdapter(str, con); // נשלח את הסטרינג לכאן
            SqlCommandBuilder builder = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }

        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return this; // מחזיר איבר מסוג DB SERVICES
    }

    public DBservices Get_Students(string className, int classNum) // מחזיר איבר מסוג DBSERVICES
    {
        SqlConnection con = null;
        string TBL = "";
        try
        {
            con = connect("DBConnectionString");
            TBL = $@"SELECT *
                               FROM Student
                               where ClassName = '{className}' and ClassNum='{classNum}'";

            da = new SqlDataAdapter(TBL, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }

<<<<<<< HEAD
    //public List<Class> Get_Nums() // מחזיר איבר מסוג DBSERVICES
    //{
    //    List<Class> arr = new List<Class>(); // ניצור רשימה מסוג לוקיישן
    //    SqlConnection con = null;
    //    try
    //    {
    //        con = connect("DBConnectionString");
    //        String selectSTR = "select distinct Number,Name_ from Class_ where Name_ <> ' '";       
    //        SqlCommand cmd = new SqlCommand(selectSTR, con);

    //        // get a reader
    //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
    //        Class C = new Class();
    //        while (dr.Read())
    //        {   
    //            C.Name = (string)dr["Name_"];
    //            C.Number = (int)dr["Number"];
    //            arr.Add(C);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        if (con != null)
    //        {
    //            con.Close();
    //        }
    //    }
    //    return arr;
    //}

    ////////////////////////////////שמירת מקצועות לכיתה///////////////////////////////////////////////
    public int insertClassSub(List<ClassSubjects> classSUbObj)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numEffected = 0;
        try
        {
            con = connect("DBConnectionString");
        }
=======
>>>>>>> c0faff92f3c5c2411e4824f6aaa739439c3415c1
        catch (Exception ex)
        {
            throw (ex);
        }
<<<<<<< HEAD

        // נוסיף לולאה כי קיבל מערך והדטה יודע להכניס רשומה רשומה
        foreach (var row in classSUbObj)
        {
            String cStr = BuildInsertCommand1(row);      // לא קבוע - נשנה לפי הערכים בטבלה, 

            cmd = CreateCommand(cStr, con);

            try
            {
                numEffected += cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }
        if (con != null)
        {
            // close the db connection
            con.Close();
        }
        return numEffected;
    }
    private String BuildInsertCommand1(ClassSubjects classSUbObj) // שלב 1 - נעביר את כל המערך לדטה בייס
                                                      //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}',{1},'{2}','{3}')", classSUbObj.Name, classSUbObj.Number, classSUbObj.ClassType, classSUbObj.Profession); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO classProfession" + "(ClassName,ClassNum,ClassType,Profession)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }

=======
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
        return this; // מחזיר איבר מסוג DB SERVICES
    }
>>>>>>> c0faff92f3c5c2411e4824f6aaa739439c3415c1
}



