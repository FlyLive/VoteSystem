$.fn.extend({
    animateCss: function (animationName) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        $(this).addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);
        });
    }
});
$(".image img").mouseover(function () {
    $(this).animateCss("rotateIn");
})
$(".left").click(function () {
    $(".image div").css({"margin-left":"-620px"})
})
$(".right").click(function () {
    $(".image div").css({ "margin-left": "0px" })
})