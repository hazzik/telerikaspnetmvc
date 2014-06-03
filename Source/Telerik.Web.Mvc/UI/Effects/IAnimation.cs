using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Web.Mvc.UI
{
    public interface IAnimation
    {
        string Name { get; }
        int OpenDuration { get; set; }
        int CloseDuration { get; set; }
    }
}
