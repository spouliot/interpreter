using Foundation;
using ObjCRuntime;
using System;
using UIKit;

// System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.MissingMethodException: Method not found: void UIKit.UIView..ctor(CoreGraphics.CGRect)
[assembly: Preserve (typeof (UIView), AllMembers = true)]
// type won't load (and we'll throw a `ArgumentNullException`) if we won't preserve the protocol
[assembly: Preserve (typeof (INSUrlProtocolClient), AllMembers = true)]

namespace subclass {

	public partial class ViewController : UIViewController {

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// use `Type` to load the assembly we bundled into the app, i.e. `mtouch` is *NOT* aware of it
			var t = Type.GetType ("BundledCode.CustomView, customcode");
			// this would crash if 'remove-dynamic-registrar' was not turned off
			//  System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> ObjCRuntime.RuntimeException: Can't register the class SubclassDemo.CustomView when the dynamic registrar has been linked away.
			var v = (UIView) Activator.CreateInstance (t, UIScreen.MainScreen.Bounds);

			// this would crash (without much informative details) if 'inline-isdirectbinding' was not turned off
			v.BackgroundColor = UIColor.Blue;
			// that won't crash but it might not do what we expect if 'seal-and-devirtualize' was not turned off
			if (v.BackgroundColor != UIColor.Red)
			    throw new InvalidProgramException ("'seal-and-devirtualize' is likely enabled, causing the AOT code to be sealed and disallowing dynamically loaded code to subclass/override some types/members");
			Add (v);

			// dynamically loading protocols conflicts when the 'register-protocols' optimization
			var p = Type.GetType ("BundledCode.CustomProtocol, customcode");
			var u = (NSObject) Activator.CreateInstance (p, null, null, null);
			var x = new Protocol ("NSURLProtocolClient");
			if (!u.ConformsToProtocol (x.Handle))
			    throw new InvalidProgramException ("'register-protocols' is likely enabled, causing the dynamically loaded code to skip protocol registration");
		}
	}
}