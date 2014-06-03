namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;
    
    public class GridToggleDetailViewAdornerTests
    {
        [Fact]
        public void Should_create_td()
        {
            var target = ArrangeAndAct();
            
            Assert.Equal("td", target.Children[0].TagName);
        }

        [Fact]
        public void Should_apply_class()
        {
            var target = ArrangeAndAct();

            Assert.Equal(UIPrimitives.Grid.HierarchyCell, target.Children[0].Attribute("class"));
        }

        [Fact]
        public void Should_render_toggle_link()
        {
            HtmlTag target = ArrangeAndAct();

            var td = target.Children[0];
            
            Assert.Equal("a", td.Children[0].TagName);
            Assert.Equal("#", td.Children[0].Attribute("href"));
        }

        [Fact]
        public void Should_apply_toggle_link_css_classes_for_collapsed_detail_view()
        {
            HtmlTag target = ArrangeAndAct();

            var td = target.Children[0];
            Assert.Equal("t-icon t-plus", td.Children[0].Attribute("class"));
        }

        [Fact]
        public void Should_apply_toggle_link_css_classes_for_expanded_detail_view()
        {
            HtmlTag target = ArrangeAndAct(true);

            var td = target.Children[0];
            Assert.Equal("t-icon t-minus", td.Children[0].Attribute("class"));
        }

        private HtmlTag ArrangeAndAct()
        {
            return ArrangeAndAct(false);
        }
        
        private HtmlTag ArrangeAndAct(bool expanded)
        {
            var adorner = new GridToggleDetailViewAdorner
            {
                Expanded = expanded
            };
             
            var target = new HtmlTag("tr");
            adorner.ApplyTo(target);
            return target;
        }
    }
}
