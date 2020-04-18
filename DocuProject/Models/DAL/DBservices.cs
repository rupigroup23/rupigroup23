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
    public SqlDataAdapter da;
    public DataTable dt;

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

        sb.AppendFormat("Values('{0}','{1}' ,'{2}','{3}','{4}','{5}' ,{6},'{7}','{8}',{9},'{10}')", student.FName, student.LName, student.PhoneNum, student.Email, student.City, student.Address, student.Id, student.Bday, student.ClassName, student.ClassNum, student.Password); // לפי האובייקט במחלקה
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

            String selectSTR = "select distinct FName,Lname,Id_ from Teacher__"; // נכתוב שאילתה להוצאת הטבלה 
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                Teacher T = new Teacher();
                T.FName = (string)dr["FName"];
                T.LName = (string)dr["LName"];
                T.Id = (int)dr["Id_"];
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


    public List<Student> getFromDBST() 
    {
        List<Student> listStudent = new List<Student>();
        SqlConnection con = null;

        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select distinct Id_ from Student"; // נכתוב שאילתה להוצאת הטבלה 
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                Student St = new Student();
                St.Id = (int)dr["Id_"];
                listStudent.Add(St);
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
        return listStudent; // מחזיר מערך 

    }
    // read class
    public List<Class> getFromDBClass()
    {
        List<Class> listClass = new List<Class>();
        SqlConnection con = null;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select distinct Name_, Number, year_, teacherName, classType from Class_"; // נכתוב שאילתה להוצאת הטבלה 
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Class C = new Class();
                C.Name = (string)dr["Name_"];
                C.Number = (int)dr["Number"];
                C.Year = (string)dr["year_"];
                C.TeacherName = (string)dr["teacherName"];
                C.ClassType = (string)dr["classType"];
                listClass.Add(C);
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
        return listClass; // מחזיר מערך 

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
                A.Fname = (string)dr["FName"];
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

    public DBservices Get_Nums() 
    {
        SqlConnection con = null;
        string str = "";
        try
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

    public int insertClassSub(List<ClassSubjects> classSUbObj)
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

            // נוסיף לולאה כי קיבל מערך והדטה יודע להכניס רשומה רשומה
            foreach (var row in classSUbObj)
            {
                cStr = BuildInsertCommand1(row);      // לא קבוע - נשנה לפי הערכים בטבלה, 
                cmd = CreateCommand(cStr, con);  ///// קבועה - לא לגעת
                numEffected += cmd.ExecuteNonQuery(); // קבועה - לא לגעת , מבצעת את הפקודה 

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


    private String BuildInsertCommand1(ClassSubjects classSUbObj) // שלב 1 - נעביר את כל המערך לדטה בייס
                                                                  //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}')", classSUbObj.Name, classSUbObj.Number, classSUbObj.ClassType, classSUbObj.Profession, classSUbObj.Id_teacher, classSUbObj.Teacher_name); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO classProfession" + "(ClassName,ClassNum,ClassType,Profession,Id_teacher,Teacher_name)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }


    public DBservices Get_Techers() // מחזיר איבר מסוג DBSERVICES
    {
        SqlConnection con = null;
        string str = "";
        try
        {
            con = connect("DBConnectionString");
            str = " SELECT * FROM Teacher__";
            da = new SqlDataAdapter(str, con);
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

    public List<ClassSubjects> getCSFromDB(string name, string num)
    {
        //יצירת רשימה לשמירת הנתונים
        List<ClassSubjects> listClassSubj = new List<ClassSubjects>();
        SqlConnection con = null; //שורה קבועה
        try
        {   //שורה קבועה
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = $@"SELECT *
                               FROM classProfession
                               where ClassName = '{name}' and ClassNum='{num}'";
            //שורה קבועה
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            //קורא שורה סוגר וככה הלאה //שורה קבועה
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
                ClassSubjects CS = new ClassSubjects();

                CS.Name = (string)dr["className"];
                CS.Number = (string)dr["classNum"];
                CS.Profession = (string)dr["Profession"];

                listClassSubj.Add(CS);
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
        return listClassSubj; // מחזיר מערך 
    }
    public void Delete(string str ,int id) //כמו GET
    {
        string cStr = "";
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("DBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        try
        {
            if (str == "Student")
            {
                 cStr = $@"delete from
                              Student
                              where id='{id}'";
            }
            else
            {
                 cStr = $@"delete from
                              Teacher__
                              where id='{id}'";
            }

            int numEffected = 0;
            //String cStr = $@"delete from
            //                  Student
            //                  where id='{id}'";

            cmd = CreateCommand(cStr, con);
            numEffected += cmd.ExecuteNonQuery();

            // return numEffected;
        }
        catch (Exception ex)
        {
            //Console.WriteLine(cStr);
            //return 0;
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    
    /// //////////////////////////שמירת תלמיד ספציפי////////////////////////////////////////

    public int insertS2(Student StudentObj)
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
            string cStr = BuildInsertCommand_S(StudentObj);      // לא קבוע - נשנה לפי הערכים בטבלה, 
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
    private String BuildInsertCommand_S(Student StudentObj) // שלב 1 - נעביר את כל המערך לדטה בייס
                                                            //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 
        //string[] arr;
        //string bday = StudentObj.Bday.ToString();
        //arr = bday.Split('/');
        //string newBday = arr[1] + '-' + arr[0] + '-' + arr[2];
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}', {9} ,'{10}')", StudentObj.FName, StudentObj.LName, StudentObj.PhoneNum, StudentObj.Email, StudentObj.City, StudentObj.Address, StudentObj.Id, StudentObj.Bday, StudentObj.ClassName, StudentObj.ClassNum, StudentObj.Password); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO Student (FName,LName,PhoneNum,Email,City,Street,Id_,Bday,ClassName,ClassNum,Password_)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }

    ////////////////////////////////שמירת מטלות///////////////////////////////////////////////
    public int insertTask2(Task taskObj)
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
           
                                                                 //בניית פקודת דחיפה - הכנסה לדאטהבייס
            string cStr = BuildInsertCommandTask(taskObj);      // לא קבוע - נשנה לפי הערכים בטבלה, 
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

    private String BuildInsertCommandTask(Task taskObj) // שלב 1 - נעביר את כל המערך לדטה בייס
                                                        //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", taskObj.ClassName, taskObj.ClassNum, taskObj.Profession, taskObj.Deadline, taskObj.Topic, taskObj.Assignation, taskObj.Description,taskObj.Video); // לפי האובייקט במחלקה
        String prefix = "INSERT INTO Task" + "(ClassName,ClassNum,Profession,Deadline,Topic,Assignation,Description_,video)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------



    //////////////////////////שמירת תמונה לתלמיד////////////////////////////////////////

    public int insertPic(ImgStudent StudentImage)
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
            string cStr = "UPDATE Student SET Image_='" + StudentImage.Img + "' where Email='" + StudentImage.Email + "'";    // לא קבוע - נשנה לפי הערכים בטבלה, 
                                                                                                                              //בניית פקודת דחיפה - הכנסה לדאטהבייס
            cmd = CreateCommand(cStr, con);  
            numEffected += cmd.ExecuteNonQuery(); 
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }
    }

    /// //////////////////////////שמירת תמונה אדמין////////////////////////////////////////

    public int insertPic2(ImgAdmin AdminImage)
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
            string cStr = "UPDATE Admin_ SET Image_='" + AdminImage.Img + "' where Email='" + AdminImage.Email + "'";    // לא קבוע - נשנה לפי הערכים בטבלה, 
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
    }
    public List<Task> getTaskFromDB(string name, string num, string prof)
    {
        //יצירת רשימה לשמירת הנתונים
        List<Task> listTasks = new List<Task>();
        SqlConnection con = null; //שורה קבועה
        try
        {   //שורה קבועה
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = $@"SELECT *
                               FROM Task
                               where ClassName = '{name}' and ClassNum='{num}' and Profession='{prof}'";
            //שורה קבועה
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            //קורא שורה סוגר וככה הלאה //שורה קבועה
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
                Task T = new Task();

                T.ClassName = (string)dr["className"];
                T.ClassNum = (string)dr["classNum"];
                T.Profession = (string)dr["Profession"];
                T.Deadline = (string)dr["Deadline"];
                T.Topic = (string)dr["Topic"];
                //T.Assignation = (string)dr["Assignation"];
                //T.Description = (string)dr["Description_ "];

                listTasks.Add(T);
            }
            return listTasks; // מחזיר מערך 

        }
        catch (Exception ex)
        {
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

    public void update()
    {
        da.Update(dt);
    }

    public DBservices Get_Details(int ID, string str)
    {
        string selectSTR = "";
        Admin A = new Admin();
        SqlConnection con = null;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file


            if (str == "teacher")
            {
                selectSTR = $@"select * from
                              Teacher__
                              where Id_='{ID}'";
            }
            else
            {
                selectSTR = $@"select * from
                              Admin_
                              where Id_='{ID}'";

            }
            da = new SqlDataAdapter(selectSTR, con);
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

    public string getAvatarImage(string Id,string str )
    {
        string selectSTR = "";
        string imagePath = "";
        SqlConnection con = null;
        try
        {   
            if (str == "admin" )
            {
                selectSTR = "select Image_ from Admin_ where Id_='" + Id + "'";
            }
            else
            {
                selectSTR = "select Image_ from Teacher__ where Id_='" + Id + "'";

            }
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file       
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            //קורא שורה סוגר וככה הלאה //שורה קבועה
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
                imagePath = (string)dr["Image_"];
            }
            return imagePath; // מחזיר מערך 

        }
        catch (Exception ex)
        {
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


    public int Post_Admin(Admin admin)
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

            //בניית פקודת דחיפה - הכנסה לדאטהבייס
            string cStr = BuildInsertCommandAd(admin);      // לא קבוע - נשנה לפי הערכים בטבלה, 
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

    private String BuildInsertCommandAd(Admin admin)
                                                        //POST                                                   //  - לא קבוע ! מפרק את המידע ויוצר שאילתה
    { ////עובר שורה שורה 

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string


        sb.AppendFormat("Values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", admin.Fname, admin.Lname, admin.Id, admin.Email, admin.City, admin.Street, admin.Bday, admin.PhoneNum, admin.Profession,admin.Password); 
        String prefix = "INSERT INTO Admin_" + "(FName,LName,Id_,Email,City,Street,Bday,PhoneNum,Profession,Password_)"; // לפי העמודות בSQL
        command = prefix + sb.ToString();

        return command;
    }
    
    public string getSpecificTask(string class1, string numClass, string sub, string topic)
    {
        string taskPath = "";
        SqlConnection con = null; 
        try
        {   
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select Description_ from Task where ClassName = '" + class1 + "' and classNum = '" + numClass + "'and Profession = '" + sub + "'and Topic='" +topic + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
                taskPath = (string)dr["Description_"];
            }
            return taskPath; // מחזיר מערך 
        }
        catch (Exception ex)
        {
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

    public Teacher check_User2(Teacher teacher)
    {
        Teacher T = new Teacher();

        // שומרת את המשתנים שהוכנסו בפועל
        int id_ = teacher.Id;
        string pass_ = teacher.Password;

        SqlConnection con = null;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = $@"SELECT *
                               FROM Teacher__
                               where Id_ = '{id_}' and Password_='{pass_}'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                // שומרת את הנתונים מהדטה בייס לתוך אובייקט שיצרתי
                T.Id = (int)dr["Id_"];
                T.Password = (string)dr["Password_"];
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
        return T;
    }

    public int insertPic2(ImgTeacher img)
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
            string cStr = "UPDATE Teacher__ SET Image_='" + img.Img + "' where Email='" + img.Email + "'";    // לא קבוע - נשנה לפי הערכים בטבלה, 
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
    }

    public List<Task> getVideos(string ClassName, string ClassNum, string Professtion, string Topic, string Deadline)
    {
        List<Task> videoList = new List<Task>();
        string p = Professtion;
        SqlConnection con = null;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT video from Task where ClassName='" + ClassName + "' and ClassNum ='" + ClassNum + "' and Profession = '" + p + "' and Topic='" + Topic + "' and Deadline='" + Deadline + "'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                // שומרת את הנתונים מהדטה בייס לתוך אובייקט שיצרתי
                Task v = new Task();
                v.Video = (string)dr["video"];
                videoList.Add(v);
            }
            return videoList;
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
    }




    //public DBservices Get_DetailsT(int ID)
    //{
    //    Task T = new Task();
    //    SqlConnection con = null;
    //    try
    //    {
    //        con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

    //        String selectSTR = $@"select * from
    //                          Task
    //                          where Id='{ID}'";
    //        da = new SqlDataAdapter(selectSTR, con);
    //        SqlCommandBuilder builder = new SqlCommandBuilder(da);

    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        dt = ds.Tables[0];
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
    //    return this; // מחזיר איבר מסוג DB SERVICES
    //}
    //public void update1()
    //{
    //    da.Update(dt);
    //}
}












