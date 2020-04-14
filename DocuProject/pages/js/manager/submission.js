var imagePath1 = '';
var userEmail1 = '';

$(document).ready(function () {
    local = localStorage.getItem('thisTask');
    console.log('local: ', JSON.parse(local));
    TaskObj = JSON.parse(localStorage["thisTask"]);
    console.log(TaskObj);

    Showorientation();//סרגל השתלשלות
    getDatelis(); //Bring the details up
    
    $('#apprVideo').click(approveVidow);
    $('#score').click(giveScore);
    $('#apply').click(filingSubm);

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
orientationSTR = "";
function Showorientation() {
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>" + TaskObj.ClassName + "' " + TaskObj.ClassNum + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>" + TaskObj.Profession + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-tasks.html'>מטלות</a></li>";
    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-tasks.html'>מטלה מספר " + TaskObj.Task.Num + "</a></li>";
    orientationSTR += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-tasks.html'>הגשות</a></li>";
    document.getElementById("orientation").innerHTML = orientationSTR;
}

//Bring the details up
function getDatelis() {
    document.getElementById("taskNum").innerHTML = TaskObj.Task.Num;
    //diffDays = diffDays(TaskObj.Task.Date);
    diffDays = diffDays("4/30/2020");
    document.getElementById("topic").innerHTML = TaskObj.Task.Topic;
    document.getElementById("restDate").innerHTML = diffDays;
}

//Filing submissions
function filingSubm() {
    //radio
    waitingToAppr = document.getElementById("waiting");
    approved = document.getElementById("approved");
    notApproved = document.getElementById("statuses_No");
    allstatus = document.getElementById("allstatus");
    //
    var Waiting = document.getElementById("showWaiting");
    var Approved = document.getElementById("showApproved");
    var NotApproved = document.getElementById("showNotApproved");
    var Allstatus = document.getElementById("show_num_sort");

    if (waitingToAppr.checked) {
        if (Waiting.style.display === "none") {
            Waiting.style.display = "block";
            Allstatus.style.display = "block";
            Approved.style.display = "none";
            NotApproved.style.display = "none";
        }
        else {
            Waiting.style.display = "none";
            Allstatus.style.display = "none";
        }
    }
    if (approved.checked) {
        if (Approved.style.display === "none") {
            Approved.style.display = "block";
            Allstatus.style.display = "block";
            Waiting.style.display = "none";
            NotApproved.style.display = "none";
        }
        else {
            Approved.style.display = "none";
            Allstatus.style.display = "none";
        }
    }
    if (notApproved.checked) {
        if (NotApproved.style.display === "none") {
            NotApproved.style.display = "block";
            Allstatus.style.display = "block";
            Approved.style.display = "none";
            Waiting.style.display = "none";
        }
        else {
            NotApproved.style.display = "none";
            Allstatus.style.display = "none";
        }
    }
    if (allstatus.checked) {
        if (NotApproved.style.display === "none" || Approved.style.display === "none" || Waiting.style.display === "none" ) {
            NotApproved.style.display = "block";
            Allstatus.style.display = "none";
            Approved.style.display = "block";
            Waiting.style.display = "block";
        }
        else {
            NotApproved.style.display = "none";
            Allstatus.style.display = "none";
            Approved.style.display = "none";
            Waiting.style.display = "none";
        }
    }

}

//The time difference
function diffDays(subm) {
    //submission = TaskObj.Task.Date;
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    //var submission = new Date('4/30/2020');
    var submission = new Date(subm);
    var date2 = new Date(month + '/' + day + '/' + year);
    var diffDays = submission.getDate() - date2.getDate();
    return diffDays;
}

//הכפתורים בכל צוות
function approveVidow() {
    Swal.fire({
        title: 'האם את/ה בטוח/ה?',
        text: "אישורך יביא לפרסום הסרטון!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#990099',
        //cancelButtonColor: '#d33',
        confirmButtonText: 'אני בטוח/ה',
        cancelButtonText: 'ביטול'
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'הצלחת!',
                text: 'הסרטון התפרסם במועד ההגשה המתוכן',
                icon: 'success',
                confirmButtonColor: '#990099',
                confirmButtonText: 'הבנתי',
            })
        }
    })
}

function giveScore() {
    (async () => {
        const { value: formValues } = await Swal.fire({
            title: 'ציון ומשוב',
            html: 'משוב- אנא כתבו משוב עבור הפתרון שהועלה' +
                '<input id="swal-input1" class="swal2-input">' + 'ציון- העניקו ציון עבור הסרטון' +
                '<input id="swal-input2" class="swal2-input">',
            focusConfirm: false,
            confirmButtonText: 'הבא',
            confirmButtonColor: '#990099',
            cancelButtonText: 'ביטול',
            showCancelButton: true,
            preConfirm: () => {
                return [
                    document.getElementById('swal-input1').value,
                    document.getElementById('swal-input2').value
                ]
            }
        })
        if (formValues) {
            Swal.fire({
                    text: JSON.stringify(formValues),
                    icon: 'success',
                    showCancelButton: true,
                    confirmButtonColor: '#990099',
                    //cancelButtonColor: '#d33',
                    confirmButtonText: 'שמור',
                    cancelButtonText: 'ביטול'
                })
        }

    })()}

function showVideo(btn) {
    id = btn.id;
    console.log(id);
    var x = document.getElementById("headerPopup" + id);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

