using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.Mvc.Infrastructure;

namespace Telerik.Web.Mvc.UI
{
    public class AnimationBuilder : IHideObjectMembers
    {
        private readonly IAnimation animation;

        public AnimationBuilder(IAnimation animation)
        {
            this.animation = animation;
        }

        public AnimationBuilder OpenDuration(int value)
        {
            Guard.IsNotNegative(value, "value");

            animation.OpenDuration = value;

            return this;
        }

        public AnimationBuilder CloseDuration(int value)
        {
            Guard.IsNotNegative(value, "value");

            animation.CloseDuration = value;

            return this;
        }

        public AnimationBuilder OpenDuration(AnimationDuration value)
        {
            return OpenDuration((int)value);
        }

        public AnimationBuilder CloseDuration(AnimationDuration value)
        {
            return CloseDuration((int)value);
        }

    }
}
