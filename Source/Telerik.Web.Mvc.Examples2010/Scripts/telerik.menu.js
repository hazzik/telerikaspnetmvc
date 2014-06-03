(function($) {
    var $t = $.telerik;

    $.extend($t, {
        menu: function(element, options) {
            this.element = element;
            this.nextItemZIndex = 100;

            $.extend(this, options);

            var item = $('.t-item:not(.t-state-disabled)', this.element);

            item.live('mouseenter', $t.delegate(this, this.mouseenter), true)
				.live('mouseleave', $t.delegate(this, this.mouseleave), true)
				.live('click', $t.delegate(this, this.click));

            $('.t-item:not(.t-state-disabled) > .t-link', this.element)
				.live('mouseenter', $t.hover)
				.live('mouseleave', $t.leave);

            $(document).click($t.delegate(this, this.documentClick));

            if (this.onOpen)
                item.live('open.Menu', this.onOpen);
            if (this.onClose)
                item.live('close.Menu', this.onClose);
            if (this.onSelect)
                item.live('select.Menu', this.onSelect);

            if (this.onLoad)
                $(this.element).bind('load.Menu', this, this.onLoad);
                
            $(this.element).trigger({ type: 'load.Menu' });
        }
    });

    var getEffectOptions = function(item) {
        var parent = item.parent();
        return {
            direction: parent.hasClass('t-menu') ? parent.hasClass('t-menu-vertical') ? 'right' : 'bottom' : 'right'
        };
    };

    $t.menu.prototype = {

        enable: function(li) {
            $(li).each($t.bind(this, function(index, item) {
                $(item)
                    .addClass('t-state-default')
				    .removeClass('t-state-disabled');
            }));
        },

        disable: function(li) {
            $(li).each($t.bind(this, function(index, item) {
                $(item)
                    .removeClass('t-state-default')
				    .addClass('t-state-disabled');
            }));
        },

        open: function($li) {
            var menu = this;
            
            $($li).each($t.bind(this, function(index, item) {

                var $item = $(item);

                clearTimeout($item.data('timer'));

                $item.data('timer', setTimeout($t.bind(this, function() {
                    var ul = $item.find('.t-group:first');
                    if (ul.length == 0) return;

                    $item.css('z-index', menu.nextItemZIndex++);

                    $t.fx.play(this.effects, ul, getEffectOptions($item));
                }, this.expandDelay)));
            }));
        },

        close: function($li) {
            var menu = this;
            
            $($li).each($t.bind(this, function(index, item) {

                var $item = $(item);
                
                clearTimeout($item.data('timer'));

                $item.data('timer', setTimeout($t.bind(this, function() {
                    var ul = $item.find('.t-group:first');
                    if (ul.length == 0) return;

                    $t.fx.rewind(this.effects, ul, getEffectOptions($item), function() {
                        $item.css('zIndex', '');
                        if ($(menu.element).find('.t-group:visible').length == 0)
                            menu.nextItemZIndex = 100;
                    });

                    ul.find('.t-group').stop(false, true);
                }, this.collapseDelay)));
            }));
        },

        mouseenter: function(e, element) {
            if (!this.openOnClick || this.clicked) {
                this.triggerEvent('open', $(element));

                this.open($(element));
            }

            if (this.openOnClick && this.clicked) {

                this.triggerEvent('close', $(element));

                $(element).siblings().each($t.bind(this, function(i, sibling) {
                    this.close($(sibling));
                }));
            }
        },

        mouseleave: function(e, element) {
            if (!this.openOnClick) {

                this.triggerEvent('close', $(element));

                this.close($(element));
            }
        },

        click: function(e, element) {
            var $li = $(element);

            $li.find('> .t-link').trigger({ type: 'select' });

            if (!$li.parent().hasClass('t-menu') || !this.openOnClick)
                return;

            e.preventDefault();

            this.clicked = true;

            this.triggerEvent('open', $li);

            this.open($li);
        },

        documentClick: function(e, element) {
            if ($t.contains(this.element, e.target))
                return;

			if (this.clicked) {
				this.clicked = false;
				$(this.element).children('.t-item').each($t.bind(this, function(i, item) {
					this.close($(item));
				}));
            }
        },

        hasChildren: function($li) {
            return $li.find('.t-group:first').length;
        },

        triggerEvent: function(eventName, $li) {
            if (this.hasChildren($li))
                $li.find('> .t-link').trigger({ type: eventName });
        }
    }

    // Plugin declaration
    $.fn.tMenu = function(options) {
        // Extend our default options with those provided.
        // Note that the first arg to extend is an empty object -
        // this is to keep from overriding our "defaults" object.
        options = $.extend({}, $.fn.tMenu.defaults, options);

        return this.each(function() {
            // support for Metadata plugin
            options = $.meta ? $.extend({}, options, $(this).data()) : options;

            // Initialize only if necessary
            if (!$(this).data('tMenu'))
                $(this).data('tMenu', new $t.menu(this, options));
        });
    };

    // default options

    $.fn.tMenu.defaults = {
        orientation: 'horizontal',
        effects: [
			$t.fx.slide.defaults()
		],
        openOnClick: false,
        expandDelay: 200,
        collapseDelay: 200
    };
})(jQuery);