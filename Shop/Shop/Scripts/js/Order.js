if ($('.show_listCart button.btn_slc').length > 0) {
    $('.show_listCart button.btn_slc').click(function () {
        console.log($('#disclosure_content').attr('class'));
        if ($('#disclosure_content').attr('class').indexOf("hidden") != -1) {
            $('#disclosure_content').css({
                'height': 'auto',
                'overflow': 'visible'
            })
            $('#disclosure_content').removeClass("hidden");
        } else {
            $('#disclosure_content').css({
                'height': '0px',
                'overflow': 'hidden'
            })
            $('#disclosure_content').addClass("hidden");
        }
    });
}


Address = {
    'Hà Nội': ['Quận Ba Đình', 'Quận Cầu Giấy', 'Quận Đống Đa', 'Quận Hà Đông', 'Huyện Thường Tín'],
    'TP Hồ Chí Minh': ['Quận 1', 'Quận 2', 'Quận 3', 'Quận 4', 'Quận 5'],
    'Đà Nẵng': ['Quận Hải Châu', 'Quận Thanh Khê', 'Quận Sơn Trà', 'Quận Ngũ Hành Sơn', 'Quận Liên Chiểu'],
    'Quận Ba Đình': ['Phường Phúc Xá', 'Phường Trúc Bạch', 'Phường Vĩnh Phúc'],
    'Quận Cầu Giấy': ['Phường Nghĩa Đô', 'Phường Nghĩa Tân', 'Phường Mai Dịch', 'Phường Dịch Vọng'],
    'Quận Đống Đa': ['Phường Cát Linh', 'Phường Văn Miếu', 'Phường Quốc Tử Giám', 'Phường Láng Thượng'],
    'Huyện Thường Tín': ['Thị Trấn Thường Tín', 'Xã Nghiêm Xuyên', 'Xã Thắng Lợi']
}

$('.inf_form-order .field_input select').change(function () {
    console.log($(this).val());
    var value = $(this).val();
    if (Address[value]) {
        var h = '';
        h += '<option value="">-----------------</option>';
        $.each(Address[value], function (key, value) {
            h += '<option value="' + value + '">' + value + '</option>';
        });
        var name = $(this).attr('name');
        if (name == 'TinhThanh') { $('.inf_form-order .field_input select[name="QuanHuyen"]').html(h); }
        if (name == 'QuanHuyen') { $('.inf_form-order .field_input select[name="PhuongXa"]').html(h); }
    }
})

$('.inf_form-order form').submit(function (event) {
    event.preventDefault();
});

$('.inf_form-order form#form_order button[name="submit"]').click(function () {
    var par = $(this).parents('#form_order');
    var Name = $('input[name="Name"]', par).val();
    var SDT = $('input[name="SDT"]', par).val();
    var DiaChi = $('input[name="DiaChi"]', par).val();
    var TinhThanh = $('select[name="TinhThanh"]', par).val();
    var QuanHuyen = $('select[name="QuanHuyen"]', par).val();
    var PhuongXa = $('select[name="PhuongXa"]', par).val();
    var GhiChu = $('textarea[name="GhiChu"]', par).val();
    data = {
        Name: Name,
        SDT: parseInt(SDT),
        DiaChi: DiaChi,
        TinhThanh: TinhThanh,
        QuanHuyen: QuanHuyen,
        PhuongXa: PhuongXa,
        GhiChu: GhiChu
    }
    console.log(data);
    $.ajax({
        url: "/Order/ValidateOrder",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('.erorr_page').html();
            console.log($('.erorr_page').html(""));
            if (response != null) {

                $.each(response, function (index, value) {
                    $('.erorr_page.error_' + index, par).html(value);
                });
            }
            
            if (response == "Login") {
                window.location = "https://localhost:44355/Login/login";
            }
            if (response == "Success") {
                alert("Dat hang thanh cong");
                window.location = "https://localhost:44355";
            } else {
                $.each(response, function (index, value) {
                    $('.erorr_page.error_' + index, par).html(value);
                });
            }


        },
        error: function () {

        }
    });
})