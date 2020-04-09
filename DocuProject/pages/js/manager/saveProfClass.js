$(document).ready(function () {
    readFromDB(); //  קריאת כל המקצועות מהדאטה בייס
    local = localStorage.getItem('studentObj');
    console.log('local: ', JSON.parse(local));
    classObj = JSON.parse(localStorage["studentObj"]);
    console.log(classObj);
    readClassSUbjFromDB();
    Showorientation();

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

//add all subjects from the table (DB)
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

///////////////////////////////////

var profession = "";
ClassSubArr = [];
function addSubj() // שמירת נתוני המורה בדאטה
{
    profession = $("#selectProf").val();
    if (profession == "1") {
        Swal.fire({
            icon: 'error',
            title: 'שגיאה...',
            text: 'לא בחרת מקצוע 😓',
            confirmButtonText: 'אוקי',
            showCloseButton: true
        })
    }
    else {
        if (profession == "אחר") {
            var X = $("#txtbox").val();
            profession = X;
        }
        checkSub();
    }
}

function checkSub() {
    for (var i = 0; i < ProfARR.length; i++) {
        if (ProfARR[i] == profession) {
            Swal.fire({
                icon: 'error',
                title: 'שגיאה...',
                text: 'המקצוע כבר קיים',
                confirmButtonText: 'אוקי',
                showCloseButton: true
            })
            return;
        }
    }
    //אין כזה מקצוע
    ClassSubObj =
        {
            "Name": classObj.ClassName,
            "Number": classObj.ClassNum,
            "ClassType": 'רגילה',
            "Profession": profession,
        }
    for (var i = 0; i < ClassSubArr.length; i++) {
        if (ClassSubArr[i].Profession == profession) {
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
        text: 'ניתן להוסיף עוד מקצועות, בסיום יש ללחוץ על שמור!',
        confirmButtonText: 'אוקי',
        showCloseButton: true
    })
    ClassSubArr.push(ClassSubObj);
}

/////////////////////////

function saveSubj() {
    if (ClassSubArr == 0) {
        Swal.fire({
            icon: 'error',
            title: 'שגיאה...',
            text: 'לא בחרת מקצועות 😓',
            confirmButtonText: 'אוקי',
            showCloseButton: true
        })
    }
    else {
        ajaxCall("POST", "../api/Docu/postClassSub", JSON.stringify(ClassSubArr), POST1success, POST1error);
    }
}
function POST1success() {
    Swal.fire({
        icon: 'success',
        title: 'הצלחת',
        text: 'המקצועות נוספו לכיתה 😀',
    })
}
function POST1error(err) { console.log(err) };

/////////////////////////

function readClassSUbjFromDB() {
    var apiProf = "../api/Docu/GetClassSubj/" + classObj.ClassName + "/" + classObj.ClassNum;
    ajaxCall("GET", apiProf, "", GETsuccessCB2, GETerrorCB2);
}
ProfARR = [];
ProgSTR = "";
function GETsuccessCB2(data) {
    listCS = data;

    for (var i = 0; i < listCS.length; i++) {
        ProfARR[i] = listCS[i].Profession;
    }
    console.log(ProfARR);
    for (var i = 0; i < ProfARR.length; i++) {
        ProgSTR += " <div class='col-xl-3 col-sm-6 mb-3'>";
        ProgSTR += "<div class='channels-card'>";
        ProgSTR += " <div id='prof" + i + "' class='create-text' style='color: black; font-size: 35px'>";
        ProgSTR += ProfARR[i];
        ProgSTR += "<br /><a href='manager-tasks.html' onclick='savelocal(this)' id='"+i+"'>";
        ProgSTR += "<img class='img-button' src='img/arrow.png' alt='מעבר למקצוע' /></a>"
        ProgSTR += "</div></div></div>";
        document.getElementById("professions").innerHTML = ProgSTR;
    }
}
function GETerrorCB2(err) {
    console.log(err);
}



function savelocal(thisprof) {
    id = thisprof.id; //1
    profession = ProfARR[id]; //פיזיקה

    thisProfObj = {
        "ClassName": listCS[0].Name, //ח
        "ClassNum": listCS[0].Number,//4
        "Profession": profession, //פיזיקה
    }
    console.log(thisProfObj); 
    localStorage.setItem('thisProfObj', JSON.stringify(thisProfObj));
}


/////////////////////////
orientationSTR = "";
function Showorientation() {
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' >" + classObj.ClassName + "' " + classObj.ClassNum + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-professions.html'>מקצועות</a></li>";
    document.getElementById("orientation").innerHTML = orientationSTR;
}
