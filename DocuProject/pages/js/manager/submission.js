$(document).ready(function () {
    local = localStorage.getItem('thisProfObj');
    console.log('local: ', JSON.parse(local));
    ProfObj = JSON.parse(localStorage["thisProfObj"]);
    console.log(ProfObj);
    //
    //Showorientation();
});

///////////////////////
orientationSTR = "";
//function Showorientation() {
//    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-index.html'>בית</a></li>";
//    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' >" + ProfObj.ClassName + "' " + ProfObj.ClassNum + "</a></li>";
//    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' href='manager-professions.html'>מקצועות</a></li>";
//    orientationSTR += "<li class='breadcrumb-item'><a style='color: black' >" + ProfObj.Profession + "</a></li>";
//    orientationSTR += "<li class='breadcrumb-item active' aria-current='page'><a href='manager-tasks.html'>מטלות</a></li>";
//    document.getElementById("orientation").innerHTML = orientationSTR;
//}


//approveVidow

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

//function giveScore() {
//    Swal.mixin({
//        confirmButtonText: 'הבא &rarr;',
//        cancelButtonText: 'ביטול',
//        showCancelButton: true,
//        progressSteps: ['1', '2']
//    }).queue([
//        {
//            title: 'משוב',
//            text: 'אנא כתבו משוב עבור הפתרון שהועלה ',
//            input: 'textarea',
//        }, {
//            title: 'ציון',
//            text: ' העניקו ציון עבור הסרטון ',
//            input: 'text',
//        }
//    ])
//}



//Swal.fire({
//    title: 'משוב',
//    text: 'אנא כתבו משוב עבור הפתרון שהועלה ',
//    input: 'textarea',
//    //icon: 'warning',
//    showCancelButton: true,
//    confirmButtonColor: '#3085d6',
//    cancelButtonColor: '#d33',
//    confirmButtonText: 'שמור',
//    cancelButtonText: 'ביטול'
//}).then((result) => {
//    if (result.value) {
//        Swal.fire(
//            'תודה!',
//            'המשוב נשלח לצוות',
//            'success'
//        )
//    }
//})