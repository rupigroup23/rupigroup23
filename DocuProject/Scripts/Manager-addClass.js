
    $(document).ready(function () {

        getTachears(); ////////////////////////
    $('#saveClass').click(saveClassDB);
    $('#upload').click(uploadStudents);

});

        function getTachears() { //////////////////////////

        ajaxCall("GET", "../api/Docu/GetT", "", GETsuccessT, GETerrorT);

}

        function GETsuccessT(data) {

        console.log(data);
    listT = data;

    //insert options to select
            for (var i = 0; i < listT.length; i++) {
                var select = document.getElementById("teacherName");
    var option = document.createElement("option");
    option.text = listT[i].FName + " " + listT[i].LName;
    select.add(option);
}
}
        function GETerrorT(err) {console.log(err)}


    /////////////////////

    function saveClassDB() {
        Year = $("#Year").val();
        NumOfStudents = $("#NumOfs").val();

  

            if (Year == "" || NumOfStudents == "") {
        swal("ישנם שדות חסרים", "אנא מלא/י את כל השדות", "warning");
    return false;
}
classObj =
                {

    "Name": $("#classSelect").val(),
    "Number": $("#classNum").val(),
    "Year": $("#Year").val(),
    "NumOfStudents": $("#NumOfs").val(),
    "TeacherName": $("#teacherName").val(),
    "ClassType": $("#classType").val()
}

ajaxCall("POST", "../api/Docu/postClass", JSON.stringify(classObj), POSTsuccessCB, POSTerrorCB);

}

        function POSTsuccessCB() {

        swal("הכיתה נוספה למערכת", "לחץ להמשך", "success")
            // לאחר לחיצה מאפס לחצנים
            $("#classSelect option:first").attr('selected', 'selected');
    $("#classNum option:first").attr('selected', 'selected');
    $("#Year").val("");
    $("#NumOfs").val("");
    $("#teacherName option:first").attr('selected', 'selected');
    $("#classType option:first").attr('selected', 'selected');
    /////////////
}

        function POSTerrorCB(err) {console.log(err)}; // ממירה קובץ אקסל לאובייקט+ דחיפה למערך

 studentsArr = new Array();
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
                                var student = {};
                                student.FName = cells[0];
                                student.LName = cells[1];
                                student.PhoneNum = cells[2];
                                student.Address = cells[3];
                                student.ClassName = $("#classSelect").val();
                                student.ClassNum = $("#classNum").val();
                  
                                student.Password = Math.random().toString(36).substring(7);

                                studentsArr.push(student);
                                console.log(studentsArr[0]);
                                console.log(studentsArr.length);

                            }
                        }
             
                    }
                    reader.readAsText($("#fileUpload")[0].files[0]);
                } else {
                    swal("This browser does not support HTML5.", "", "warning")

                }
            }
            else {
                swal("Please upload a valid CSV file", "", "warning")

            }

            //ajaxCall("POST", "../api/Docu/addStudents", JSON.stringify(studentsArr), POSTsuccess, POSTerror);
        });
});
        function uploadStudents() {
        ajaxCall("POST", "../api/Docu/addStudents", JSON.stringify(studentsArr), POSTsuccess, POSTerror);

}
        function POSTsuccess(data) {console.log(data)}
    function POSTerror(err) {console.log(err)}


