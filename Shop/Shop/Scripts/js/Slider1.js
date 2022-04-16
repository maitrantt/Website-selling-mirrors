SlideProductColumn = function (arr, idListProduct) {
    var n = this;
    n.marginItem = 15;
    n.index = 1;
    n.col = 1;
    n.length = arr.length;
    n.setArr = function () {
        if ($(window).width() > 0) { // check with ban dau
            var width = $(window).width();
            if (width < 400) {
                n.col = 1;
            }
            if (width >= 400 && width < 992) {
                n.col = 2;
            }
            if (width >= 992 && width < 1200) {
                n.col = 3;
            }
            if (width >= 1200) {
                n.col = 4;
            }
            n.widthSize(n.col);
        }

    }

    n.widthSize = function (col) { // set with cho từng item
        var width = $(idListProduct + ' .container').width();
        var twidth = (width - n.marginItem * (col - 1)) / col; // with của từng item so với chiều rộng web

        $(idListProduct + ' .product-list .product_list-wrap').css({
            'float': 'left',
            'transform': 'translate3d(0px, 0px, 0px)',
            'transition': 'all 0s ease 0s',
            'width': `${(twidth + n.marginItem) * n.length}`
        });
        $.each(arr, function () {
            $(this).css({ 'float': 'left', 'width': `${twidth}px`, 'marginRight': `${n.marginItem}px` });
        });
        if (n.length <= n.col) {
            $(idListProduct + " .slider_controls").css({ 'display': 'none' })
        } else {
            $(idListProduct + " .slider_controls").css({ 'display': 'flex' })
        }
    }
    n.transformSize = function (index, col) { // doi tranform khi nhan next hoac prev

        var t = $(idListProduct + ' .product-list .product_list-wrap').width() / (n.length / col) * -index;
        if (index == Math.ceil(n.length / col) - 1 && n.length > col) { // nếu mà chieu dai mang kia dư
            var du = n.length / n.col - (Math.ceil(n.length / col) - 1); // lay ra số dư
            var itemsize = ($(idListProduct + ' .container').width() - n.marginItem * (col - 1)) / col; // lấy ra chiều dài của item
            t += (col - du * col) * (itemsize + n.marginItem); // lui lại $col - $du lần
        }
        $(idListProduct + ' .product-list .product_list-wrap').css({
            'transform': `translate3d(${t}px, 0px, 0px)`
        });
    }
    n.checkIndexSlide = function (width, index) { // check thoi diem hien tai cua tranform khi index thay doi
        if (width < 400) {
            n.col = 1;
        }
        if (width >= 400 && width < 992) {
            n.col = 2;
        }
        if (width >= 992 && width < 1200) {
            n.col = 3;
        }
        if (width >= 1200) {
            n.col = 4;
        }
        n.widthSize(n.col);
        n.transformSize(index - 1, n.col);
    }
    n.windowResize = function () {
        $(window).resize(function () { // thay doi theo dieu  khien khi thu nho chieu rong web va nguoc lai
            var width = $(window).width();
            if (n.index > Math.ceil(n.length / n.col)) {
                n.index = 1;
            }
            n.checkIndexSlide(width, n.index);
        });
    }
    n.leftSlide = function () {
        $(idListProduct + ' .slider_controls button.button-prev').mousedown(() => { // nhan nut prev

            n.index--;
            console.log(n.index);
            var width = $(window).width();
            if (n.index < 1) {
                n.index = (Math.ceil(n.length / n.col));
            }
            n.checkIndexSlide(width, n.index);
        });
    }
    n.rigthSlide = function () {
        $(idListProduct + ' .slider_controls button.button-next').mousedown(() => { // nhan nut next

            n.index++;
            console.log(n.index);
            var width = $(window).width();
            if (n.index > Math.ceil(n.length / n.col)) {
                n.index = 1;
            }
            n.checkIndexSlide(width, n.index);
        });
    }
    n.start = function () {
        n.setArr();
        n.windowResize();
        n.leftSlide();
        n.rigthSlide();
    }
}
var arrProductHot = $('#product_hot .product_list-content .product-column');
SlideProductHot = new SlideProductColumn(arrProductHot, '#product_hot');
SlideProductHot.start();

var arrProductStanding = $('#product_StandingMirror .product_list-content .product-column');
SlideProductStanding = new SlideProductColumn(arrProductStanding, '#product_StandingMirror');
SlideProductStanding.start();


var arrProductHanging = $('#product_HangingMirror .product_list-content .product-column');
SlideProductHanging = new SlideProductColumn(arrProductHanging, '#product_HangingMirror');
SlideProductHanging.start();

var arrProductHanging = $('#product_HangingMirror .product_list-content .product-column');
SlideProductHanging = new SlideProductColumn(arrProductHanging, '#product_HangingMirror');
SlideProductHanging.start();