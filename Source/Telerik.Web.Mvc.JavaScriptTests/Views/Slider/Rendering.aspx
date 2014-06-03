<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <input id="slider" />
    <input id="slider1" />
    <input id="slider2" />
    <input id="slider3" />

    <% Html.Telerik().ScriptRegistrar().DefaultGroup(group => group
           .Add("telerik.common.js")
           .Add("telerik.draganddrop.js")
           .Add("telerik.slider.js"));
    %>
</asp:Content>

<asp:Content ContentPlaceHolderID="TestContent" runat="server">
    <script type="text/javascript">

        test("horizontal slider should apply max value", function () {
            var slider = $("#slider").tSlider({ val: 10, minValue: 0, maxValue: 10 }).data("tSlider");

            var trackDivWidth = slider.wrapper.find(".t-slider-track").width();
            var selectionDivWidth = slider.wrapper.find(".t-slider-selection").width();

            equal(trackDivWidth, selectionDivWidth);
        });

        test("height slider should apply max value", function () {
            var slider = $("#slider1").tSlider({ val: 10, minValue: 0, maxValue: 10, orientation: "vertical" }).data("tSlider");

            var trackDivHeight = slider.wrapper.find(".t-slider-track").height();
            var selectionDivHeight = slider.wrapper.find(".t-slider-selection").height();

            equal(trackDivHeight, selectionDivHeight);
        });

        test("height slider should apply max value", function () {
            var slider = $("#slider1").tSlider({ val: 10, minValue: 0, maxValue: 10, orientation: "vertical" }).data("tSlider");
            
            var trackDivHeight = slider.wrapper.find(".t-slider-track").height();
            var selectionDivHeight = slider.wrapper.find(".t-slider-selection").height();

            equal(trackDivHeight, selectionDivHeight);
        });

        test("height slider should apply max value", function () {
            var slider = $("#slider1").tSlider({ val: 10, minValue: 0, maxValue: 10, orientation: "vertical" }).data("tSlider");

            var trackDivHeight = slider.wrapper.find(".t-slider-track").height();
            var selectionDivHeight = slider.wrapper.find(".t-slider-selection").height();

            equal(trackDivHeight, selectionDivHeight);
        });

        test("slider should apply style", function () {
            var style = "width: 200px; height: 30px;";

            var slider = $("#slider2").tSlider({ val: 10, minValue: 0, maxValue: 10, orientation: "horizontal", style: style }).data("tSlider");

            equal("200px", slider.wrapper.css("width"));
            equal("30px", slider.wrapper.css("height"));
        });

        test("slider should apply option value to the input", function() {
            var value = 10;
            
            var slider = $("#slider1").tSlider({ val: value, minValue: 0, maxValue: 10, orientation: "horizontal"});

            equal($(slider).val(), value);
        });

        test("getValueFromPosition should increase value", function () {
            var slider = $("#slider3").tSlider({ val: 1, minValue: 0, maxValue: 10, orientation: "horizontal", smallStep: 2 }).data("tSlider");

            var dragableArea = $t.slider.getDragableArea(slider.trackDiv, slider.maxSelection, slider.orientation);

            var step = 2 * (144 / 10);

            equal($t.slider.getValueFromPosition(dragableArea.startPoint + step, dragableArea, slider), 2);
        });

    </script>
</asp:Content>