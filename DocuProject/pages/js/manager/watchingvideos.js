var imagePath1 = '';
var userEmail1 = '';

$(document).ready(function () {
    local = localStorage.getItem('thisTask');
    console.log('local: ', JSON.parse(local));
    TaskObj = JSON.parse(localStorage["thisTask"]);
    console.log(TaskObj);

    Showorientation();//סרגל השתלשלות

    //User image

    local = localStorage.getItem('admin');
    objAdmin = JSON.parse(local);
    showDetalis(objAdmin);

    function showDetalis(objAdmin) {
        Id = objAdmin.Id;
        var url = "../api/Docu/GetDetails/" + Id;

        //ID = objAdmin.Id;
        //var url = "../api/Docu/GetDetails/" + ID;
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


//סרגל השתלשלות
let orientationSTR = "";
function Showorientation() {
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>" + TaskObj.ClassName + "' " + TaskObj.ClassNum + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>" + TaskObj.Profession + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-tasks.html'>מטלות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-tasks.html'>מטלה מספר " + TaskObj.Task.Num + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-watchingvideos.html'>צפייה בסרטונים</a></li>";
    document.getElementById("orientation").innerHTML = orientationSTR;
    document.getElementById("taskNum").innerHTML = TaskObj.Task.Num;
}

//Bring the details up
function getDatelis() {
    //date = diffDays(TaskObj.Task.Date);
    date = "4/30/2020";
    document.getElementById("topic").innerHTML = TaskObj.Task.Topic;
    document.getElementById("date").innerHTML = date;
}