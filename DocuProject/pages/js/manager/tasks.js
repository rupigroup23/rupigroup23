$(document).ready(function () {
    local = localStorage.getItem('thisProfObj');
    console.log('local: ', JSON.parse(local));
    ProfObj = JSON.parse(localStorage["thisProfObj"]);
    console.log(ProfObj);

    Showorientation();
    readTaskFromDB();
    //$('#oldTask').click(oldTask);
    $('#newTask').click(forCreateTask);
});

function forCreateTask() {
    if (listCS.length == 0) {
        idT = 1;
    }
    else {
        idT = Tnum;
    }
    forNewTask = {
        "ClassName": ProfObj.ClassName, //ח
        "ClassNum": ProfObj.ClassNum,//4
        "Profession": ProfObj.Profession, //פיזיקה
        "Task": {
            "Num": idT,
            "Topic": "",
            "Date": "",
        }
    }
    console.log(forNewTask);

    localStorage.setItem('forNewTask', JSON.stringify(forNewTask));
}

///////////////////////
orientationSTR = "";
function Showorientation() {
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>" + ProfObj.ClassName + "' " + ProfObj.ClassNum + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html' >" + ProfObj.Profession + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-tasks.html'>מטלות</a></li>";
    document.getElementById("orientation").innerHTML = orientationSTR;
}

task = "";
//שומר את הערכים 
function savelocal(thisTask) {
    idT = thisTask.id; //2
    task = listCS[idT-1].Topic; //מרובע

    thisTask= {
        "ClassName": ProfObj.ClassName, //ח
        "ClassNum": ProfObj.ClassNum,//4
        "Profession": ProfObj.Profession, //פיזיקה
        "Task": {
            "Num": idT,    
            "Topic": listCS[idT - 1].Topic,
            "Date": listCS[idT - 1].Deadline,
        }      
    }
    console.log(thisTask);

    localStorage.setItem('thisTask', JSON.stringify(thisTask));
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
    if (listCS.length==0) {
        document.getElementById("oldTask").disabled = true;
    }
    else {
    //
    $('#oldTask').click(Items);

    for (var i = 0; i < listCS.length; i++) {
        Tnum = i + 1;
        tasksSTR += "<div class='col-xl-4 col-sm-6 mb-3'><div class='channels-card' style='padding: 24px 25px;'>";
        tasksSTR += "<div class='row'><div class='col-lg-8 col-sm-12 col-xs-3 choose' style='text-align: right'>";
        tasksSTR += "מטלה מספר ";
        tasksSTR += Tnum;  //1
        tasksSTR += "</div><div class='col-lg-4 col-sm-12 col-xs-3 choose' style='text-align:left'>";
        tasksSTR += "</div></div>";
        tasksSTR += "<div class='row'><div class='text2'>נושא: </div></br> <div class='text2' style='color: black'>";
        tasksSTR += "<h3 class='textSquareGRAY' style='width: 120%'>";
        tasksSTR += listCS[i].Topic; //0
        tasksSTR += "</h3></div></div>";
        tasksSTR += "<div class='row'><div class='text2'>תאריך: </div><div class='text2'>";
        tasksSTR += "<h3 class='textSquareGRA' style='width: 120 %'>";
        tasksSTR += listCS[i].Deadline; //0
        tasksSTR += "</h3></div></div> ";
        tasksSTR += "<div class='row' style='padding-right: inherit; display: block;'>";
        tasksSTR += "<div class='channels-card-image'>";
        tasksSTR += "<a href='manager-submission.html'><img onclick='savelocal(this)' id='" + Tnum +"' class='img-fluid' src='img/הגשות.png' alt='הגשות>'</a>&nbsp;";
        tasksSTR += "<a href='manager-PresentationAssignment.html'><img onclick='savelocal(this)' id='" + Tnum +"' class='img-fluid' src='img/הצגה ועריכה.png' alt='הצגה ועריכה'></a>&nbsp;";
        tasksSTR += "<a href='manager-watchingvideos.html'><img onclick='savelocal(this)' id='" + Tnum +"' class='img-fluid' src='img/צפיה בסרטונים.png' alt='סרטונים'></a>";
        tasksSTR += "</div></div></div></div>";
        
        document.getElementById("tasks").innerHTML = tasksSTR;
    }
    showLastTask(listCS["length"]);
}
}
function GETerrorT1(err) {
    console.log(err);
}

lastSTR = "";
function showLastTask(LastNum) {
    lastSTR += "<div class='row text2' >&nbsp;מטלה אחרונה: ";
    lastSTR += "</div>";
    lastSTR += "<div class='channels-card' style='height: 75%;'>";
    lastSTR += "<div class='row'><div class='col-lg-8 col-sm-12 col-xs-3 choose' style='text-align:right'>מטלה מספר "; 
    lastSTR += LastNum;
    lastSTR += "</div><div class='col-lg-4 col-sm-12 col-xs-3 choose' style = 'text-align:left'>"
    lastSTR += "</div></div>";
    lastSTR += "<div class='row'><div class='col-xl-6 col-sm-6 mb-3'><div class='row'>";
    lastSTR += "<div class='text2'>&nbsp;    נושא: ";
    lastSTR += "</div><div class='text2' style='color: black;'><h3 class='textSquareGRAY' style='width: 120%'>"
    lastSTR += listCS[LastNum-1].Topic;
    lastSTR += "</h3></div></div><div class='row'>";
    lastSTR += "<div class='text2'> &nbsp; תאריך:"
    lastSTR += "</div><div class='text2'><h3 class='textSquareGRAY' style='width: 120 %'>"
    lastSTR += listCS[LastNum-1].Deadline;
    lastSTR += "</h3> </div></div></div>";
    lastSTR += "<div class='col-xl-6 col-sm-6 mb-3'><div style='padding-right: inherit; display: block;'>";
    lastSTR += "<div class='channels-card-image'>";
    lastSTR += "<a href='manager-submission.html'><img onclick='savelocal(this)' id='" + LastNum +"'class='img-fluid' src='img/הגשות.png' alt='הגשות>'</a>&nbsp;";
    lastSTR += "<a href='manager-PresentationAssignment.html' ><img onclick='savelocal(this)' id='" + LastNum +"'class='img-fluid' src='img/הצגה ועריכה.png' alt='הצגה ועריכה'></a>&nbsp;";
    lastSTR += "<a href='manager-watchingvideos.html'><img onclick='savelocal(this)' id='" + LastNum +"'class='img-fluid' src='img/צפיה בסרטונים.png' alt='סרטונים'></a>";
    lastSTR += "</div></div></div></div></div>";

    document.getElementById("lastTask").innerHTML = lastSTR;
}