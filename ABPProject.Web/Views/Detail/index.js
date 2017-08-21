$(function () {
    
    //返回上一页
    $(".back").on("click", function () {
        location.href = "../Product/Index";
    })
    //图片hover效果
    $(".showPhoto > li").on("mouseover mouseout", function (event) {
        if (event.type == "mouseover") {
            $(this).find("p").show();
        } else if (event.type == "mouseout") {
            $(this).find("p").hide();
        }
    })

    var vmDetail = new Vue({
        el: "#mainWrap",
        data: {
            slideImg: [
                {
                    imgSrc: "../../images/sw (1).jpg",
                    href: "javascript:;"
                },
                {
                    imgSrc: "../../images/sw (2).jpg",
                    href: "javascript:;"
                }
            ]
        },
        mounted: function () {
            var swiper = new Swiper('.swiper-container', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                spaceBetween: 30,
                centeredSlides: true,
                autoplay: 2500,
                autoplayDisableOnInteraction: false
            });
        }
    })
})