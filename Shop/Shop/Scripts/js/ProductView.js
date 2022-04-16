const arrQuickView = $('.product-list .product-column .product_btn-iconQuickView');
arrQuickView.click(function () {
    var idsp = $(this).parents('.product-column').children('.id-item').attr('data-id');
    console.log(idsp);
    $.ajax({
        url: '/Home/QuickViewProduct?idSp=' + idsp,
        type: "GET",
        dataType: "html",
        success: function (response) {
            $('#quick-view').html(response);
            $('#quick-view').css({ 'display': 'block' });
            $('#quick-view .button-close').click(() => {
                $('#quick-view').css({ 'display': 'none' });
            });
        }

    });

});




$('.quickView-content form').submit(function (event) {
    var form = $('.quickView-content form');
    var IdSp = $('input[name="product_id"]', form).val();
    var size = $('.product-size div.sizeItem input:checked', form).val();
    var soluong = $('input[name="quantity"]', form).val();

    var data = {
        IdSp: IdSp,
        Size: size.trim(),
        SoLuong: soluong
    }

    $.ajax({
        url: "/Cart/AddProduct",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            /*  console.log(response)*/
            if (response == "Login") {
                window.location = "https://localhost:44355/Login/login";
            }
            if (response == "Update" || response == "Insert") {
                $('.notification_cart').css({ 'display': 'block' });
                $('.notification_cart .notification_cart-title').html("Them san pham thanh cong");
                setTimeout(function () {
                    $('.notification_cart').css({ 'display': 'none' });
                    $('.notification_cart .notification_cart-title').html("");
                }, 3000);

            }
            if (response == "Faile") {
                $('.notification_cart').css({ 'display': 'block' });
                $('.notification_cart .notification_cart-title').html("Them san pham khong thanh cong");
                setTimeout(function () {
                    $('.notification_cart').css({ 'display': 'none' });
                    $('.notification_cart .notification_cart-title').html("");
                }, 3000);

            }
            var sl = $('.ico-cart .cart-count').html();
            response == "Insert" ? $('.ico-cart .cart-count').html(parseInt(sl) + 1) : false;
        },
        error: function () {

        }
    });
    event.preventDefault();
});