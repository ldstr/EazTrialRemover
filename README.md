# ⚒️ EazTrialRemover
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![dotnet6.0](https://img.shields.io/badge/.NET-6.0-blue?style=for-the-badge)
![License-MIT](https://img.shields.io/badge/License-MIT-Green?style=for-the-badge)

Removes Eazfuscator.NET's evaluation period (aka trial limit) from protected assemblies — this allows it to run forever instead of expiring after seven days. [Download](https://github.com/Plot1337/EazTrialRemover/releases)

---

### ❓ Usage
`EazTrialRemover.exe <assembly|assemblies>` or drag-and-drop your protected assembly (or assemblies) into `EazTrialRemover.exe`; yes, you can remove trials from multiple protected assemblies at the same time.

### 📜 Notes
- Don't use `PEVerify`
- This works with the latest Eazfuscator.NET 2022.2 (release build `2022.2.751.89`) and below.
- The developers didn't update the trial system for ages; thus, it should be working in future versions.
- Tested and works with Class Libraries, Console Apps, and WinForms; should work with others, too.
- If there are any bugs, please open an issue.

#### Tool not working?
Once again, open an issue or you can change the date of your PC to something in the future (perhaps 10 years) and then protect your assembly. Yes, this means you don't even need this tool (or anything similar) to remove the expiration.

### ⚠️ Disclaimer
This tool could get you into legal issues; please use it with caution. If you have the money, please consider [purchasing a license](https://www.gapotchenko.com/eazfuscator.net/purchase).

### 📋 Requirements
- Windows System
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### 🔨 Dependencies
- [dnlib](https://github.com/0xd4d/dnlib)
