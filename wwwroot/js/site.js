// Make nav bar have a background color when scrolled
$(function () {
    $(document).scroll(function () {
        var $nav = $(".scrolled-nav-bar");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
    });
});