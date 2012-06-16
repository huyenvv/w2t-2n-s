	$(document).ready(function() {

			var but = $("#Button1");
			var act = $("article");
			var wid = $("body").width();
			var hei = $("body").height();

			but.css({"marginLeft":wid/2-100/2,"marginTop":50});

			but.click(function(){
				var sec = $("article section:nth-child(1)");
				sec.hide("fast","linear",function(){
					act.append($(this));
					$(this).css("display","block");
				})
			})
			
			// CAPTION IMAGES
			var secHover = $("article section");
			var par = $("article section p");
			var parHeight = par.height()+15;
				par.css("bottom",-parHeight);

				secHover.hover(
					function(){
						par.addClass("active");
						$(this).find(".active").animate({"bottom":0},"fast","linear");
					},function(){
						par.animate({"bottom":-parHeight},"fast","linear");
						par.removeClass("active");
					})


			// BEGIN LIGHTBOX
			$("body").prepend('<div id="lightbox"></div>')
			$("body").prepend('<div id="gray"><a href="#" title="">Close</a></div>');
			var lb = $("#lightbox");
			var gray = $("#gray");

			lb.append("<img>");
			lb.css({"marginLeft":wid/2-400,"marginTop":hei/2-553/2});

			secHover.click(function(){
				var imgSrc = $(this).find("img").attr("src");
				var text = $(this).find("p").text();
				var img = $("#lightbox img");
				gray.css("display","block");
				lb.css("display","block");
				lb.append("<p>"+text+"</p>");
				img.attr({
					src:imgSrc,
					title:"testing img",
					width:800,
					height:553
				})
			})

			gray.click(function(){
				gray.css("display","none");
				lb.css("display","none");
			})

		});
