var funs = {
    goPage: function (pageName) {
        $("#mainWrap").load(pageName, function () {

        })
    }
}


$(function () {
	
	/*增加筛选条件*/
	$(".breedInner > a > span").click(function (){
		var _span = $(this).html();
		var hasChoose = false;
		if($(".filterTit > em").length > 0){
			var leng = $(".filterTit > em").length;
			for(var i=0; i < leng; i++){
				if($(".filterTit > em").eq(i).find("span").html() == _span){
					hasChoose = true;
				}else {
					hasChoose = false;
				}
			}
		}
		if(hasChoose){
			return;
		}else{
			var str = '<em><span>'+_span+'</span><i></i></em>';
			$(".filterTit").append(str);
		}
	})
	
	/*删除筛选条件*/
	$(".filterTit").on("click","em > i",function (){
		$(this).parent().remove();
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

        var $obj = $(this);
        var $me = $obj.parents('.portlet-body').find('ul.chats');

        if (e.which == 13) {
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
                element += "<span class='chat-datetime'>at July 6, 2014 " + h + ":" + m + "</span>";
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
        }
    });
    $('.chat-form span#btn-chat').on("click", function (e) {

        e.preventDefault();
        var $obj = $(this).parents('.chat-form').find('input#input-chat');
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
    });
    
    $("#mainWrap").on("click", ".productName", function () {
        location.href = "../Detail/Index";
    })

})
