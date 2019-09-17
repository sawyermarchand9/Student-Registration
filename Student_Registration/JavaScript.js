var modal = document.getElementById("myModal");

// Get the button
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
btn.onclick = function () {
    // get the new users name
    var firstName = document.getElementById("first-name").value;
    var lastName = document.getElementById("last-name").value;
    // set the complete message
    document.getElementById("message-complete").innerHTML = "Thank you for registering at Merganser University "
        + firstName + " " + lastName;
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}