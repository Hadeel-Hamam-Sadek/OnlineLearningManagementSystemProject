function myFunction() {
  var x = document.getElementById("myDIV");
  if (x.style.display === "none") {
    x.style.display = "block";
  } else {
    x.style.display = "none";
  }
}
function myFunction2() {
  var x = document.getElementById("myDIV2");
  if (x.style.display === "none") {
    x.style.display = "block";
  } else {
    x.style.display = "none";
  }
}
function myFunction3() {
  var x = document.getElementById("myDIV3");
  if (x.style.display === "none") {
    x.style.display = "block";
  } else {
    x.style.display = "none";
  }
}
let ancors = document.querySelectorAll(".nav-link");
let icons = document.querySelectorAll(".icons");

function changeIcon1() {
  document.getElementById("icon1").style.top = "-17px";
  document.getElementById("icon1").style.color = "#C270A8";
  document.getElementById("icon2").style.top = "-115px";
  document.getElementById("icon2").style.color = "#481D3B";
}
function changeIcon2() {
  document.getElementById("icon2").style.top = "-17px";
  document.getElementById("icon2").style.color = "#C270A8";
  document.getElementById("icon1").style.top = "-115px";
  document.getElementById("icon1").style.color = "#481D3B";
}

for (let i = 0; i < ancors.length; i++) {
  const ancor = ancors[i];
  let icon = icons[i];
  if (ancor.href == window.location.href) {
    icon.style.color = "#C270A8";
    icon.style.top = "-17px";
  }
}
