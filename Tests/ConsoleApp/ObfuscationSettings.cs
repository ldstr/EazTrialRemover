using System.Reflection;

[assembly: Obfuscation(Feature = "code control flow obfuscation", Exclude = false)]
[assembly: Obfuscation(Feature = "apply to type *: apply to member * when method or constructor: virtualization", Exclude = false)]