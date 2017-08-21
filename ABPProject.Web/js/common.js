function fmoney(s, n) {
    n = n > 0 && n <= 20 ? n : 3;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
    if (s < 0) {
        s = parseFloat(Math.abs(s));
        s = parseFloat(s).toFixed(3);
        var l = s.split(".")[0].split("").reverse(),
            r = s.split(".")[1];
        t = "";
        for (i = 0; i < l.length; i++) {
            t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
        }
        return "-" + t.split("").reverse().join("") + "." + r;
    }

    var l = s.split(".")[0].split("").reverse(),
        r = s.split(".")[1];

    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
    }
    return t.split("").reverse().join("") + "." + r;
}

$(function () {
	
	
	$(".member").hover(function (){
		$(".personalList").show();
	},function(){
		$(".personalList").hide();
	})
	
	/*塑料分类*/
	
	$(".plasticMenu").hover(function (){
		$(".leftBar").show();
	},function (){
		$(".leftBar").hide();
	})
	
	$(".leftBarCon > li").hover(function (){
		$(this).find(".barSub").show();
	},function (){
		$(this).find(".barSub").hide();
	})
	
	
	/*navlist 切换*/
	$(".navList > li").click(function (){
		$(this).addClass("active").siblings().removeClass("active");
	})
	
	
	/*倒计时*/
	calculateTime();
	function calculateTime(){
		var tm1 = new Date(),timer2 = null;
		var hours1 = tm1.getHours();
		var minutes1 = tm1.getMinutes();
		var seconds1 = tm1.getSeconds();
		var total1 = hours1*60*60 + minutes1*60 + seconds1;
		var total2 = 17*60*60;
		if(hours1 >= 17 && hours1 < 24){
			var timeDiff = 24*60*60-total1+9*60*60;
			$(".clock span i").html("距离开市:");
		}else if(hours1 >= 0 && hours1 < 9){
			var timeDiff = 9*60*60 - total1;
			$(".clock span i").html("距离开市:");
		}else{
			var timeDiff = total2-total1;
			$(".clock span i").html("距离闭市:");
		}
		clearInterval(timer2);
		timer2 = setInterval(function (){
			
			if(timeDiff <= 0){
				if($(".clock span i").html() == "距离闭市"){
					$(".clock span i").html("距离开市:");
					timeDiff = 24*60*60-17*60*60+9*60*60;
				}else if($(".clock span i").html() == "距离开市"){
					$(".clock span i").html("距离闭市:");
					timeDiff = 17*60*60-9*60*60;
				}
				window.location.reload(true);
			}
			var hours = Math.floor(timeDiff/(60*60));
			var hoursLeave = timeDiff%(60*60);
			var minutes = Math.floor(hoursLeave/(60));
			var seconds = hoursLeave%(60);
			if(hours < 10){
				hours = "0"+hours;
			}
			if(minutes < 10){
				minutes = "0"+minutes;
			}
			if(seconds < 10){
				seconds = "0"+seconds;
			}
			timeDiff-=1;
			
			$(".clock span em").eq(0).html(hours);
			$(".clock span em").eq(1).html(minutes);
			$(".clock span em").eq(2).html(seconds);
		},1000)
	}
	
	
	/*搜索*/
    $(".searchWrap > input")
        .focus(function () {
            $(this).css({
                border: "2px solid #0070dc"
            })
		    $(this).next().hide();
	    })
        .blur(function () {
            $(this).css({
                border: "1px solid #ccc"
            })
		    if($(this).val() == ""){
			    $(this).next().show();
		    }else {
			    $(this).next().hide();
		    }
	    })
	
})
