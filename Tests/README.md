# Obfuscation Settings

```csharp
using System.Reflection;

[assembly: Obfuscation(Feature = "apply to type *: apply to member * when method or constructor: virtualization", Exclude = false)]
```

Ran into some issues with the suggested way to input them; thus, you can also manually add the settings via [dnSpy](https://github.com/dnSpy/dnSpy) or anything similar.
