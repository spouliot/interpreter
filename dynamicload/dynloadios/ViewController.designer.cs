// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace dynloadios
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView textBox { get; set; }

        [Action ("DownloadDown:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DownloadDown (UIKit.UIButton sender);

        [Action ("EmitDown:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void EmitDown (UIKit.UIButton sender);

        [Action ("FailDown:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void FailDown (UIKit.UIButton sender);

        [Action ("PassDown:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void PassDown (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (textBox != null) {
                textBox.Dispose ();
                textBox = null;
            }
        }
    }
}