$(function (){

    /*小喇叭*/
    var timer1;
    var heights = $(".noticeList > ul")[0].offsetHeight;
    var tops = 0;
    clearInterval(timer1);
    timer1 = setInterval(function () {
        if (tops <= -heights + 18) {
            tops = 0;
        } else {
            tops -= 18;
        }
        $(".noticeList > ul").animate({
            top: tops
        }, 500)
    }, 1000)
    
    /*属性    牌号    价格*/
    $(".productInfo").on("mouseover mouseout", ".productList .productLi", function (ev) {
        
        if (ev.type == "mouseover") {
            $(this).find(".litips").show();
            $(this).css({
                backgroundColor: "#eef8ff"
            })
        } else if (ev.type == "mouseout") {
            $(this).find(".litips").hide();
            $(this).css({
                backgroundColor: "#fff"
            })
        }
    })

    //产品列表
    var vm = new Vue({
        el: ".productList",
        data: {
            isXianhuo: true,
            productInfo: [
                {
                    isQihuo: true,
                    type: "期货",
                    name: "LEDF",
                    companyName: "上海鲨鱼",
                    tradeMark: "赛科线性聚乙烯0220KJ",
                    price: "999.00",
                    amount: "1.000",
                    site: "杭州",
                    launchTime: "一天前",
                    dealTime: "2017-07-20",
                    dealPlace: "杭州",
                    minAmount: "20.000"
                },
                {
                    isQihuo: false,
                    type: "现货",
                    name: "LEDF",
                    companyName: "上海鲨鱼",
                    tradeMark: "赛科线性聚乙烯0220KJ",
                    price: "999.00",
                    amount: "1.000",
                    site: "杭州",
                    launchTime: "一天前",
                    dealTime: "2017-07-20",
                    dealPlace: "杭州",
                    minAmount: "20.000"
                }
            ]
        }
    })

    //筛选列表
    var vmScreen = new Vue({
        el: ".breed",
        data: {
            screenList: [
                {
                    screenName: "品  种",
                    screenItem: [
                        { items: "LEDF" ,isHide: false},
                        { items: "LEDF23", isHide: false },
                        { items: "LEDG44",isHide: true}
                    ]
                },
                {
                    screenName: "应  用",
                    screenItem: [
                        { items: "重包", isHide: false },
                        { items: "薄膜", isHide: false },
                        { items: "拉丝", isHide: false },
                        { items: "涂覆", isHide: false },
                        { items: "注塑", isHide: false },
                        { items: "中空", isHide: false },
                        { items: "管材", isHide: true }
                    ]
                },
                {
                    screenName: "生产商",
                    screenItem: [
                        { items: "陕北北元", isHide: false },
                        { items: "新疆天业", isHide: false }

                    ]
                },
                {
                    screenName: "所在地",
                    screenItem: [
                        { items: "陕西", isHide: false },
                        { items: "新疆", isHide: false }
                    ]
                }
            ]
        }
    })
})
