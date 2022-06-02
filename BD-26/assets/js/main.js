$(document).ready(function(){
  $(".articles-carousel").owlCarousel({
    loop: true,
    rtl: true,
    margin: 10,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1,
            nav: true,
            loop: true
        },
        600: {
            items: 2,
            nav: true,
            loop: true
        },
        1000: {
            items: 3,
            nav: true,
            loop: true
        }
    }
});

$(document).on('click', '.navbar-nav li', function() {
    $(".navbar-nav li").removeClass("active");
    $(this).addClass("active");
});


  });