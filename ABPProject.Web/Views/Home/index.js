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
})