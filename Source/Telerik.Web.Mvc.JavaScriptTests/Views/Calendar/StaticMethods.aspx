﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.Telerik().ScriptRegistrar().DefaultGroup(group => group.Add("telerik.common.js").Add("telerik.calendar.js")); %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">

    var $t;
    module("Calendar / StaticMethods", {
        setup: function () {
            $t = $.telerik;
        }
    });

    test("formatUrl should replace {0} with date formatted to general date format", function () {
        var formatUrl = '/test?date="{0}"';
        var date = new $t.datetime(2000, 10, 10);

        var expectedFormattedDate = $t.datetime.format(date.toDate(), $t.cultureInfo.generalDateTime);

        var result = $t.calendar.formatUrl(formatUrl, date);

        ok(result.indexOf(expectedFormattedDate));
    });

    test("formatUrl should format date with {0:yyyy-MM-dd}", function () {
        var formatUrl = '/test/{0:yyyy-MM-dd}?test="fake"';
        var date = new $t.datetime(2000, 10, 10);

        var expectedUrl = "/test/" + $t.datetime.format(date.toDate(), "yyyy-MM-dd") + '?test="fake"';
        
        var result = $t.calendar.formatUrl(formatUrl, date);

        equal(result, expectedUrl);
    });

    test("formatUrl should format date with {0:yyyy-MM-dd hh:mm}", function () {
        var formatUrl = '/test?date={0:yyyy-MM-dd hh:mm}&test="fake"';
        var date = new $t.datetime(2000, 10, 10);

        var expectedUrl = "/test?date=" + $t.datetime.format(date.toDate(), "yyyy-MM-dd hh:mm") + '&test="fake"';
        var result = $t.calendar.formatUrl(formatUrl, date);

        equal(result, expectedUrl);
    });

    test("formatUrl should return URL if no {0}", function () {
        var formatUrl = '/test?test="fake"';
        var date = new $t.datetime(2000, 10, 10);

        var result = $t.calendar.formatUrl(formatUrl, date);

        equal(result, formatUrl);
    });

</script>

</asp:Content>