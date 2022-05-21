using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Client;
public static class MenuHelper
{
    public static string Input(string display, string defaultValue = "", bool requireContent = false)
    {
        begin:
        if(defaultValue is not null or "")
            display += $" [{defaultValue}]";
        Console.WriteLine(display);
        Console.Write("> ");
        string? input = Console.ReadLine();
        bool isNullOrEmpty = input is null or "";
        if (isNullOrEmpty && requireContent)
            goto begin;
        return isNullOrEmpty ? (defaultValue ?? "") : input!.Trim();
    }
    public static void Pause(string message = "Pressione qualquer tecla para continuar...")
    {
        Console.WriteLine(message);
        Console.ReadKey(true);
    }
}