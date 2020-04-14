
<<<<<<< HEAD
$(document).ready(function () {
    $('#form1').submit(sub);
    $('#addTeacher').click(saveTeacherDB);
    readFromDB(); 
    //$('#upload').click(uploadTeachers);

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
    $('#addsub').click(addSubj);
    //$('#saveprof').click(saveSubj);
});

function sub() { 
    return false; 
}

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
        Swal.fire({
            icon: 'warning',
            title: 'שדות חסרים',
            text: 'אנא מלא/י את כל השדות😀',
        })
        return false;
    }
    saveAll(teacherArr);
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
            confirmButtonText: 'אישור',
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
    for (var i = 0; i < teacherArr.length; i++) {
        if (teacherArr[i] == profession) {
            Swal.fire({
                icon: 'error',
                title: 'שגיאה...',
                text: 'הכנסת עכשיו את המקצוע הזה',
                confirmButtonText: 'אישור',
                showCloseButton: true
            })
            return;
        }
    }
    Swal.fire({
        icon: 'success',
        title: ' המקצוע נשמר 😀',
        text: 'ניתן להוסיף עוד מקצועות! בסיום יש ללחוץ על שמירה',
        confirmButtonText: 'אישור',
        showCloseButton: true
    })
    teacherArr.push(profession);
    //saveSubj(teacherArr);
}

function saveAll(teacherArr) {
    if (teacherArr == 0) {
        Swal.fire({
                icon: 'error',
                title: 'שגיאה...',
                text: 'לא בחרת מקצועות 😓',
            confirmButtonText: 'אישור',
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
                    "Profession": teacherArr[i],
                    "Password": Math.random().toString(36).substring(7),
                }
            ajaxCall("POST", "../api/Docu/postTeach", JSON.stringify(TeacherObj), POSTsuccess, POSTerror);
        }
    }
}

count = 0;
function POSTsuccess() { /// לנקות לחצנים לאחר שמירה
    count += 1;
    if (count == teacherArr.length) {
        Swal.fire({
            icon: 'success',
            text: 'המורה נוסף/ה למערכת בהצלחה😀',
        })
    }

    // לאחר לחיצה מאפס לחצנים
    //$("#classSelect option:first").attr('selected', 'selected');
    //$("#classNum option:first").attr('selected', 'selected');
    //$("#Year").val("");
    //$("#NumOfs").val("");
    //$("#teacherName option:first").attr('selected', 'selected');
    //$("#classType option:first").attr('selected', 'selected');
}
function POSTerror() { console.log(err) };

=======
>>>>>>> 43a038fdc69b16a68338ac0657333fb1017a35e6

