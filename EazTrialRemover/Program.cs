using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace EazTrialRemover
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Eaz.NET Trial Remover - Plot1337";

            if (args.Length == 0)
            {
                Exit("Usage: EazTrialRemover <assembly>");
                return;
            }

            string path = args[0];

            if (!path.EndsWith("exe") && !path.EndsWith("dll"))
            {
                Exit("Invalid input!");
                return;
            }
            else if (!File.Exists(path))
            {
                Exit("Assembly not found!");
                return;
            }

            try { Patch(path); }
            catch { WriteLnColored("Failed to patch!", ConsoleColor.Red); }

            Exit();
        }

        private static void Patch(string path)
        {
            var module = ModuleDefMD.Load(path);
            var types = module.GetTypes();

            bool patched = false;

            foreach (var type in types)
            {
                if (!type.HasMethods)
                    continue;

                const string BOOL = "System.Boolean";
                var methods = type.FilterMethods(BOOL);

                if (methods.Count != 1)
                    continue;

                var method = methods.FirstOrDefault();

                if (method == null || !method.HasBody)
                    continue;

                var body = method.Body;

                if (!body.HasInstructions)
                    continue;

                var insts = body.Instructions;

                if (insts.Count != 3)
                    continue;

                if (
                    insts[1].OpCode == OpCodes.Call &&
                    insts[2].OpCode == OpCodes.Ret
                    )
                {
                    Console.Write("Found: ");
                    WriteLnColored("0x" + type.MDToken, ConsoleColor.Red);

                    insts.Clear();
                    insts.Add(OpCodes.Ldc_I4_1.ToInstruction());
                    insts.Add(OpCodes.Ret.ToInstruction());

                    body.ExceptionHandlers.Clear();

                    WriteLnColored("Patched!");
                    Save(module, path);

                    patched = true;
                }
            }

            if (!patched)
                WriteLnColored("Failed to patch!", ConsoleColor.Red);
        }

        private static void Save(ModuleDefMD module, string path)
        {
            string name = path.EndsWith("exe") ? "exe" : "dll";

            module.Write(path.Replace('.' + name, "_p." + name));
            WriteLnColored("Saved!");
        }

        private static void WriteLnColored(string text, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void Exit(string? message = null, int code = 0)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(code);
        }
    }
}
