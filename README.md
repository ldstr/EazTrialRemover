# EazTrialRemover
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![dotnet6.0](https://img.shields.io/badge/.NET-6.0-blue?style=for-the-badge)
![License-MIT](https://img.shields.io/badge/License-MIT-Green?style=for-the-badge)

Removes Eazfuscator.NET's evaluation period (aka trial limit) from protected assemblies — this allows it to run forever instead of expiring after seven days. [Download](https://github.com/Plot1337/EazTrialRemover/releases)

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
