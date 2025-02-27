document.addEventListener("DOMContentLoaded", function () {
    const darkModeToggle = document.getElementById("darkModeToggle");
    const darkModeIcon = document.getElementById("darkModeIcon");
    const html = document.documentElement; // Apply dark mode to <html>

    // Check if dark mode was previously enabled
    if (localStorage.getItem("darkMode") === "enabled") {
        html.classList.add("dark-mode");
        darkModeIcon.classList.replace("fa-moon", "fa-sun");
    }

    darkModeToggle.addEventListener("click", function () {
        // Add a short delay for smooth transition
        setTimeout(() => {
            html.classList.toggle("dark-mode");

            // Update icon based on mode
            if (html.classList.contains("dark-mode")) {
                darkModeIcon.classList.replace("fa-moon", "fa-sun");
                localStorage.setItem("darkMode", "enabled"); // Save to localStorage
            } else {
                darkModeIcon.classList.replace("fa-sun", "fa-moon");
                localStorage.setItem("darkMode", "disabled"); // Save to localStorage
            }
        }, 50); // Tiny delay for a better effect
    });
});
