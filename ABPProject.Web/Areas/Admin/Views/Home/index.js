$(function () {
    var mainHub = $.connection.mainHub; 
    //ÏûÏ¢·¢ËÍ
    mainHub.client.sendMessage = showMessageOtherSend;
    abp.event.on('abp.signalr.connected', function () { });

    //BEGIN TO-DO-LIST
    $('.todo-list').slimScroll({
        "width": '100%',
        "height": '250px',
        "wheelStep": 30
    });
    $(".sortable").sortable();
    $(".sortable").disableSelection();
    //END TO-DO-LIST

    //BEGIN CHAT FORM
    $('.chat-scroller').slimScroll({
        "width": '100%',
        "height": '400px',
        "wheelStep": 30,
        "scrollTo": "100px"
    });
    $('.chat-form input#input-chat').on("keypress", function (e) {
        var $obj = $(this);
        var $me = $obj.parents('.portlet-body').find('ul.chats');
        if (e.which == 13) {
            var content = $obj.val();
            showMessageISend(content);
            mainHub.server.sendMessage(5, content);
        }
    });
    $('.chat-form span#btn-chat').on("click", function (e) {
        e.preventDefault();
        var $obj = $(this).parents('.chat-form').find('input#input-chat');
        var $me = $obj.parents('.portlet-body').find('ul.chats');
        var content = $obj.val();
        showMessageISend(content)
        mainHub.server.sendMessage(5,content);
    });

    function toChatEnd() {
        var $obj = $('.chat-form input#input-chat').parents('.chat-form').find('input#input-chat');
        var $me = $obj.parents('.portlet-body').find('ul.chats');
        var height = 0;
        $me.find('li').each(function (i, value) {
            height += parseInt($(this).height());
        });
        height += '';
        $('.chat-scroller').slimScroll({
            scrollTo: height,
            "wheelStep": 30,
        });
    };
    toChatEnd();
    function showMessageISend(content) {
        var $obj = $('.chat-form input#input-chat').parents('.chat-form').find('input#input-chat');
        var $me = $obj.parents('.portlet-body').find('ul.chats');
        if (content !== "") {
            $me.addClass(content);
            var d = new Date();
            var time = d.toLocaleString();
            $obj.val(""); 
            var element = "";
            element += "<li class='out'>";
            element += "<img class='avatar' src='/images/myicon.png'>";
            element += "<div class='message'>";
            element += "<span class='chat-arrow'></span>";
            element += "<span class='chat-datetime'>" + time + "</span>";
            element += "<span class='chat-body'>" + content + "</span>";
            element += "</div>";
            element += "</li>";

            $me.append(element);
            toChatEnd();
        }
    }

    function showMessageOtherSend(user, content) {
        var $obj = $('.chat-form input#input-chat').parents('.chat-form').find('input#input-chat');
        var $me = $obj.parents('.portlet-body').find('ul.chats');
        if (content !== "") {
            $me.addClass(content);
            var d = new Date();
            var time = d.toLocaleString();
            $obj.val(""); 
            var element = "";
            element += "<li class='in'>";
            element += "<img class='avatar' src='/images/myicon.png'>";
            element += "<div class='message'>";
            element += "<span class='chat-arrow'></span>";
            element += "<a class='chat-name' href='#'>" + user + " &nbsp;</a>";
            element += "<span class='chat-datetime'>" + time + "</span>";
            element += "<span class='chat-body'>" + content + "</span>";
            element += "</div>";
            element += "</li>";

            $me.append(element);
            toChatEnd();
        }
    }
    //END CHAT FORM
});


