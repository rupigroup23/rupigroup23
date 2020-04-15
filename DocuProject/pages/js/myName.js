function savelocal1(name) {
    myName = {
        "name": name,
    }
    localStorage.setItem('myName', JSON.stringify(myName));
}