using System.Reflection;

namespace Nac.Lib.Utilities;

public static class AssemblyProperties
{
    private static AssemblyName GetAssemblyName() => Assembly.GetEntryAssembly()!.GetName();
    private static string GetVersionPrivate(AssemblyName a) => a.Version!.ToString();
    private static string GetAppNamePrivate(AssemblyName a) => a.Name!;

    public static string GetVersion() => GetVersionPrivate(GetAssemblyName());

    public static string GetAppName() => GetAppNamePrivate(GetAssemblyName());

    public static string GetNamedVersion()
    {
        var assemblyName = GetAssemblyName();
        return $"{GetAppNamePrivate(assemblyName)}_v{GetVersionPrivate(assemblyName)}";
    }

    public static string GetCompany()
    {
        Assembly currentAssem = Assembly.GetEntryAssembly()!;
        object[] attribs = currentAssem.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
        if (attribs.Length > 0)
        {
            return ((AssemblyCompanyAttribute)attribs[0]).Company;
        }
        return "unknown";
    }

    public static string GetCopyright()
    {
        Assembly currentAssem = Assembly.GetEntryAssembly()!;
        object[] attribs = currentAssem.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
        if (attribs.Length > 0)
        {
            return ((AssemblyCopyrightAttribute)attribs[0]).Copyright;
        }
        return "unknown";
    }

}
