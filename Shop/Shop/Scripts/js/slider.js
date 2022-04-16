
var widt = $(window).width();
if (widt > 800) {
    $('.slider-banner .img-mb').addClass('hidden');
    $('.slider-banner .img-desk').removeClass('hidden');
} else {
    $('.slider-banner .img-mb').removeClass('hidden');
    $('.slider-banner .img-desk').addClass('hidden');
}
$(window).resize(function () {
    var widt = $(window).width();
    if (widt > 800) {
        $('.slider-banner .img-mb').addClass('hidden');
        $('.slider-banner .img-desk').removeClass('hidden');
    } else {
        $('.slider-banner .img-mb').removeClass('hidden');
        $('.slider-banner .img-desk').addClass('hidden');
    }
})

var a = $('.slider-banner .slider-wrapper > .slider-items');
var b = $('.slider-banner .slider-wrapper')
SliderHeader = new Slider(a, b, '.slider-banner', $(window));
SliderHeader.start();


