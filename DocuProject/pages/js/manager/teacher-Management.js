//Excel

$(document).ready(function () {
    $('#upload').click(uploadTeachers);
});

teachersArr = new Array();
$(function () {
    $("#load").bind("click", function () {
        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.csv|.txt)$/;
        if (regex.test($("#fileUpload").val().toLowerCase())) {
            if (typeof (FileReader) != "undefined") {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var rows = e.target.result.split("\r\n");
                    for (var i = 0; i < rows.length; i++) {
                        var cells = rows[i].split(",");
                        if (cells.length > 1) {
                            var teacher = {};
                            teacher.FName = cells[0];
                            teacher.LName = cells[1];
                            teacher.Email = cells[2];
                            teacher.City = cells[3];
                            teacher.Street = cells[4];
                            teacher.Id = cells[5];
                            teacher.Bday = cells[6];
                            teacher.PhoneNum = cells[7];
                            teacher.Profession = cells[8];
                            teacher.Password = Math.random().toString(36).substring(7);

                            teachersArr.push(teacher);
                            console.log(teachersArr[0]);
                            console.log(teachersArr.length);
                        }
                    }
                }
                reader.readAsText($("#fileUpload")[0].files[0]);
            }
            else {
                swal("This browser does not support HTML5.", "", "warning")
            }
        }
        else {
            swal("Please upload a valid CSV file", "", "warning")
        }
    });
});
function uploadTeachers() {
    ajaxCall("POST", "../api/Docu/postTeach2", JSON.stringify(teachersArr), POSTsuccess_, POSTerror_);
}
function POSTsuccess_() {
    swal("מורים נוספו למערכת בהצלחה", "על מנת לצפות ברשימת המורים, גש/י למורים", "success")

}
function POSTerror_(err) { console.log(err) }