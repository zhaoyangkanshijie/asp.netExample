$(() => {
    $(".product-show-list-item-presentation-each").hover(function() {
        $(this).find(".product-show-list-item-presentation-each-container-detail").removeClass("not-visible");
    }, function () {
        $(this).find(".product-show-list-item-presentation-each-container-detail").addClass("not-visible");
    });
});