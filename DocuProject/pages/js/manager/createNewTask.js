$(document).ready(function () {
    local = localStorage.getItem('forNewTask');
    console.log('local: ', JSON.parse(local));
    newTaskObj = JSON.parse(localStorage["forNewTask"]);
    Showorientation();

    //$('#saveTask').attr("disabled", true) //?

    //
    $('#uploadFile').click(UploadFile);
    $('#uploadtxtbox').click(UploadTxt);
    //
    $('#upload').click(UploadFileReal);
    $('#saveTask').click(saveTask);

    //User image
    local = localStorage.getItem('admin');
    objAdmin = JSON.parse(local);
    showDetalis(objAdmin);
    function showDetalis(objAdmin) {
        Id = objAdmin.Id;
        var url = "../api/Docu/GetDetails/" + Id;
        ajaxCall("GET", url, "", funcsuccess, funcerror);
    }
    function funcsuccess(data) {
        obj = data;
        userEmail1 = obj[0].Email;
        ajaxCall('GET', '../api/Docu/getavatar/' + obj[0].Id_, '', getAvatarImageSuccess, getAvatarImageError)
    }
    function getAvatarImageSuccess(imagePath1) {

        src = imagePath1; // קיבלנו את הניתוב הארוך למיקום התמונה בשרת
        let arr = src.split('http://localhost:44328/') //למחוק כשמעלים לשרת זה מפצל את החלק של השרת סתם כדי שנראה שעובד

        $("#avatarImage").attr("src", '../' + arr[1] /*imagePath1*/); //בשרת אנחנו מכניסים במקום הניתוב את ה src
    }
    function getAvatarImageError(err) {
        console.log(err)
    }
    function funcerror() { }
    //END- User image
});

//user img
var imagePath1 = '';
var userEmail1 = '';

function UploadFile() {
    //radio
    radioFile = document.getElementById("uploadFile");
    radioTxt = document.getElementById("uploadtxtbox");
    //
    var file = document.getElementById("showUpload"); //קובץ
    var txtbox = document.getElementById("showTxtbox"); //תיבת טקסט

    if (radioFile.checked) {
        if (file.style.display === "none") {
            file.style.display = "block";
            txtbox.style.display = "none";
        }
        else {
            file.style.display = "none";
        }
    }
    if (radioTxt.checked) {
        if (txtbox.style.display === "none") {
            txtbox.style.display = "block";
            file.style.display = "none";
        }
        else {
            txtbox.style.display = "none";
        }
    }
}
function UploadTxt() {
    //radio
    radioFile = document.getElementById("uploadFile");
    radioTxt = document.getElementById("uploadtxtbox");
    //
    var file = document.getElementById("showUpload"); //קובץ
    var txtbox = document.getElementById("showTxtbox"); //תיבת טקסט

    if (radioFile.checked) {
        if (file.style.display === "none") {
            file.style.display = "block";
            txtbox.style.display = "none";
        }
        else {
            file.style.display = "none";
        }
    }
    if (radioTxt.checked) {
        if (txtbox.style.display === "none") {
            txtbox.style.display = "block";
            file.style.display = "none";
        }
        else {
            txtbox.style.display = "none";
        }
    }
}   
//סרגל השתלשלות
let orientationSTR1 = "";
function Showorientation() {
    orientationSTR1 += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR1 += "<li class='breadcrumb-item'><a style='color: black' >" + newTaskObj.ClassName + "' " + newTaskObj.ClassNum + "</a></li>";
    orientationSTR1 += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
    orientationSTR1 += "<li class='breadcrumb-item'><a style='color: black' >" + newTaskObj.Profession + "</a></li>";
    orientationSTR1 += "<li class='breadcrumb-item'><a style='color: black' href='manager-tasks.html'>מטלות</a></li>";
    orientationSTR1 += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-createNewTask.html'>הקמת מטלה חדשה</a></li>";
    document.getElementById("orientation").innerHTML = orientationSTR1; 
    document.getElementById("numTasks").innerHTML = newTaskObj.Task.Num;

}

