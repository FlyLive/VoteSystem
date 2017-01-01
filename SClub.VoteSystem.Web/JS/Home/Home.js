function StartTime() {
    var newDate = new Date();
    var div = document.getElementById("time");
    var h = newDate.getHours();
    var m = newDate.getMinutes();
    var s = newDate.getSeconds();
    m = Single(m);
    s = Single(s);
    div.innerHTML = h + ":" + m + ":" + s;
    t = setTimeout('StartTime()', 500);
}
function Single(obj) {
    if (obj < 10)
        return "0" + obj;
    return obj;
}
$("#login").click(function () {
    $("#loginIframe").fadeIn();
    $(".film").show();
})
$("#register").click(function () {
    $("#registerIframe").fadeIn();
    $(".film").show();
})
$("#loginClose").click(function () {
    $("#loginIframe").fadeOut();
    $(".film").hide();
})
$("#registerClose").click(function () {
    $("#registerIframe input").html("");
    $("#registerIframe").fadeOut();
    $(".film").hide();
})

$.fn.extend({
    animateCss: function (animationName) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        $(this).addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);
        });
    }
});