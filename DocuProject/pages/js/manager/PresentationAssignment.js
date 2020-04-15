var imagePath1 = '';
var userEmail1 = '';
var taskPath = '';

$(document).ready(function () {
    //Menus
    localN = localStorage.getItem('myName');
    console.log('local: ', JSON.parse(localN));
    myNameObj = JSON.parse(localStorage["myName"]);
    document.getElementById("myNameN1").innerHTML = myNameObj.name;
    document.getElementById("myNameN2").innerHTML = myNameObj.name;

    local = localStorage.getItem('thisTask');
    TaskObj = JSON.parse(localStorage["thisTask"]);
    Showorientation();//סרגל השתלשלות
    getDatelis(); //Bring the details up

    TaskObj11 = JSON.parse(localStorage["thisTask"]);
    $('#addPic').attr("disabled", true);
    let class1 = TaskObj11.ClassName;
    let numClass = TaskObj11.ClassNum;
    let sub = TaskObj11.Profession
    var topic = TaskObj11.Task.Topic


    ajaxCall('get', '../api/Docu/specifictask/' + class1 + '/' + numClass + '/' + sub + '/' + topic, '', Success, Error)

    function Success(data) {
        taskPath = data;
        $('#addPic').attr("disabled", false);

    }
    function Error(err) {
        console.log(err)
    }


    $('#addPic').click(function () {
        window.location.replace(taskPath);
    });

    //User image
    localAdmin = localStorage.getItem('user');
    objAdmin = JSON.parse(localAdmin);
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

    function getSpecificTaskSuccess(data) {
        $('#addpic').attr("disabled", false);
        taskPath = data;

    }
    function getSpecificTaskError(err) { console.log(err) }
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