var assignation = "";
var description = "";

function saveTask() {
    // בדיקה האם השדות מלאים
    Topic = $("#topic").val();
    DateT = $("#dateInput").val();
    
    if (Topic == "" || DateT == "") {
        Swal.fire({
            icon: 'error',
            title: 'שדות חסרים...',
            text: 'יש למלא את כל השדות 😓',
            confirmButtonText: 'אישור',
            showCloseButton: true
        })
        return false;
    }

    radiosStaffing = document.getElementsByName('staffing');
    radiosContent = document.getElementsByName('taskcontent');
    ////////////
    countStaf = "";
    for (var i = 0; i < radiosStaffing.length; i++) {
        if (radiosStaffing[i].checked) {
            assignation = radiosStaffing[i].value;
            console.log(radiosStaffing[i].value);
            countStaf++;
        }
    }
    if (countStaf == 0) {
        Swal.fire({
            icon: 'error',
            title: 'שגיאה...',
            text: 'לא הכנסת אופציית שיבוץ 😓',
            confirmButtonText: 'אישור',
            showCloseButton: true
        })
        return false;

    }
  
    ////////////
    countCont = "";
    val = "";
    for (var i = 0; i < radiosContent.length; i++) {
        if (radiosContent[i].checked) {
            //description = radiosContent[i].value;
            countCont++;
        }
    }
    if (countCont == 0) {
        Swal.fire({
            icon: 'error',
            title: 'שגיאה...',
            text: 'לא בחרת אופציית העלה 😓',
            confirmButtonText: 'אישור',
            showCloseButton: true
        })
        return false;

    }
    if (radiosContent[0].checked==true) { //קובץ
        ///*var taskStr = `http://localhost:44328/${taskPath}`*/ // להחליף בין הניתובים לפני שמעלים לשרת!!!
        var taskStr = `http://proj.ruppin.ac.il/igroup23/prod/${taskPath}`;
    }
    else if (radiosContent[1].checked==true) { //טקסט
        taskStr = $("#taskcontent-txtbox").val();
    }


    TaskObj =
        {
        "ClassName": newTaskObj.ClassName,
        "ClassNum": newTaskObj.ClassNum,
        "Profession": newTaskObj.Profession,
        "Topic": $("#topic").val(),
        "DateT": $("#dateInput").val(),
        "Assignation": assignation,
        "Description": taskStr,
        }
    console.log(TaskObj)

    ajaxCall("POST", "../api/Docu/Tasks", JSON.stringify(TaskObj), POSTsuccess, POSTerror);

}

function POSTsuccess() {
    Swal.fire({
        icon: 'success',
        title: 'הצלחת',
        text: 'המטלה נוצרה בהצלחה! 😀',
    })

}
function POSTerror() {
    Swal.fire({
        icon: 'error',
        title: 'פרטים חסרים',
        text: 'אנא מלא/י את כל הפרטים',
    })
    }

/// העלאת קובץ
//files
var taskPath = '';
var taskId = 0;
var data = "";
var files = "";
function UploadFileReal() {
    console.log('clicked')
    data = new FormData();
    files = $("#fileUpload").get(0).files;
    console.log(files);
    console.log(data);

    // Add the uploaded file to the form data collection
    if (files.length > 0) {
        for (f = 0; f < files.length; f++) {
            data.append("UploadedTasks", files[f]);
        }
        data.append("name", "Shir");
        // aopend what ever data you want to send along with the files. See how you extract it in the controller.
    }
    ajaxCall("POST", "../api/Docu/uploadtask", JSON.stringify(files), POST1success, error);
    return false;

}    
function POST1success() {
    Swal.fire({
        icon: 'success',
        title: 'הקובץ נטען בהצלחה',
        confirmButtonColor: '#990099',
    })
    //$('#saveTask').attr("disabled", false);
    taskPath = files[0].name;

}
function error() {
    alert("wrong");
    //console.log(data);
}

