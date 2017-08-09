$(function (){
	
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
	
})
