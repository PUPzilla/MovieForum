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
        icon.classList.replace("fa-moon", "fa-sun"); // Change to sun icon
        updateIconColor();
    }

    toggleButton.addEventListener("click", function () {
        body.classList.toggle("dark-mode");

        if (body.classList.contains("dark-mode")) {
            localStorage.setItem("darkMode", "enabled");
            icon.classList.replace("fa-moon", "fa-sun"); // Change to sun
        } else {
            localStorage.setItem("darkMode", "disabled");
            icon.classList.replace("fa-sun", "fa-moon"); // Change to moon
        }

        updateIconColor();
    });
});
