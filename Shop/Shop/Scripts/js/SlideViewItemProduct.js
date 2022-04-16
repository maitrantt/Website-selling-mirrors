var arrSwiperImage = $('#quick-view .swiper-listImage .imageItem');
var partArr = $('#quick-view .swiper-listImage');
var ElementWidth = $('#quick-view .swiper-ImageContent');
SliderQuickView = new Slider(arrSwiperImage, partArr, '#quick-view', ElementWidth);
SliderQuickView.start();
$('#quick-view .slider-controls .slider-dots').addClass('hidden');
