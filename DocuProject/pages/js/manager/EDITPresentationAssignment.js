$(document).ready(function () {
    local = localStorage.getItem('thisTask');
    console.log('local: ', JSON.parse(local));
    TaskObj = JSON.parse(localStorage["thisTask"]);
    console.log(TaskObj);

    Showorientation();//סרגל השתלשלות
    getDatelis(); //Bring the details up

    $('#saveDetails').click(saveDetails);

});


//סרגל השתלשלות
let orientationSTR = "";
function Showorientation() {
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>" + TaskObj.ClassName + "' " + TaskObj.ClassNum + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>" + TaskObj.Profession + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-tasks.html'>מטלות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-tasks.html'>מטלה מספר " + TaskObj.Task.Num + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-PresentationAssignment.html'>הצגה ועריכה</a></li>";
    document.getElementById("orientation").innerHTML = orientationSTR;
}

//Bring the details up
function getDatelis() {
    document.getElementById("taskNum").innerHTML = TaskObj.Task.Num;
    //date = diffDays(TaskObj.Task.Date);
    date = "4/30/2020";
    $('#topic').val(TaskObj.Task.Topic);
    $('#date').val(date);
}

function saveDetails() {
    id = TaskObj.Task.Num;
    Details = {
        "ClassName": TaskObj.ClassName,
        "ClassNum": TaskObj.ClassNum,
        "Profession": TaskObj.Profession,
        "Topic": $("#topic").val(),
        "DateT": $("#date").val(),
    }
    ApiEditTask = "../api/Docu/updatetTask/"+id;
    console.log(Details);
    console.log(ApiEditTask);
    //ajaxCall("PUT", url, JSON.stringify(ApiEditTask), updateDsuccess, updateDerror);
}
