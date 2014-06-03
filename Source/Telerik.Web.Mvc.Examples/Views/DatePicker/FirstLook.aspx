<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FirstLookModelView>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

    <h3>DatePicker</h3>
    <%= Html.Telerik().DatePicker()
            .Name("DatePicker")
            .MinDate(Model.DatePickerAttributes.MinDate.Value)
            .MaxDate(Model.DatePickerAttributes.MaxDate.Value)
            .ShowButton(Model.DatePickerAttributes.ShowButton.Value)
            .Value(Model.DatePickerAttributes.SelectedDate.Value)
    %>
    
    <% using (Html.Configurator("The date picker should...")
                  .PostTo("FirstLook", "DatePicker")
                  .Begin())
       { %>
	    <ul>
		    <li>
                <%= Html.CheckBox("DatePickerAttributes.ShowButton", Model.DatePickerAttributes.ShowButton.Value)%>
                <label for="DatePickerAttributes_ShowButton">show a popup button</label>
		    </li>
		    <li>
			    <label for="DatePickerAttributes_MinDate-input">show dates between</label>
                <%= Html.Telerik().DatePicker()
                        .Name("DatePickerAttributes.MinDate")
                        .MinDate(new DateTime(999, 1, 1))
                        .Value(Model.DatePickerAttributes.MinDate.Value)
                %>
			    <label for="DatePickerAttributes_MaxDate-input">and</label>
                <%= Html.Telerik().DatePicker()
                        .Name("DatePickerAttributes.MaxDate")
                        .MaxDate(new DateTime(2999, 12, 31))
                        .Value(Model.DatePickerAttributes.MaxDate.Value)
                %>
		    </li>
		    <li>
			    <label for="DatePickerAttributes_SelectedDate-input">have pre-selected</label>
                <%= Html.Telerik().DatePicker()
                        .Name("DatePickerAttributes.SelectedDate")
                        .MinDate(Model.DatePickerAttributes.MinDate.Value)
                        .MaxDate(Model.DatePickerAttributes.MaxDate.Value)
                        .Value(Model.DatePickerAttributes.SelectedDate.Value)
                %>
		    </li>
	    </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

    <h3>TimePicker</h3>
    <%= Html.Telerik().TimePicker()
            .Name("TimePicker")
            .Min(Model.TimePickerAttributes.MinTime.Value)
            .Max(Model.TimePickerAttributes.MaxTime.Value)
            .ShowButton(Model.TimePickerAttributes.ShowButton.Value)
            .Interval(Model.TimePickerAttributes.Interval.Value)
            .Value(Model.TimePickerAttributes.SelectedDate.Value)
    %>

    <% using (Html.Configurator("The time picker should...")
                  .PostTo("FirstLook", "DatePicker")
                  .Begin())
       { %>
	    <ul>
		    <li>
                <%= Html.CheckBox("TimePickerAttributes.ShowButton", Model.TimePickerAttributes.ShowButton.Value)%>
                <label for="TimePickerAttributes_ShowButton">show a popup button</label>
		    </li>
		    <li>
			    <label for="TimePickerAttributes_MinTime-input">show time between</label>
                <%= Html.Telerik().TimePicker()
                        .Name("TimePickerAttributes.MinTime")
                        .Value(Model.TimePickerAttributes.MinTime.Value)
                %>
			    <label for="TimePickerAttributes_MaxTime-input">and</label>
                <%= Html.Telerik().TimePicker()
                        .Name("TimePickerAttributes.MaxTime")
                        .Value(Model.TimePickerAttributes.MaxTime.Value)
                %>
		    </li>
		    <li>
			    <label for="TimePickerAttributes_SelectedDate-input">have pre-selected</label>
                <%= Html.Telerik().TimePicker()
                        .Name("TimePickerAttributes.SelectedDate")
                        .Min(Model.TimePickerAttributes.MinTime.Value)
                        .Max(Model.TimePickerAttributes.MaxTime.Value)
                        .Value(Model.TimePickerAttributes.SelectedDate.Value)
                %>
		    </li>
            <li>
			    <label for="TimePickerAttributes_Interval-input">leave a </label>
                <%=  Html.Telerik().IntegerTextBox()
                         .Name("TimePickerAttributes.Interval")
                         .MinValue(0)
                         .MaxValue(60)
                         .Value(Model.TimePickerAttributes.Interval.Value)
                %> minute interval between values
		    </li>
	    </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

    <h3>DateTimePicker</h3>
    <%= Html.Telerik().DateTimePicker()
            .Name("DateTimePicker")
            .Min(Model.DateTimePickerAttributes.MinDate.Value)
            .Max(Model.DateTimePickerAttributes.MaxDate.Value)
            .Interval(Model.DateTimePickerAttributes.Interval.Value)
            .Value(Model.DateTimePickerAttributes.SelectedDate.Value)
    %>

    <% using (Html.Configurator("The date time picker should...")
                  .PostTo("FirstLook", "DatePicker")
                  .Begin())
       { %>
	    <ul>
		    <li>
			    <label for="DateTimePickerAttributes_MinDate-input">show time between</label>
                <%= Html.Telerik().DateTimePicker()
                        .Name("DateTimePickerAttributes.MinDate")
                        .Value(Model.DateTimePickerAttributes.MinDate.Value)
                %>
			    <label for="DateTimePickerAttributes_MaxDate-input">and</label>
                <%= Html.Telerik().DateTimePicker()
                        .Name("DateTimePickerAttributes.MaxDate")
                        .Value(Model.DateTimePickerAttributes.MaxDate.Value)
                %>
		    </li>
		    <li>
			    <label for="DateTimePickerAttributes_SelectedDate-input">have pre-selected</label>
                <%= Html.Telerik().DateTimePicker()
                        .Name("DateTimePickerAttributes.SelectedDate")
                        .Min(Model.DateTimePickerAttributes.MinDate.Value)
                        .Max(Model.DateTimePickerAttributes.MaxDate.Value)
                        .Value(Model.DateTimePickerAttributes.SelectedDate.Value)
                %>
		    </li>
            <li>
			    <label for="DateTimePickerAttributes_Interval-input">leave a </label>
                <%=  Html.Telerik().IntegerTextBox()
                         .Name("DateTimePickerAttributes.Interval")
                         .MinValue(0)
                         .MaxValue(60)
                         .Value(Model.DateTimePickerAttributes.Interval.Value)
                %> minute interval between values
		    </li>
	    </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

    <% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => {%>
	    /* client-side validation */
        $('.configurator button').click(function(e) {
            $('.configurator :text').each(function () {
                if ($(this).hasClass('t-state-error')) {
                    alert("TextBox `" + this.name + "` has an invalid param!");
                    e.preventDefault();
                }
            });
        });
    <%}); %>

</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #DatePicker, #TimePicker, #DateTimePicker
        {
            margin: 0 150px 230px 0;
            float: left;
            width: 100px;
        }
		
		#TimePicker,
		#DateTimePicker
        {
            clear: both;
        }
		
		#DateTimePicker
        {
            margin-right: 100px;
            width: 150px;
        }
		
	    .example .configurator
	    {
	        width: 500px;
	        float: left;
	        margin: 0;
	        display: inline;
	    }
	    
	    #TimePickerAttributes_Interval .t-input,
	    #DateTimePickerAttributes_Interval .t-input
	    {
	        width: 40px;
	    }
	    
	    .configurator p
	    {
	        margin: 0;
	        padding: .7em 0;
	    }
    </style>
</asp:content>
