# DynamicLoad

This sample shows

* the use of C# `dynamic`, both a 
	- working case, `pass.dll`; and
	- an incomplete assembly `fail.dll` which throws [1]
* the use of `Assembly.Load` on non-AOT'ed assemblies [2]
	- two assemblies are included, as **BundleResource**, inside the .app
	- an assembly can be downloaded from the network
* the use of `System.Reflection.Emit` to 
	- generate and save an assembly on device; and then 
	- load and execute it

[1] the stack trace confirms the SLE interpreter (using for AOT builds) is not being used

[2] edit the URL to a web server that you control

This sample does **not** show

* Good UI taste :-)

## Building Notes

* By design this sample uses assemblies (e.g. `pass.dll` and `fail.dll`) that are not known to the iOS project (`dynloadios.exe`). This requires building `pass.csproj` and `fail.csproj` before the main `dynloadios` project. This ensure that `mtouch` is unaware of the assemblies and can't prepare (or change anything) to make them run (e.g. no AOT'ing, no linking...)

* Downloading an assembly requires you to start a local web server and update the URL inside `ViewController.cs`. Otherwise you'll get an exception.

```csharp
var url = "http://192.168.2.35:8000/download.dll";
```

## FAQ

1. What happens if I don't provide the `--interpreter` argument ?

When touching any of the labels the sample will crash with something like:
```
error: Failed to load AOT module '.../dynloadios.app/pass.dll.so' in aot-only mode.
```

2. What happens if I don't use Xamarin.iOS 12.7.1.x ?

The sample won't build since it uses `System.Reflection.Emit` types that Xamarin.iOS does not provide. 
However you can comment this part to try the rest of the sample.
