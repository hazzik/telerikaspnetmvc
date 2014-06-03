namespace Telerik.Web.Mvc.UnitTest.Effects
{
	using System;
	using System.IO;
	using System.Web.UI;
	using System.Collections.Generic;

	using Xunit;
	using Moq;

	using Telerik.Web.Mvc.UI;

	public class EffectFactoryTests
	{
		Mock<IEffectEnabled> container;
		IList<IEffect> effectsCollection;

		EffectFactory factory;

		public EffectFactoryTests()
		{
			effectsCollection = new List<IEffect>();

			container = new Mock<IEffectEnabled>();
			container.SetupGet(c => c.Effects).Returns(effectsCollection);

			factory = new EffectFactory(container.Object);
		}

		[Fact]
		public void Adding_effects_adds_them_to_effects_collection()
		{
			factory.Slide();

			Assert.Equal(1, container.Object.Effects.Count);
		}

		[Fact]
		public void Adding_effects_preserves_order_in_the_collection()
		{
			factory.Slide().Opacity().Toggle().Expand();

			Assert.True(container.Object.Effects[0] is SlideAnimation);
			Assert.True(container.Object.Effects[1] is PropertyAnimation);
			Assert.True(container.Object.Effects[2] is ToggleEffect);
			Assert.True(container.Object.Effects[3] is PropertyAnimation);
		}

		[Fact]
		public void Test_setting_slide_effect_properties_with_actions()
		{
			factory.Slide(props =>
			{
				props
					.OpenDuration(AnimationDuration.Slow)
					.CloseDuration(AnimationDuration.Slow);
			});

			var slide = effectsCollection[0] as SlideAnimation;

			Assert.Equal((int)AnimationDuration.Slow, slide.OpenDuration);
			Assert.Equal((int)AnimationDuration.Slow, slide.CloseDuration);
		}

		[Fact]
		public void Test_setting_opacity_effect_properties_with_actions()
		{
			factory.Opacity(props =>
			{
				props
					.OpenDuration(AnimationDuration.Slow)
					.CloseDuration(AnimationDuration.Slow);
			});

			var opacity = effectsCollection[0] as PropertyAnimation;

			Assert.Equal((int)AnimationDuration.Slow, opacity.OpenDuration);
			Assert.Equal((int)AnimationDuration.Slow, opacity.CloseDuration);
		}

        [Fact]
        public void Test_setting_height_height_effect_with_actions()
        {
            factory.Expand(props =>
            {
                props
                    .OpenDuration(AnimationDuration.Slow)
                    .CloseDuration(AnimationDuration.Slow);
            });

            var height = effectsCollection[0] as PropertyAnimation;

            Assert.Equal((int)AnimationDuration.Slow, height.OpenDuration);
            Assert.Equal((int)AnimationDuration.Slow, height.CloseDuration);
        }
	}
}