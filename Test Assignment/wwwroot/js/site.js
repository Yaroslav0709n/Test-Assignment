$(document).ready(function () {

    $('#saveBtn').click(function () {
        var message = $('#message').val();

        $.post('/Home/Message', { message: message }, function (data) {
            if (data.success) {
                alert(data.message);
                location.reload();
            }
        });
    });

    loadAllMessages();
    loadCurrentUserMessages()

    function loadCurrentUserMessages() {
        $.ajax({
            url: '/Home/GetCurrentUserMessages',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var tableBody = $('#messageTableBody1');
                tableBody.empty();

                $.each(data, function (index, message) {
                    var row = $('<tr>');
                    row.append('<td>' + (index + 1) + '</td>');
                    row.append('<td  class="name-collumn">' + message.userName + '</td>');
                    row.append('<td  class="message-collumn">' + message.message + '</td>');
                    row.append('<td>' + (new Date(message.time)).toLocaleString() + '</td>');
                    tableBody.append(row);
                });
            },
            error: function () {
                console.log('Error loading data.');
            }
        });
    }

    function loadAllMessages() {
        $.ajax({
            url: '/Home/GetAllUsersMessages',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var tableBody = $('#messageTableBody2');
                tableBody.empty();

                $.each(data, function (index, message) {
                    var row = $('<tr>');
                    row.append('<td>' + (index + 1) + '</td>');
                    row.append('<td  class="name-collumn">' + message.userName + '</td>');
                    row.append('<td  class="message-collumn">' + message.message + '</td>');
                    row.append('<td>' + (new Date(message.time)).toLocaleString() + '</td>');
                    tableBody.append(row);
                });
            },
            error: function () {
                console.log('Error loading data.');
            }
        });
    }
});