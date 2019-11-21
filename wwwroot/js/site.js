// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(document).scroll(function () {
        var $nav = $(".scrolled-nav-bar");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
    });
});