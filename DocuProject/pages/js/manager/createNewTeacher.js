$(document).ready(function () {
    $('#form1').submit(sub);//FORM
    $('#addTeacher').click(saveTeacherDB); // שמירת פרטי המורה בדאטה
    readFromDB(); //  קריאת כל המקצועות מהדאטה בייס
    $('#upload').click(uploadTeachers);

/////// שינוי בתיבת בחירה ל-"אחר" - תפתח תיבת טקסט חדשה בכל אחת מהאופציות

    $('#selectProf').change(function () {
        var value = $(this).val();
        var X = document.getElementById("txtbox");
        if (value == "אחר") {
            X.style.display = "block";
        }
        else {
            X.style.display = "none";
        }
    });
});

function sub() { // לא יתן להמשיך אם לא יוזנו נתונים
    return false; //////// לא עובד
}

function saveTeacherDB() // שמירת נתוני המורה בדאטה
{
    //checkPreffesion(); // אם הוזן מקצוע חדש, מקצוע מתווסף לטבלת מקצועות
    ////יצירת סיסמא מאחורי הקלעים
    pass = Math.random().toString(36).substring(7);
    ////
    //if ($("#profession").attr('checked', 'checked')) {
    //    addAnotherTeacher(); // אם למורה מספר מקצועות, מתווספת רשומה חדשה למורה
    //}

    //if ($("#selectProf").val() != "1" && $("#selectProf").val() != "שם המקצוע") {
    //    // בודק במידה וזה מקצוע חדש - ישמור את הערך מהתיבת טקסט
    //    if ($("#selectProf").val() == "אחר")
    //        proff = $("#txtbox").val();
    //    else
    //        proff = $("#selectProf").val();
    //}
    // בדיקה האם השדות מלאים
    FName = $("#fname").val();
    LName = $("#lname").val();
    Email = $("#email").val();
    City = $("#city").val();
    Street = $("#street").val();
    Id = $("#idT").val();
    Bday = $("#bday").val();
    PhoneNum = $("#phone").val();
    //Profession = $("#selectProf").val();
    Profession = proff;

    console.log(proff);
    if (FName == "" || LName == "" || Id == "" || Profession == "") {
        swal("ישנם שדות חסרים", "אנא מלא/י את כל השדות", "warning")
        return false;
    }
    ///////////////
    TeacherObj =
    {
        "FName": $("#fname").val(),
        "LName": $("#lname").val(),
        "Email": $("#email").val(),
        "City": $("#city").val(),
        "Street": $("#street").val(),
        "Id": $("#idT").val(),
        "Bday": $("#bday").val(),
        "PhoneNum": $("#phone").val(),
        "Profession": proff,
        "Password": pass
    }
    console.log(TeacherObj);
    ajaxCall("POST", "../api/Docu/postTeach", JSON.stringify(TeacherObj), POSTsuccess, POSTerror);
}

///שמירת ערכי המקצועות
//proff = $("#txtbox").val();
//selcet = $("#selectProf").val();

//function checkPreffesion() { // במידה והוזנו מקצועות חדשים, שומרת לטבלת מקצועות
//    proff = $("#txtbox").val();
//    if ($("#selectProf").val() == "אחר" && proff != "") {
//        proffObj =
//            {
//                "Name": proff
//            }
//        ajaxCall("POST", "../api/Docu/addProff", JSON.stringify(proffObj), POSTsuccess1, POSTerror1);
//    }
//}

function addTeachersPreff() {
    TeacherObj =
    {
        "FName": $("#fname").val(),
        "LName": $("#lname").val(),
        "Email": $("#email").val(),
        "City": $("#city").val(),
        "Street": $("#street").val(),
        "Id": $("#idT").val(),
        "Bday": $("#bday").val(),
        "PhoneNum": $("#phone").val(),
        "Profession": proff,
        "Password": pass
    }
    console.log(TeacherObj);
}
/////////
function POSTsuccess1(data) {console.log(data)}
function POSTerror1() {}

function POSTsuccess2() {}
function POSTerror2() {}

function POSTsuccess3() {}
function POSTerror3() {}

function POSTsuccess4() {}
function POSTerror4() {}

    ///////////////////////

function POSTsuccess_1(data) {console.log(data)}
function POSTerror_1() {}
function POSTsuccess_2(data) {console.log(data)}
function POSTerror_2() {}
function POSTsuccess_3(data) {console.log(data)}
function POSTerror_3() {}
    //////////////////
