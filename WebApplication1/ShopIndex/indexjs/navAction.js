$(() => {
    $(".banner-nav-list-item:nth-of-type(n+2),.banner-nav-detail").hover(function () {
        $(".banner-nav-detail").removeClass("not-visible");
    }, function () {
        $(".banner-nav-detail").addClass("not-visible");
    });
    $(".banner-nav-aside-type-item,.banner-nav-aside-detail").hover(function () {
        $(".banner-nav-aside-detail").removeClass("not-visible");
    }, function () {
        $(".banner-nav-aside-detail").addClass("not-visible");
    });
});