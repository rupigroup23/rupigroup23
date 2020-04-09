$(document).ready(function () {
    local = localStorage.getItem('thisProfObj');
    console.log('local: ', JSON.parse(local));
    ProfObj = JSON.parse(localStorage["thisProfObj"]);
    console.log(ProfObj);

    Showorientation();
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
