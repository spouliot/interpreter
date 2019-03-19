# interpreter

Quick and dirty experiments with Xamarin.iOS and the IL interpreter

* **dynamicload**: a quick sample that does `Assembly.Load`, `dynamic`, code downloading and `System.Reflection.Emit` - all the things you can't normally do on iOS


## Requirements

* Xamarin.iOS 12.7.1.x build

This is a special build that includes 
* a `mscorlib.dll` with extra types, so compiling code using `System.Reflection.Emit` is possible;
* a `libmono` runtime including the `System.Reflection.Emit` native code (icalls) for devices;
* a `System.Core.dll` build for the _normal_ dynamic support (not the SLE interpreter);

The other (experimental) features are already included in Xamarin.iOS 12.6+


## How-to roll your own interpreted application

1. Create a new iOS project (or use an existing one);
2. In the project's options, **iOS Build**, add `--interpreter` to the **Additional mtouch arguments**
3. Build and deploy to device



## FAQ

1. What happens if I don't provide the `--interpreter` argument ?

The application will be AOT'ed just like normal Xamarin.iOS applications always have been.


2. What happens if I don't use Xamarin.iOS 12.7.1.x ?

Some extra features, like `System.Reflection.Emit`, won't be supported.


3. What happens if I use a newer Xamarin.iOS, like 12.8.x+ ?

Some extra features, like `System.Reflection.Emit`, won't be supported. Xamarin.iOS 12.7.1 is a special build to preview features that will become available in a future stable release.


4. How can I control what's AOT'ed and what's interpreted ?

```
--interpreter[=VALUE]  Enable the *experimental* interpreter. Optionally
                       takes a comma-separated list of assemblies to
                       interpret (if prefixed with a minus sign, the
                       assembly will be AOT-compiled instead). 'all'
                       can be used to specify all assemblies. This
                       argument can be specified multiple times.
```


## Feedback

If you are experimenting with this build then come talk to us on our [gitter channel](https://gitter.im/xamarin/xamarin-macios).

Please report issues on [github issues](https://github.com/xamarin/xamarin-macios/issues/new) and include all the requested information.
