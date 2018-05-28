//调整置顶和客服按钮位置
let fixAids = () => {
    let totalWidth = 1920;
    let windowWidth = $(window).width();
    let deltaWidth = 288 - (totalWidth - windowWidth) / 2;
    $('.aids').css("right", deltaWidth + "px");
}

//置顶按钮显示和隐藏
let backToTopFun = () => {
    ($(document).scrollTop() > 0) ? $(".aids .aids-top").show() : $(".aids .aids-top").hide();
}

$(() => {
    $(window).resize(() => {
        fixAids();
    });
    fixAids();
    backToTopFun();
    
    $(".aids .aids-top").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
    });

    $(".aids .aids-service").click(function () {
        window.open('https://www.baidu.com/', '', 'width=940,height=690,top=' + (screen.height / 2 - 345) + ',left=' + (screen.width / 2 - 475) + '');
    });

    $(window).bind("scroll", backToTopFun);
});