 $(function () {

            var url = "http://zumabluejay-apitest.azurewebsites.net";
            var token = $("html").find("custom").attr("token");

            console.log(token);


            $(".sliderDiv").excoloSlider({
              repeat: false,
              autoPlay: false,
              animationDuration : 250,
              prevnextNav: false,
              autoSize : true,
              width: 434,
              height: 240
            });

            $("span.play").on("click", function(){
              // $.ajax({
              //  type: 'GET',
              //  url: url,
              //  headers: {
              //      "Authorization":"bearer "+ token,
              //  }

              $(this).parent().html('<video width="432" height="240" controls autoPlay><source src="http://www.w3schools.com/html/movie.mp4" type="video/mp4"> Your browser does not support the video tag.</video> -->');
              $(this).parent().html("").css("background", "black");
            });

        });