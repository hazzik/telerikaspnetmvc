﻿(function ($) {

    var $t = $.telerik;
    
    $t.slider = function (element, options) {
        var $element = $(element);
        this.element = element;
        options.distance = options.maxValue - options.minValue;
        $.extend(this, options);
        options.position = this.orientation == "horizontal" ? "left" : "bottom";
        options.size = this.orientation == "horizontal" ? "width" : "height";
        options.orientation = this.orientation;

        createHtml(element, options);
        this.wrapper = $element.closest(".t-slider");
        this.trackDiv = this.wrapper.find(".t-slider-track");

        $t.slider.setTrackDivWidth(this.wrapper, options);

        this.maxSelection = this.trackDiv[options.size]();
        
        var sizeBetweenTicks = this.maxSelection / ((this.maxValue - this.minValue) / this.smallStep);

        if (options.tickPlacement != "none" && sizeBetweenTicks >= 2) {
            this.trackDiv.before(createSliderItems(options));
            $t.slider.setItemsWidth(this.wrapper, this.trackDiv, options);
            $t.slider.setItemsTitle(this.wrapper, options);
            $t.slider.setItemsLargeTick(this.wrapper, options);
        } else {
            this.pixelStepsArray = $t.slider.getPixelSteps(this.trackDiv, options);
        }

        var settings = {
            element: element,
            dragHandle: this.wrapper.find(".t-draghandle"),
            orientation: options.orientation,
            size: options.size,
            position: options.position,
            owner: this
        };

        this._setValueInRange(options.val);

        this[options.enabled ? 'enable' : 'disable']();

        new $t.slider.Selection(settings);
        new $t.slider.Drag(settings);

        this.keyMap = {
            37: decreaseValue(options.smallStep), // left arrow
            40: decreaseValue(options.smallStep), // down arrow
            39: increaseValue(options.smallStep), // right arrow
            38: increaseValue(options.smallStep), // up arrow
            35: setValue(options.maxValue), // end
            36: setValue(options.minValue), // home
            33: increaseValue(options.largeStep), // page up
            34: decreaseValue(options.largeStep)  // page down
        };

        $t.bind(this, {
            slide: this.onSlide,
            change: this.onChange,
            load: this.onLoad
        });
    }

    $.extend($t.slider, {
        setTrackDivWidth: function (wrapper, options) {
            var trackDiv = wrapper.find('.t-slider-track');
            var trackDivPosition = parseFloat(trackDiv.css(options.position), 10) * 2;
            trackDiv[options.size]((wrapper[options.size]() - 2) - trackDivPosition);
        },

        setItemsWidth: function (wrapper, trackDiv, options) {
            var itemsCount = Math.floor(options.distance / options.smallStep),
                items = wrapper.find(".t-tick"),
                sum = 0,
                maxSelection = trackDiv[options.size]();

            var pixelWidths = this.calculateItemsWidth(wrapper, options, itemsCount);

            if (options.orientation == "horizontal") {
                for (var i = 0; i < items.length - 2; i++) {
                    $(items[i + 1])[options.size](pixelWidths[i]);
                }
            } else {
                pixelWidths = pixelWidths.reverse();

                for (var i = 2; i < items.length; i++) {
                    $(items[i - 1])[options.size](pixelWidths[i]);
                }
            }

            if (options.orientation == "horizontal") {
                $(items[0]).addClass("t-first")[options.size](pixelWidths[itemsCount]);
                $(items[items.length - 1]).addClass("t-last")[options.size](pixelWidths[itemsCount - 1]);
            } else {
                $(items[items.length - 1]).addClass("t-first")[options.size](pixelWidths[0]);
                $(items[0]).addClass("t-last")[options.size](pixelWidths[1]);
            }

            if (options.distance % options.smallStep != 0 && options.orientation == "vertical") { 
                for (var i = 0; i < pixelWidths.length; i++) {
                    sum += pixelWidths[i];
                }

                wrapper.find(".t-slider-items").css("padding-top", 29 + (maxSelection - sum));
            }
        },

        setItemsTitle: function (wrapper, options) {
            var items = wrapper.find(".t-tick"),
                titleNumber = options.minValue;

            if (options.orientation == "horizontal") {
                for (var i = 0; i < items.length; i++) {
                    $(items[i]).attr("title", $t.formatString(options.tooltip.format || "{0}", parseFloat(titleNumber.toFixed(3), 10)));
                    titleNumber += options.smallStep;
                }
            } else {
                for (var i = items.length - 1; i >= 0; i--) {
                    $(items[i]).attr("title", $t.formatString(options.tooltip.format || "{0}", parseFloat(titleNumber.toFixed(3), 10)));
                    titleNumber += options.smallStep;
                }
            }
        },

        setItemsLargeTick: function (wrapper, options) {
            if ((1000 * options.largeStep) % (1000 * options.smallStep) == 0) {
                var items = wrapper.find(".t-tick"),
                    item = {},
                    step = parseFloat((options.largeStep / options.smallStep).toFixed(3), 10);

                if (options.orientation == "horizontal") {
                    for (var i = 0; i < items.length; i = parseFloat((i + step).toFixed(3), 10)) {
                        item = $(items[i]);
                    
                        item.addClass("t-tick-large")
                            .html($("<span class='t-label'></span>").html(item.attr("title")));
                    }
                } else {
                    for (var i = items.length - 1; i >= 0; i = parseFloat((i - step).toFixed(3), 10)) {
                        item = $(items[i]);

                        item.addClass("t-tick-large")
                            .html($("<span class='t-label'></span>").html(item.attr("title")));

                        if (i != 0 && i != items.length - 1) {
                            item.css('line-height', item[options.size]() + 'px');
                        }
                    }
                }
            }
        },

        calculateItemsWidth: function (wrapper, options, itemsCount) {
            var trackDivSize = parseFloat(wrapper.find('.t-slider-track').css(options.size)) + 1,
                pixelStep = trackDivSize / options.distance;

            if ((options.distance / options.smallStep) - Math.floor(options.distance / options.smallStep) > 0) {
                trackDivSize -= ((options.distance % options.smallStep) * pixelStep);
            }

            var itemWidth = trackDivSize / itemsCount,
                pixelWidths = new Array();

            for (var i = 0; i < itemsCount - 1; i++) {
                pixelWidths[i] = itemWidth;
            }

            pixelWidths[itemsCount - 1] = pixelWidths[itemsCount] = itemWidth / 2;
            return this.roudWidths(pixelWidths);
        },

        roudWidths: function (pixelWidthsArray) {
            var balance = 0;

            for (i = 0; i < pixelWidthsArray.length; i++) {
                balance += (pixelWidthsArray[i] - Math.floor(pixelWidthsArray[i]));
                pixelWidthsArray[i] = Math.floor(pixelWidthsArray[i]);
            }

            balance = Math.round(balance);

            return this.addAdditionalSize(balance, pixelWidthsArray);
        },

        addAdditionalSize: function (additionalSize, pixelWidthsArray) {
            if (additionalSize == 0) {
                return pixelWidthsArray;
            }

            //set step size
            var step = parseFloat(pixelWidthsArray.length - 1) / parseFloat(additionalSize == 1 ? additionalSize : additionalSize - 1);

            for (var i = 0; i < additionalSize; i++) {
			    pixelWidthsArray[parseInt(Math.round(step * i))] += 1;
			}

            return pixelWidthsArray;
        },

        getPixelSteps: function (trackDiv, options) {
            var trackDivSize = parseInt(trackDiv.css(options.size)),
                pixelSteps = new Array(),
                pixelStep = parseFloat(((trackDivSize / options.distance) * options.smallStep).toFixed(5), 10),
                result = trackDivSize;
                i = 0;
            
            if (pixelStep == 0) {
                return pixelSteps;
            }

            while (result != 0) {
                pixelSteps[i] = pixelStep;
                result = parseFloat((result - pixelStep).toFixed(5), 10);
                i++;

                if (result <= pixelStep) {
                    pixelSteps[i] = parseFloat(result.toFixed(5), 10);
                    result = 0;
                }
            }
            
            return pixelSteps;
        },

        getValueFromPosition: function (mousePosition, dragableArea, owner) {
            var step = Math.max(owner.smallStep * (owner.maxSelection / owner.distance), 0),
                position,
                halfStep = (step / 2),
                i = 0;

            if (owner.orientation == "horizontal") {
                position = mousePosition - dragableArea.startPoint;
            } else {
                position = dragableArea.startPoint - mousePosition;
            }

            if (owner.maxSelection - ((parseInt(owner.maxSelection % step) - 3) / 2) < position) {
                return owner.maxValue;
            }

            position += halfStep;

            if (position >= halfStep) {
                while (position > step) {
                    position -= step;
                    i += owner.smallStep;
                }
            }

            return parseFloat((owner.minValue + i).toFixed(3));
        },

        getDragableArea: function (trackDiv, maxSelection, orientation) {
            var offsetLeft = trackDiv.offset().left,
                offsetTop = trackDiv.offset().top;

            return {
                startPoint: orientation == "horizontal" ? offsetLeft : offsetTop + maxSelection,
                endPoint: orientation == "horizontal" ? offsetLeft + maxSelection : offsetTop
            };
        },

        fixDragHandlePosition: function (val, itemsUl, options) {
            var selectionValue = val - options.owner.minValue,
                selection = 0;

            if (val == options.owner.minValue || val == options.owner.maxValue) {
                if (val == options.owner.maxValue) {
                    selection = options.owner.maxSelection;
                }   
            } else {
                var itemIndex = parseInt(((options.orientation == "horizontal" ? selectionValue : options.owner.maxValue - val) / options.owner.smallStep).toFixed(3)),
                    item = $(itemsUl.find(".t-tick")[itemIndex]),
                    halfItemSize = item[options.size]() / 2,
                    itemOffset = item.offset(),
                    dragableArea = $t.slider.getDragableArea(options.owner.trackDiv, options.owner.maxSelection, options.orientation);
                   
                if (options.orientation == "horizontal") {
                    selection = itemOffset.left - dragableArea.startPoint + halfItemSize;
                } else {
                    selection = (dragableArea.startPoint - (itemOffset.top + halfItemSize)) + 1;
                    if (!$.browser.mozilla) {
                        selection += (selection - Math.floor(selection)) > 0 ? 1 : 0;
                    }
                }
            }

            return selection;
        }
    });
    
    function increaseValue(step) {
        return function (value) {
            return value + step;
        }
    }

    function decreaseValue(step) {
        return function (value) {
            return value - step;
        }
    }

    function setValue(value) {
        return function () {
            return value;
        }
    }

    $t.slider.prototype = {
        enable: function () {
            this.wrapper
                .removeAttr("disabled")
                .removeClass("t-state-disabled")
                .addClass("t-state-default");

            var clickHandler = $.proxy(function (e) {
                var mousePosition = this.orientation == "horizontal" ? e.pageX : e.pageY,
                    dragableArea = $t.slider.getDragableArea(this.trackDiv, this.maxSelection, this.orientation);

                this._update($t.slider.getValueFromPosition(mousePosition, dragableArea, this));
            }, this)

            this.wrapper
                .find(".t-tick").bind("click", clickHandler)
                .end()
                .find(".t-slider-track").bind("click", clickHandler);

            if (this.showButtons) {
                var mouseDownHandler = $.proxy(function(e, sign) {
                    if (e.which == 1) {
                        this._setValueInRange(this.val + (sign * this.smallStep));
                        this.timeout = setTimeout($.proxy(function () {
                            this.timer = setInterval($.proxy(function () {
                                this._setValueInRange(this.val + (sign * this.smallStep));
                            }, this), 60);
                        }, this), 200);
                    }
                }, this);

                this.wrapper.find(".t-button")
                    .unbind("mousedown")
                    .unbind("mouseup")
                    .bind("mouseup", $.proxy(function (e) {
                        this._clearTimer();
                    }, this))
                    .unbind("mouseover")
                    .bind("mouseover", function (e) {
                        $(e.currentTarget).addClass("t-state-hover");
                    })
                    .unbind("mouseout")
                    .bind("mouseout", $.proxy(function (e) {
                        $(e.currentTarget).removeClass("t-state-hover");
                        this._clearTimer();
                    }, this))
                    .eq(0)
                    .bind("mousedown", $.proxy(function (e) {
                        mouseDownHandler(e, 1);
                    }, this))
                    .end()
                    .eq(1)
                    .bind("mousedown", $.proxy(function (e) {
                        mouseDownHandler(e, -1);
                    }, this))
            }

            this.wrapper
                .find(".t-draghandle").bind({
                    keydown: $.proxy(this._keydown, this)
                });

            this.enabled = true;
        },

        disable: function () {
            this.wrapper
                .attr("disabled", "disabled")
                .removeClass("t-state-default")
                .addClass("t-state-disabled");

            var preventDefault = $t.preventDefault;

            this.wrapper
                .find(".t-button")
                .unbind("mousedown")
                .bind("mousedown", preventDefault)
                .unbind("mouseup")
                .bind("mouseup", preventDefault)
                .unbind("mouseleave")
                .bind("mouseleave", preventDefault)
                .unbind("mouseover")
                .bind("mouseover", preventDefault);

            this.wrapper
                .find(".t-tick").unbind("click")
                .end()
                .find(".t-slider-track").unbind("click");

            this.wrapper
                .find(".t-draghandle")
                .unbind("keydown")
                .bind("keydown", preventDefault)

            this.enabled = false;
        },

        _update: function (val) {
            var change = this.value() != val;
            
            this.value(val);
            
            if (change) {
                $t.trigger(this.element, 'change', { value: this.val });
            }
        },

        value: function (val) {
            val = parseFloat(parseFloat(val, 10).toFixed(3), 10);
            if (isNaN(val)) {
                return this.val;
            }

            if (val >= this.minValue && val <= this.maxValue) {
                if (this.val != val) {
                    $(this.element).val(val);
                    this.val = val;
                    this.refresh();
                }
            }
        },

        refresh: function () {
            $t.trigger(this.element, 't:moveSelection', { value: this.val });
        },

        _clearTimer: function (e) {
            clearTimeout(this.timeout);
            clearInterval(this.timer);
        },

        _keydown: function (e) {
            if (e.keyCode in this.keyMap) {
                this._setValueInRange(this.keyMap[e.keyCode](this.val));
                e.preventDefault();
            }
        },

        _setValueInRange: function (val) {
            val = parseFloat(parseFloat(val, 10).toFixed(3), 10);
            if (isNaN(val)) {
                this._update(this.minValue);
                return;
            }

            val = Math.max(val, this.minValue);
            val = Math.min(val, this.maxValue);
            this._update(val);
        }
    }

    $t.slider.Selection = function (options) {
        var $element = $(options.element);

        function moveSelection (val) {
            var selectionValue = val - options.owner.minValue,
                itemsUl = options.owner.wrapper.find(".t-slider-items"),
                i = 0,
                selection = 0;

            if (itemsUl.length != 0) {
                selection = $t.slider.fixDragHandlePosition(val, itemsUl, options);
            } else {
                if (options.owner.pixelStepsArray.length == 0) {
                    selection = 0;
                } else {
                    while (selectionValue > 0) {
                        selectionValue = parseFloat((selectionValue - options.owner.smallStep).toFixed(5), 10);
                        selection += options.owner.pixelStepsArray[i];
                        i++;
                    }
                }
            }
        
            var selectionDiv = options.owner.trackDiv.find(".t-slider-selection"),
                halfDragHanndle = parseInt(options.dragHandle[options.size]() / 2, 10) + 1;
        
            selectionDiv[options.size](selection);
            options.dragHandle.css(options.position, selection - halfDragHanndle);
        }

        moveSelection(parseFloat($element.val(), 10));

        var handler = function (e) {
            moveSelection(parseFloat(e.value, 10));
        };

        $element.bind({ "change": handler, "slide": handler, "t:moveSelection": handler });
    }

    $t.slider.Drag = function (options) {
        options.dragHandleSize = options.dragHandle[options.size]();

        $.extend(this, options);

        var selector = "";

        switch (options.type) {
            case "leftHandle": selector = ".t-draghandle:first";
                break;
            case "rightHandle": selector = ".t-draghandle:last";
                break;
            default: selector = ".t-draghandle";
                break;
        }
        
        new $t.draggable({
            distance: 0,
            owner: options.owner.wrapper[0],
            selector: selector,
            scope: options.element.id,
            start: $.proxy(this.start, this),
            drag: $.proxy(this.drag, this),
            stop: $.proxy(this.stop, this)
        });
    };

    $t.slider.Drag.prototype = {
        start: function (e) {
            if (!this.owner.enabled) {
                return false;
            }

            $(this.element).unbind('mouseover');
            
            this.val = parseFloat($(this.element).val());
            this.dragableArea = $t.slider.getDragableArea(this.owner.trackDiv, this.owner.maxSelection, this.orientation);
            this.step = Math.max(this.owner.smallStep * (this.owner.maxSelection / this.owner.distance), 0);

            this.selectionStart = this.owner.selectionStart;
            this.selectionEnd = this.owner.selectionEnd;
            this.oldVal = this.val;
            this.format = this.owner.tooltip.format || "{0}";

            if (this.type) {
                this.owner._setZIndex(this.type);
            }

            if (this.owner.tooltip.enabled) {
                this.tooltipDiv = $("<div class='t-widget t-tooltip'><!-- --></div>").appendTo(document.body);
                
                if (this.type) {
                    var formattedSelectionStart = $t.formatString(this.format, this.selectionStart),
                        formattedSelectionEnd = $t.formatString(this.format, this.selectionEnd);

                    this.tooltipDiv.html(formattedSelectionStart + ' - ' + formattedSelectionEnd );
                } else {                 
                    var tooltipArrow = "t-callout-";

                    if (this.orientation == "horizontal") {
                        if (this.owner.tickPlacement == "topLeft") {
                            tooltipArrow += "n";
                        } else {
                            tooltipArrow += "s";
                        }                    } else {
                        if (this.owner.tickPlacement == "topLeft") {
                            tooltipArrow += "w";
                        } else {
                            tooltipArrow += "e";
                        }
                    }

                    this.tooltipInnerDiv = "<div class='t-callout " + tooltipArrow + "'><!-- --></div>";
                    this.tooltipDiv.html($t.formatString(this.owner.tooltip.format || "{0}", this.val) + this.tooltipInnerDiv);
                }

                this.moveTooltip(this.tooltipDiv);
            }
        },

        drag: function (e) {
            if (this.orientation == "horizontal") {
                this.val = this.horizontalDrag(e);
            } else {
                this.val = this.verticalDrag(e);
            }

            if (this.oldVal != this.val) {
                this.oldVal = this.val;

                if (this.type) {
                    if (this.type == "leftHandle") {
                        if (this.val < this.selectionEnd) {
                            this.selectionStart = this.val;
                        } else {
                            this.selectionStart = this.selectionEnd = this.val;
                        }
                    } else {
                        if (this.val > this.selectionStart) {
                            this.selectionEnd = this.val;
                        } else {
                            this.selectionStart = this.selectionEnd = this.val;
                        }
                    }

                    $t.trigger(this.element, "slide", { values: [this.selectionStart, this.selectionEnd] });

                    if (this.owner.tooltip.enabled) {
                        var formattedSelectionStart = $t.formatString(this.format, this.selectionStart),
                            formattedSelectionEnd = $t.formatString(this.format, this.selectionEnd);

                        this.tooltipDiv.html(formattedSelectionStart + ' - ' + formattedSelectionEnd );
                    }
                } else {
                    $t.trigger(this.element, "slide", { value: this.val });
                    
                    if (this.owner.tooltip.enabled) {
                        this.tooltipDiv.html($t.formatString(this.format, this.val) + this.tooltipInnerDiv);
                    }
                }

                if (this.owner.tooltip.enabled) {
                    this.moveTooltip(this.tooltipDiv);
                }
            }
        },

        stop: function (e) {
            if (e.keyCode == 27) { // ESC
                this.owner.refresh();
            } else {
                if (this.type) {
                    this.owner._update(this.selectionStart, this.selectionEnd);
                } else {
                    this.owner._update(this.val);
                }
            }

            if (this.owner.tooltip.enabled) {
                this.tooltipDiv.remove();
            }

            $(this.element).bind('mouseover');
            
            return false;
        },

        moveTooltip: function (tooltipDiv) {
            var top = 0,
                left= 0;

            if (this.type) {
                var dragHandles = this.owner.wrapper.find(".t-draghandle"),
                    firstDragHandleOffset = dragHandles.eq(0).offset(),
                    secondDragHandleOffset = dragHandles.eq(1).offset();

                if (this.orientation == "horizontal") {
                    top = secondDragHandleOffset.top;
                    left = firstDragHandleOffset.left + ((secondDragHandleOffset.left - firstDragHandleOffset.left) / 2);
                } else {
                    top = firstDragHandleOffset.top + ((secondDragHandleOffset.top - firstDragHandleOffset.top) / 2);
                    left = secondDragHandleOffset.left;
                }
            } else {
                var dragHandleOffset = this.dragHandle.offset();

                top = dragHandleOffset.top;
                left = dragHandleOffset.left;
            }

            var halfTooltipDiv = tooltipDiv[this.size]() / 2;

            if (this.orientation == "horizontal") {
                left -= halfTooltipDiv;

                if (this.owner.tickPlacement != "topLeft") {
                    top -= 35;
                } else {
                    top += 33;
                }
            } else {
                top -= halfTooltipDiv;

                if (this.owner.tickPlacement != "topLeft") {
                    left -= tooltipDiv.width() + 23;
                } else {
                    left += 31;
                }
            }

            tooltipDiv.css({ top: top, left: left });
        },

        horizontalDrag: function (mousePosition) {
            var val = 0;

            if (this.dragableArea.startPoint < mousePosition.pageX && mousePosition.pageX < this.dragableArea.endPoint) {
                val = $t.slider.getValueFromPosition(mousePosition.pageX, this.dragableArea, this.owner);
            } else if (mousePosition.pageX >= this.dragableArea.endPoint) {
                val = this.owner.maxValue;
            } else {
                val = this.owner.minValue;
            }

            return val;
        },

        verticalDrag: function (mousePosition) {
            var val = 0;

            if (this.dragableArea.startPoint > mousePosition.pageY && mousePosition.pageY > this.dragableArea.endPoint) {
                val = $t.slider.getValueFromPosition(mousePosition.pageY, this.dragableArea, this.owner);
            } else if (mousePosition.pageY <= this.dragableArea.endPoint) {
                val = this.owner.maxValue;
            } else {
                val = this.owner.minValue;
            }

            return val;
        }
    }

    function createWrapper (options, element) {
        var $element = $(element),
            orientationCssClass = options.orientation == "horizontal" ? " t-slider-horizontal" : " t-slider-vertical",
            tickPlacementCssClass;

        if (options.tickPlacement == "bottomRight") {
            tickPlacementCssClass = " t-slider-bottomright";
        } else if (options.tickPlacement == "topLeft") {
            tickPlacementCssClass = " t-slider-topleft";
        }

        var style = options.style ? options.style : $element.attr("style");

        return new $t.stringBuilder()
                     .cat("<div class='t-widget t-slider")
                     .cat(orientationCssClass)
                     .catIf(" ", $element.attr("class"), $element.attr("class"))
                     .cat("'")
                     .catIf(" style='", style, "'", style)
                     .cat(">")
                     .cat("<div class='t-slider-wrap")
                     .catIf(" t-slider-buttons", options.showButtons)
                     .catIf(tickPlacementCssClass, tickPlacementCssClass)
                     .cat("'></div></div>")
                     .string();
    }
    
    function createButton (options, type) {
        var buttonCssClass,
            isHorizontal = options.orientation == "horizontal";

        if (type == "increase") {
            buttonCssClass = isHorizontal ? "t-arrow-next" : "t-arrow-up";
        } else {
            buttonCssClass = isHorizontal ? "t-arrow-prev" : "t-arrow-down";
        }

        return new $t.stringBuilder()
                     .cat("<a ")
                     .cat("class='t-button ")
                     .cat("t-button-" + type)
                     .cat("'><span class='t-icon ")
                     .cat(buttonCssClass)
                     .cat("' title='")
                     .cat(options[type + "ButtonTitle"])
                     .cat("'>")
                     .cat(options[type + "ButtonTitle"])
                     .cat("</span></a>")
                     .string();
    }

    function createSliderItems (options) {
        return new $t.stringBuilder()
                     .cat("<ul class='t-reset t-slider-items'>")
                     .rep("<li class='t-tick'>&nbsp;</li>", (Math.floor((options.distance / options.smallStep).toFixed(3), 10) + 1))
                     .cat("</ul>")
                     .string();
    }

    function createTrack ($element) {
        var dragHandleCount = $element.is("input") ? 1 : 2;

        return new $t.stringBuilder()
                     .cat("<div class='t-slider-track'>")
                     .cat("<div class='t-slider-selection'><!-- --></div>")
                     .cat("<a href='javascript:void(0)' class='t-draghandle' title='Drag'>Drag</a>")
                     .catIf("<a href='javascript:void(0)' class='t-draghandle t-draghandle1' title='Drag'>Drag</a>", dragHandleCount > 1)
                     .cat("</div>")
                     .string();
    }

    function createHtml (element, options) {
        var $element = $(element);
        $element.val(options.val);
        $element.wrap(createWrapper(options, element)).hide();

        if (options.showButtons) {
            $element.before(createButton(options, "increase"))
                    .before(createButton(options, "decrease"));
        }

        $element.before(createTrack($element));
    }

    // jQuery extender
    $.fn.tSlider = function (options) {
        return $t.create(this, {
            name: "tSlider",
            init: function (element, options) {
                return new $t.slider(element, options);
            },
            options: options
        });
    };

    // default options
    $.fn.tSlider.defaults = {
        enabled: true,
        minValue: 0,
        maxValue: 10,
        val: 0,
        smallStep: 1,
        largeStep: 5,
        showButtons: true,
        increaseButtonTitle: "Increase",
        decreaseButtonTitle: "Decrease",
        orientation: "horizontal",
        tickPlacement: "both",
        tooltip: { enabled: true, format: "{0}" }
    };

    //
    // RangeSlider
    //

    $t.rangeSlider = function (element, options) {
        var $element = $(element);
        this.element = element;
        options.distance = options.maxValue - options.minValue;
        $.extend(this, options);
        options.position = this.orientation == "horizontal" ? "left" : "bottom";
        options.size = this.orientation == "horizontal" ? "width" : "height";

        createHtml(element, options);
        this.wrapper = $element.closest(".t-slider");
        this.trackDiv = this.wrapper.find(".t-slider-track");

        $t.slider.setTrackDivWidth(this.wrapper, options);
        this.maxSelection = this.trackDiv[options.size]();

        var sizeBetweenTicks = this.maxSelection / ((this.maxValue - this.minValue) / this.smallStep);

        if (options.tickPlacement != "none" && sizeBetweenTicks >= 2) {
            this.trackDiv.before(createSliderItems(options));
            $t.slider.setItemsWidth(this.wrapper, this.trackDiv, options);
            $t.slider.setItemsTitle(this.wrapper, options);
            $t.slider.setItemsLargeTick(this.wrapper, options);
        } else {
            this.pixelStepsArray = $t.slider.getPixelSteps(this.trackDiv, options);
        }

        this._correctValues(this.selectionStart, this.selectionEnd);

        var leftDrag = {
            element: element,
            type: "leftHandle",
            dragHandle: this.wrapper.find(".t-draghandle:first"),
            orientation: options.orientation,
            size: options.size,
            position: options.position,
            owner: this
        };

        new $t.slider.Drag(leftDrag);
        new $t.rangeSlider.Selection(leftDrag);

        var rightDrag = {
            element: element,
            type: "rightHandle",
            dragHandle: this.wrapper.find(".t-draghandle:last"),
            orientation: options.orientation,
            size: options.size,
            position: options.position,
            owner: this
        };

        new $t.slider.Drag(rightDrag);

        this[options.enabled ? 'enable' : 'disable']();

        this.keyMap = {
            37: decreaseValue(options.smallStep), // left arrow
            40: decreaseValue(options.smallStep), // down arrow
            39: increaseValue(options.smallStep), // right arrow
            38: increaseValue(options.smallStep), // up arrow
            35: setValue(options.maxValue), // end
            36: setValue(options.minValue), // home
            33: increaseValue(options.largeStep), // page up
            34: decreaseValue(options.largeStep)  // page down
        };

        $t.bind(this, {
            slide: this.onSlide,
            change: this.onChange,
            load: this.onLoad
        });
    }

    $t.rangeSlider.prototype = {
        enable: function () {
            this.wrapper
                .removeAttr("disabled")
                .removeClass("t-state-disabled")
                .addClass("t-state-default");

            var clickHandler = $.proxy(function (e) {
                var mousePosition = this.orientation == "horizontal" ? e.pageX : e.pageY,
                    dragableArea = $t.slider.getDragableArea(this.trackDiv, this.maxSelection, this.orientation),
                    val = $t.slider.getValueFromPosition(mousePosition, dragableArea, this);

                if (val < this.selectionStart) {
                    this._setValueInRange(val, this.selectionEnd);
                } else if (val > this.selectionEnd) {
                    this._setValueInRange(this.selectionStart, val);
                } else {
                    if (val - this.selectionStart <= this.selectionEnd - val) {
                        this._setValueInRange(val, this.selectionEnd);
                    } else {
                        this._setValueInRange(this.selectionStart, val);
                    }
                }
            }, this)

            this.wrapper
                .find(".t-tick").bind("click", clickHandler)
                .end()
                .find(".t-slider-track").bind("click", clickHandler);

            this.wrapper.find(".t-draghandle")
                .eq(0).bind({
                    keydown: $.proxy(function(e) {
                        this._keydown(e, true);
                    }, this)
                })
                .end()
                .eq(1).bind({
                    keydown: $.proxy(function(e) {
                        this._keydown(e, false);
                    }, this)
                });

            this.enabled = true;
        },

        disable: function () {
            this.wrapper
                .attr("disabled", "disabled")
                .removeClass("t-state-default")
                .addClass("t-state-disabled");

            this.wrapper
                .find(".t-tick").unbind("click")
                .end()
                .find(".t-slider-track").unbind("click");

            this.wrapper
                .find(".t-draghandle")
                .unbind("keydown")
                .bind("keydown", $t.preventDefault)
                
            this.enabled = false;
        },

        _keydown: function (e, isLeftHandle) {
            var selectionStartValue = this.selectionStart,
                selectionEndValue = this.selectionEnd;

            if (e.keyCode in this.keyMap) {
                if (isLeftHandle) {
                    selectionStartValue = this.keyMap[e.keyCode](selectionStartValue);
                    
                    if (selectionStartValue > selectionEndValue) {
                        selectionEndValue = selectionStartValue;
                    }
                } else {
                    selectionEndValue = this.keyMap[e.keyCode](selectionEndValue);

                    if (selectionEndValue < selectionStartValue) {
                        selectionStartValue = selectionEndValue;
                    }
                }

                this._setValueInRange(selectionStartValue, selectionEndValue);
                e.preventDefault();
            }
        },

        _update: function (selectionStart, selectionEnd) {
            var values = this.values();

            var change = values[0] != selectionStart || values[1] != selectionEnd;

            this.values(selectionStart, selectionEnd);
            
            if (change) {
                $t.trigger(this.element, 'change', { values: [selectionStart, selectionEnd] });
            }
        },

        values: function (selectionStart, selectionEnd) {
            var values = [this.selectionStart, this.selectionEnd];

            selectionStart = parseFloat(parseFloat(selectionStart, 10).toFixed(3), 10);
            if (isNaN(selectionStart)) {
                return values;
            }

            selectionEnd = parseFloat(parseFloat(selectionEnd, 10).toFixed(3), 10);
            if (isNaN(selectionEnd)) {
                return values;
            }

            if (selectionStart >= this.minValue && selectionStart <= this.maxValue
            && selectionEnd >= this.minValue && selectionEnd <= this.maxValue && selectionStart <= selectionEnd) {
                if (this.selectionStart != selectionStart || this.selectionEnd != selectionEnd) {
                    $(this.element).find("input")
                                   .eq(0).val(selectionStart)
                                   .end()
                                   .eq(1).val(selectionEnd);
                    
                    this.selectionStart = selectionStart;
                    this.selectionEnd = selectionEnd;
                    this.refresh();
                }
            }
        },

        refresh: function() {
            $t.trigger(this.element, 't:moveSelection', { values: [this.selectionStart, this.selectionEnd] });

            if (this.selectionStart == this.maxValue && this.slectionEnd == this.maxValue) {
                this._setZIndex("leftHandle");
            }
        },

        _setValueInRange: function (selectionStart, selectionEnd) {
            selectionStart = Math.max(selectionStart, this.minValue);
            selectionStart = Math.min(selectionStart, this.maxValue);

            selectionEnd = Math.max(selectionEnd, this.minValue);
            selectionEnd = Math.min(selectionEnd, this.maxValue);

            if (this.selectionStart == this.maxValue && this.slectionEnd == this.maxValue) {
                this._setZIndex("leftHandle");
            }

            this._update(selectionStart, selectionEnd);
        },

        _correctValues: function (selectionStartValue, selectionEndValue) {
            if (selectionStartValue >= selectionEndValue) {
                this._setValueInRange(selectionEndValue, selectionStartValue);
            } else {
                this._setValueInRange(selectionStartValue, selectionEndValue);
            }
        },

        _setZIndex: function (type) {
            var dragHandles = this.wrapper.find(".t-draghandle"),
                firstHandle = dragHandles.eq(0),
                secondHandle = dragHandles.eq(1),
                zIndex = "z-index";

            if (type == "leftHandle") {
                firstHandle.css(zIndex, "1");
                secondHandle.css(zIndex, "");
            } else {
                firstHandle.css(zIndex, "");
                secondHandle.css(zIndex, "1");
            }
        }
    }

    $t.rangeSlider.Selection = function (options) {
        function moveSelection(values) {
            var selectionStartValue = values[0] - options.owner.minValue, 
                selectionEndValue = values[1] - options.owner.minValue,
                itemsUl = options.owner.wrapper.find(".t-slider-items"),
                selectionStart = 0,
                selectionEnd = 0,
                i = 0;

            if (itemsUl.length != 0) {
                selectionStart = $t.slider.fixDragHandlePosition(values[0], itemsUl, options);
                selectionEnd = $t.slider.fixDragHandlePosition(values[1], itemsUl, options);
            } else {
                while (selectionStartValue > 0) {
                    selectionStartValue = parseFloat((selectionStartValue - options.owner.smallStep).toFixed(5), 10);
                    selectionStart += options.owner.pixelStepsArray[i];
                    i++;
                }

                i = 0;
                while (selectionEndValue > 0) {
                    selectionEndValue = parseFloat((selectionEndValue - options.owner.smallStep).toFixed(5), 10);
                    selectionEnd += options.owner.pixelStepsArray[i];
                    i++;
                }
            }

            var dragHandles = options.owner.wrapper.find(".t-draghandle");

            var halfHandle = parseInt(dragHandles.eq(0)[options.size]() / 2, 10) + 1;

            dragHandles.eq(0).css(options.position, selectionStart - halfHandle)
                       .end()
                       .eq(1).css(options.position, selectionEnd - halfHandle);

            makeSelection(selectionStart, selectionEnd);
        }

        function makeSelection(selectionStart, selectionEnd) {
            var selection = 0,
                selectionPosition = 0;

            if (selectionStart < selectionEnd) {
                selection = selectionEnd - selectionStart;
                selectionPosition = selectionStart;
            } else {
                selection = selectionStart - selectionEnd;
                selectionPosition = selectionEnd;
            }

            var selectionDiv = options.owner.trackDiv.find(".t-slider-selection");
            selectionDiv[options.size](selection);
            selectionDiv.css(options.position, selectionPosition - 1);
        }

        var inputs = $(options.owner.element).find("input");

        moveSelection([parseFloat(inputs[0].getAttribute("value"), 10), parseFloat(inputs[1].getAttribute("value"), 10)]);

        var handler = function (e) {
            moveSelection(e.values);
        };

        $(options.owner.element).bind({ "change": handler, "slide": handler, "t:moveSelection": handler  });
    }

    // jQuery extender
    $.fn.tRangeSlider = function (options) {
        return $t.create(this, {
            name: "tRangeSlider",
            init: function (element, options) {
                return new $t.rangeSlider(element, options);
            },
            options: options
        });
    };

    // default options
    $.fn.tRangeSlider.defaults = {
        enabled: true,
        minValue: 0,
        maxValue: 10,
        slectionStart: 0,
        slectionEnd: 10,
        smallStep: 1,
        largeStep: 5,
        orientation: "horizontal",
        tickPlacement: "both",
        tooltip: { enabled: true, format: "{0}" }
    };
})(jQuery);
