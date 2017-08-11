$(function () {
    $(".noteNetNav").hover(function () {
        $(this).find(".navBox").show();
    }, function () {
        $(this).find(".navBox").hide();    
    })

    $(".searchType").hover(function () {
        $(this).find(".searchToggle").slideDown(100);
    }, function () {
        $(this).find(".searchToggle").slideUp(0);
    })

    $("#owl-demo").owlCarousel({
        items: 1,
        autoPlay: true
    });
    $(".fastLink > li").hover(function () {
        $(this).addClass("selected").siblings().removeClass("selected");
    }, function (ev) {
        $(".fastLink > li:nth-child(3)").addClass("selected").siblings().removeClass("selected");
    })

})