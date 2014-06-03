(function($) {

    var $t = $.telerik;
    var expandModeEnum = { "single": 0, "multi": 1 };
    //var expandedItem;

    $.extend($t, {
        panelbar: function(element, options) {
            this.element = element;

            // attach options to object
            $.extend(this, options);

            var item = $(':not(.t-state-disabled) > .t-link', this.element);

            item.live('click', $t.delegate(this, this._click));

            item
				.live('mouseenter', $t.hover)
				.live('mouseleave', $t.leave);

			if (this.onExpand)
                item.live('expand.PanelBar', this.onExpand);
            
			if (this.onCollapse)
                item.live('collapse.PanelBar', this.onCollapse);
            
			if (this.onSelect)
                item.live('select.PanelBar', this.onSelect);
				
			if (this.onError)
                $(this.element).bind('error.PanelBar', this.onError);

			if (this.onLoad)
                $(this.element).bind('load.PanelBar', this.onLoad);
                
            $(this.element).trigger({ type: 'load.PanelBar' })
        }
    });

    $.extend($t.panelbar.prototype, {

        expand: function(li) {
            $(li).each($t.bind(this, function(index, item) {
                var $item = $(item);
                if (!$item.hasClass('.t-state-disabled')) {

                    if ($item.find('> .t-group, > .t-content').length > 0) {

                        if (this.expandMode == expandModeEnum.single) {
                            if (this._collapseAllExpanded($item))
                                return;
                        }
                        this._toggleItem($item, false, null);
                    }
                }
            }));
        },

        collapse: function(li) {
            $(li).each($t.bind(this, function(index, item) {
                var $item = $(item);
                var contents = $item.find('> .t-group, > .t-content');
                if (contents.length > 0) {
                    if (!$item.hasClass('.t-state-disabled') && contents.is(':visible')) {
                        this._toggleItem($item, true, null);
                    }
                }
            }));
        },

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

        _click: function(e, element) {
            if ($(e.target).closest('.t-link')[0] != element)
                return;

            var $element = $(element);
            var $item = $element.closest('.t-item');

            $('.t-link', this.element).removeClass('t-state-selected');
            $element.addClass('t-state-selected');

            $element.trigger($t.defaultActionEvent('select', e));

            var contents = $item.find('> .t-content, > .t-group');

            if (($element.attr('href') && $element.attr('href').charAt(0) == "#") ||
                (contents.length > 0 && contents.children().length == 0))
                e.preventDefault();
            else return;

            if (this.expandMode == expandModeEnum.single) {

                if (this._collapseAllExpanded($item))
                    return;
            }

            if (contents.length != 0) {

                var visibility = contents.is(':visible');

                if (!visibility) {
                    $element.trigger({ type: 'expand' });
                } else {
                    $element.trigger({ type: 'collapse' });
                }

                this._toggleItem($item, visibility, e);
            }
        },

        _toggleItem: function($element, isVisible, e) {
            var childGroup = $element.find('> .t-group');

            if (childGroup.length != 0) {

                this._toggleGroup(childGroup, isVisible);
                if (e != null)
                    e.preventDefault();
            } else {

                var itemIndex = $element.parent().children().index($element);
                var contentElement = $element.find('> .t-content');

                if (contentElement.length > 0) {
                    if (e != null)
                        e.preventDefault();
                    if (contentElement.children().length > 0) {
                        this._toggleGroup(contentElement, isVisible);
                    } else {
                        this._ajaxRequest($element, contentElement, isVisible);
                    }
                }
            }
        },

        _toggleGroup: function($element, visibility) {

            $element
			    .parent()
	            .toggleClass('t-state-default', visibility)
				.toggleClass('t-state-active', !visibility)
				.find('> .t-link > .t-icon')
					.toggleClass('t-arrow-up', !visibility)
					.toggleClass('t-arrow-down', visibility);

            $t.fx[!visibility ? 'play' : 'rewind'](this.effects, $element);
        },

        _collapseAllExpanded: function($item) {
            if ($item.find('> .t-link').hasClass('t-header')) {
                if ($item.find('> .t-content, > .t-group').is(':visible') || $item.find('> .t-content, > .t-group').length == 0) {
                    return true;
                } else {
                    $(this.element).children().find('> .t-content, > .t-group')
                            .filter(function() { return $(this).is(':visible') })
                            .each($t.bind(this, function(index, content) {
                                this._toggleGroup($(content), true);
                            }));
                }
            }
        },

        _ajaxRequest: function($element, contentElement, isVisible) {

            var statusIcon = $('.t-arrow-up, .t-arrow-down', $element);
            var loadingIconTimeout = setTimeout(function() {
                statusIcon.addClass('t-loading');
            }, 100);

            var data = {};

            $.ajax({
                type: 'GET',
                url: $element.find('.t-link').attr('href'),
                dataType: 'html',
                data: data,

                error: $t.bind(this, function(xhr, status) {

                    if ($t.ajaxError('error.PanelBar', xhr, status))
                        return;

                    if (status == 'error')
                        alert('Error! The server returned: "' + xhr.status + ' ' + xhr.statusText + '"');
                    if (status == 'timeout')
                        alert('Error! Server timeout.');
                    if (status == '0')
                        alert('You are offline!!\n Please Check Your Network.');
                    if (status == '404')
                        alert('Requested URL not found.');
                    if (status == '500')
                        alert('Internal Server Error.');

                }),

                complete: function() {
                    clearTimeout(loadingIconTimeout);
                    statusIcon.removeClass('t-loading');
                },

                success: $t.bind(this, function(data, textStatus) {
                    contentElement.html(data);
                    this._toggleGroup(contentElement, isVisible);
                    var $link = contentElement.prev('.t-link');
                    $link.data('ContentUrl', $link.attr('href'))
                         .attr('href', '#');
                })
            });
        }
    });

    // Plugin declaration
    $.fn.tPanelBar = function(options) {
        // Extend our default options with those provided.
        options = $.extend({}, $.fn.tPanelBar.defaults, options);

        return this.each(function() {
            // support for Metadata plugin
            options = $.meta ? $.extend({}, options, $(this).data()) : options;

            //Initialize only if needed
            if (!$(this).data('tPanelBar'))
                $(this).data('tPanelBar', new $t.panelbar(this, options));
        });
    };

    $.fn.tPanelBar.defaults = {
        effects: [
			$t.fx.property.defaults('height')
		]
    };
})(jQuery);