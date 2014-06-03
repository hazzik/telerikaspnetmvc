(function($) {
    var $t = $.telerik;
    var dateRegExp = /"\\\/Date\((.*?)\)\\\/"/g;

    $.extend($t, {

        grid: function(element, options) {
            this.element = element;
            this.sorted = [];

            $.extend(this, options);

            $('.t-pager .t-state-disabled', this.element)
                .live('click', $t.preventDefault);

            $('.t-pager .t-link:not(.t-state-disabled)', this.element)
                .live('mouseenter', $t.hover)
                .live('mouseleave', $t.leave);

            $('.t-pager input[type=text]', this.element)
                .live('keydown', $t.delegate(this, this.pagerKeyDown));

            if (this.sort) {
                $('.t-header .t-link', this.element)
					.live('mouseenter', $t.hover)
					.live('mouseleave', $t.leave);
            }

            for (var i = 0; i < this.plugins.length; i++)
                $t[this.plugins[i]].initialize(this);

            var headerWrap = this.$headerWrap = $('.t-grid-header-wrap', this.element);

            var $content = $('.t-grid-content', this.element)
                .bind('scroll', function() {
                    headerWrap.scrollLeft(this.scrollLeft);
                });

            this.$tbody = $('.t-grid-content table tbody', this.element);

            if (!this.$tbody.length)
                this.$tbody = $('table tbody', this.element);

            $('.t-refresh', this.element)
                .live('click', $t.delegate(this, this.refreshClick));

            if (this.onError)
                $(element).bind('error.Grid', this.onError);

            if (this.onDataBinding)
                $(element).bind('dataBinding.Grid', this.onDataBinding);

            if (this.onRowDataBound)
                $(element).bind('rowDataBound.Grid', this.onRowDataBound);

            if (this.isAjax()) {
                $('.t-pager .t-link:not(.t-state-disabled)', this.element)
                    .live('click', $t.delegate(this, this.pagerClick));

                if (this.sort)
                    $('.t-header > .t-link', this.element)
					    .live('click', $t.delegate(this, this.headerClick));
            }
            
            if (this.onLoad)
                $(this.element).bind('load.Grid', this, this.onLoad).trigger('load.Grid');
        },

        formatString: function() {
            var s = arguments[0];

            for (var i = 0, l = arguments.length - 1; i < l; i++) {
                var reg = new RegExp("\\{" + i + "(:([^\\}]+))?\\}", "gm");

                var argument = arguments[i + 1];

                var formatter = this.formatters[getType(argument)];
                if (formatter) {
                    var match = reg.exec(s);
                    argument = formatter(argument, match[2]);
                }
                s = s.replace(reg, argument);
            }
            return s;
        },

        formatters: {
            date: formatDate
        },

        cultureInfo: {
            longDate: "dddd, MMMM dd, yyyy",
            shortDate: "M/d/yyyy",
            fullDateTime: "dddd, MMMM dd, yyyy h:mm:ss tt",
            generalDateShortTime: "M/d/yyyy h:mm tt",
            generalDateTime: "M/d/yyyy h:mm:ss tt",
            sortableDateTime: "yyyy'-'MM'-'ddTHH':'mm':'ss",
            universalSortableDateTime: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
            monthYear: "MMMM, yyyy",
            monthDay: "MMMM dd",
            days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            abbrDays: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
            abbrMonths: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            months: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            am: "AM",
            pm: "PM",
            dateSeparator: "/",
            timeSeparator: ":"
        }
    });

    // Returns the type as a string. Not full. Used in string formatting
    function getType(obj) {
        if (obj instanceof Date)
            return 'date';
        return 'object';
    }

    function pad(value) {
        if (value < 10)
            return '0' + value;
        return value;
    }

    var dateFormatters = {
        /* Day formatting */
        d: function(date) { return date.getDate(); },
        dd: function(date) { return pad(this.d(date)); },
        ddd: function(date) { return $t.cultureInfo.abbrDays[date.getDay()]; },
        dddd: function(date) { return $t.cultureInfo.days[date.getDay()]; },

        /* Month formatting */
        M: function(date) { return date.getMonth() + 1; },
        MM: function(date) { return pad(this.M(date)); },
        MMM: function(date) { return $t.cultureInfo.abbrMonths[date.getMonth()]; },
        MMMM: function(date) { return $t.cultureInfo.months[date.getMonth()]; },

        /* Year formatting */
        yy: function(date) { return pad(this.yyyy(date) % 100); },
        yyyy: function(date) { return date.getFullYear(); },

        /* Hour formatting */
        h: function(date) {
            var hour = this.H(date) % 12;
            if (!hour) hour = 12;
            return hour;
        },
        hh: function(date) { return pad(this.h(date)); },
        H: function(date) { return date.getHours(); },
        HH: function(date) { return pad(this.H(date)); },

        /* Minute formatting */
        m: function(date) { return date.getMinutes(); },
        mm: function(date) { return pad(this.m(date)); },

        /* Second formatting*/
        s: function(date) { return date.getSeconds(); },
        ss: function(date) { return pad(this.s(date)); },
        /* Millisecond formatting */
        f: function(date) { return Math.floor(this.fff(date) / 100); },
        ff: function(date) { return Math.floor(this.fff(date) / 10); },
        fff: function(date) { return date.getMilliseconds(); },
        /* AM-PM */
        tt: function(date) { return date.getHours() < 12 ? $t.cultureInfo.am : $t.cultureInfo.pm; }
    };

    function processLiteral(literal, result) {
        var quotedOrEscaped = 0;
        var escaped = false;
        for (var i = 0, l = literal.length; i < l; i++) {
            var c = literal.charAt(i);
            switch (c) {
                case '\\':
                    if (escaped)
                        result.append("\\");
                    quotedOrEscaped++;
                    escaped = !escaped;
                    break;
                case '\"':
                    if (escaped)
                        result.append("\"");
                    else
                        quotedOrEscaped++;
                    escaped = false;
                case '\'':
                    if (escaped)
                        result.append("'");
                    else
                        quotedOrEscaped++;
                    escaped = false;
                    break;
                default:
                    result.append(c);
                    escaped = false;
                    break;
            }
        }
        return quotedOrEscaped;
    }

    function formatDate(date, format) {

        var l = $t.cultureInfo;

        var standardFormats = {
            d: l.shortDate,
            D: l.longDate,
            F: l.fullDateTime,
            g: l.generalDateShortTime,
            G: l.generalDateTime,
            m: l.monthDay,
            M: l.monthDay,
            s: l.sortableDateTime,
            u: l.universalSortableDateTime,
            y: l.monthYear,
            Y: l.monthYear
        };

        format = format ? format : "G";
        format = standardFormats[format] ? standardFormats[format] : format;

        var dateRegExp = /dddd|ddd|dd|d|fff|ff|f|hh|h|HH|H|mm|m|MMMM|MMM|MM|M|yyyy|yy|ss|s|tt/g;

        var match;
        var result = new $t.stringBuilder();
        var quotedOrEscaped = 0;
        do {
            var index = dateRegExp.lastIndex;
            match = dateRegExp.exec(format);
            var literal = format.substring(index, match ? match.index : format.length);

            if (literal == '/')
                literal = $t.cultureInfo.dateSeparator;
            if (literal == ':')
                literal = $t.cultureInfo.timeSeparator;

            quotedOrEscaped += processLiteral(literal, result);

            if (match && (quotedOrEscaped % 2) == 1) {
                result.append(match[0]);
                continue;
            }

            if (match)
                result.append(dateFormatters[match[0]](date));

        } while (match);

        return result.string();
    }

    function simpleEvaluate(key) {
        return function(data) {
            return data[key];
        }
    }

    function nestedEvaluate(fields) {
        return function(data) {
            var result;
            for (var i = 0, l = fields.length; i < l; i++) {
                result = data[fields[i]];
                data = result;
            }
            return result;
        }
    }

    $.extend($t.grid.prototype, {
        headerClick: function(e, element) {
            e.preventDefault();
            this.sort(element.parentNode.cellIndex);
        },

        refreshClick: function(e, element) {
            e.preventDefault();
            if ($(element).is('.t-loading'))
                return;
            if (this.isAjax())
                this.ajaxRequest(true);
            else
                location.reload();
        },

        sort: function(columnIndex) {
            this.orderBy = this.sortExpr(columnIndex);
            this.ajaxRequest();
        },

        sortExpr: function(columnIndex) {
            var column = this.columns[columnIndex];

            var direction = 'asc';

            if (column.sortDirection == 'asc')
                direction = 'desc';
            else if (column.sortDirection == 'desc')
                direction = null;

            column.sortDirection = direction;

            var sortedIndex = $.inArray(column, this.sorted);

            if (this.sortMode == 'single' && sortedIndex < 0) {
                for (var i = 0; i < this.sorted.length; i++)
                    this.sorted[i].sortDirection = null;

                this.sorted = [];
            }
            if (sortedIndex < 0 && direction)
                this.sorted.push(column);

            if (!direction)
                this.sorted.splice(sortedIndex, 1);

            var expr = [];
            for (var i = 0, length = this.sorted.length; i < length; i++)
                expr.push(this.sorted[i].name + "-" + this.sorted[i].sortDirection);

            return expr.join("~");
        },

        pagerKeyDown: function(e, element) {
            if (e.keyCode == 13) {
                var page = this.sanitizePage($(element).val());
                if (page != this.currentPage)
                    this.pageTo(page);
                else
                    $(element).val(page);
            }
        },

        isAjax: function() {
            return this.ajaxUrl || this.onDataBinding;
        },

        pagerClick: function(e, element) {
            e.preventDefault();

            var page = this.currentPage;
            var pagerButton = $(element).find('.t-icon');

            if (pagerButton.hasClass('t-arrow-next'))
                page++;
            else if (pagerButton.hasClass('t-arrow-last'))
                page = this.totalPages();
            else if (pagerButton.hasClass('t-arrow-prev'))
                page--;
            else if (pagerButton.hasClass('t-arrow-first'))
                page = 1;
            else {
                var linkText = $(element).text();

                if (linkText == '...') {
                    var elementIndex = $(element).parent().children().index(element);

                    if (elementIndex == 0) {
                        page = parseInt($(element).next().text(), 10) - 1;
                    } else {
                        page = parseInt($(element).prev().text(), 10) + 1;
                    }

                } else {
                    page = parseInt(linkText, 10);
                }
            }

            this.pageTo(page);
        },

        pageTo: function(page) {
            this.currentPage = page;
            this.ajaxRequest();
        },

        ajaxRequest: function(forceLoading) {
            if ($t.trigger(this.element, 'dataBinding.Grid', {
                page: this.currentPage,
                sortedColumns: this.sorted, 
                filteredColumns: $.grep(this.columns, function(column) {
                        return column.filters;
                    })
                }))
                return;

            if (!this.ajaxUrl)
                return;

            var data = {};

            data[this.queryString.page] = this.currentPage;
            data[this.queryString.size] = this.pageSize;
            data[this.queryString.orderBy] = this.orderBy || "";
            data[this.queryString.filter] = (this.filterExpression || "").replace(/\"/g, '\\"');

            var statusIcon = $('.t-status .t-icon', this.element);
            var showLoading = function() {
                statusIcon.addClass('t-loading');
            };

            if (forceLoading)
                showLoading();

            var loadingIconTimeout = setTimeout(showLoading, 100);

            var options = {
                type: 'POST',
                url: this.ajaxUrl,
                dataType: 'json',
                dataFilter: function(data, dataType) {
                    return data.replace(dateRegExp, 'new Date($1)');
                },
                error: $t.bind(this, function(xhr, status) {
                    if ($t.ajaxError(this.element, 'error.Grid', xhr, status))
                        return;

                    if (status == 'parsererror')
                        alert('Error! The requested URL "' + this.ajaxUrl + '" did not return JSON.');
                    if (status == 'error')
                        alert('Error! The requested URL "' + this.ajaxUrl + '" returned ' + xhr.status + ' - ' + xhr.statusText);
                    if (status == 'timeout')
                        alert('Error! Server timeout.');
                }),
                complete: function() {
                    clearTimeout(loadingIconTimeout);
                    statusIcon.removeClass('t-loading');
                },

                success: $t.bind(this, function(data) {
                    data = data.d || data; //Support the `d` returned by MS Web Services 
                    this.total = data.total || data.Total;
                    this.dataBind(data.data || data.Data);
                })
            };

            if (this.ws) {
                var json = [];

                for (var key in data)
                    json.push($t.formatString('"{0}":"{1}"', key, data[key]));
                options.data = '{' + json.join(',') + '}';
                options.contentType = 'application/json; charset=utf-8';
            } else {
                options.data = data;
            }

            $.ajax(options);
        },

        createMapping: function(column, evaluate) {
            if (column.format || column.type == "Date")
                return function(value) {
                    return $t.formatString(column.format || '{0:G}', evaluate(value));
                }

            return evaluate;
        },

        createColumnMappings: function(data) {
            for (var key in data) {
                for (var i = 0, l = this.columns.length; i < l; i++) {
                    var column = this.columns[i];

                    if (!column.name)
                        continue;
                    
                    if (column.name == key)
                        column.mapping = this.createMapping(column, simpleEvaluate(key));
                    else if (column.name.indexOf(key) > -1)
                        column.mapping = this.createMapping(column, nestedEvaluate(column.name.split('.')));
                }
            }
        },

        bindTo: function(data) {
            this.$tbody.find('tr.t-no-data').remove();

            this.createColumnMappings(data[0]);

            var rows = this.$tbody[0].rows;
            var rowLength = rows.length;
            var dataLength = Math.min(this.pageSize, data.length);

            if (this.pageSize == 0)
                dataLength = data.length;

            if (rowLength < dataLength) {
                var rowHtml = new $t.stringBuilder();

                for (var rowIndex = rowLength; rowIndex < dataLength; rowIndex++) {
                    if (rowIndex % 2 == 1)
                        rowHtml.append('<tr class="t-alt">');
                    else
                        rowHtml.append('<tr>');

                    for (var cellIndex = 0; cellIndex < this.columns.length; cellIndex++)
                        rowHtml.append('<td/>');

                    rowHtml.append('</tr>');
                }

                $(rowHtml.string()).appendTo(this.$tbody);
            }

            for (var rowIndex = 0; rowIndex < dataLength; rowIndex++) {
                var row = rows[rowIndex];
                for (var i = 0, l = this.columns.length; i < l; i++) {
                    var evaluate = this.columns[i].mapping;
                    if (evaluate)
                        row.cells[i].innerHTML = evaluate(data[rowIndex]);
                }
                
                if (this.onRowDataBound)
                    $t.trigger(this.element, 'rowDataBound.Grid', {row:row, dataItem:data[rowIndex]});
            }

            for (; rowIndex < rowLength; rowIndex++)
                $(rows[rows.length - 1]).remove();
        },

        updatePager: function(total) {
            var totalPages = this.totalPages(total);
            var currentPage = this.currentPage;
            var $pager = $('.t-pager', this.element);

            // nextPrevious
            // work-around for weird issue in IE, when using comma-based selector
            $pager.find('.t-arrow-next').parent().add($pager.find('.t-arrow-last').parent())
	            .toggleClass('t-state-disabled', currentPage >= totalPages)
	            .removeClass('t-state-hover');

            $pager.find('.t-arrow-prev').parent().add($pager.find('.t-arrow-first').parent())
	            .toggleClass('t-state-disabled', currentPage == 1)
	            .removeClass('t-state-hover');

            // pageInput
            $pager.find('.t-page-i-of-n').each(function() {
                this.innerHTML = $t.formatString('Page <input type="text" value="{0}" /> of {1}', currentPage, totalPages);
            });

            // numeric
            $pager.find('.t-numeric').each($t.bind(this, function(index, element) {
                this.numericPager(element, currentPage, totalPages);    
            }));
            
            // status
            $('.t-status-text', this.element)
                .text($t.formatString('Displaying items {0} - {1} of {2}',
                    this.firstItemInPage(),
	                this.lastItemInPage(),
	                this.total));
        },
        
        numericPager: function(pagerElement, currentPage, totalPages) {
            var numericLinkSize = 10;
            var numericStart = 1;

            if (currentPage > numericLinkSize) {
                var reminder = (currentPage % numericLinkSize);

                numericStart = (reminder == 0) ? (currentPage - numericLinkSize) + 1 : (currentPage - reminder) + 1;
            }

            var numericEnd = (numericStart + numericLinkSize) - 1;

            numericEnd = Math.min(numericEnd, totalPages);

            var pagerHtml = new $t.stringBuilder();
            if (numericStart > 1)
                pagerHtml.append('<a class="t-link">...</a>');

            for (var page = numericStart; page <= numericEnd; page++) {
                if (page == currentPage) {
                    pagerHtml.append('<span class="t-state-active">')
                        .append(page)
                        .append('</span>');
                } else {
                    pagerHtml.append('<a class="t-link">')
	                .append(page)
	                .append('</a>');
                }
            }

            if (numericEnd < totalPages)
                pagerHtml.append('<a class="t-link">...</a>');

            pagerElement.innerHTML = pagerHtml.string();
        },

        updateSorting: function() {
            $('.t-header', this.element).each($t.bind(this, function(i, header) {
                var direction = this.columns[i].sortDirection;
                var $link = $(header).children('.t-link');
                var $icon = $link.children('.t-icon');

                if (!direction) {
                    $icon.hide();
                } else {
                    if ($icon.length == 0)
                        $icon = $('<span class="t-icon"/>').appendTo($link);

                    $icon.toggleClass('t-arrow-up', direction == 'asc')
                        .toggleClass('t-arrow-down', direction == 'desc')
                        .show();
                }
            }));
        },

        sanitizePage: function(value) {
            var result = parseInt(value, 10);
            if (isNaN(result) || result < 1)
                return 1
            return Math.min(result, this.totalPages());
        },

        totalPages: function() {
            return Math.ceil(this.total / this.pageSize);
        },

        firstItemInPage: function() {
            return (this.currentPage - 1) * this.pageSize + 1;
        },

        lastItemInPage: function() {
            return Math.min(this.currentPage * this.pageSize, this.total);
        },

        dataBind: function(data) {
            this.bindTo(data);
            this.updatePager();
            this.updateSorting();
        },
        
        rebind: function (args) {
            this.sorted = [];
            this.currentPage = 1;
            
            $.each(this.columns, function() {
                this.sortDirection = null;
                this.filters = [];
            });
            
            for (var key in args) {
                var regExp = new RegExp($t.formatString('({0})=([^&]*)', key), "g");
                this.ajaxUrl = this.ajaxUrl.replace(regExp, '$1=' + args[key]);
            }
            
            this.ajaxRequest();
        }
    });

    $.fn.tGrid = function(options) {
        options = $.extend({}, $.fn.tGrid.defaults, options);

        return this.each(function() {
            options = $.meta ? $.extend({}, options, $(this).data()) : options;

            if (!$(this).data('tGrid')) {
                var grid = new $t.grid(this, options);

                $(this).data('tGrid', grid);

                if (grid.$tbody.find('tr.t-no-data').length)
                    grid.ajaxRequest();
            }
        });
    }

    // default options

    $.fn.tGrid.defaults = {
        columns: [],
        plugins: [],
        currentPage: 1,
        pageSize: 10,
        queryString: {
            page: 'page',
            size: 'size',
            orderBy: 'orderBy',
            filter: 'filter'
        }
    };
})(jQuery);