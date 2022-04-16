Slider = function (arr, partArr, left, ElementWidth) {
    var n = this;
    n.width = ElementWidth.width();
    n.length = arr.length;
    n.index = 0;
    n.arrDots = '';
    n.windowResize = function () {
        $(window).resize(function () {
            n.width = ElementWidth.width();
            partArr.css({
                'width': n.width * n.length + 'px',
                'transform': `translate3d(0px, 0px, 0px)`,
            })
            arr.css({
                'width': n.width + 'px'
            });

        })
    }
    n.setPartArr = function () {
        n.width = ElementWidth.width();
        partArr.css({
            'width': n.width * n.length + 'px',
            'transform': `translate3d(0px, 0px, 0px)`,
        })
        arr.css({
            'width': n.width + 'px'
        });
        $(arr.get(n.index)).addClass('active');
    }
    n.setArrDots = function () {
        partArr.parent().append(
            `<div class="w-full slider-controls w-full flex items-center justify-center">
                        <div class="slider-dots">
                        </div>
                    </div>`
        );

        for (var i = 0; i < n.length; i++) {
            $(".slider-controls .slider-dots", partArr.parent()).append(`
                        <span class="dot-item" data-value=${i}>
                                <svg width="65px" height="65px" viewBox="0 0 72 72" aria-hidden="true" xmlns="http://www.w3.org/2000/svg">
                                    <circle class="time" stroke-width="5" fill="none" stroke-linecap="round" cx="33" cy="33" r="28"></circle>
                                </svg>
                            </span>
                    `);
        }
        n.arrDots = $(".slider-dots > span.dot-item", partArr.parent());
        $(n.arrDots.get(n.index)).addClass('dot-item-active');
        console.log(n.arrDots);

    }
    n.leftSlide = function () {
        $(arr.get(n.index)).removeClass('active');
        if (n.index == 0) {
            n.index = n.length;
        }
        n.index--;
        $(arr.get(n.index)).addClass('active');
        partArr.css({
            'transform': `translate3d(${-n.width * n.index}px, 0px, 0px)`,
            'transition': 'all .3s'
        })
    }
    n.rigthSlide = function () {
        $(arr.get(n.index)).removeClass('active');
        if (n.index == n.length - 1) {
            n.index = -1;
        }
        n.index++;
        $(arr.get(n.index)).addClass('active');
        partArr.css({
            'transform': `translate3d(${-n.width * n.index}px, 0px, 0px)`,
            'transition': 'all .3s'
        })
    }
    n.buttonLeft = function () {
        $(`${left} .button-prev`).click(() => n.leftSlide());
    }
    n.buttonRight = function () {
        $(`${left} .button-next`).click(() => n.rigthSlide());
        console.log('nhan');

    }
    n.buttonDots = function () {
        n.arrDots.click(function () {
            if ($(this).attr('class').indexOf('dot-item-active') == -1) {
                n.arrDots.removeClass('dot-item-active');
                $(this).addClass('dot-item-active');
                $(arr.get(n.index)).removeClass('active');
                n.index = $(this).attr('data-value');
                $(arr.get(n.index)).addClass('active');
                partArr.css({
                    'transform': `translate3d(${-n.width * n.index}px, 0px, 0px)`,
                    'transition': 'all .3s'
                });
            }
        });
    }
    n.start = function () {
        n.setPartArr();
        n.setArrDots();
        n.windowResize();
        n.buttonLeft();
        n.buttonRight();
        n.buttonDots();
        console.log(n.width);
    }
}