$(document).ready( function (){
    getUserInfo();
});

function getUserInfo()
{
    /*
    $.ajax({
        type: "POST",
        url: '/Account/GetUserInfo',
        success: function (result) {
            if (result.isAuth) {
                $(".na").remove();
                $("#username").text(result.username);
                $("#duty").text(result.duty);
            }
            else {
                $(".aa").remove();
                $("#username").text("незнакомец");
            }
        }
    });
    */
}