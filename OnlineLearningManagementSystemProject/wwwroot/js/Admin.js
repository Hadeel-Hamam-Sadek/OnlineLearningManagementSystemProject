function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
      tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
      tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
  }
  document.getElementById("defaultOpen").click();

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