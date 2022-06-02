$(document).ready(function(){
    
    $(document).on('click', '.navbar-nav li', function() {
        $(".navbar-nav li").removeClass("active");
        $(this).addClass("active");
    });
    
    $( function() {
        $( "#datepicker1" ).datepicker({
    
        });
      } );
    
      $( function() {
        $( "#datepicker2" ).datepicker({
    
        });
      } );
    
    
    
      });