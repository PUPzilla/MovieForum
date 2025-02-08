document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("darkModeToggle");
    const icon = document.getElementById("darkModeIcon");
    const body = document.body;

    function updateIconColor() {    
        icon.style.color = body.classList.contains("dark-mode") ? "white" : "black";
    }

    // Check if dark mode was previously enabled
    if (localStorage.getItem("darkMode") === "enabled") {
        body.classList.add("dark-mode");

        // Change to sun icon
        icon.classList.replace("fa-moon", "fa-sun");
        updateIconColor();
    }

    toggleButton.addEventListener("click", function () {
        body.classList.toggle("dark-mode");

        if (body.classList.contains("dark-mode")) {
            localStorage.setItem("darkMode", "enabled");
            // Change to sun icon
            icon.classList.replace("fa-moon", "fa-sun");
        } else {
            localStorage.setItem("darkMode", "disabled");
            // Change to moon icon
            icon.classList.replace("fa-sun", "fa-moon");
        }

        updateIconColor();
    });
});
