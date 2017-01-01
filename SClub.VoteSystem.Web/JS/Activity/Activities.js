function AuthorHover() {
    $("#author2").show();
}
function AuthorOut() {
    $("#author2").hide();
}
$(".close").click(function () {
    $(".film").hide();
    $("#recordIframe").fadeOut();
});
function show() {
    $(".film").show();
    $("#recordIframe").fadeIn();
}
function showRecord(data) {
    $.ajax({
        type: "GET",
        url: "/Vote/VoteRecordByActivityId",
        data: data,
        datatype: "html",
        success: function (data) {
            $('.recordIframe').html(data);
            show();
        },
        error: function () {
            alert("读取记录失败,错误详情请咨询管理员!");
        }
    });
}
$(".recordCheck").click(function () {
    var data = { id: $(this).val() };
    showRecord(data);
})
function showVoteRank(data) {
    $.ajax({
        type: "GET",
        url: "/Vote/VoteRankByActivityId",
        data: data,
        datatype: "html",
        success: function (data) {
            $('.recordIframe').html(data);
            show();
        },
        error: function () {
            alert("读取记录失败,错误详情请咨询管理员!");
        }
    });
}
$(".voteRankCheck").click(function () {
    var data = { id: $(this).val() };
    showVoteRank(data);
})
$(".logo").mouseover(function(){
    $(this).animateCss("swing");
})

$.fn.extend({
    animateCss: function (animationName) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        $(this).addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);
        });
    }
});