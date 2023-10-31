using System.Reflection;

namespace CodventureV1.Persistence;

public static class AssemblyProvider
{
    public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
}
