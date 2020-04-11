$(document).ready(function () {
    local = localStorage.getItem('forNewTask');
    console.log('local: ', JSON.parse(local));
    newTaskObj = JSON.parse(localStorage["forNewTask"]);

    console.log(newTaskObj);
    $('#saveTask').click(saveTask);

    Showorientation();
});

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

///////////////////////
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
    /////////////

    var radiosStaffing = document.getElementsByName('staffing');
    var radiosContent = document.getElementsByName('taskcontent');
    /////////////
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
    }
    ////////////
    countCont = "";
    for (var i = 0; i < radiosContent.length; i++) {
        if (radiosContent[i].checked) {
            description = radiosContent[i].value;
            console.log(radiosContent[i].value);
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
    }
    ////////////
    
    TaskObj =
        {
        "ClassName": newTaskObj.ClassName,
        "ClassNum": newTaskObj.ClassNum,
        "Profession": newTaskObj.Profession,
        "Topic": $("#topic").val(),
        "DateT": $("#dateInput").val(),
        "Assignation": assignation,
        "Description": description,
        }
    console.log(TaskObj)

    ajaxCall("POST", "../api/Docu/Tasks", JSON.stringify(TaskObj), POSTsuccess, POSTerror);

}

function POSTsuccess(data) {
    Swal.fire({
        icon: 'success',
        title: 'הצלחת',
        text: 'המטלה נוצרה בהצלחה! 😀',
    })

}
function POSTerror() {
    alert("ops");
    }
