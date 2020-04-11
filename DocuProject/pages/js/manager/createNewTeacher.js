$(document).ready(function () {
    $('#form1').submit(sub);
    $('#addTeacher').click(saveTeacherDB);
    readFromDB(); 
    $('#upload').click(uploadTeachers);

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
    saveprof
    $('#addsub').click(addSubj);
    //$('#saveprof').click(saveSubj);

});

function sub() { 
    return false; 
}

//password
//pass = Math.random().toString(36).substring(7);

function saveTeacherDB() 
{
    // בדיקה האם השדות מלאים
    FName = $("#fname").val();
    LName = $("#lname").val();
    Email = $("#email").val();
    City = $("#city").val();
    Street = $("#street").val();
    Id = $("#idT").val();
    Bday = $("#bday").val();
    PhoneNum = $("#phone").val();

    if (FName == "" || LName == "" || Id == "") {
        swal("ישנם שדות חסרים", "אנא מלא/י את כל השדות", "warning")
        return false;
    }
}

//Profession
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

//post
var profession = "";
teacherArr = []; 

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
        checkSub();
    }
}
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

    Swal.fire({
        icon: 'success',
        title: ' המקצוע נשמר 😀',
        text: 'ניתן להוסיף עוד מקצועות! בסיום יש ללחוץ על הוספה',
        confirmButtonText: 'אוקי',
        showCloseButton: true
    })
    teacherArr.push(profession);
    saveSubj(teacherArr);
}

function saveSubj(teacherArr) {
    if (teacherArr == 0) {
        Swal.fire({
                icon: 'error',
                title: 'שגיאה...',
                text: 'לא בחרת מקצועות 😓',
                confirmButtonText: 'אוקי',
                showCloseButton: true
        })
    }
    else {       
        for (var i = 0; i < teacherArr.length; i++) {
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
                    "Profession": ClassSubArr[i],
                    "Password": Math.random().toString(36).substring(7),
                }
            ajaxCall("POST", "../api/Docu/postTeach", JSON.stringify(TeacherObj), POSTsuccess, POSTerror);
        }
    }
}

function POSTsuccess() { /// לנקות לחצנים לאחר שמירה
    Swal.fire({
        icon: 'success',
        //title: 'הצלחת',
        text: 'מורה נוסף/ה למערכת בהצלחה😀',
    })
    // לאחר לחיצה מאפס לחצנים
    //$("#classSelect option:first").attr('selected', 'selected');
    //$("#classNum option:first").attr('selected', 'selected');
    //$("#Year").val("");
    //$("#NumOfs").val("");
    //$("#teacherName option:first").attr('selected', 'selected');
    //$("#classType option:first").attr('selected', 'selected');
}
function POSTerror(err) { console.log(err) };


