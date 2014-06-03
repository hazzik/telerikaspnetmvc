(function ($) {

    var $t = $.telerik,
        keycodes = [8, // backspace
                    9, // tab
                    37, // left arrow
                    38, // up arrow
                    39, // right arrow
                    40, // down arrow
                    46, // delete
                    35, // end
                    36, // home
                    44], //","
        styles = ["font-family",
                  "font-size",
                  "font-stretch",
                  "font-style",
                  "font-weight",
                  "letter-spacing",
                  "line-height",
                  "color",
                  "text-align",
                  "text-decoration",
                  "text-indent",
                  "text-transform"];

    function getStyles(input) {
        var retrievedStyles = {};
        for (var i = 0, length = styles.length; i < length; i++) {
            var style = styles[i],
                value = input.css(style);

            if (value) {
                if (styles[i] != "font-style" && value != "normal") {
                    retrievedStyles[style] = value;
                }
            }
        }
        return retrievedStyles;
    }

    $t.textbox = function (element, options) {
        if (element.nodeName.toLowerCase() !== "input" && element.type.toLowerCase() !== "text") {
            throw "Target element is not a INPUT";
        }

        $.extend(this, options);

        this.element = element;
        var $element = this.$element = $(element)
            .bind({
                focus: function (e) {
                    var input = e.target;
                    setTimeout(function () {
                        if ($.browser.msie) {
                            input.select();
                        } else {
                            input.selectionStart = 0;
                            input.selectionEnd = input.value.length;
                        }
                    }, 0);
                },
                keydown: $.proxy(this._keydown, this),
                keypress: $.proxy(this._keypress, this)
            })
            .bind("paste", $.proxy(this._paste, this));

        var builder = new $t.stringBuilder();

        if (element.parentNode.nodeName.toLowerCase() !== "div") {
            $element.addClass('t-input')
                    .wrap($('<div class="t-widget t-numerictextbox"></div>'));

            if (this.showIncreaseButton) {
                builder.cat('<a class="t-link t-icon t-arrow-up" href="#" tabindex="-1" title="')
                       .cat(this.increaseButtonTitle)
                       .cat('">Increment</a>');
            }

            if (this.showDecreaseButton) {
                builder.cat('<a class="t-link t-icon t-arrow-down" href="#" tabindex="-1" title="')
                       .cat(this.decreaseButtonTitle)
                       .cat('">Decrement</a>');
            }

            if (builder.buffer.length > 0) {
                $(builder.string()).insertAfter($element);
            }
        }

        this.$wrapper = $element.closest('.t-numerictextbox')
            .find('.t-arrow-up, .t-arrow-down')
                .bind({
                    click: $t.preventDefault,
                    dragstart: $t.preventDefault
                })
            .end()
            .bind({
                focusin: $.proxy(this._focus, this),
                focusout: $.proxy(this._blur, this)
            });

        this.enabled = !$element.is('[disabled]');

        builder.buffer = [];
        builder.cat('[ |')
               .cat(this.groupSeparator)
               .catIf('|' + this.symbol, this.symbol)
               .cat(']');
        this.replaceRegExp = new RegExp(builder.string(), 'g');

        var inputValue = $element.attr('value');

        builder.buffer = [];
        builder.cat('<div class="t-formatted-value')
               .cat((inputValue == '' && this.enabled) ? ' t-state-empty' : '')
               .cat('">')
               .cat(inputValue || (this.enabled ? this.text : ''))
               .cat('</div>');

        this.$text = $(builder.string())
                        .insertBefore($element)
                        .css(getStyles($element))
                        .click(function (e) {
                            element.focus();
                        });

        //set text color to the background-color.
        this._blur();
        this[this.enabled ? 'enable' : 'disable']();

        this.numFormat = this.numFormat === undefined ? this.type.charAt(0) : this.numFormat;
        var separator = this.separator;
        this.step = this.parse(this.step, separator);
        this.val = this.parse(this.val, separator);
        this.minValue = this.parse(this.minValue, separator);
        this.maxValue = this.parse(this.maxValue, separator);
        this.decimals = { '190': '.', '188': ',', '110': separator };

        this.value(inputValue || this.val);

        $t.bind(this, {
            load: this.onLoad,
            valueChange: this.onChange
        });
    }

    $t.textbox.prototype = {
        _paste: function (e) {
            var val = this.$element.val();
            if ($.browser.msie) {
                var selectedText = this.element.document.selection.createRange().text;
                var text = window.clipboardData.getData("Text");

                if (selectedText && selectedText.length > 0) {
                    val = val.replace(selectedText, text);
                } else {
                    val += text;
                }
            }

            if (val == '-') return true;

            var parsedValue = this.parse(val, this.separator);
            if (parsedValue || parsedValue == 0) {
                this._update(parsedValue);
            }
        },

        _keydown: function (e) {
            var key = e.keyCode,
                $element = this.$element,
                separator = this.separator,
                value = $element.val();

            setTimeout($.proxy(function () {
                $element.toggleClass('t-state-error', !this.inRange(this.parse($element.val(), this.separator), this.minValue, this.maxValue));
            }, this));

            // Allow decimal
            var decimalSeparator = this.decimals[key];
            if (decimalSeparator) {
                if (decimalSeparator == separator
                && this.digits > 0
                && value.indexOf(separator) == -1) {
                    return true;
                } else {
                    e.preventDefault();
                }

            }

            if (key == 8 || key == 46) { //backspace and delete
                setTimeout($.proxy(function () {
                    this._update(this.parse($element.val()));
                }, this));
                return true;
            }

            if (key == 38 || key == 40) {
                var direction = key == 38 ? 1 : -1;
                this._modify(direction * this.step);
                return true;
            }

            if (key == 222) e.preventDefault();
        },

        _keypress: function (e) {
            var $element = $(e.target),
                key = e.keyCode || e.which;

            if (e.shiftKey && key != 45) {
                return false;
            }

            if (key == 0 || $.inArray(key, keycodes) != -1 || e.ctrlKey || (e.shiftKey && key == 45))
                return true;

            if (((this.minValue !== null ? this.minValue < 0 : true)
                    && String.fromCharCode(key) == "-"
                    && $t.caretPos($element[0]) == 0
                    && $element.val().indexOf("-") == -1)
                || this.inRange(key, 48, 57)) {

                setTimeout($.proxy(function () {
                    var parsedValue = this.parse($element.val());
                    if (parsedValue != null && this.digits) {
                        var factor = Math.pow(10, this.digits);
                        parsedValue = parseInt(parsedValue * factor) / factor;
                    }
                    if (this.val != parsedValue) {
                        if ($t.trigger(this.element, 'valueChange', { oldValue: this.val, newValue: parsedValue })) {
                            parsedValue = this.val; //revert changes
                        }
                        this._value(parsedValue);
                    }
                }, this));

                return true;
            }

            e.preventDefault();
        },

        _focus: function () {
            this.$element
                .css('color', this.$text.css("color"));

            this.$text.hide();
        },

        _blur: function () {
            this.$element
                .css('color', this.$element.css('background-color'))
                .removeClass('t-state-error');

            if (this.enabled) {
                this.$text.show();
            }

            var min = this.minValue,
                max = this.maxValue,
                parsedValue = this.parse(this.$element.val());

            if (parsedValue != null) {
                if (min != null && parsedValue < min) {
                    parsedValue = min;
                } else if (max != null && parsedValue > max) {
                    parsedValue = max;
                }
                parsedValue = parseFloat(parsedValue.toFixed(this.digits));
            }
            this._update(parsedValue);
        },

        _clearTimer: function (e) {
            clearTimeout(this.timeout);
            clearInterval(this.timer);
            clearInterval(this.acceleration);
        },

        _stepper: function (e, stepMod) {
            if (e.which == 1) {

                var step = this.step;

                this._modify(stepMod * step);

                this.timeout = setTimeout($.proxy(function () {
                    this.timer = setInterval($.proxy(function () {
                        this._modify(stepMod * step);
                    }, this), 80);

                    this.acceleration = setInterval(function () { step += 1; }, 1000);
                }, this), 200);
            }
        },

        _modify: function (step) {
            var value = this.parse(this.element.value),
                min = this.minValue,
                max = this.maxValue;

            value = value ? value + step : step;

            if (min !== null && value < min) {
                value = min;
            } else if (max !== null && value > max) {
                value = max;
            }

            this._update(parseFloat(value.toFixed(this.digits)));
        },

        _update: function (val) {
            if (this.val != val) {
                if ($t.trigger(this.element, 'valueChange', { oldValue: this.val, newValue: val })) {
                    val = this.val; //revert changes
                }
            }
            this._value(val);
        },

        _value: function (value) {
            var parsedValue = (typeof value === "number") ? value : this.parse(value, this.separator),
                text = this.enabled ? this.text : '',
                isNull = parsedValue === null;

            if (parsedValue != null) {
                parsedValue = parseFloat(parsedValue.toFixed(this.digits));
            }

            this.val = parsedValue;
            this.$element.val(isNull ? '' : this.formatEdit(parsedValue));
            this.$text.html(isNull ? text : this.format(parsedValue));
            if (isNull) {
                this.$text.addClass('t-state-empty');
            } else {
                this.$text.removeClass('t-state-empty');
            }
        },

        enable: function () {
            var $buttons = this.$wrapper.find('.t-arrow-up, .t-arrow-down'),
                clearTimerProxy = $.proxy(this._clearTimer, this);

            this.enabled = true;
            this.$element.removeAttr("disabled");

            if (!this.val && this.val != 0) {
                this.$text
                    .addClass('t-state-empty')
                    .html(this.text);
            } else if (true == $.browser.msie) {
                this.$text.show();
            } else {
                this.$element.css('color', this.$element.css('background-color'))
            }

            this.$wrapper.removeClass('t-state-disabled');
            $buttons.unbind('mouseup').unbind('mouseout').unbind('dblclick')
                    .bind({
                        mouseup: clearTimerProxy,
                        mouseout: clearTimerProxy,
                        dblclick: clearTimerProxy
                    });

            var eventName = "mousedown";
            $buttons.eq(0)
                    .unbind(eventName)
                    .bind(eventName, $.proxy(function (e) {
                        this._stepper(e, 1);
                    }, this));

            $buttons.eq(1)
                    .unbind(eventName)
                    .bind(eventName, $.proxy(function (e) {
                        this._stepper(e, -1);
                    }, this));
        },

        disable: function () {
            this.enabled = false;

            this.$wrapper
                .addClass('t-state-disabled')
                .find('.t-icon')
                    .unbind('mousedown')
                    .bind('mousedown', $t.preventDefault);

            this.$element
                .attr('disabled', 'disabled')

            if (!this.val && this.val != 0)
                this.$text.html('');
            else if (true == $.browser.msie)
                this.$text.hide();
            else
                this.$element.css('color', this.$element.css('background-color'))
        },

        value: function (value) {
            if (value === undefined) {
                return this.val;
            }

            var parsedValue = (typeof value === "number") ? value : this.parse(value, this.separator);
            if (!this.inRange(parsedValue, this.minValue, this.maxValue)) {
                parsedValue = null;
            }

            this._value(parsedValue);
        },

        formatEdit: function (value) {
            var separator = this.separator;
            if (value && separator != '.')
                value = value.toString().replace('.', separator);
            return value;
        },

        format: function (value) {
            return $t.textbox.formatNumber(value,
                                           this.numFormat,
                                           this.digits,
                                           this.separator,
                                           this.groupSeparator,
                                           this.groupSize,
                                           this.positive,
                                           this.negative,
                                           this.symbol,
                                           true);
        },

        inRange: function (key, min, max) {
            return key === null || ((min !== null ? key >= min : true) && (max !== null ? key <= max : true));
        },

        parse: function (value, separator) {
            var result = null;
            if (value || value == "0") {
                if (typeof value == typeof 1) return value;

                value = value.replace(this.replaceRegExp, '');
                if (separator && separator != '.')
                    value = value.replace(separator, '.');

                var negativeFormatPattern = $.fn.tTextBox.patterns[this.type].negative[this.negative]
                        .replace(/(\(|\))/g, '\\$1').replace('*', '').replace('n', '([\\d|\\.]*)'),
                    negativeFormatRegEx = new RegExp(negativeFormatPattern);

                if (negativeFormatRegEx.test(value))
                    result = -parseFloat(negativeFormatRegEx.exec(value)[1]);
                else
                    result = parseFloat(value);
            }
            return isNaN(result) ? null : result;
        }
    }

    $.fn.tTextBox = function (options) {
        var type = 'numeric';
        if (options && options.type) {
            type = options.type;
        }

        var defaults = $.fn.tTextBox.defaults[type];
        defaults.digits = $t.cultureInfo[type + 'decimaldigits'];
        defaults.separator = $t.cultureInfo[type + 'decimalseparator'];
        defaults.groupSeparator = $t.cultureInfo[type + 'groupseparator'];
        defaults.groupSize = $t.cultureInfo[type + 'groupsize'];
        defaults.positive = $t.cultureInfo[type + 'positive'];
        defaults.negative = $t.cultureInfo[type + 'negative'];
        defaults.symbol = $t.cultureInfo[type + 'symbol'];

        options = $.extend({}, defaults, options);
        options.type = type;

        return this.each(function () {
            var $element = $(this);
            options = $.meta ? $.extend({}, options, $element.data()) : options;

            if (!$element.data('tTextBox')) {
                $element.data('tTextBox', new $t.textbox(this, options));
                $t.trigger(this, 'load');
            }
        });
    };

    var commonDefaults = {
        val: null,
        text: '',
        step: 1,
        inputAttributes: '',
        increaseButtonTitle: "Increase value",
        decreaseButtonTitle: "Decrease value",
        showIncreaseButton: true,
        showDecreaseButton: true
    };

    $.fn.tTextBox.defaults = {
        numeric: $.extend(commonDefaults, {
            minValue: -100,
            maxValue: 100
        }),
        currency: $.extend(commonDefaults, {
            minValue: 0,
            maxValue: 1000
        }),
        percent: $.extend(commonDefaults, {
            minValue: 0,
            maxValue: 100
        })
    };

    // * - placeholder for the symbol
    // n - placeholder for the number
    $.fn.tTextBox.patterns = {
        numeric: {
            negative: ['(n)', '-n', '- n', 'n-', 'n -']
        },
        currency: {
            positive: ['*n', 'n*', '* n', 'n *'],
            negative: ['(*n)', '-*n', '*-n', '*n-', '(n*)', '-n*', 'n-*', 'n*-', '-n *', '-* n', 'n *-', '* n-', '* -n', 'n- *', '(* n)', '(n *)']
        },
        percent: {
            positive: ['n *', 'n*', '*n'],
            negative: ['-n *', '-n*', '-*n']
        }
    };

    if (!$t.cultureInfo.numericnegative)
        $.extend($t.cultureInfo, { //default en-US settings
            currencydecimaldigits: 2,
            currencydecimalseparator: '.',
            currencygroupseparator: ',',
            currencygroupsize: 3,
            currencynegative: 0,
            currencypositive: 0,
            currencysymbol: '$',
            numericdecimaldigits: 2,
            numericdecimalseparator: '.',
            numericgroupseparator: ',',
            numericgroupsize: 3,
            numericnegative: 1,
            percentdecimaldigits: 2,
            percentdecimalseparator: '.',
            percentgroupseparator: ',',
            percentgroupsize: 3,
            percentnegative: 0,
            percentpositive: 0,
            percentsymbol: '%'
        });

    var customFormatRegEx = /[0#?]/;

    function reverse(str) {
        return str.split('').reverse().join('');
    }

    function injectInFormat(val, format, appendExtras) {
        var i = 0, j = 0,
            fLength = format.length,
            vLength = val.length,
            builder = new $t.stringBuilder();

        while (i < fLength && j < vLength && format.substring(i).search(customFormatRegEx) >= 0) {

            if (format.charAt(i).match(customFormatRegEx))
                builder.cat(val.charAt(j++));
            else
                builder.cat(format.charAt(i));

            i++;
        }

        builder.catIf(val.substring(j), j < vLength && appendExtras)
               .catIf(format.substring(i), i < fLength);

        var result = reverse(builder.string()),
            zeroIndex;

        if (result.indexOf('#') > -1)
            zeroIndex = result.indexOf('0');

        if (zeroIndex > -1) {
            var first = result.slice(0, zeroIndex),
                second = result.slice(zeroIndex, result.length);
            result = first.replace(/#/g, '') + second.replace(/#/g, '0');
        } else {
            result = result.replace(/#/g, '');
        }

        if (result.indexOf(',') == 0)
            result = result.replace(/,/g, '');

        return appendExtras ? result : reverse(result);
    }

    $t.textbox.formatNumber = function (number,
                                        format,
                                        digits,
                                        separator,
                                        groupSeparator,
                                        groupSize,
                                        positive,
                                        negative,
                                        symbol,
                                        isTextBox) {

        if (!format) return number;

        var type, customFormat, negativeFormat, zeroFormat, sign = number < 0;

        format = format.split(':');
        format = format.length > 1 ? format[1].replace('}', '') : format[0];

        var isCustomFormat = format.search(customFormatRegEx) != -1;

        if (isCustomFormat) {
            format = format.split(';');
            customFormat = format[0];
            negativeFormat = format[1];
            zeroFormat = format[2];
            format = (sign && negativeFormat ? negativeFormat : customFormat).indexOf('%') != -1 ? 'p' : 'n';
        }

        switch (format.toLowerCase()) {
            case 'd':
                return Math.round(number).toString();
            case 'c':
                type = 'currency'; break;
            case 'n':
                type = 'numeric'; break;
            case 'p':
                type = 'percent';
                if (!isTextBox) number = Math.abs(number) * 100;
                break;
            default:
                return number.toString();
        }

        var zeroPad = function (str, count, left) {
            for (var l = str.length; l < count; l++)
                str = left ? ('0' + str) : (str + '0');

            return str;
        }

        var addGroupSeparator = function (number, groupSeperator, groupSize) {
            if (groupSeparator && groupSize != 0) {
                var regExp = new RegExp('(-?[0-9]+)([0-9]{' + groupSize + '})');
                while (regExp.test(number)) {
                    number = number.replace(regExp, '$1' + groupSeperator + '$2');
                }
            }
            return number;
        }

        var cultureInfo = cultureInfo || $t.cultureInfo,
            patterns = $.fn.tTextBox.patterns,
            undefined;

        //define Number Formating info.
        digits = digits || digits === 0 ? digits : cultureInfo[type + 'decimaldigits'];
        separator = separator !== undefined ? separator : cultureInfo[type + 'decimalseparator'];
        groupSeparator = groupSeparator !== undefined ? groupSeparator : cultureInfo[type + 'groupseparator'];
        groupSize = groupSize || groupSize == 0 ? groupSize : cultureInfo[type + 'groupsize'];
        negative = negative || negative === 0 ? negative : cultureInfo[type + 'negative'];
        positive = positive || positive === 0 ? positive : cultureInfo[type + 'positive'];
        symbol = symbol || cultureInfo[type + 'symbol'];

        var exponent, left, right;

        if (isCustomFormat) {
            var splits = (sign && negativeFormat ? negativeFormat : customFormat).split('.'),
                leftF = splits[0],
                rightF = splits.length > 1 ? splits[1] : '',
                lastIndexZero = $t.lastIndexOf(rightF, '0'),
                lastIndexSharp = $t.lastIndexOf(rightF, '#');
            digits = (lastIndexSharp > lastIndexZero ? lastIndexSharp : lastIndexZero) + 1;
        }

        var factor = Math.pow(10, digits);
        var rounded = (Math.round(number * factor) / factor);
        number = isFinite(rounded) ? rounded : number;

        var split = number.toString().split(/e/i);
        exponent = split.length > 1 ? parseInt(split[1]) : 0;
        split = split[0].split('.');

        left = split[0];
        left = sign ? left.replace('-', '') : left;

        right = split.length > 1 ? split[1] : '';

        if (exponent) {
            if (!sign) {
                right = zeroPad(right, exponent, false);
                left += right.slice(0, exponent);
                right = right.substr(exponent);
            } else {
                left = zeroPad(left, exponent + 1, true);
                right = left.slice(exponent, left.length) + right;
                left = left.slice(0, exponent);
            }
        }

        var rightLength = right.length;
        if (digits < 1 || (isCustomFormat && lastIndexZero == -1 && rightLength === 0))
            right = ''
        else
            right = rightLength > digits ? right.slice(0, digits) : zeroPad(right, digits, false);

        var result;
        if (isCustomFormat) {
            if (left == 0) left = '';

            left = injectInFormat(reverse(left), reverse(leftF), true);
            left = leftF.indexOf(',') != -1 ? addGroupSeparator(left, groupSeparator, groupSize) : left;

            right = right && rightF ? injectInFormat(right, rightF) : '';

            result = number === 0 && zeroFormat ? zeroFormat
                : (sign && !negativeFormat ? '-' : '') + left + (right.length > 0 ? separator + right : '');

        } else {

            left = addGroupSeparator(left, groupSeparator, groupSize)
            patterns = patterns[type];
            var pattern = sign ? patterns['negative'][negative]
                        : symbol ? patterns['positive'][positive]
                        : null;

            var numberString = left + (right.length > 0 ? separator + right : '');

            result = pattern ? pattern.replace('n', numberString).replace('*', symbol) : numberString;
        }
        return result;
    }

    $.extend($t.formatters, {
        number: $t.textbox.formatNumber
    });
})(jQuery);