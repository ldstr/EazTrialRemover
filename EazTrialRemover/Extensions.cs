using dnlib.DotNet;

namespace EazTrialRemover
{
    public static class Extensions
    {
        public static List<MethodDef> FilterMethods(
            this TypeDef type,
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
    }
}
