$(function (){
	
    /*实例化页面*/
    layui.use(['laypage', 'layer'], function () {
        var laypage = layui.laypage, layer = layui.layer;

        laypage({
            cont: 'pages',
            pages: 100,//总页数
            groups: 5 //连续显示分页数
        });

    });

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

})
