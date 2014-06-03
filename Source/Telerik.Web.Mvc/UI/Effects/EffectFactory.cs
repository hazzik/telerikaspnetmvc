// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;

    using Infrastructure;

    /// <summary>
    /// Factory  for the effect builder.
    /// </summary>
    public class EffectFactory : IHideObjectMembers
    {
        private readonly IEffectEnabled container;

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectFactory"/> class.
        /// </summary>
        /// <param name="container">Container which consists all animations.</param>
        public EffectFactory(IEffectEnabled container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;

            this.container.Effects.Clear();
        }

        /// <summary>
        /// Enables toggle animation.
        /// </summary>
        public EffectFactory Toggle()
        {
            container.Effects.Add(new ToggleEffect());

            return this;
        }

        /// <summary>
        /// Enables slide animation.
        /// </summary>
        public EffectFactory Slide()
        {
            container.Effects.Add(new SlideAnimation());

            return this;
        }

        /// <summary>
        /// Enables slide animation.
        /// </summary>
        /// <param name="setEffectProperties">Builder, which sets different slide properties.</param>
        public EffectFactory Slide(Action<AnimationBuilder> setEffectProperties)
        {
            var effect = new SlideAnimation();

            setEffectProperties(new AnimationBuilder(effect));

            container.Effects.Add(effect);

            return this;
        }

        /// <summary>
        /// Enables opacity animation.
        /// </summary>
        public EffectFactory Opacity()
        {
            container.Effects.Add(new PropertyAnimation(PropertyAnimationType.Opacity));

            return this;
        }

        /// <summary>
        /// Enables opacity animation.
        /// </summary>
        /// <param name="setEffectProperties">Builder, which sets different opacity properties.</param>
        public EffectFactory Opacity(Action<AnimationBuilder> setEffectProperties)
        {
            var effect = new PropertyAnimation(PropertyAnimationType.Opacity);

            setEffectProperties(new AnimationBuilder(effect));

            container.Effects.Add(effect);

            return this;
        }

        /// <summary>
        /// Enables expand animation.
        /// </summary>
        public EffectFactory Expand()
        {
            container.Effects.Add(new PropertyAnimation(PropertyAnimationType.Height));

            return this;
        }

        /// <summary>
        /// Enables expand animation.
        /// </summary>
        /// <param name="setEffectProperties">Builder, which sets different expand properties.</param>
        public EffectFactory Expand(Action<AnimationBuilder> setEffectProperties)
        {
            var effect = new PropertyAnimation(PropertyAnimationType.Height);

            setEffectProperties(new AnimationBuilder(effect));

            container.Effects.Add(effect);

            return this;
        }
    }
}