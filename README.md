# ⚒️ EazTrialRemover

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 7.0](https://img.shields.io/badge/.NET-7.0-blue?style=for-the-badge)
![License-MIT](https://img.shields.io/badge/License-MIT-Green?style=for-the-badge)

EazTrialRemover is a powerful utility that removes the evaluation period, also known as the trial limit, from assemblies protected by Eazfuscator.NET. This allows these assemblies to run indefinitely, without the limitations imposed by the seven-day trial period. [Download EazTrialRemover](https://github.com/Plot1337/EazTrialRemover/releases)

---

### 🔧 Building from Source
If you want to build EazTrialRemover from source code, follow these steps:

1. Clone the repository to your local machine:

   ```shell
   git clone https://github.com/Plot1337/EazTrialRemover.git
   ```

2. Navigate to the project directory:

   ```shell
   cd EazTrialRemover
   ```

3. Build the solution using the .NET CLI:

   ```shell
   dotnet build
   ```

4. Once the build is complete, you will find the EazTrialRemover executable in the `bin/Debug` or `bin/Release` directory, depending on the build configuration you choose.

Now, you have successfully built EazTrialRemover from the source code, and you can use it as described in the usage section.

### ❓ Usage
To remove the trial limitation, you can either use the command-line interface with the following syntax:
```shell
EazTrialRemover.exe <assembly|assemblies>
```
Alternatively, you can simply drag and drop your protected assembly or assemblies onto the `EazTrialRemover.exe` executable. This means you can effortlessly remove trials from multiple protected assemblies simultaneously.

### 📜 Notes
Here are some essential notes regarding EazTrialRemover:
- **Do Not Use PEVerify**: It is important to avoid using `PEVerify` after running EazTrialRemover, as it might produce inaccurate results.
- Compatibility: EazTrialRemover is compatible with Eazfuscator.NET version 2023.3 and versions below it. Given that the developers have not updated the trial system in quite some time, it is likely to work with future versions as well.
- Versatile Compatibility: EazTrialRemover has been successfully tested with Class Libraries, Console Apps, and WinForms applications. It should also work with other types of protected assemblies.
- Reporting Issues: If you encounter any bugs or issues, please don't hesitate to open an issue on the project's GitHub page.

#### Tool Not Working?
If EazTrialRemover fails to work for you, consider changing the system date on your PC to a future date (possibly 10 years into the future) and then protect your assembly using Eazfuscator.NET. This workaround eliminates the need for this tool or any similar utilities to remove the trial expiration.

### ⚠️ Disclaimer
It's important to note that using this tool may potentially lead to legal issues. Please use it with caution and only for ethical purposes. If you have the financial means, consider supporting the developers by [purchasing a license](https://www.gapotchenko.com/eazfuscator.net/purchase).

### 📋 Requirements
To run EazTrialRemover, make sure your system meets the following requirements:
- **Operating System**: Windows OS
- **.NET 7.0**: You can download and install .NET 7.0 from the official [Microsoft .NET website](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

### 🔨 Dependencies
EazTrialRemover relies on the [dnlib](https://github.com/0xd4d/dnlib) library to perform its functions effectively.
