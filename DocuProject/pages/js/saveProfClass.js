$(document).ready(function () {
    local = localStorage.getItem('studentObj');
    console.log('local: ', JSON.parse(local));
    $('#NewProfBTN').click(saveProfClassDB);
});

ClassSubArr = [];

function saveProfClassDB() {
    var val = $("NewProfession").val();
    console.log(val);
    if (val) {
        alert("אנא הקלידו מקצוע מבוקש")
    }
    else {
            ClassSubObj =
                {
                    "Name": $("#classSelect").val(),
                    "Number": $("#classNum").val(),
                    "ClassType": $("#classType").val(),
                    "Profession": val,
                }
            console.log(ClassSubArr);
        }

        ajaxCall("POST", "../api/Docu/postClassSub", JSON.stringify(ClassSubArr), POST1success, POST1error);

    }
function POST1success() {
    swal("המקצוע נוסף למערכת בהצלחה", "", "success")
}
function POST1error(err) { console.log(err) };
