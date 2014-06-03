(function($) {
    // fix background flickering under IE6
    try {
        if (document.execCommand)
            document.execCommand('BackgroundImageCache', false, true);
    } catch (e) { }

    // Patching jQuery to support live events with context and mouseenter/mouseleave. Remove once this becomes a built-in feature.

    function liveConvert(type, selector) {
        return ['live', type, selector.replace(/\./g, '`').replace(/ /g, '|')].join('.');
    };

    function isBogus(element) {
        try {
            var parent = element.parentNode;
            return false;
        } catch (error) {
            return true;
        }
    }

    var contains = document.compareDocumentPosition ? function(a, b) {
        return (a && b ? a.compareDocumentPosition(b) & 16 : true);
    } : function(a, b) {
        return (a && b ? a !== b && (a.contains ? a.contains(b) : true) : true);
    };

    var routedLiveEvents = {
        mouseenter: 'mouseover',
        mouseleave: 'mouseout'
    };

    $.fn.extend({
        live: function(type, fn, bubble) {
            var proxy;

            if (routedLiveEvents[type]) {
                type = routedLiveEvents[type];
                proxy = $.event.proxy(fn, function(e) {
                    if (!isBogus(e.relatedTarget)) {
                        if (this !== e.relatedTarget && !contains(this, e.relatedTarget))
                            fn.apply(this, arguments);

                        if (bubble)
                            $(this.parentNode).trigger(e);
                    }
                });
            } else {
                proxy = $.event.proxy(fn);
            }

            proxy.guid += this.selector + type;
            $(this.context).bind(liveConvert(type, this.selector), this.selector, proxy);

            return this;
        },

        die: function(type, fn) {
            if (routedLiveEvents[type])
                type = routedLiveEvents[type];

            $(this.context).unbind(liveConvert(type, this.selector), fn ? { guid: fn.guid + this.selector + type} : null);
            return this;
        }
    });

    // End of patch

    // All Telerik-specific code and component objects will live in this namespace

    $.telerik = {
        contains: contains,

        delegate: function(context, handler) {
            return function(e) {
                handler.apply(context, [e, this]);
            };
        },

        hover: function() {
            $(this).addClass('t-state-hover');
        },

        leave: function() {
            $(this).removeClass('t-state-hover');
        },
		
		buttonHover : function() {
			$(this).addClass('t-button-hover');
		},
		
		buttonLeave : function() {
			$(this).removeClass('t-button-hover');
		},

        bind: function(context, handler) {
            return function() {
                handler.apply(context, arguments);
            };
        },

        preventDefault: function(e) {
            e.preventDefault();
        },

        stringBuilder: function() {
            this.buffer = [];
        },

        ajaxError: function(element, eventName, xhr, status) {
            return this.trigger(element, eventName, 
                {
                    XMLHttpRequest : xhr,
                    textStatus : status
                }
            );
        },
        
        trigger: function(element, eventName, options) {
            var e = new $.Event(eventName);
            $.extend(e, options);
            $(element).trigger(e);
            return e.isDefaultPrevented();
        },
        
        defaultActionEvent: function(type, e) {
            return {
                type: type,
                preventDefault: function() {
                    e.preventDefault();
                }
            };
        },

        fx: {}
    };

    $.extend($.telerik.stringBuilder.prototype, {

        append: function(what) {
            this.buffer[this.buffer.length] = what;
            return this;
        },

        string: function() {
            return this.buffer.join('');
        }
    });

    // Effects ($.telerik.fx)

    $.extend($.telerik.fx, {
        _wrap: function(element) {
            if (!element.parent().hasClass('.t-animation-container')) {
                element.wrap(
					$('<div/>')
						.addClass('t-animation-container')
						.css({
						    width: element.outerWidth(),
						    height: element.outerHeight()
						}));
            }

            return element.parent();
        },

        play: function(effects, target, options, end) {
            if (target.length == 0) {
                if (end) end();
                return;
            }

            var effect;

            target.stop(false, true);
            
            var animationsToPlay = effects.length;
            var afterAnimation = function() {
                if (--animationsToPlay == 0 && end)
                    end();
            };

            for (var i = 0, len = effects.length; i < len; i++) {

                effect = new $.telerik.fx[effects[i].name](target);

                if (!target.data('effect-' + i)) {
                    effect.play($.extend(effects[i], options), afterAnimation);

                    target.data('effect-' + i, effect);
                }
            }
        },

        rewind: function(effects, target, options, end) {
            if (target.length == 0) {
                if (end) end();
                return;
            }

            var effect;
            
            var animationsToPlay = effects.length;
            var afterAnimation = function() {
                if (--animationsToPlay == 0 && end)
                    end();
            };

            for (var i = effects.length - 1; i >= 0; i--) {
                effect = target.data('effect-' + i) || new $.telerik.fx[effects[i].name](target);

                effect.rewind($.extend(effects[i], options), afterAnimation);

                target.data('effect-' + i, null);
            }
        }
    });

    // simple show/hide toggle

    $.telerik.fx.toggle = function(element) {
        this.element = element.stop(false, true);
    };

    $.extend($.telerik.fx.toggle.prototype, {
        play: function(options, end) {
            this.element.show();
            if (end) end();
        },
        rewind: function(options, end) {
            this.element.hide();
            if (end) end();
        }
    });

    $.telerik.fx.toggle.defaults = function() {
        return { name: 'toggle' };
    };

    // slide animation

    $.telerik.fx.slide = function(element) {
        this.element = element;

        this.animationContainer = $.telerik.fx._wrap(element);
    };

    $.extend($.telerik.fx.slide.prototype, {
        play: function(options, end) {

            var element = this.element;
            var animationContainer = this.animationContainer;
            var $parent = this.animationContainer.parent();

            animationContainer.parents('.t-animation-container:first').css('overflow', '');

            if ($.inArray($parent.css('position'), ['relative', 'absolute']) < 0)
                this.animationContainer.parent().css('position', 'relative'); /* ie8 :( */

            animationContainer
				.css({
				    overflow: 'hidden',
				    display: 'block'
				});

            var animatedProperty;
            var width = element.width();

            switch (options.direction) {
                case 'bottom':
                    element.css('marginTop', -element.height());
                    animatedProperty = { marginTop: 0 };
                    break;
                case 'right':
                    element.css({
                        marginLeft: -width,
                        width: width /* ie6 :( */
                    });
                    animatedProperty = { marginLeft: 0 };
                    break;
            }

            element
				.css('display', 'block')
				.animate(animatedProperty, {
				    queue: false,
				    duration: options.openDuration,
				    easing: 'linear',
				    complete: function() {
						if (end) end();
				    }
				});
        },

        rewind: function(options, complete) {
            var element = this.element;
            var animationContainer = this.animationContainer;

            animationContainer.css({
                overflow: 'hidden'
            });

            var animatedProperty;

            switch (options.direction) {
                case 'bottom': animatedProperty = { marginTop: -element.height() }; break;
                case 'right': animatedProperty = { marginLeft: -element.width() }; break;
            }

            element
				.animate(animatedProperty, {
				    queue: false,
				    duration: options.closeDuration,
				    easing: 'linear',
				    complete: function() {
				        animationContainer
							.css('display', 'none')
							.parent().css('position', ''); /* ie8 :( */
							
						if (complete) complete();
				    }
				});
        }
    });

    $.telerik.fx.slide.defaults = function() {
        return { name: 'slide', openDuration: 'fast', closeDuration: 'fast' };
    };

    // property animation

    $.telerik.fx.property = function(element) {
        this.element = element;
    };

    $.extend($.telerik.fx.property.prototype, {
        _animate: function(properties, duration, reverse, end) {
            var startValues = {},
				endValues = {},
				$element = this.element;

            $.each(properties, function(i, prop) {
                var value;

                switch (prop) {
                    case 'height':
                    case 'width': value = $element[prop](); break;

                    case 'opacity': value = 1; break;

                    default: value = $element.css(prop); break;
                }

                startValues[prop] = reverse ? value : 0;
                endValues[prop] = reverse ? 0 : value;
            });

            $element
				.css($.extend({
				    overflow: 'hidden'
				}, startValues))
				.show()
				.animate(endValues, {
				    queue: false,
				    duration: duration,
				    easing: 'linear',
				    complete: function() {
				        if (reverse)
				            $element.hide();

				        $.each(endValues, function(property) {
				            endValues[property] = '';
				        });

				        $element.css($.extend({ overflow: 'visible' }, endValues));

				        if (end) end();
				    }
				});
        },

        play: function(options, complete) {
            this._animate(options.properties, options.openDuration, false, complete);
        },

        rewind: function(options, complete) {
            this._animate(options.properties, options.closeDuration, true, complete);
        }
    });

    $.telerik.fx.property.defaults = function() {
        return { name: 'property', properties: arguments, openDuration: 'fast', closeDuration: 'fast' };
    };
})(jQuery);