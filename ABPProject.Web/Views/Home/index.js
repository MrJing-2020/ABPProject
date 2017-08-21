
$(function () {
    var homeFuns = {
        addSearchInfo: function (target) {
            $(".searchIpt > input").val(target.html());
        },
        switchNav: function (target) {
            target.addClass("active").siblings().removeClass("active");
        }
    }
    //网站导航
    $("#homeWrapper").on("mouseover mouseout", ".noteNetNav", function (ev) {
        if (ev.type == "mouseover") {
            $(this).find(".navBox").show();
        } else if (ev.type == "mouseout") {
            $(this).find(".navBox").hide();
        }
    })
    //产品
    $(".searchType").hover(function () {
        $(this).find(".searchToggle").slideDown(100);
    }, function () {
        $(this).find(".searchToggle").slideUp(0);
    })
    //洽谈
    $("#owl-demo").owlCarousel({
        items: 1,
        autoPlay: true
    });
    //采购 销售 储运 融资 咨询
    $(".fastLink > li").hover(function () {
        $(this).addClass("selected").siblings().removeClass("selected");
    }, function (ev) {
        $(".fastLink > li:nth-child(3)").addClass("selected").siblings().removeClass("selected");
    })
    //首页 煤炭交易 钢铁交易 仓储货运 在线融资
    $(".List > li").click(function () {
        var _this = $(this);
        homeFuns.switchNav(_this);
    })
    //动力煤 炼焦煤 无烟煤 喷吹煤
    $("#homeWrapper").on("click", ".hotWord a", function () {
        var _this = $(this);
        homeFuns.addSearchInfo(_this);
    })
    //二维码移入移出
    $(".wrapper").on("mouseover mouseout", ".netAboutErwei", function (ev) {
        if (ev.type == "mouseover") {
            $(".bigCode").show();
        } else if (ev.type == "mouseout") {
            $(".bigCode").hide();
        }
    })

    var homeApp = new Vue({
        el: ".navList",
        data: {
            liShort: [
                {
                    title: "聚丙烯交易",
                    isLong: false,
                    liInfo: [
                        {
                            href: "javascript:;",
                            info: "聚丙烯现货"
                        },
                        {
                            href: "javascript:;",
                            info: "聚丙烯期货"
                        }
                    ]
                }, {
                    title: "电脑交易",
                    isLong: false,
                    liInfo: [
                        {
                            href: "javascript:;",
                            info: "电脑现货"
                        },
                        {
                            href: "javascript:;",
                            info: "电脑托盘"
                        }
                    ]
                }, {
                    title: "仓储货运",
                    isLong: true,
                    liInfo: [
                        {
                            href: "javascript:;",
                            info: "货运追踪"
                        },
                        {
                            href: "javascript:;",
                            info: "运能查询"
                        },
                        {
                            href: "javascript:;",
                            info: "运费试算"
                        },
                        {
                            href: "javascript:;",
                            info: "业务知识"
                        },
                        {
                            href: "javascript:;",
                            info: "货运网点"
                        },
                        {
                            href: "javascript:;",
                            info: "品牌推介"
                        }
                    ]
                }, {
                    title: "在线融资",
                    isLong: true,
                    liInfo: [
                        {
                            href: "javascript:;",
                            info: "业务指南"
                        },
                        {
                            href: "javascript:;",
                            info: "金融机构"
                        },
                        {
                            href: "javascript:;",
                            info: "监管仓库"
                        },
                        {
                            href: "javascript:;",
                            info: "支持品种"
                        }
                    ]
                }, {
                    title: "信息咨询",
                    isLong: true,
                    liInfo: [
                        {
                            href: "javascript:;",
                            info: "行业咨询"
                        },
                        {
                            href: "javascript:;",
                            info: "金融动态"
                        },
                        {
                            href: "javascript:;",
                            info: "价格中心"
                        },
                        {
                            href: "javascript:;",
                            info: "专家访谈"
                        },
                        {
                            href: "javascript:;",
                            info: "储运服务"
                        },
                        {
                            href: "javascript:;",
                            info: "展会信息"
                        }
                    ]
                }
            ]
        }
    })
})