document.onkeydown = function () {
    if (window.event.keyCode == '13') {
        var form = document.getElementById('LoginForm').submit();
    }
}