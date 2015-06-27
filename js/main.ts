/// <reference path="..\_typings\jquery.d.ts"/>

$(() => {

  var window_width = $(window).width();

  /*Preloader*/
  $(window).load(() => setTimeout(() =>  $('body').addClass('loaded'), 200)  );

  $('.show-search').click(() => {
    $('.search-out').fadeToggle( "50", "linear");
    $('.search-out-text').focus();

  });

  // Check first if any of the task is checked
  $('#task-card input:checkbox').each(() => checkbox_check(this));

  // Task check box
  $('#task-card input:checkbox').change(() => checkbox_check(this));

  // Check Uncheck function
  function checkbox_check(el){
      if (!$(el).is(':checked')) {
          $(el).next().css('text-decoration', 'none');
      } else {
          $(el).next().css('text-decoration', 'line-through');
      }
  }
});
