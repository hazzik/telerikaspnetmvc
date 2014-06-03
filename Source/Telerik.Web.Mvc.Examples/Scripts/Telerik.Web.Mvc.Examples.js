$(function(){
	var $t = $.telerik;
	var fx = [$t.fx.slide.defaults()];
	var themeRegex = /[\?&]theme=([^&#]*)/;
	var availableSkins = [
		'Black', 'Default', 'Forest', 'Hay', 'Office2007', 'Outlook',
		'Simple', 'Sitefinity', 'Sunset', 'Telerik', 'Vista', 'Web20', 'WebBlue'
	];
	
	function onThemeChange(e) {
		e.preventDefault();
		
		var theme = this.className.replace('theme-preview-', '');
		
		var url = window.location.href;
		var newUrl = new $t.stringBuilder();
		
		if (url.indexOf('theme=') > 0) {
			var matches = themeRegex.exec(url);
			var oldThemeIndex = url.indexOf(matches[1]);
			
			newUrl
				.append(url.substring(0, oldThemeIndex))
				.append(theme)
				.append(url.substring(oldThemeIndex + matches[1].length));
		} else {
			// won't work with hashes in url
			newUrl
				.append(url)
				.append((url.indexOf('?') > 0) ? '&' : '?')
				.append('theme=')
				.append(theme);
		}
		
		window.location.href = newUrl.string();
	}
	
	function getThemeGalleryHtml()
	{
		var themeGalleryHtml = new $t.stringBuilder();
		
		themeGalleryHtml.append('<div id="theme-gallery"><ul>');

		var url = window.location.href;
		var currentTheme = themeRegex.test(url) ? themeRegex.exec(url)[1] : 'vista';
		
		$.each(availableSkins, function() {
			themeGalleryHtml
				.append('<li')
				.append((this.toLowerCase() == currentTheme) ? ' class="selected"' : '')
				.append('><a href="#" class="theme-preview-')
				.append(this.toLowerCase())
				.append('"><img src="')
				.append(themePreviewsLocation)
				.append(this)
				.append('.png" alt="" width="90" height="90" />')
				.append(this)
				.append('</a></li>');
		});
		
		themeGalleryHtml.append('</ul></div>');
		
		return themeGalleryHtml.string();
	}
	
	var themeGalleryOpened = false;

	$('#theming .t-drop-down')
		.click(function(e) {
			e.preventDefault();
			
			if (!themeGalleryOpened) 
				e.stopPropagation();
			
			var $themeGallery = $('#theme-gallery');
			
			if ($themeGallery.length == 0) {
				$themeGallery = $(getThemeGalleryHtml()).appendTo(this.parentNode)
				
				$themeGallery.find('a').click(onThemeChange);
			
				$(document).click(function (e, element) {
					if (e.which == 3)
						return;
						
					if ($(e.target).parents('#theme-gallery').length > 0)
						return;
			            
					$('#theming .t-drop-down').removeClass('state-active');
					$t.fx.rewind(fx, $themeGallery, { direction: 'bottom' });
					themeGalleryOpened = false;
				});	
			}
		
			$(this).addClass('state-active');
			$t.fx.play(fx, $themeGallery, { direction: 'bottom' });
			themeGalleryOpened = true;
		});
		
	$('.configurator .t-button')
		.live('mouseenter', $t.buttonHover)
		.live('mouseleave', $t.buttonLeave);
});

/*
 * jQuery Color Animations
 * Copyright 2007 John Resig
 * Released under the MIT and GPL licenses.
 */

(function(jQuery){

	// We override the animation for all of these color styles
	jQuery.each(['backgroundColor', 'borderBottomColor', 'borderLeftColor', 'borderRightColor', 'borderTopColor', 'color', 'outlineColor'], function(i,attr){
		jQuery.fx.step[attr] = function(fx){
			if ( fx.state == 0 ) {
				fx.start = getColor( fx.elem, attr );
				fx.end = getRGB( fx.end );
			}

			fx.elem.style[attr] = ["rgb(", [
				Math.max(Math.min( parseInt((fx.pos * (fx.end[0] - fx.start[0])) + fx.start[0]), 255), 0),
				Math.max(Math.min( parseInt((fx.pos * (fx.end[1] - fx.start[1])) + fx.start[1]), 255), 0),
				Math.max(Math.min( parseInt((fx.pos * (fx.end[2] - fx.start[2])) + fx.start[2]), 255), 0)
			].join(","), ")"].join('');
		}
	});

	// Color Conversion functions from highlightFade
	// By Blair Mitchelmore
	// http://jquery.offput.ca/highlightFade/

	// Parse strings looking for color tuples [255,255,255]
	function getRGB(color) {
		var result;

		// Check if we're already dealing with an array of colors
		if ( color && color.constructor == Array && color.length == 3 )
			return color;

		// Look for rgb(num,num,num)
		if (result = /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/.exec(color))
			return [parseInt(result[1]), parseInt(result[2]), parseInt(result[3])];

		// Look for #a0b1c2
		if (result = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(color))
			return [parseInt(result[1],16), parseInt(result[2],16), parseInt(result[3],16)];

		// Otherwise, we're most likely dealing with a named color
		return colors[jQuery.trim(color).toLowerCase()];
	}
	
	function getColor(elem, attr) {
		var color;

		do {
			color = jQuery.curCSS(elem, attr);

			// Keep going until we find an element that has color, or we hit the body
			if ( color != '' && color != 'transparent' || jQuery.nodeName(elem, "body") )
				break; 

			attr = "backgroundColor";
		} while ( elem = elem.parentNode );

		return getRGB(color);
	};
	
})(jQuery);
