# ResourceAssemblyUtil

**Utility for loading assemblies from resources automatically.**

To apply this utility, use the code below:

```C#
AppDomain.CurrentDomain.AssemblyResolve += ResourceAssemblyUtil.AssemblyResolveHandler;
```

You can do it in the constructor of the App like this:

```C#
public partial class App : Application
{
    public App(){
        AppDomain.CurrentDomain.AssemblyResolve += ResourceAssemblyUtil.AssemblyResolveHandler;
    }
}
```

Remember to modity the assembly list **ResourceAssemblyFiles** using relative uri as required like this:

```C#
private static readonly string[] ResourceAssemblyFiles = new string[] { "Libs\\IPlugins.dll" };
```
