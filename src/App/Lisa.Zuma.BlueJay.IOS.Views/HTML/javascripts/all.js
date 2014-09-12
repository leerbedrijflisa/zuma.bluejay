 $(function () {

            var url = "http://zumabluejay-api.azurewebsites.net/api/";
            var token = $("html").find("custom").attr("token");

              $(".imgOverlay").bind("tap", function(){
              	$(this).fadeOut();
              });


             $("img.noteImage").bind("tap", function(){
              var src = $(this).attr("src");

				$(".imgDivv" ).fadeIn();
				$(".imgDivv").html('<img class="replaced" src='+src+'>');
				$(".replaced").css("max-height", $("body").height() - 50 +"px");
				$(".replaced").css("max-width", $("body").width()- 50+"px");
				$(".imgDivv").css("width", $("img.replaced").width());
				$(".imgDivv").css("height", $("img.replaced").height());
              	$(".imgOverlay").fadeIn();

              	//alert($(".replaced").css("max-width"))
              	var parseWidh = $("body").width() - $("img.replaced").width();
              	parseWidh = parseWidh / 2;

              	var parseHeight = $("body").height() - $("img.replaced").css('max-width');
              	parseHeight = parseHeight / 2;

              	$('.imgDivv').css('margin-left', parseWidh);
              	$('.imgDivv').css('margin-top', parseHeight);

             });

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

            $(".play").bind("tap", function(){

              var videoFrontDiv = $(this).find(".videoHolder");
			  var splitted = $(videoFrontDiv).attr("class").split(' ');

              var dosierId = splitted[1];
              var noteId = splitted[2];
              var mediaId = splitted[3];

              $(this).find("img").attr("src", "../HTML/images/loader.gif");
              $(this).find(".playbutton").hide();
               $.ajax({
	                type: 'GET',
	                dataType: 'json',
	                url: url + "dossier/" + dosierId +"/notes/"+noteId+"/media/"+mediaId,
	                headers: {
	                    "Authorization": token
	                },
					success: function(data){
						
						$(videoFrontDiv).find('.video').show();
						//$(videoFrontDiv).find('.play').toggle();
						$(videoFrontDiv).find('.video').html('<source src="'+data.location+'" type="video/mp4"> Your browser does not support the video tag.');
						$(videoFrontDiv).css("padding", 0, "margin", "0");
						$(videoFrontDiv).find('.video').play();
					}
				});

            });
           
        });