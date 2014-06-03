// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;
    using UI;

    /// <summary>
    /// Used for action methods when using Ajax or Custom binding
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class GridActionAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridActionAttribute"/> class.
        /// </summary>
        public GridActionAttribute()
        {
            ActionParameterName = "command";
        }

        /// <summary>
        /// Gets or sets the name of the action parameter. The default value is "command".
        /// </summary>
        /// <value>The name of the action parameter.</value>
        /// <example>
        /// <code lang="CS">
        /// [GridAction(ActionParameterName="param")]
        /// public ActionResult Index(GridCommand param)
        /// {
        /// }
        /// </code>
        /// </example>
        public string ActionParameterName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether custom binding is enabled. Used when implementing custom ajax binding.
        /// </summary>
        /// <value><c>true</c> if custom binding is enabled; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        /// <example>
        /// <code lang="CS">
        /// [GridAction(EnableCustomBinding=true)]
        /// public ActionResult Index(GridCommand param)
        /// {
        /// }
        /// </code>
        /// </example>
        public bool EnableCustomBinding
        {
            get;
            set;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey(ActionParameterName))
            {
                GridCommand command = new GridCommand
                {
                  Page = filterContext.Controller.ValueProvider.ValueOf<int>(GridUrlParameters.CurrentPage)
                };

                string orderBy = filterContext.Controller.ValueProvider.ValueOf<string>(GridUrlParameters.OrderBy);

                command.SortDescriptors.AddRange(GridSortDescriptorSerializer.Deserialize(orderBy));
                
                string filter = filterContext.Controller.ValueProvider.ValueOf<string>(GridUrlParameters.Filter);

                command.FilterDescriptors.AddRange(FilterDescriptorFactory.Create(filter));

                filterContext.ActionParameters[ActionParameterName] = command;
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest() &&
                (filterContext.Result is ViewResultBase))
            {
                ViewResultBase actionResult = filterContext.Result as ViewResultBase;

                IGridModel model = actionResult.ViewData.Model as IGridModel;

                if (model == null)
                {
                    return;
                }

                GridActionBindingContext context = new GridActionBindingContext(EnableCustomBinding, filterContext.Controller.ValueProvider, model.Data, model.Total);
                GridDataProcessor dataProcessor = new GridDataProcessor(context);

                filterContext.Result = new JsonResult 
                { 
                    Data = new
                    {
                        data = dataProcessor.ProcessedDataSource,
                        total = dataProcessor.Total
                    } 
                };
            }
        }
    }
}