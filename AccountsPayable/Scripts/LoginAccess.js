$(document).ready(function () {
    $("#frm").submit(function (e) {
        e.preventDefault();

        // Access the Access Controller to obtain login data
        var url = window.location.origin + '/Access/LoginAuthorize';
        var param = $(this).serialize();

        $.post(url, param, function (data) {
            // If login data matches with DB Data, redirects to the main page
            if (data == "1") {
                // Trigger the notification check
                checkNotifications();

                // Redirect to the main page
                document.location.href = window.location.origin + '/Main/Create';
            } else {
                // Send error message to label
                document.getElementById('errormessage').innerHTML = (data);
            }
        });
    });

    const password = document.querySelector('#password');
    const message = document.querySelector('.message');

    password.addEventListener('keyup', function (e) {
        if (e.getModifierState('CapsLock')) {
            message.textContent = 'Caps lock is on';
        } else {
            message.textContent = '';
        }
    });
});
function checkNotifications() {
    $.ajax({
        type: 'POST',
        url: '/AssetsNotifications/CheckNotifications',
        success: function (data) {
            if (data.success) {
                var alertMessages = data.alertMessages;
            }
        }
    });
}
