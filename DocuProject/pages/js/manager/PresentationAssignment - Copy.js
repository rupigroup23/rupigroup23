//user img
var imagePath1 = '';
var userEmail1 = '';

var taskPath = '';

var className = "";
var classNum = "";
var subj = "";
var topic = "";

$(document).ready(function () {
    //Menus
    localN = localStorage.getItem('myName');
    console.log('local: ', JSON.parse(localN));
    myNameObj = JSON.parse(localStorage["myName"]);
    document.getElementById("myNameN1").innerHTML = myNameObj.name;
    document.getElementById("myNameN2").innerHTML = myNameObj.name;

    //localstorage for the orientation
    local = localStorage.getItem('thisTask');
    TaskObj = JSON.parse(localStorage["thisTask"]);
    //TaskObj11 = JSON.parse(localStorage["thisTask"]);

    className = TaskObj.ClassName;
    classNum = TaskObj.ClassNum;
    subj = TaskObj.Profession;
    topic = TaskObj.Task.Topic;

    Showorientation();

    getDatelis(); //Bring the details up


    //let class1 = TaskObj11.ClassName;
    //let numClass = TaskObj11.ClassNum;
    //let sub = TaskObj11.Profession
    //var topic = TaskObj11.Task.Topic

    ajaxCall('get', '../api/Docu/specifictask/' + className + '/' + classNum + '/' + subj + '/' + topic, '', Success, Error)

    function Success(data) {
        taskPath = data;
        strcheck = taskPath.search("http://proj.ruppin.ac.il");
        if (strcheck == 0) {
            $('#downloadFile').attr("disabled", false);
            $('#showTxt').attr("disabled", true);

        }
        if (strcheck == -1) {
            $('#showTxt').attr("disabled", false);
            $('#downloadFile').attr("disabled", true);
        }
    }
    function Error(err) {
        console.log(err)
    }

    $('#showTxt').click(Showtxt);


    $('#downloadFile').click(function () {
        window.location.replace(taskPath);
    });

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
    function funcerror(err) { console.log(err) }
    //END- User image
    function getSpecificTaskSuccess(data) {
        $('#addpic').attr("disabled", false);
        taskPath = data;

    }
    function getSpecificTaskError(err) { console.log(err) }

});

function Showtxt() {
    txt = document.getElementById("showtxtbox");
    if (txt.style.display === "none") {
        txt.style.display = "block";
    } else {
        txt.style.display = "none";
    }

    //valuTxt
    document.getElementById("valuTxt").innerHTML = taskPath;

}


//סרגל השתלשלות
let orientationSTR = "";
function Showorientation() {
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>" + className + "' " + classNum + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>" + subj + "</a></li>";
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
    document.getElementById("topic").innerHTML = TaskObj.Task.Topic;
    document.getElementById("date").innerHTML = date;
}