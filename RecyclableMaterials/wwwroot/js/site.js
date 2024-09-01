.star-rating {
    direction: rtl;
    display: inline-block;
    font-size: 2rem;
    unicode-bidi: bidi-override;
}

.star-rating input[type="radio"] {
    display: none;
}

.star-rating label {
    color: #ccc;
    cursor: pointer;
    display: inline-block;
    font-size: 2rem;
    padding: 0;
    text-shadow: 1px 1px #bbb;
}

.star-rating label:hover,
.star-rating label:hover ~ label,
.star-rating input[type="radio"]:checked ~ label {
    color: #f2b600;
}





"use strict";

// إنشاء الاتصال مع Hub
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

// استقبال الإشعار من الخادم
connection.on("ReceiveNotification", function (message) {
    var notificationBell = document.getElementById("notificationBell");
    notificationBell.classList.add("new-notification"); // إضافة فئة CSS للإشارة إلى إشعار جديد

    var notificationsContainer = document.getElementById("notificationsContainer");
    var notificationItem = document.createElement("li");
    notificationItem.textContent = message;
    notificationsContainer.appendChild(notificationItem); // إضافة الإشعار إلى قائمة الإشعارات
});

// بدء الاتصال مع Hub
connection.start().catch(function (err) {
    return console.error(err.toString());
});


