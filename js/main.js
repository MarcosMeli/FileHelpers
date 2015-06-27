/// <reference path="..\_typings\jquery.d.ts"/>
var _this = this;
$(function () {
    var window_width = $(window).width();
    /*Preloader*/
    $(window).load(function () { return setTimeout(function () { return $('body').addClass('loaded'); }, 200); });
    $('.show-search').click(function () {
        $('.search-out').fadeToggle("50", "linear");
        $('.search-out-text').focus();
    });
    // Check first if any of the task is checked
    $('#task-card input:checkbox').each(function () { return checkbox_check(_this); });
    // Task check box
    $('#task-card input:checkbox').change(function () { return checkbox_check(_this); });
    // Check Uncheck function
    function checkbox_check(el) {
        if (!$(el).is(':checked')) {
            $(el).next().css('text-decoration', 'none');
        }
        else {
            $(el).next().css('text-decoration', 'line-through');
        }
    }
});
