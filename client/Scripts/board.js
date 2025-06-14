function hideToast() {
    document.getElementsByClassName("page_toast_notification")[0].style.display = "none";
}

function toggleIconDropdown() {
    const iconDropdown = document.getElementsByClassName("board-profile-icon_dropdown")[0];
    const notificationDropdown = document.getElementsByClassName("board-profile-notification_dropdown")[0];

    if (notificationDropdown.style.display === "block") {
        notificationDropdown.style.display = "none";
    }

    if (iconDropdown.style.display === "block") {
        iconDropdown.style.display = "none";
    } else {
        iconDropdown.style.display = "block";
    }
}
function toggleNotificationDropdown() {
    const iconDropdown = document.getElementsByClassName("board-profile-icon_dropdown")[0];
    const notificationDropdown = document.getElementsByClassName("board-profile-notification_dropdown")[0];

    if (iconDropdown.style.display === "block") {
        iconDropdown.style.display = "none";
    }

    if (notificationDropdown.style.display === "block") {
        notificationDropdown.style.display = "none";
    } else {
        notificationDropdown.style.display = "block";
    }
}