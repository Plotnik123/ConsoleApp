using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {
        private class Options
        {
            public static DirectoryInfo sourc;
            public static DirectoryInfo destin;
            public static int I = 0;
            public static int count = 0;
            public static bool D;
            public static bool P;
            public static double catalogSize = 0;
        }
        public static void Main(string[] args)
        {
          Console.WriteLine("Путь к исходному каталогу(IN):");
            string IN = Console.ReadLine();
            while (!Directory.Exists(IN))
            {
                Console.WriteLine("Исходный каталог отсутствует, введите повторно путь к исходному каталогу(IN):");
                IN = Console.ReadLine();
            }
            Options.sourc = new DirectoryInfo(IN);

            Console.WriteLine("Путь к каталогу назначения (OUT):");
            string OUT = Console.ReadLine();
            while (!Directory.Exists(OUT))
            {
                Console.WriteLine("Каталог назначения отсутствует, введите повторно путь к каталогу назначения(OUT):");
                OUT = Console.ReadLine();
            }
            Options.destin = new DirectoryInfo(OUT);

            Console.WriteLine("Интервал чтения данных (I):");
            while (!Int32.TryParse(Console.ReadLine(), out Options.I) || Options.I < 500)
            {
                Console.WriteLine("Введите интервал чтения данных повторно (I):");
            }

            Console.WriteLine("Нужно ли удалять прочитанные файлы? (D, true/false):");
            while (!Boolean.TryParse(Console.ReadLine(), out Options.D))
            {
                Console.WriteLine("Введите значение флага повторно(D):");
            }
            Console.WriteLine("нужно ли в процессе чтения выводить имена прочитанных файлов в консоль? (P, true/false):");
            while (!Boolean.TryParse(Console.ReadLine(), out Options.P))
            {
                Console.WriteLine("Введите значение флага повторно(P):");
            }
            Timer t = new Timer(Reading, null, 0, Options.I);
            Console.WriteLine("Введите \'stop\' чтобы отобразить объем скопированных файлов.");
            while (Console.ReadLine() != "stop")
            {
            };
            Console.WriteLine("Общий объем скопированных данных состовляет (в байтах): {0}, нажмите любую клавишу чтобы выйти..", Options.catalogSize);
            Console.ReadKey();
        }

        public static void Reading(object source)
        {

            if (Options.sourc.GetFiles().Length <= Options.count)
            {
                if (Options.sourc.GetFiles().Length == Options.count)
                {
                    return;
                }
                else
                {
                    Options.count = Options.sourc.GetFiles().Length;
                    return;
                }
            }
            foreach (FileInfo itemin in Options.sourc.GetFiles())
            {
                if (File.Exists(Options.destin + @"\" + itemin.Name))
                {
                }
                else
                {
                    itemin.CopyTo(Options.destin + @"\" + itemin.Name, true);
                    Options.catalogSize =+ itemin.Length;
                    if (Options.P == true)
                    {
                        Console.WriteLine("Файл" + itemin.Name + " считан");
                    }
                    if (Options.D == true)
                    {
                        File.Delete(Options.sourc + @"\" + itemin.Name);
                        Options.count--;
                    }
                }
            }
        }
    }
}
