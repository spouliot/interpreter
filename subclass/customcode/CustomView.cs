using CoreGraphics;
using UIKit;

namespace BundledCode
{
	public class CustomView : UIView {

		public CustomView (CGRect frame) : base (frame)
		{
		}

        // all colors are fine - as long as it's red
		public override UIColor BackgroundColor {
			get => base.BackgroundColor;
			set => base.BackgroundColor = UIColor.Red;
		}
	}
}
