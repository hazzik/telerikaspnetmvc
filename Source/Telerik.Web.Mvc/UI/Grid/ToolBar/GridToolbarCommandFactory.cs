// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.ComponentModel;

    using Infrastructure;

    public class GridToolBarCommandFactory<T> : IHideObjectMembers where T : class
    {
        public GridToolBarCommandFactory(GridToolBarSettings<T> settings)
        {
            Guard.IsNotNull(settings, "settings");

            Settings = settings;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public GridToolBarSettings<T> Settings
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            get;
            private set;
        }

#if MVC2 || MVC3
        public virtual GridToolBarInsertCommandBuilder<T> Insert()
        {
            GridToolBarInsertCommand<T> command = new GridToolBarInsertCommand<T>();

            Settings.Commands.Add(command);
            
            Settings.Grid.Editing.Enabled = true;

            return new GridToolBarInsertCommandBuilder<T>(command);
        }
#endif
        public virtual GridToolBarCustomCommandBuilder<T> Custom()
        {
            GridToolBarCustomCommand<T> command = new GridToolBarCustomCommand<T>();

            Settings.Commands.Add(command);

            return new GridToolBarCustomCommandBuilder<T>(command);
        }

        public void Template(Func<object, object> template)
        {
            Guard.IsNotNull(template, "template");

            Settings.Template.InlineTemplate = template;
        }

        public void Template(Action template)
        {
            Guard.IsNotNull(template, "template");

            Settings.Template.Content = template;
        }

        public void Template(string template)
        {
            Guard.IsNotNullOrEmpty(template, "template");

            Settings.Template.Html = template;
        }
    }
}