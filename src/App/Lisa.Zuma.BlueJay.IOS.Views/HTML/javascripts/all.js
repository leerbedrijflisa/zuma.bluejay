 $(function () {

            var url = "http://zumabluejay-apitest.azurewebsites.net/api/";
            var token = $("html").find("custom").attr("token");

            console.log(token);

            $('img.noteImage').bind("tapstart tap", function(){
              var imgURL = $(this).attr("src");
              $(".imgDivv").html('<img src="'+ imgURL +'">');
              $(".imgOverlay").fadeIn();
            });

            $('.imgOverlay').bind('tapstart tap', function(){
              $(this).fadeOut();
            });

            $('img.noteImage').bind("tapstart tap", function(){ alert($(this).attr("src")); });

            		$.each($(".noteImage"), function(i, val){
            		//alert('update');
            			var splitted = $(this).attr("class").split(' ');

             			 var dosierId = splitted[1];
             			 var noteId = splitted[2];
             			 var mediaId = splitted[3];
             			 var img = $(this);

               			 $.ajax({
	               			 type: 'GET',
	               			 dataType: 'json',
	               			 crossdomain: true,
	               			 url: url +"dossier/"+ dosierId +"/notes/"+noteId+"/media/"+mediaId,
	               			 headers: {
	               			     "Authorization": token,
	               			 },
	               			 success: function(data){
	               				if(!img.hasClass("replaced")){
				            		img.attr("src", data.location);
				            		img.addClass("replaced");
		            			}	
	               			 },
	               			 error: function(){
	               			 }
              			});
              		
            		});




            $("span.play").bind("touchstart tap", function(){

              /*$(this).bind("canplay", function() {
              	alert('hello');
              });*/
             
              var videoFrontDiv = $(this).parent();
			  var splitted = $(videoFrontDiv).attr("class").split(' ');

              var dosierId = splitted[1];
              var noteId = splitted[2];
              var mediaId = splitted[3];

              $(this).find("img").attr("src", "../HTML/images/loader.gif");

               $.ajax({
	                type: 'GET',
	                dataType: 'json',
	                url: url + "dossier/" + dosierId +"/notes/"+noteId+"/media/"+mediaId,
	                headers: {
	                    "Authorization": token
	                },
					success: function(data){
						$(videoFrontDiv).find('.play').toggle();
							$(videoFrontDiv).append('<video style=" width:418px; height:240px;" class="video"  preload="auto" controls><source src="'+data.location+'" type="video/mp4"> Your browser does not support the video tag.</video>');
						$(videoFrontDiv).css("padding", 0, "margin", "0");
					}
				});
             // 

             //console.log(video);

            });
             
       /*     $(".sliderDiv").excoloSlider({
              repeat: false,
              autoPlay: false,
              animationDuration : 250,
              prevnextNav: false,
              autoSize : true,
              width: 434,
              height: 240
            });

            */
        });