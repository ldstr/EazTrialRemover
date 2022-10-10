using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Diagnostics;

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

            int len = args.Length;
            Console.WriteLine($"Loaded {len} {(len > 1 ? "assemblies" : "assembly")}!");

            foreach (string path in args)
            {
                var sw = Stopwatch.StartNew();
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
                catch { WriteLnColored("Failed to patch!", ConsoleColor.Red); }
                finally
                {
                    sw.Stop();

                    WriteLnColored(
                        $"Task completed in {sw.Elapsed.TotalSeconds} seconds!",
                        ConsoleColor.Yellow
                        );
                }
            }

            Exit();
        }

        private static void Patch(string path)
        {
            var module = ModuleDefMD.Load(path);
            var types = module.GetTypes();

            bool
                patchedCheck = false,
                patchedVoid = false;

            for (int i = 0; i < types.Count(); i++)
            {
                var type = types.ElementAt(i);

                if (patchedCheck && patchedVoid)
                    break;
                else if (!type.HasMethods)
                    continue;

                if (!patchedCheck)
                    patchedCheck = PatchCheckMethod(ref type);

                if (!patchedVoid)
                    patchedVoid = PatchVoidMethod(ref type);
            }

            if (patchedCheck && patchedVoid) Save(module, path);
            else WriteLnColored("Failed to patch!", ConsoleColor.Red);
        }

        private static bool PatchCheckMethod(ref TypeDef type)
        {
            bool ok = TryFindMethod(
                type,
                "System.Boolean",
                3,
                out var body,
                out var insts
                );

            if (!ok || body == null || insts == null)
                return false;
            else if (
                insts[1].OpCode == OpCodes.Call &&
                insts[2].OpCode == OpCodes.Ret
                )
            {
                Console.Write("Found check method: ");
                WriteLnColored("0x" + type.MDToken, ConsoleColor.Red);

                insts.Clear();
                insts.Add(OpCodes.Ldc_I4_1.ToInstruction());
                insts.Add(OpCodes.Ret.ToInstruction());

                WriteLnColored("Patched check method!", ConsoleColor.Green);
                return true;
            }
            else return false;
        }

        private static bool PatchVoidMethod(ref TypeDef type)
        {
            bool ok = TryFindMethod(
                type,
                "System.Void",
                4,
                out var body,
                out var insts
                );

            if (!ok || body == null || insts == null)
                return false;
            else if (
                insts[1].OpCode == OpCodes.Call &&
                insts[2].OpCode == OpCodes.Pop
                )
            {
                Console.Write("Found void method: ");
                WriteLnColored("0x" + type.MDToken, ConsoleColor.Red);

                insts.Clear();

                WriteLnColored("Patched void method!", ConsoleColor.Green);
                return true;
            }
            else return false;
        }

        private static bool TryFindMethod(
            TypeDef type,
            string ret,
            int instsLen,
            out CilBody? body,
            out IList<Instruction>? insts
            )
        {
            body = null;
            insts = null;

            var methods = FilterMethods(type, ret);

            if (methods.Count != 1)
                return false;

            var method = methods.First();

            if (method == null || !method.HasBody)
                return false;

            body = method.Body;

            if (!body.HasInstructions)
                return false;

            insts = body.Instructions;
            return insts.Count == instsLen;
        }

        private static List<MethodDef> FilterMethods(
            TypeDef type,
            string ret,
            params string[] input
            )
        {
            int len = input.Length;
            var result = new List<MethodDef>();

            foreach (var method in type.Methods)
            {
                var metPerms = method.Parameters;
                int metLen = metPerms.Count;

                if (
                    !method.IsStatic ||
                    method.ReturnType.FullName != ret ||
                    input.Length != metLen
                    ) continue;

                bool match = true;

                for (int i = 0; i < len && i < metLen; i++)
                    if (metPerms[i].Type.FullName != input[i])
                    {
                        match = false;
                        break;
                    }

                if (match)
                    result.Add(method);
            }

            return result;
        }

        private static void Save(ModuleDefMD module, string path)
        {
            WriteLnColored("Saving...");

            string
                name = path.EndsWith("exe") ? "exe" : "dll",
                bakFile = path.Replace('.' + name, "_backup." + name);

            File.Move(path, bakFile);

            WriteColored("Original assembly moved to: ", ConsoleColor.Blue);
            Console.WriteLine(bakFile);

            module.Write(path);
            
            WriteColored("Saved to: ", ConsoleColor.Blue);
            Console.WriteLine(path);
        }

        private static void WriteLnColored(
            string text,
            ConsoleColor color = ConsoleColor.Green
            ) => WriteColored(text + "\n", color);

        private static void WriteColored(
            string text,
            ConsoleColor color = ConsoleColor.Green
            )
        {
            Console.ForegroundColor = color;
            Console.Write(text);
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

            Console.WriteLine();
            Console.WriteLine("--");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(code);
        }
    }
}
