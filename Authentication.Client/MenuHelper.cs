using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Client;
public static class MenuHelper
{
    public static string Input(string display, string defaultValue = "")
    {
        if(defaultValue is not null or "")
            display += $" [{defaultValue}]";
        Console.WriteLine(display);
        Console.Write("> ");
        string? input = Console.ReadLine();
        return input is null or "" ? (defaultValue ?? "") : input.Trim();
    }
    public static void Pause(string message = "Pressione qualquer tecla para continuar...")
    {
        Console.WriteLine(message);
        Console.ReadKey(true);
    }
}