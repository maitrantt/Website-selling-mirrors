var MenuHeaderFixed = function () {
    var a = $('.header_desktop'),
        aHeigth = $('.header_desktop').outerHeight(true);
    $(window).scroll(function () {
        var scrollOffset = $(window).scrollTop();
        if (scrollOffset < aHeigth) {
            a.removeClass('fixed');
            a.removeAttr('style');
        }
        if (scrollOffset > aHeigth) {
            a.addClass('fixed');
            a.css({
                'height': parseInt(aHeigth) + 'px',
                'box-shadow': ' 0 5px 15px #00000012'
            });
        }
    });
}
MenuHeaderFixed();

$('#header-toogle').css({ 'height': $(window).height()+'px'});
var childrenNavMenu = '';

document.querySelector('.menu-button').onclick = () => {
    document.getElementById('header-toogle').classList.add('active');
    document.querySelector('#header-toogle .header-toogle-content').classList.remove('hidden');
    document.getElementById('mn-blocker').classList.add('active');
}


document.getElementById('mn-blocker').onclick = () => {

    document.getElementById('header-toogle').classList.remove('active');
    document.querySelector('#header-toogle .header-toogle-content').classList.add('hidden');
    document.getElementById('mn-blocker').classList.remove('active');

    if (childrenNavMenu) {
        childrenNavMenu.querySelector('.sub-links').classList.add('hidden');
        childrenNavMenu.querySelector('.sub-links').classList.remove('open-lv-1');
        document.querySelector('.header-toogle-navmenu').classList.remove('open-lv-1');
    }
}

const menuNav = document.querySelectorAll('#header-toogle ul li span.toogle-ico-submenu');
menuNav.forEach(element => {
    element.onclick = () => {
        var partent = element.parentElement;
        childrenNavMenu = partent;
        partent.querySelector('.sub-links').classList.remove('hidden');
        partent.querySelector('.sub-links').classList.add('open-lv-1');
        document.querySelector('.header-toogle-navmenu').classList.add('open-lv-1');

        partent.querySelector('.sub-links button.sub-link-back').onclick = () => {
            partent.querySelector('.sub-links').classList.add('hidden');
            partent.querySelector('.sub-links').classList.remove('open-lv-1');
            document.querySelector('.header-toogle-navmenu').classList.remove('open-lv-1');
        }
    }
});


