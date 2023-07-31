var form = document.getElementById("editable");
var elements = form.elements;
for (var i = 0, len = elements.length; i < len; ++i) {
    elements[i].readOnly = true;

}

function edit() {
    inputs = document.querySelectorAll(".inputs");

    var form = document.getElementById("editable");
    var elements = form.elements;

    for (let i = 0; i < inputs.length; i++) {
        inputs[i].readOnly = false;
        inputs[i].style.border = "1px solid";
    }

    document.querySelector('button[type=submit]').removeAttribute('hidden');
}

let ancors = document.querySelectorAll('.nav-link');
let icons=document.querySelectorAll('.icons');

for (let i = 0; i < ancors.length; i++) {
    const ancor = ancors[i];
    let icon=icons[i];
    if (ancor.href == window.location.href) {

        icon.style.color = "#C270A8";
        icon.style.top = "-17px";

    }

}