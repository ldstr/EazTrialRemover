# EazTrialRemover
Removes Eazfuscator.NET's evaluation period (aka trial limit) from protected assemblies — this allows it to run forever instead of expiring after seven days.

---

Usage: `EazTrialRemover.exe <assembly>` or drag-and-drop your protected assembly into `EazTrialRemover.exe`

NOTE: This works with Eaz.NET 2022.1; however, I am not sure if it will work in the future or in older versions. Works with Console and WinForms; should work for class-library and others, too. If there any any bugs, please open a issue.

### Disclaimer
This tool could get you into legal issues; please use it with caution. If you have the money, please consider [purchasing a license](https://www.gapotchenko.com/eazfuscator.net/purchase).

### Requirements
- Windows System
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### Dependencies
- [dnlib](https://github.com/0xd4d/dnlib)
