/*
    CS 4540 Web Software Architecture
    Admin Controller
    Author: Anderson Porta
    Date: 12-6-2019
*/
// Make nav bar have a background color when scrolled
$(function () {
    $(document).scroll(function () {
        var $nav = $(".scrolled-nav-bar");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
    });
});