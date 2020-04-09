$(document).ready(function () {
    local = localStorage.getItem('thisProfObj');
    console.log('local: ', JSON.parse(local));
    ProfObj = JSON.parse(localStorage["thisProfObj"]);
    console.log(ProfObj);

    Showorientation();
    savelocal();
    readTaskFromDB();
});

///////////////////////
orientationSTR = "";
function Showorientation() {
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' >" + ProfObj.ClassName + "' " + ProfObj.ClassNum + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' >" + ProfObj.Profession + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-tasks.html'>מטלות</a></li>";
    document.getElementById("orientation").innerHTML = orientationSTR;
}
//שומר את הערכים 
function savelocal() {
    thisProfObj1 = {
        "ClassName": ProfObj.ClassName, //ח
        "ClassNum": ProfObj.ClassNum,//4
        "Profession": ProfObj.Profession, //פיזיקה
    }
    console.log(thisProfObj1);
    localStorage.setItem('thisProfObj1', JSON.stringify(thisProfObj1));
}

/////////////////////////

function readTaskFromDB() {
    var apiTasks = "../api/Docu/GetTasks/" + ProfObj.ClassName + "/" + ProfObj.ClassNum + "/" + ProfObj.Profession;
    ajaxCall("GET", apiTasks, "", GETsuccessT1, GETerrorT1);
}
tasksARR = [];
tasksSTR = "";
function GETsuccessT1(data) {
    listCS = data;
    console.log(listCS);

    for (var i = 0; i < listCS.length; i++) {
        tasksSTR += "<div class='col-xl-4 col-sm-6 mb-3'>";
        tasksSTR += "<div class='channels-card' style='padding: 24px 25px;'><div class='row'>";
        tasksSTR += "<div class='col-lg-8 col-sm-12 col-xs-3 choose' style='text-align: right'>";
        tasksSTR += "מטלה מספר ";
        tasksSTR += i+1;
        tasksSTR += "</div><div class='col-lg-4 col-sm-12 col-xs-3 choose' style='text-align:left'>";
        tasksSTR += "<i class='far fa-edit'></i> <i class='far fa-trash-alt'></i>";
        tasksSTR += "</div></div><div class='row'><div class='text2'> נושא:</div> <div class='text2' style='color: black'>";
        tasksSTR += "<h3 class='textSquareGRAY' style='width: 120% %'>";
        tasksSTR += listCS[i].Topic;
        tasksSTR += "</h3></div></div><div class='row'><div class='text2'>תאריך:";
        tasksSTR += "</div><div class='text2'><h3 class='textSquareGRA' style='width: 120 %'>";
        tasksSTR += listCS[i].Deadline;
        tasksSTR += "</h3></div></div> <div class='row' style='padding-right: inherit; display: block;'>";
        tasksSTR += "<div class='channels-card-image'><a href='manager-submission.html'>";
        tasksSTR += " <img class='img-fluid' src='img/הגשות.png' alt='הגשות>'</a>&nbsp;";
        tasksSTR += " <a href='manager-PresentationAssignment.html'><img class='img-fluid' src='img/הצגה ועריכה.png' alt='הצגה ועריכה'>";
        tasksSTR += "</a>&nbsp;< a href = 'manager-watchingvideos.html'><img class='img-fluid' src='img/צפיה בסרטונים.png' alt='סרטונים'></a>";
        tasksSTR += "</div></div></div></div>";
        
        document.getElementById("tasks").innerHTML = tasksSTR;
    }
}
function GETerrorT1(err) {
    console.log(err);
}