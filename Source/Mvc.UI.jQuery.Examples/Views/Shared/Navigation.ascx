<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script runat="server">
    public bool ShouldSelectTab(string controllerName)
    {
        string currentControllerName = ViewContext.RouteData.Values["controller"] as string;

        return (string.Compare(currentControllerName, controllerName, StringComparison.OrdinalIgnoreCase) == 0);
    }
</script>
<% Html.jQuery().Accordion()
                .Name("navigation")
                .Items(parent =>
                               {
                                   parent.Add()
                                       .Text("Accordion")
                                       .Selected(ShouldSelectTab("Accordion"))
                                       .Content(() =>
                                                        {%>
                                                            <ul>
                                                                <li>
                                                                    <%= Html.ActionLink("Basic example", "Basic", "Accordion")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Animation example", "Animation", "Accordion")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("No auto height example", "NoAutoHeight", "Accordion")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Open on mouse over example", "OpenOnMouseOver", "Accordion")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Collapsible content example", "CollapsibleContent", "Accordion")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Custom icons example", "CustomIcons", "Accordion")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Events example", "Events", "Accordion")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Content With Render Partial", "ContentWithRenderPartial", "Accordion")%></li>
                                                            </ul>
                                                        <%}
                                                );

                                   parent.Add()
                                       .Text("DatePicker")
                                       .Selected(ShouldSelectTab("DatePicker"))
                                       .Content(() =>
                                                        {%>
                                                            <ul>
                                                                <li>
                                                                    <%= Html.ActionLink("Basic example", "Basic", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Populate alternate field example", "PopulateAlternateField", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Constrain input example", "ConstrainInput", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Month year menu example", "MonthYearMenu", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Date format example", "DateFormat", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Default value example", "DefaultValue", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Restrict date range example", "RestrictDateRange", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Number of months to show example", "NumberOfMonthsToShow", "DatePicker")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Show on example", "ShowOn", "DatePicker")%></li>
                                                            </ul>
                                                        <%}
                                                );

                                   parent.Add()
                                       .Text("MessageBox")
                                       .Selected(ShouldSelectTab("MessageBox"))
                                       .Content(() =>
                                                        {%>
                                                            <ul>
                                                                <li><%= Html.ActionLink("Info box", "Info", "MessageBox")%></li>
                                                                <li><%= Html.ActionLink("Error box", "Error", "MessageBox")%></li>
                                                            </ul>
                                                        <%}
                                                );

                                   parent.Add()
                                       .Text("ProgressBar")
                                       .Selected(ShouldSelectTab("ProgressBar"))
                                       .Content(() =>
                                                        {%>
                                                            <ul>
                                                                <li>
                                                                    <%= Html.ActionLink("Basic example", "Basic", "ProgressBar") %></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Aute retrieve value example", "AutoRetrieve", "ProgressBar")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Update elements example", "UpdateElements", "ProgressBar")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Events example", "Events", "ProgressBar")%></li>
                                                            </ul>
                                                        <%}
                                                );

                                   parent.Add()
                                       .Text("Slider")
                                       .Selected(ShouldSelectTab("Slider"))
                                       .Content(() =>
                                                        {%>
                                                            <ul>
                                                                <li>
                                                                    <%= Html.ActionLink("Basic example", "Basic", "Slider") %></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Vertical orientation example", "VerticalOrientation", "Slider")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Range example", "Range", "Slider")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Minimum, maximum, steps example", "MinimumMaximumSteps", "Slider")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Form submit with value example", "FormSubmitWithValue", "Slider")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Form submit with values example", "FormSubmitWithValues", "Slider")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Events example", "Events", "Slider")%></li>
                                                            </ul>
                                                        <%}
                                                );

                                   parent.Add()
                                       .Text("Tab")
                                       .Selected(ShouldSelectTab("Tab"))
                                       .Content(() =>
                                                        {%>
                                                            <ul>
                                                                <li>
                                                                    <%= Html.ActionLink("Basic example", "Basic", "Tab") %></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Animation example", "Animation", "Tab") %></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Load via ajax example", "LoadViaAjax", "Tab")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Open on mouse over example", "OpenOnMouseOver", "Tab")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Collapsible content example", "CollapsibleContent", "Tab")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Roatation example", "Roatation", "Tab")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Dynamic items example", "Dynamic", "Tab")%></li>
                                                                <li>
                                                                    <%= Html.ActionLink("Events example", "Events", "Tab")%></li>
                                                            </ul>
                                                        <%}
                                                );
                               }
                      )
                .Render(); %>