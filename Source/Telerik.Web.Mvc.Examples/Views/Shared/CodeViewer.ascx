<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3>About this example</h3>

<% Html.Telerik().TabStrip()
		.Name("code-viewer-tabs")
		.Items(tabstrip => {
			
			Regex tabs = new Regex("\t", RegexOptions.Compiled | RegexOptions.Multiline);

			bool hasDescription = !string.IsNullOrEmpty(ViewData.Get<string>("Description"));
			if (hasDescription)
			{
				tabstrip.Add()
					.Text("Description")
					.Content(() =>
					{%>
						<div class="description"><%= ViewData.Get<string>("Description")%></div>
					<%});
			}

			Func<string, bool, Action> formatSourceCode = (sourceCode, prettify) =>
				() => { %><pre<%= prettify ? " class=\"prettyprint\"" : "" %>><%= Html.Encode(tabs.Replace(sourceCode, "    "))%></pre><% };

			var codeFiles = ViewData.Get<Dictionary<string, string>>("codeFiles");

			bool firstPrettified = hasDescription;

			foreach (var codeFile in codeFiles)
			{
				tabstrip.Add()
						.Text(codeFile.Key)
						.Content(formatSourceCode(codeFile.Value, !firstPrettified));

				firstPrettified = true;
			}
		})
		.SelectedIndex(0)
		.ClientEvents(c =>
			c.OnSelect(() => {%>
				function(e) {
					var contentElement = $('#code-viewer-tabs-' + ($(e.target.parentNode).children().index(e.target) + 1));
					$('.prettyprint').removeClass('prettyprint').addClass('prettified');
					contentElement.find('pre:not(.prettified)').addClass('prettyprint');
					
					prettyPrint();
				}
			<%}))
		.Render(); 
%>