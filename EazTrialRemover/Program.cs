using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Diagnostics;

Console.Title = "Eaz.NET Trial Remover - Plot1337";

if (args.Length == 0)
{
    Exit("Usage: EazTrialRemover <assembly>");
    return;
}

int len = args.Length;

Console.WriteLine(
    "Loaded {0} {1}!",
    len, len > 1 ? "assemblies" : "assembly"
    );

var sw = Stopwatch.StartNew();

foreach (string path in args)
{
    sw.Reset();
    sw.Start();

    WriteColored("Attempting to patch: ", ConsoleColor.Blue);
    Console.WriteLine(path);

    if (!path.EndsWith("exe") && !path.EndsWith("dll"))
    {
        Exit("Invalid input!");
        continue;
    }
    else if (!File.Exists(path))
    {
        Exit("Assembly not found!");
        continue;
    }

    try { Patch(path); }
    catch (Exception ex)
    {
        WriteLnColored("Failed to patch!", ConsoleColor.Red);
        WriteLnColored(ex.ToString(), ConsoleColor.Red);
    }

    sw.Stop();

    WriteLnColored(
        $"Task completed in {sw.Elapsed.TotalSeconds} seconds!",
        ConsoleColor.Yellow
        );
}

Exit();

static void Patch(string path)
{
    var module = ModuleDefMD.Load(path);
    var types = module.GetTypes().ToList();

    for (int i = 0; i < types.Count; i++)
    {
        var type = types[i];

        if (!IsTrialCheckClass(type))
            continue;

        Console.Write($"Found trial-check class: ");
        WriteLnColored("0x" + type.MDToken, ConsoleColor.Red);

        var methods = type.Methods;

        foreach (var method in methods)
        {
            if (!method.HasBody)
                continue;

            var insts = method.Body.Instructions;

            insts.Clear();
            method.Body.ExceptionHandlers.Clear();

            if (MethodRets(method, "Boolean"))
                insts.Add(OpCodes.Ldc_I4_1.ToInstruction());

            insts.Add(OpCodes.Ret.ToInstruction());
        }

        Save(module, path);
        return;
    }

    WriteLnColored("Failed to patch!", ConsoleColor.Red);
}

static bool IsTrialCheckClass(TypeDef type)
{
    if (!type.HasMethods)
        return false;

    var methods = type.Methods;

    if (methods.Count != 4)
        return false;

    for (int i = 0; i < 4; i++)
    {
        if (i < 2)
        {
            if (!MethodRets(methods[i], "Boolean"))
                return false;
        }
        else
        {
            if (!MethodRets(methods[i]))
                return false;
        }
    }

    return true;
}

static bool MethodRets(
    MethodDef method,
    string? retType = null
    )
{
    if (!method.HasReturnType)
        return string.IsNullOrEmpty(retType) || retType == "Void";

    return ("System." + retType) == method.ReturnType.FullName;
}

static void Save(ModuleDefMD module, string path)
{
    WriteLnColored("Saving...");

    string
        name = path.EndsWith("exe") ? "exe" : "dll",
        bakFile = path.Replace('.' + name, "_backup." + name);

    if (File.Exists(bakFile))
        File.Delete(bakFile);

    File.Move(path, bakFile);

    WriteColored(
        "Original assembly moved to: ",
        ConsoleColor.Blue
        );

    Console.WriteLine(bakFile);

    module.Write(path, new(module)
    {
        AddCheckSum = true
    });

    WriteColored("Saved to: ", ConsoleColor.Blue);
    Console.WriteLine(path);
}

static void WriteLnColored(
    string text,
    ConsoleColor color = ConsoleColor.Green
    ) => WriteColored(text + "\n", color);

static void WriteColored(
    string text,
    ConsoleColor color = ConsoleColor.Green
    )
{
    Console.ForegroundColor = color;
    Console.Write(text);
    Console.ResetColor();
}

static void Exit(string? message = null)
{
    if (!string.IsNullOrEmpty(message))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    Console.WriteLine("\n--\nPress any key to exit...");
    Console.ReadKey(true);
    Environment.Exit(0);
}