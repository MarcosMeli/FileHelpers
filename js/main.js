/// <reference path="..\_typings\jquery.d.ts"/>
var _this = this;
if (window.location.host == 'marcosmeli.github.io' || window.location.host == 'www2.filehelpers.com')
    window.location.href = window.location.href.replace('marcosmeli.github.io/FileHelpers', 'www.filehelpers.net');
$(function () {
    "use strict";
    var window_width = $(window).width();
    $(window).load(function () { return setTimeout(function () { return $('body').addClass('loaded'); }, 200); });
    $('.show-search').click(function () {
        $('.search-out').fadeToggle("50", "linear");
        $('.search-out-text').focus();
    });
    $('#task-card input:checkbox').each(function () { return checkbox_check(_this); });
    $('#task-card input:checkbox').change(function () { return checkbox_check(_this); });
    function checkbox_check(el) {
        if (!$(el).is(':checked')) {
            $(el).next().css('text-decoration', 'none');
        }
        else {
            $(el).next().css('text-decoration', 'line-through');
        }
    }
});
