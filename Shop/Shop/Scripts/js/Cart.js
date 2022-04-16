// xoa san pham

function removeItemCart(idSp, size, moneyRemove) {
    var data = {
        IdSp: parseInt(idSp),
        size: size
    }

    $.ajax({
        url: "/Cart/RemoveCart",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response == "Login") {
                window.location = "https://localhost:44355/Login/login";
            }
            if (response == "Success") {
                $.each($(`.main-cart .cart__form .cart__item`), function (index, value) {

                    var id = $(this).attr('data-id').trim();
                    var siz = $(this).attr('data-size').trim();
                    console.log(id, siz);
                    if (id == idSp && siz == size.trim()) {
                        value.remove();
                    }
                });
                var sl = $('.ico-cart .cart-count').html();
                $('.ico-cart .cart-count').html(parseInt(sl) - 1);
                var money = $('.cart__summary .cart-subtotal__price').attr('data-cart-subtotal-price');
                $('.cart__summary .data-cart-subtotal-price').html('$' + parseFloat(money) - parseFloat(moneyRemove));
            }


        },
        error: function () {

        }
    });
}
$('.main-cart .cart__form').submit(function (event) {
    event.preventDefault();
});
$('.main-cart .cart__form .cart__item button.scd-item__remove').click(function (event) {
    var parent = $(this).parents('.cart__item');
    var id = parent.attr('data-id');
    var siz = parent.attr('data-size');
    // loi money
    console.log(parent.children('.order-discount').html());
    var money = parent.children('.cart__item--final-price .order-discount').attr('data-cart-subtotal-price');
    /*money = money.replace(/[\$]/isg, '');*/
    removeItemCart(id, siz, money);
    console.log(id, siz, money);

})


function updateQuantity(idSp, size, count) {
    var data = {
        IdSp: parseInt(idSp),
        Size: size,
        SoLuong: parseInt(count)
    }
    $.ajax({
        url: "/Cart/UpdateQuantity",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response == "Login") {
                window.location = "https://localhost:44355/Login/login";
            }
            if (response == "Update") {
                alert('thanh cong');
            } else {
                alert('loi');
            }

        },
        error: function () {

        }
    });

}

$('.cart__item .scd-item__qty button').click(function () {

    var dataQtyChange = $(this).attr('data-qty-change');
    var parent = $(this).parents('.scd-item__qty');
    var qtyInput = parent.children('input[name="countItemCart"]');
    var count = qtyInput.val();
    if (dataQtyChange == 'dec') {
        qtyInput.val(parseInt(count) - 1);
        count = parseInt(count) - 1;
        console.log(count, dataQtyChange);
    }
    if (dataQtyChange == 'inc') {
        qtyInput.val(parseInt(count) + 1);
        count = parseInt(count) + 1;
        console.log(count, dataQtyChange);
    }
    qtyInput.change(function () {
        count = qtyInput.val();
        console.log(count);
    });
    var parent = $(this).parents('.cart__item');
    var id = parent.attr('data-id');
    var siz = parent.attr('data-size');

    if (count == 0) {
        var result = confirm("bạn có muốn xóa sản phẩm không?");
        if (result) {
            removeItemCart(id, siz);
        } else {
            qtyInput.val(1);
        }
    } else {
        updateQuantity(id, siz, count);
    }
})