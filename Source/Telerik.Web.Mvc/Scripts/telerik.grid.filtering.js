(function($) {
    var $t = $.telerik;
    var escapeQuoteRegExp = /'/ig;
    var filterFunctions = ['startswith', 'substringof', 'endswith'];
    var formatRegExp = /\{0(:([^\}]+))?\}/;

    var filterCommandsByType = {
        'String': {
            'Is equal to': 'eq',
            'Is not equal to': 'ne',
            'Starts with': 'startswith',
            'Contains': 'substringof',
            'Ends with': 'endswith'
        },
        'Number': {
            'Is equal to': 'eq',
            'Is not equal to': 'ne',
            'Is less than': 'lt',
            'Is less than or equal to': 'le',
            'Is greater than': 'gt',
            'Is greater than or equal to': 'ge'
        },
        'Date': {
            'Is equal to': 'eq',
            'Is not equal to': 'ne',
            'Is before': 'lt',
            'Is before or equal to': 'le',
            'Is after': 'gt',
            'Is after or equal to': 'ge'
        }
    };

    var fx = [$t.fx.slide.defaults()];

    $t.gridFilter = {};

    /* Here `this` is the Grid instance*/

    $t.gridFilter.implementation = {
        hideFilter: function(filterCallback) {
            filterCallback = filterCallback || function() { return true; };
            $('.t-filter-options', this.element)
                .filter(filterCallback)
                .each(function() {
                    $t.fx.rewind(fx, $(this), { direction: 'bottom' });
                });
        },

        showFilter: function(e, element) {
            e.stopPropagation();
            this.hideFilter(function() {
                return this.parentNode != element;
            });
            
            var $element = $(element);
            var $filterMenu = $element.data('filter');

            if (!$filterMenu) {
                var filterMenuHtml = new $t.stringBuilder();
                var column = this.columns[$element.parent()[0].cellIndex];
                var filters = filterCommandsByType[column.type];

                var appendFilteringUi =
					function(stringBuilder) {

					    stringBuilder.append('<select>');

					    for (var filter in filters)
					        stringBuilder
								.append('<option value="')
								.append(filters[filter])
								.append('">')
								.append(filter)
								.append('</option>');

					    stringBuilder
							.append('</select>')
							.append('<input type="text" />');
					};

                filterMenuHtml
					.append('<div class="t-animation-container"><div class="t-filter-options t-group" style="display:none">')
						.append('<button class="t-button t-state-default t-clear-button"><span class="t-icon t-clear-filter"></span>Clear Filter</button>')

						.append('<label>Show rows with value that</label>');

                if (column.type != 'Boolean') {
                    appendFilteringUi(filterMenuHtml);
                    filterMenuHtml.append('<label>And</label>');
                    appendFilteringUi(filterMenuHtml);
                } else {
                    var id = $(this.element).attr('id') + column.name;
                    var appendBooleanUI = function(value) {
                        filterMenuHtml
							.append('<div><input type="radio" style="width:auto;display:inline" id="')
							.append(id + value)
							.append('" name="').append(id)
							.append('" value="').append(value)
							.append('" />')
							.append('<label style="display:inline" for="').append(id + value)
							.append('">Is ')
							.append(value)
							.append('</label></div>');
                    };
                    appendBooleanUI(true);
                    appendBooleanUI(false);
                }
                filterMenuHtml
						.append('<button class="t-button t-state-default t-filter-button"><span class="t-icon t-filter"></span>Filter</button>')
					.append('</div></div>');

                $filterMenu = $(filterMenuHtml.string()).appendTo(this.element)
                    .click(function(e) {
                        e.stopPropagation();
                    });
                
                $filterMenu.data('column', column);
                
                $element.data('filter', $filterMenu);
                
                $filterMenu.find('.t-filter-button').click($t.delegate(this, this.filterClick));
                $filterMenu.find('.t-clear-button').click($t.delegate(this, this.clearClick));
                $filterMenu.find('input[type=text]').keyup($t.delegate(this, this.filterKeyUp));
            }
            
            var width = - this.$headerWrap.scrollLeft() - 1;
            var parent = $element.parent()[0];
            
            $element.parent().parent().find('.t-header').each(function() {
                width += $(this).outerWidth();
                if (this == parent)
                    return false;
            });

            $filterMenu.css({ left: width - $element.outerWidth() + "px", top: $element.outerHeight() + "px" });

            $t.fx[$filterMenu.find('.t-group').is(":visible") ? 'rewind' : 'play'](fx, $filterMenu.find('.t-group'), { direction: 'bottom' });
        },

        getColumn: function($element) {
            return $element.closest('.t-animation-container').data('column');
        },

        clearClick: function(e, element) {
            var $element = $(element);
            var column = this.getColumn($element);

            column.filters = [];

            $element.siblings('input[type=text]')
                .val('')
                .removeClass('t-state-error');

            this.filter(this.filterExpr());
        },

        filterClick: function(e, element) {
            var $element = $(element);
            var column = this.getColumn($(element));
            column.filters = [];
            var hasErrors = false;

            $element.parent().children('input[type=text]').each($t.bind(this, function(index, input) {
                var $input = $(input);
                var value = $.trim($input.val());

                if (!value) {
                    $input.removeClass('t-state-error');
                    return true;
                }

                var valid = this.isValidFilterValue(column, value);

                $input.toggleClass('t-state-error', !valid);

                if (!valid) {
                    hasErrors = true;
                    return true;
                }

                var operator = $input.prev('select').val()
                column.filters.push({ operator: operator, value: value });
            }));
            
            $element.parent().find('input:checked').each($t.bind(this, function(index, input) {
                var $input = $(input);
                var value = $(input).attr('value');
                column.filters.push({ operator: 'eq', value: value });
            }));

            if (!hasErrors) {
                this.filter(this.filterExpr());
                this.hideFilter();
            }
        },

        filterKeyUp: function(e, element) {
            if (e.keyCode != 13)
                return;
            this.filterClick(e, element);
        },

        filterDocumentClick: function(e, element) {
            if (e.which == 3)
                return;

            this.hideFilter();
        },

        isValidFilterValue: function(column, value) {
            if (column.type == 'Date') {
                return !isNaN(Date.parse(value));
            } else if (column.type == 'Number') {
                return !isNaN(value);
            }

            return true;
        },

        parseDate: function(column, value) {
            return new Date(Date.parse(value));
        },

        getFormat: function(column) {
            var match = formatRegExp.exec(column.format);
            return match ? match[2] : $t.cultureInfo.shortDate;
        },

        encodeFilterValue: function(columnIndex, value) {
            var column = this.columns[columnIndex];
            switch (column.type) {
                case 'String':
                    return "'" + value.replace(escapeQuoteRegExp, "''") + "'";
                case 'Date':
                    var date = this.parseDate(column, value);
                    return "datetime'" + $t.formatString('{0:yyyy-MM-ddThh-mm-ss}', date) + "'";
            }

            return value;
        },

        filterExpr: function() {
            var result = [];
            for (var columnIndex = 0, length = this.columns.length; columnIndex < length; columnIndex++) {
                var column = this.columns[columnIndex];

                if (!column.filters)
                    continue;

                for (var filterIndex = 0; filterIndex < column.filters.length; filterIndex++) {
                    var filter = column.filters[filterIndex];
                    var expr = new $t.stringBuilder();
                    if ($.inArray(filter.operator, filterFunctions) > -1) {
                        expr.append(filter.operator)
                        .append('(')
                        .append(column.name)
                        .append(',')
                        .append(this.encodeFilterValue(columnIndex, filter.value))
                        .append(')');
                    } else {
                        expr.append(column.name)
                        .append('~')
                        .append(filter.operator)
                        .append('~')
                        .append(this.encodeFilterValue(columnIndex, filter.value));
                    }
                    result.push(expr.string());
                }
            }

            return result.join('~and~');
        },

        filter: function(filterExpression) {
            this.currentPage = 1;
            this.filterExpression = filterExpression;
            if (!this.isAjax())
                location.href = $t.formatString(unescape(this.urlFormat), 
                    this.currentPage, this.orderBy || '~', escape(this.filterExpression) || '');
                            
            this.ajaxRequest();
        }
    };

    $.extend($t.gridFilter, {
        initialize: function(grid) {
            $.extend(grid, $t.gridFilter.implementation);
            
            $('.t-grid-content', grid.element).bind('scroll', function() {
                grid.hideFilter();
            });
            
            $(document).click($t.delegate(grid, grid.filterDocumentClick));
            $('.t-grid-filter', grid.element)
				.click($t.delegate(grid, grid.showFilter))
				.live('mouseenter', $t.hover)
				.live('mouseleave', $t.leave);
				
			$('.t-filter-options .t-button', grid.element)
				.live('mouseenter', $t.buttonHover)
				.live('mouseleave', $t.buttonLeave);
        }
    });
})(jQuery);