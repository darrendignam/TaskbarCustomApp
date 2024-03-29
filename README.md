# Simple Task Launcher
### Easily make your own custom popup menu for Windows 10

This is a quick little project to add a custom popup menu in the windows taskbar. Put your custom commands into a CSV file - and they will be a couple of easy clicks away. By right clicking on the icon down by the clock, all your custom commands will appear in the popup menu.

Any command you can type in a CMD window, or in the Run dialog should work. 

No more making shortcuts to batch files.

Just run the EXE and it will produce a template CSV file in the same folder as the EXE file. The format is simple: 

```
"Menu Item","Shell Command"
"Default Display Only","displayswitch.exe /internal"
"Extend Displays","displayswitch.exe /extend"
"-",""
"CMD","start cmd.exe"
"Hello World","start cmd.exe /k echo Hello World"
```

![Example Menu](/images/example.png)

Two columns - the first is the text in the menu item, the second is the command.

Above you can see the example I use to quickly switch between single and dual monitors (my laptop has a quick key for this - but my desktop has not got this usability feature). Plus there is the ability to insert a seperator by enclosing a hyphen /minus sign "-" in the first position.

The project file was created with Visual Studio 2019 Community Edition, but the files can probably be compiled without the full IDE. It is a .NET framework app, so this documentation should work:

https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/
