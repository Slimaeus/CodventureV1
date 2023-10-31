using System.Reflection;

namespace CodventureV1.Application;

public static class AssemblyProvider
{
    public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
}
