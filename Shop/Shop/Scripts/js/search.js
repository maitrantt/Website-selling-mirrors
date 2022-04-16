// lấy giá trị search

$('.search-cart .ico-search').click(function () {
    $('#search_canvas').addClass('act_opened');
})
$('.mini_cart-header i').click(function () {
    $('#search_canvas').removeClass('act_opened');
});

function updateFormSearch() {
    var form = $('#search_canvas form[action="/search"]');
    var inputSearch = form.find('input[name="seach_input"]');
    registerEvent(inputSearch); // thêm dữ liệu khi thực hiện hành động

}



function instantSearch(obj) {
    var variable = $(obj).val();
    console.log(variable);
    $.ajax({
        url: "/Home/SearchProduct",
        type: "GET",
        data: { a: variable },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            console.log(response);
            if (response.trim() != "") {
                $('#search_canvas .product_list_widget .js_prs_search').html(response);
            } else {
                $('#search_canvas .product_list_widget .js_prs_search').html('<p>Không có sản phẩm</p>');
            }
        },
        error: function (e) {
            console.log(e.message);
            $('#search_canvas .product_list_widget .js_prs_search').html('<p>Không có sản phẩm</p>');
        }
    });

}

function registerEvent(obj) {
    $(obj).attr("auticomplete", "off");
    $(obj).focus(function () {
        if ($(obj).val() == '' || $(obj).val() == null) {
            $('#search_canvas .product_list_widget .js_prs_search').html('<p>Không có sản phẩm</p>');
        }
    });
    $(obj).keyup(function () {
        if ($(this).val() == '' || $(this).val() == null) {
            $('#search_canvas .product_list_widget .js_prs_search').html('<p>Không có sản phẩm</p>');
        }
        instantSearch(obj);
    });
}

updateFormSearch();
