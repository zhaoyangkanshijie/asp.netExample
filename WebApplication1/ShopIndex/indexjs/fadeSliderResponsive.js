let slideSliderResponsive = slideSliderResponsive || ($ => {
    return {
		//初始化函数：传入设置的值
		init : () => {
			
			//调整导航点左margin，使其居中
			let fixNavCtrl = () => {
				//获取导航点个数
				let navCtrlNum = $('.fade-slider .fade-slider-ctrl').children('span').length;
				//导航点width：10px，左margin：12px
				let setNavCtrlLeft = 0;
				if(navCtrlNum % 2 == 1){
					setNavCtrlLeft = (-1) * ((10 / 2) + Math.floor(navCtrlNum / 2) * (10 + 12));
				}else{
					setNavCtrlLeft = (-1) * ((12 / 2) + (navCtrlNum / 2) * 10 + (navCtrlNum / 2 - 1) * 12);
				}
				$('.fade-slider .fade-slider-ctrl').css('marginLeft',setNavCtrlLeft);
			}

			//调整前后按钮位置
			let fixPrevNext = () => {
				let totalWidth = 1920;
				let windowWidth = $(window).width();
				//console.log(windowWidth);
				let deltaWidth = 164 - (totalWidth - windowWidth) / 2;
				$('.fade-slider .fade-slider-prev').css("left", deltaWidth+"px");
				$('.fade-slider .fade-slider-next').css("right", deltaWidth + "px");
			} 
			
			//广告鼠标悬停事件
			// $('.fade-slider,.fade-slides,.fade-slider .fade-slider-next,.fade-slider .fade-slider-prev').hover(() => {
			// 	$('.fade-slider .fade-slider-prev,.fade-slider .fade-slider-next').css("visibility","visible");
			// },() =>{
			// 	$('.fade-slider .fade-slider-prev,.fade-slider .fade-slider-next').css("visibility","hidden");
			// });
			
			fixNavCtrl();
			fixPrevNext();
			$(window).resize(() => {
				fixPrevNext();
			});
        }
    }
})($);