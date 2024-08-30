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


