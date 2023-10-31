using System.Reflection;

namespace CodventureV1.Infrastructure;

public static class AssemblyProvider
{
    public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
}
