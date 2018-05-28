$(() => {
    $(".header-search-input").focus(function () {
        $(".header-search-keywords").addClass("not-visible");
    });
    $(".header-search-input").blur(function() {
        let val = $(".header-search-input").val();
        if (val == "") {
            $(".header-search-keywords").removeClass("not-visible");
        }
    });

    $.ajax({
        url: "indexminjs/autocomplete/data.json",
        type: "get",
        dataType: "json",
        success: function (data) {
            $(".header-search-input").autocomplete({
                source: data,
                minLength: 1,
                appendTo: ".header-search-autocomplete",
                response: function (event, ui) {
                    ui.content.splice(4); //显示前4项
                }
            });
        }
    });

    let num = parseInt($(".header-cart-button-num").html());
    $(".header-cart,.header-cart-below").hover(function () {
        if(num > 0){
            $(".header-cart-below").removeClass("not-visible");
        }
        else{
            $(".header-cart-nothing").removeClass("not-visible");
        }
    }, function () {
        if (num > 0) {
            $(".header-cart-below").addClass("not-visible");
        }
        else{
            $(".header-cart-nothing").addClass("not-visible");
        }
    });
});