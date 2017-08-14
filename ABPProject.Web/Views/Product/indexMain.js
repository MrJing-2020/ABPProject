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

})
