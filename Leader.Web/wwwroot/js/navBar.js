var navbarToggle = document.getElementsByClassName("navbar-toggle")[0]
var navLinks = document.getElementsByClassName("navbar-links")[0]

if (navbarToggle != null) {
    navbarToggle.addEventListener('click', () => {
        navLinks.classList.toggle('active');
    })
}