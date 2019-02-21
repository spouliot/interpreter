# Subclass

This sample shows the use of the interpreter and the linker / optimizations.

It can be tricky to get both working together. The linker assume it knows all the code inside the application. Anything loaded dynamically cannot be considered at build time. Still, if some care is applied, you can use the interpreter and the linker together.

To help this scenario some of the default optimizations are turned off when the interpreter is enabled. You can turn them back on if your application does not break the optimization assumptions.


## FAQ

1. What happens if I don't provide the `--interpreter` argument ?

When touching any of the labels the sample will crash with something like:
```
error: Failed to load AOT module '.../subclass.app/customcode.dll.so' in aot-only mode.
```

2. What happens if I don't use Xamarin.iOS 12.7.1.x ?

The default options for many optimizations won't work with the interpreter when linking is enabled, leading to exceptions/crashes.