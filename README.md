I needed a very simple way to read an .ini file and iterate over the sections and values.

I took some sample code from the following article: https://www.codeproject.com/Articles/646296/A-Cross-platform-Csharp-Class-for-Using-INI-Files

That code has locking on critical sections and writing values and caching values & I didn't need any of that so I simplified it.

## Try It Out
You can get the code and try it out if you have .NET Core 8.0.x

Once you clone just :

$ dotnet run

There's a test.ini file to show you how it works.

It's all driven from Program.cs.

Output looks like the following:

![image](https://github.com/raddevus/IniReader/assets/16722666/9d134d79-094f-4176-9e5e-1f93ccd59434)
