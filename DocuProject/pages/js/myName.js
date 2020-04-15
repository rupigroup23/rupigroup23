function savelocal1() {
    //name = myNameObj.firstName + " " + myNameObj.lastName;
    //myName = {
    //    "name": name,
    //}
    //console.log(myName);
    name = "שיר מלכה"
    myName = {
        "name": name,
    }
    console.log(myName);
    localStorage.setItem('myName', JSON.stringify(myName));
}