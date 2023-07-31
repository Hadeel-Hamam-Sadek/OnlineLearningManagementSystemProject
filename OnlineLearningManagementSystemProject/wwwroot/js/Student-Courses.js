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