function readFromDB() {
        ajaxCall("GET", "../api/Docu/GetP", "", GETsuccessCB, GETerrorCB);
}

function GETsuccessCB(data) {
    console.log(data);
    listP = data;

    //insert options to select
    for (var i = 0; i < listP.length; i++) {
        var select = document.getElementById("selectProf");
        var option = document.createElement("option");
        option.text = listP[i].Name;
        select.add(option);
    }
}
function GETerrorCB(err) {
        console.log(err);
}

function POSTsuccess() { /// לנקות לחצנים לאחר שמירה

        swal("מורה נוסף/ה למערכת בהצלחה", "", "success")
        // לאחר לחיצה מאפס לחצנים
        //$("#classSelect option:first").attr('selected', 'selected');
        //$("#classNum option:first").attr('selected', 'selected');
        //$("#Year").val("");
        //$("#NumOfs").val("");
        //$("#teacherName option:first").attr('selected', 'selected');
        //$("#classType option:first").attr('selected', 'selected');
        /////////////
    }

function POSTerror(err) {console.log(err)};

teachersArr = new Array();
$(function () {
        $("#load").bind("click", function () {
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.csv|.txt)$/;
            if (regex.test($("#fileUpload").val().toLowerCase())) {
                if (typeof (FileReader) != "undefined") {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var rows = e.target.result.split("\r\n");
                        for (var i = 0; i < rows.length; i++) {
                            var cells = rows[i].split(",");
                            if (cells.length > 1) {
                                var teacher = {};
                                teacher.FName = cells[0];
                                teacher.LName = cells[1];
                                teacher.Email = cells[2];
                                teacher.City = cells[3];
                                teacher.Street = cells[4];
                                teacher.Id = cells[5];
                                teacher.Bday = cells[6];
                                teacher.PhoneNum = cells[7];
                                teacher.Profession = cells[8];
                                teacher.Password = Math.random().toString(36).substring(7);

                                teachersArr.push(teacher);
                                console.log(teachersArr[0]);
                                console.log(teachersArr.length);
                            }
                        }
                    }
                    reader.readAsText($("#fileUpload")[0].files[0]);
                }
                else {
                    swal("This browser does not support HTML5.", "", "warning")
                }
            }
            else {
                swal("Please upload a valid CSV file", "", "warning")
            }
        });
});
function uploadTeachers() {
        ajaxCall("POST", "../api/Docu/postTeach2", JSON.stringify(teachersArr), POSTsuccess_, POSTerror_);
}

function POSTsuccess_() {
        swal("מורים נוספו למערכת בהצלחה", "על מנת לצפות ברשימת המורים, גש/י למורים", "success")

    }
function POSTerror_(err) {console.log(err)}



////////////////////////shirr

var profession = ""; //המקצוע הרלוונטי
ClassSubArr = []; //מערך המקצועות שהמשתמש שומר ביצירת המורה החדש

function addSubj() //שהמשתמש לוחץ על הוספת מקצוע 
{
    profession = $("#selectProf").val();
    if (profession == "1") { // אם לחץ על שם המקצוע (כלומר לא הוסיף כלום)
        Swal.fire({
            icon: 'error',
            title: 'שגיאה...',
            text: 'לא בחרת מקצוע 😓',
            confirmButtonText: 'אוקי',
            showCloseButton: true
        })
    }
    else { //אם לחץ על אחר
        if (profession == "אחר") {
            var X = $("#txtbox").val();
            profession = X;
        }
        checkSub(); //בדיקה אם המקצוע נמצא כבר בכיתה או אם המקצוע נמצא כבר במערך החדש שהוא יוסיף
    }
}
 //בדיקה אם המקצוע נמצא כבר בכיתה או אם המקצוע נמצא כבר במערך החדש שהוא יוסיף
function checkSub() {
    for (var i = 0; i < ClassSubArr.length; i++) {
        if (ClassSubArr[i] == profession) {
            Swal.fire({
                icon: 'error',
                title: 'שגיאה...',
                text: 'הכנסת עכשיו את המקצוע הזה',
                confirmButtonText: 'אוקי',
                showCloseButton: true
            })
            return;
        }
    }
    proff = profession
    console.log(proff);
    Swal.fire({
        icon: 'success',
        title: ' המקצוע נשמר 😀',
        text: 'ניתן להוסיף עוד מקצועות! בסיום יש ללחוץ על הוספה',
        confirmButtonText: 'אוקי',
        showCloseButton: true
    })
    ClassSubArr.push(proff);
}

