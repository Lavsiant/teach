$(window).on('load', function() {

  // Hide replacing on loading
  $('#site-header').css('opacity', '1');

  // Replace elements
  var itemsToAppend = ['.search-wrapper', '.site-menu-wrapper', '.user-bar'];
  var placeToAppend = '#mobile-header';
  var placesToInsert = [
    '.site-header-inner .site-menu-wrapper',
    '.site-header-inner .logo',
    '.site-header-inner .search-wrapper'];

  resizeAppend();

  function resizeAppend() {
    if ($(window).width() <= 768) {
      for (var i = 0; i < itemsToAppend.length; i++)
        $(itemsToAppend[i]).appendTo(placeToAppend);
    } else {
      for (var i = 0; i < itemsToAppend.length; i++)
        $(itemsToAppend[i]).insertAfter(placesToInsert[i]);
    }
  }

  // Check window resizing
  $(window).resize(function() {
    resizeAppend();
  });

});

$(document).ready(function() {

  // Open mobile header
  $('#open-mobile-header').on('click', function() {
    $('.overlay').fadeIn();
    $('#mobile-header').css('right', '0');
  });

  // Close mobile header
  $('#close-mobile-header').on('click', function() {
    $('#mobile-header').css('right', '-100%');
    $('.overlay').fadeOut();
  });

  $('.mobile-header-wrapper .overlay').on('click', function(event) {
    var item = $(event.target);
    if (!item.closest($('#mobile-header')).length) {
      $('#mobile-header').css('right', '-100%');
      $('.overlay').fadeOut();
    }
  });

  // Open sign-up
  $('#open-sign-up').on('click', function() {
    $('#sign-up .overlay').fadeIn();
    $('#sign-up').fadeIn(200);
    $('body').css('overflow-y', 'hidden');
  });

  // Close sign-up
  $('#sign-up .overlay').on('click', function(event) {
    var item = $(event.target);
    if (!item.closest($('#sign-up-form')).length) {
      $('#sign-up .overlay').fadeOut();
      $('#sign-up').fadeOut(200);
      $('body').css('overflow-y', 'auto');
    }
  });

  // Open sign-in
  $('#open-sign-in').on('click', function() {
    $('#sign-in .overlay').fadeIn();
    $('#sign-in').fadeIn(200).css('display', 'flex');
    $('body').css('overflow-y', 'hidden');
  });

  // Close sign-in
  $('#sign-in .overlay').on('click', function(event) {
    var item = $(event.target);
    if (!item.closest($('#sign-in-form')).length) {
      $('#sign-in .overlay').fadeOut();
      $('#sign-in').fadeOut(200);
      $('body').css('overflow-y', 'auto');
    }
  });

  // Toggle sub-menu
  $('.toggle-dropdown').on('click', function() {
    if ($(window).width() <= 768) {
      $(this).siblings('.link-dropdown').toggle();
      $(this).siblings('.link-sub-menu').toggle();
      $(this).find('i').toggleClass('active');
    }
  });

  // Select checkboxes
  var checkbox = $('.time input');
  // var required = 0;
  var checkedBoxes = 0;

  checkbox.on('click', function() {
    selectCheckbox(requiredBoxes);
  });

  function selectCheckbox(3) {

    var target = $(event.target);

    if (!target.closest(checkbox.not(':checked')).length) {

      checkedBoxes++;

      if (checkedBoxes === 3) {
        checkbox.not(':checked').prop('disabled', true);
      }

    } else {
      checkedBoxes--;
      checkbox.prop('disabled', false);
    }
  }

});