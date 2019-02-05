using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Net;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

using UIKit;

namespace dynloadios
{
	public partial class ViewController : UIViewController
	{
		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		int a = 1, b = 2;
		StringBuilder output = new StringBuilder ();

		void Dynamic (string option, string path)
		{
			output.Clear ();
			output.Append ("Clicked '").Append (option).AppendLine ("'...");

			if (File.Exists (path)) {
				output.Append ("Loading '").Append (Path.GetFileName (path)).AppendLine ("'...");
				try {
					var assembly = Assembly.LoadFile (path);
					var t = assembly.GetType ("DynamicClass");
					if (t != null) {
						dynamic dynamic_ec = Activator.CreateInstance (t);
						try {
							// on console only
							dynamic_ec.Print (assembly.ToString ());
						} catch (Exception e) {
							output.Append ("FAIL ").AppendLine (e.ToString ());
						}

						var addition = String.Empty;
						try {
							output.Append ("Adding ").Append (a).Append (" to ").Append (b).Append (" gives ");
							a = dynamic_ec.Add (a, b++);
							output.AppendLine (a.ToString ());
						} catch (RuntimeBinderException rbe) {
							output.Append ("Does not add up ").AppendLine (rbe.ToString ());
						} catch (Exception e) {
							output.Append ("FAIL ").AppendLine (e.ToString ());
						}
					} else {
						output.Append ("Type 'DynamicClass' was not found inside the assembly!");
					}
				} catch (Exception e) {
					output.Append ("FAIL ").AppendLine (e.ToString ());
				}
			} else {
				output.Append ("Assembly '").Append (path).AppendLine ("' not found!");
			}
			textBox.Text = output.ToString ();
		}

		partial void EmitDown (UIButton sender)
		{
			var temp = Path.GetTempFileName () + ".dll";
			var name = Path.GetFileName (temp);
			AssemblyName aname = new AssemblyName ("Emit");
			AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly (aname, AssemblyBuilderAccess.Save, Path.GetDirectoryName (temp));
			ModuleBuilder mod = ab.DefineDynamicModule ("main", name);
			TypeBuilder tb = mod.DefineType ("DynamicClass", TypeAttributes.Public);

			MethodBuilder mb = tb.DefineMethod ("Print", MethodAttributes.Public, null, new [] { typeof (string) });
			ILGenerator il = mb.GetILGenerator ();
			il.EmitWriteLine ("Hello Code Generation!");
			il.Emit (OpCodes.Ret);

			MethodBuilder mb2 = tb.DefineMethod ("Add", MethodAttributes.Public, typeof (int), new [] { typeof (int), typeof (int) });
			ILGenerator il2 = mb2.GetILGenerator ();
			il2.Emit (OpCodes.Ldarg_1);
			il2.Emit (OpCodes.Ldarg_2);
			il2.Emit (OpCodes.Add);
			il2.Emit (OpCodes.Ret);

			tb.CreateType ();
			ab.Save (name);
			Dynamic ("Emit", temp);
		}

		// e.g. `python -m SimpleHTTPServer 8000`
		partial void DownloadDown (UIButton sender)
		{
			var url = "http://192.168.2.35:8000/download.dll";
			var temp = Path.GetTempFileName ();
			try {
				new WebClient ().DownloadFile (url, temp);
				Dynamic ("Download", temp);
 			} catch (Exception e) {
				textBox.Text = $"Could not download '{url}' to '{temp}'.{Environment.NewLine}{e}";
			}
		}

		partial void FailDown (UIButton sender)
		{
			Dynamic ("Fail", Path.GetFullPath ("fail.dll"));
		}

		partial void PassDown (UIButton sender)
		{
			Dynamic ("Pass", Path.GetFullPath ("pass.dll"));
		}
	}
}
