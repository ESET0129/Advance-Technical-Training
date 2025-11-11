$(document).ready(function () {

    // --- 1. Sidebar Toggle ---

    // Check if the user has a saved preference in local storage
    if (localStorage.getItem('sidebarCollapsed') === 'true') {
        $('body').addClass('sidebar-collapsed');
    }

    // Click handler for our toggle button
    $('#sidebarToggle').on('click', function () {
        // Toggle the class on the <body> tag
        $('body').toggleClass('sidebar-collapsed');

        // Save the user's preference
        if ($('body').hasClass('sidebar-collapsed')) {
            localStorage.setItem('sidebarCollapsed', 'true');
        } else {
            localStorage.setItem('sidebarCollapsed', 'false');
        }
    });


    // --- 2. Global Search Bar ---

    // Listen for the "Enter" key in the search bar
    $('#globalSearchInput').on('keyup', function (e) {
        if (e.key === 'Enter' || e.keyCode === 13) {
            var query = $(this).val().toLowerCase().trim();
            var destination = "";

            // Map search terms to their URLs
            switch (query) {
                case "meter":
                case "meters":
                    destination = "/Meter";
                    break;
                case "consumer":
                case "consumers":
                    destination = "/Consumer";
                    break;
                case "user":
                case "users":
                    destination = "/User";
                    break;
                case "tariff":
                case "tariffs":
                    destination = "/Tariff";
                    break;
                case "slab":
                case "slabs":
                case "tariff slabs":
                    destination = "/TariffSlabs";
                    break;
                case "org unit":
                case "orgunit":
                    destination = "/OrgUnit";
                    break;
                case "upload":
                case "uploadmeter":
                    destination = "/UploadMeter";
                    break;
                case "home":
                    destination = "/Home";
                    break;
            }

            if (destination) {
                // If we found a match, go to that page
                window.location.href = destination;
            } else {
                // If no match, just alert the user
                alert("Page not found: " + $(this).val());
            }
        }
    });

    // --- 3. Theme (Dark/Light Mode) Toggle ---

    // Helper function to set the theme and icon
    function setTheme(theme) {
        if (theme === 'dark') {
            document.documentElement.setAttribute('data-bs-theme', 'dark');
            $('#theme-icon-moon').hide();
            $('#theme-icon-sun').show();
            localStorage.setItem('theme', 'dark');
        } else {
            document.documentElement.setAttribute('data-bs-theme', 'light');
            $('#theme-icon-sun').hide();
            $('#theme-icon-moon').show();
            localStorage.setItem('theme', 'light');
        }
    }

    // Set the initial icon state when the page loads
    var currentTheme = localStorage.getItem('theme') || 'light';
    setTheme(currentTheme);

    // Click handler for the toggle button
    $('#theme-toggle').on('click', function () {
        var newTheme = document.documentElement.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
        setTheme(newTheme);
    });

});