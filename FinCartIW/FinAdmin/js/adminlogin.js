function validateuser() {
    var username = $('#txtAdminUserName').val();
    var password = $('#txtAdminPassword').val();
    $("#login-button").prop('disabled', true);
    $("#login-button").text('Please Wait...');

        $.ajax({
            type: "POST",
            url: "index.aspx/Authenticate",
            data: '{username:"' + username + '",password:"' + password + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var resp = response.d;
                if (resp.error) {
                    $("#usermsg").text(resp.error);
                }
                else {
                    $("#usermsg").text("Welcome " + resp.name);
                    $("#redirect-button").attr("href", resp.page);
                    $("#redirect-button").text("Go To Dashboard");
                    $("#redirect-button").show();
                    $('.form').fadeOut(500);
                    $('.wrapper').addClass('form-success');
                }
                $("#login-button").prop('disabled', false);
                $("#login-button").text('Login');
            },
            failure: function (response) {
                var resp = response.d;
                if (resp.error) {
                    $("#usermsg").text(resp.error);
                }
                $("#login-button").prop('disabled', false);
                $("#login-button").text('Login');
            },
            error: function (response) {
                var resp = response.d;
                if (resp.error) {
                    $("#usermsg").text(resp.error);
                }
                $("#login-button").prop('disabled', false);
                $("#login-button").text('Login');
            }
        });
    }
 
function submitOnEnter(inputElement, event) {
    if (event.keyCode == 13) { // No need to do browser specific checks. It is always 13.
        validateuser();
    }
}