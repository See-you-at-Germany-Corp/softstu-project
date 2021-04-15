function showNavbar() {
    const navbar = document.querySelector(".nav-container");
    if (parseInt(navbar.style.height) < 120 || !navbar.style.height) {
        navbar.style.height = "120px";
    } else {
        navbar.style.height = "35px";
    }
}
