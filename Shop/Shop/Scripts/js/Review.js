var arrProductSimilar = $('#product_Similar .product_list-content .product-column');
SlideProductSimilar = new SlideProductColumn(arrProductSimilar, '#product_Similar');
SlideProductSimilar.start();

$('.pr-form-review .pr-form-review-rating ul i.fa-star').mouseenter(function () {
    var n = $(this).attr("data-value");
    var sao = $('.pr-form-review .pr-form-review-rating ul i.fa-star');
    for (var i = 0; i < n; i++) {
        $(sao.get(i)).css({ 'color': 'yellow' });
    }
}).mouseleave(function () {
    var n = $(this).attr("data-value");
    var sao = $('.pr-form-review .pr-form-review-rating ul i.fa-star');
    for (var i = 0; i < n; i++) {
        $(sao.get(i)).css({ 'color': 'inherit' });
    }
})

FormReview = function () {
    n = this;
    n.sao = 0;
    n.TitleText = '';
    n.Text = '';
    n.IdSp = 0;

    n.setRating = function (obj) {
        n.sao = obj.getAttribute("data-value");
        var sao = $('.pr-form-review .pr-form-review-rating ul i.fa-star');
        sao.css({ 'color': 'inherit' });
        for (var i = 0; i < parseInt(n.sao); i++) {
            $(sao.get(i)).css({ 'color': 'yellow' });
        }
    }
    n.submitForm = function (obj) {
        var par = obj.p
        n.TitleText = obj.querySelector('input[name="TitleText"]').value;
        n.Text = obj.querySelector('textarea[name="Text"]').value;
        n.IdSp = obj.querySelector('input[name="product_id"]').value;
        var data = {
            TitleText: n.TitleText,
            Text: n.Text,
            IdSp: n.IdSp,
            sao: n.sao
        }
        //var ngay = Date.ToString("MMMM dd, yyyy, hh:MM:ss tt");
        $.ajax({
            url: "/ProductItem/Write",
            type: "POST",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response);
                if (response.idbl != null) {
                    console.log(response);
                    var html = '';
                    var ngay =
                        html = `<div class="pr-review" id="pr-review-${response.idbl}">
                         <div class="pr-review-header">
                              <span class="pr-starratings pr-review-header-starratings" aria-label="@pr.sao of 5 stars" role="img">`;
                    for (var i = 0; i < Number(n.sao); i++) {
                        html += `<i class="fa fa-regular fa-star" style="color: yellow; font-size: 18px" aria-hidden="true"></i>`;
                    }

                    html += `</span >
                                <h3 class="pr-review-header-title">${n.TitleText}</h3>
                                <span class="pr-review-header-byline"><strong>${response.TenKh ? response.TenKh : ''}</strong> on <strong>${response.ngay}</strong></span>
                          </div >
                    <div class="pr-review-content">
                        <p class="pr-review-content-body">${n.Text}</p>
                    </div>
                      </div >`;
                    $('#reviews_' + n.IdSp).prepend(html);
                    console.log($('#reviews_' + n.IdSp));
                    obj.querySelector('input[name="TitleText"]').setAttribute('value', '');
                    obj.querySelector('textarea[name="Text"]').setAttribute('value', '');
                    $('#form_' + n.IdSp).css({ "display": 'none' })
                    var count = $('.pr-summary-caption .pr-summary-actions-togglereviews .count-summary').html();
                    count = count == "" || count == null ? 0 : parseInt(count);
                    $('.pr-summary-caption .pr-summary-actions-togglereviews .count-summary').html(count + 1);
                    var sumSao = $('.pr-summary .pr-starrating').attr('data-label');
                    var sumSaoNew = (parseFloat(sumSao) * count + parseInt(n.sao)) / (count + 1);
                    var htmlSao = '';


                    for (var i = 0; i < parseInt(sumSaoNew); i++) {
                        htmlSao += '<i class="fa fa-regular fa-star" style="color: yellow; font-size: 18px" aria-hidden="true"></i>';
                    }
                    if (parseFloat(sumSaoNew) - parseInt(sumSaoNew) > 0.5) {
                        htmlSao += '<i class="fa fa-solid fa-star-sharp-half" style="color: yellow; font-size: 18px"></i>';
                    }
                    console.log(htmlSao);
                    $('.pr-summary .pr-starrating').html(htmlSao);
                    $('.pr-summary .pr-starrating').attr({ 'data-label': sumSaoNew, 'aria-label': sumSaoNew + ' of 5 stars' });
                    return false;
                }
            }
        });
    }
    n.toggleForm = function (obj) {
        $('#form_' + obj).css("display") == "block" ? $('#form_' + obj).css({ "display": 'none' }) : $('#form_' + obj).css({ "display": 'block' })
    }
}

var Fr = new FormReview();

var arrSwiperImage = $('#ItemProduct .swiper-listImage .imageItem');
var partArr = $('#ItemProduct .swiper-listImage');
var ElementWidth = $('#ItemProduct .swiper-ImageContent');
SliderQuickView = new Slider(arrSwiperImage, partArr, '#ItemProduct', ElementWidth);
SliderQuickView.start();
$('#ItemProduct .slider-controls .slider-dots').addClass('hidden');
