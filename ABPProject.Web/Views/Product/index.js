
$(function () {
    var productFuns = {
        //增加筛选条件
        addScreen: function (target) {
            var _span = target.html();
            var hasChoose = false;
            if ($(".filterTit > em").length > 0) {
                var leng = $(".filterTit > em").length;
                for (var i = 0; i < leng; i++) {
                    if ($(".filterTit > em").eq(i).find("span").html() == _span) {
                        hasChoose = true;
                    } else {
                        hasChoose = false;
                    }
                }
            }
            if (hasChoose) {
                return;
            } else {
                var str = '<em><input disabled type="text" value='+_span+' /><i></i></em>';
                $(".filterTit").append(str);
            }
        },
        //删除筛选条件
        deleteScreen: function (target) {
            target.parent().remove();
        },
        //增加回话内容
        appendChatCont: function () {
            var $obj = $('.chat-form span#btn-chat').parents('.chat-form').find('input#input-chat');
            var $me = $obj.parents('.portlet-body').find('ul.chats');
            var content = $obj.val();
            if (content !== "") {
                $me.addClass(content);
                var d = new Date();
                var h = d.getHours();
                var m = d.getMinutes();
                if (m < 10) m = "0" + m;
                $obj.val(""); // CLEAR TEXT ON TEXTAREA

                var element = "";
                element += "<li class='out'>";
                element += "<img class='avatar' src='../../images/128.jpg'>";
                element += "<div class='message'>";
                element += "<span class='chat-arrow'></span>";
                element += "<a class='chat-name' href='#'>Admin &nbsp;</a>";
                element += "<span class='chat-datetime'>at July 6, 2014" + h + ":" + m + "</span>";
                element += "<span class='chat-body'>" + content + "</span>";
                element += "</div>";
                element += "</li>";

                $me.append(element);
                var height = 0;
                $me.find('li').each(function (i, value) {
                    height += parseInt($(this).height());
                });
                height += '';

                $('.chat-scroller').slimScroll({
                    scrollTo: height,
                    "wheelStep": 30,
                });
            }
        },
        //内容排序
        productAlign: function (target) {
            if (target.hasClass("active")) {
                var targetI = target.find('i')
                if (targetI.hasClass("fa-long-arrow-up")) {
                    targetI.removeClass("fa-long-arrow-up").addClass("fa-long-arrow-down");
                } else if (targetI.hasClass("fa-long-arrow-down")) {
                    targetI.removeClass("fa-long-arrow-down").addClass("fa-long-arrow-up");
                }
            } else {
                target.addClass("active");
                target.append('<i class="fa fa-long-arrow-up" aria-hidden="true"></i>');
            }
            if (target.hasClass("dealTime")) {
                $(".dealPrice").removeClass("active");
                $(".dealPrice").find("i").remove();
            } else if (target.hasClass("dealPrice")) {
                $(".dealTime").removeClass("active");
                $(".dealTime").find("i").remove();
            }
        },
        //最高价  最低价
        priceRange: function (target) {
            var val = target.val();
            if (val !== "") {
                if (isNaN(val)) {
                    $(this).val("");
                } else {
                    if (val < 0) {
                        val = 10;//最小值
                    }
                    target.val(fmoney(val, 3));
                }
            }
        },
        //认购数量
        changeCount: function (target) {
            var val = parseFloat(target.siblings("input").val());
            if (target.index() == 0) {
                if (val <= 20) {
                    target.attr("disabled", true);
                    target.find("i").css("color", "#999");
                } else {
                    target.attr("disabled", false);
                    target.find("i").css("color", "#000");
                    val -= 20;
                    if (val <= 20) {
                        target.attr("disabled", true);
                        target.find("i").css("color", "#999");
                        target.siblings("input").val(fmoney(20));
                    } else {
                        target.siblings("input").val(fmoney(val));
                    }

                }
            } else if (target.index() == 2) {
                target.siblings("button").attr("disabled", false);
                target.siblings("button").find("i").css("color", "#000");
                val += 20;
                target.siblings("input").val(fmoney(val));
            }
        },
        //打开多选
        getCheckBox: function (target) {
            target.parent().hide();
            var breedWrap = target.parent().parent();//breedWrap
            breedWrap.find(".breedInner a input").show();
            breedWrap.find(".breedSure").show();
        },
        //关闭多选
        closeCheckBox: function (target) {
            var breedWrap = target.parent().parent().parent();//.breedWrap
            breedWrap.find(".breedInner a input").hide();
            breedWrap.find(".breedSure").hide();
            breedWrap.find(".breedCheck").show();
        },
        //添加搜索2017 2018 2019
        addSearch: function (target) {
            var val = target.html();
            $(".searchWrap > input")
                .val(val)
                .focus();
        }
    }
	
	/*增加筛选条件*/
    $("#mainWrap").on("click", ".breedInner a span", function () {
        var _this = $(this);
        productFuns.addScreen(_this);
    })
	
	/*删除筛选条件*/
    $("#mainWrap").on("click", ".filterTit em  i", function () {
        var _this = $(this);
        productFuns.deleteScreen(_this);
    })

    /*洽谈*/
    $("#mainWrap").on("click", ".proBtn", function () {
        $(".chatBox").show();
    })
    $(".closeChat").on("click", function () {
        $(".chatBox").hide();
    })

    /*聊天*/
    $(".chat-scroller").slimScroll({
        "width": '100%',
        "height": '270px',
        "wheelStep": 30,
        "scrollTo": "100px"
    });
    $('.chat-form input#input-chat').on("keypress", function (e) {
        if (e.which == 13) {
            productFuns.appendChatCont();
        }
    });
    $('.chat-form span#btn-chat').on("click", function (e) {

        e.preventDefault();
        productFuns.appendChatCont();

    });

    $("#mainWrap").on("click", ".productName", function () {
        location.href = "../Detail/Index";
    })

    //交易时间 交易价格
    $(".dealTime,.dealPrice").click(function () {
        var _this = $(this);
        productFuns.productAlign(_this);
    })

    //最高价 最低价
    $(".valueRange").blur(function () {
        var _this = $(this);
        productFuns.priceRange(_this);
    })

    //认购数量
    $("#mainWrap").on("click", ".countRange > button", function () {
        var _this = $(this);
        productFuns.changeCount(_this);
    })

    /*多选    确定    取消*/
    $(".breedInner a input").hide();
    $("#mainWrap").on("click", ".breedCheck button", function () {
        var _this = $(this);
        productFuns.getCheckBox(_this);
    })
    $("#mainWrap").on("click", ".buttonClose", function () {
        var _this = $(this);
        productFuns.closeCheckBox(_this);
    })

    /*每页显示条数*/
    $(".changeCount > a").click(function () {
        $(this).addClass("active").siblings().removeClass("active");
    })

    //添加热门搜索
    $(".head").on("click", ".hotSearch a", function () {
        var _this = $(this);
        productFuns.addSearch(_this);
    })
})
