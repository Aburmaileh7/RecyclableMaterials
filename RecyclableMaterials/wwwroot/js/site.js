document.addEventListener('DOMContentLoaded', function () {
    function updateNotificationCount() {
        fetch('/Notifications/GetUnreadNotificationsCount')
            .then(response => response.json())
            .then(data => {
                const dot = document.getElementById('notification-dot');
                if (data.count > 0) {
                    dot.style.display = 'block';
                    dot.innerText = data.count;
                } else {
                    dot.style.display = 'none';
                }
            })
            .catch(error => console.error('Error fetching notifications count:', error));
    }

    updateNotificationCount();
});
