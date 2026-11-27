using System;

class Program
{
    static void Main()
    {
        bool rabotaem = true;

        while (rabotaem)
        {
            Console.WriteLine("МЕНЮ");
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Выход");

            int punkt = VvodCelogoChisla("Выберите пункт: ");

            switch (punkt)
            {
                case 1:
                    IgraUgadai();
                    break;
                case 2:
                    AboutAvtor();
                    break;
                case 3:
                    MenuSortirovki();
                    break;
                case 4:
                    rabotaem = !PodtverditVyhod();
                    break;
            }
        }
    }

    static void AboutAvtor()
    {
        Console.WriteLine("Тонконогова Софья Романовна, 6104-090301D");
    }

    static int VvodCelogoChisla(string podskazka)
    {
        Console.Write(podskazka);
        uint chislo;
        bool uspekh = uint.TryParse(Console.ReadLine(), out chislo);
        while (!uspekh)
        {
            Console.WriteLine("Ошибка! Введите целое число.");
            Console.Write(podskazka);
            uspekh = uint.TryParse(Console.ReadLine(), out chislo);
        }
        return (int)chislo;
    }

    static double VvodDrobi(string podskazka)
    {
        Console.Write(podskazka);
        double chislo;
        bool uspekh = double.TryParse(Console.ReadLine(), out chislo);
        while (!uspekh)
        {
            Console.WriteLine("Ошибка! Введите число.");
            Console.Write(podskazka);
            uspekh = double.TryParse(Console.ReadLine(), out chislo);
        }
        return chislo;
    }

    static int VvodRazmeraMassiva()
    {
        int razmer = VvodCelogoChisla("Введите длину массива (целое число больше 0): ");
            
        return razmer;
    }

    static void IgraUgadai()
    {
        Console.WriteLine("f = sqrt(((sin a)^2 + (cos b)^3) / ((sin a)^3 - (cos b)^2))");

        double a = VvodDrobi("Введите a: ");
        double b = VvodDrobi("Введите b: ");

        double pravilnyjOtvet = PoschitatFunkciyu(a, b);

        if (double.IsNaN(pravilnyjOtvet))
        {
            Console.WriteLine("Ошибка: функция не существует при таких значениях.");
        }
        else
        {
            Ugadyvat(pravilnyjOtvet);
        }
    }

    static double PoschitatFunkciyu(double a, double b)
    {
        double sinA = Math.Sin(a);
        double cosB = Math.Cos(b);

        double chislitel = Math.Pow(sinA, 2) + Math.Pow(cosB, 3);
        double znamenatel = Math.Pow(sinA, 3) - Math.Pow(cosB, 2);

        if (znamenatel == 0) return double.NaN;
        if (chislitel / znamenatel < 0) return double.NaN;

        return Math.Round(Math.Sqrt(chislitel / znamenatel), 2);
    }

    static void Ugadyvat(double pravilnyjOtvet)
    {
        int popytki = 3;
        bool ugadal = false;

        while (popytki > 0 && !ugadal)
        {
            Console.WriteLine("Попыток осталось: " + popytki);
            double otvetStudenta = VvodDrobi("Ваш ответ: ");

            if (Math.Round(otvetStudenta, 2) == pravilnyjOtvet)
            {
                Console.WriteLine("Поздравляю, вы угадали!");
                ugadal = true;
            }
            else
            {
                popytki = popytki - 1;
            }
        }

        if (!ugadal)
        {
            Console.WriteLine("Вы проиграли. Правильный ответ: " + pravilnyjOtvet);
        }
    }

    static bool PodtverditVyhod()
    {
        bool vyhod = false;
        bool otvetPoluchen = false;

        while (!otvetPoluchen)
        {
            Console.Write("Вы точно хотите выйти? (д/н): ");
            string otvet = Console.ReadLine();

            if (otvet == "д")
            {
                vyhod = true;
                otvetPoluchen = true;
            }
            else if (otvet == "н")
            {
                vyhod = false;
                otvetPoluchen = true;
            }
            else
            {
                Console.WriteLine("Ошибка. Введите д или н.");
            }
        }
        return vyhod;
    }

    static void MenuSortirovki()
    {
        int razmer = VvodRazmeraMassiva();

        int[] ishodnyjMassiv = SozdatMassiv(razmer);

        Console.WriteLine("Массив до сортировки:");
        VyvestiMassiv(ishodnyjMassiv);

        int[] massivShellya = SkopirovatMassiv(ishodnyjMassiv);
        int[] massivVstavkami = SkopirovatMassiv(ishodnyjMassiv);

        DateTime startShellya = DateTime.Now;
        SortirovkaShellya(massivShellya);
        DateTime endShellya = DateTime.Now;

        DateTime startVstavkami = DateTime.Now;
        SortirovkaVstavkami(massivVstavkami);
        DateTime endVstavkami = DateTime.Now;

        Console.WriteLine("Массив после сортировки Шелла:");
        VyvestiMassiv(massivShellya);

        Console.WriteLine("Массив после сортировки вставками:");
        VyvestiMassiv(massivVstavkami);

        TimeSpan vremyaShellya = endShellya - startShellya;
        TimeSpan vremyaVstavkami = endVstavkami - startVstavkami;

        Console.WriteLine("\nВремя Шелла (мс): " + vremyaShellya.TotalMilliseconds);
        Console.WriteLine("Время Вставок (мс): " + vremyaVstavkami.TotalMilliseconds);
    }

    static int[] SozdatMassiv(int razmer)
    {
        Random sluchaj = new Random();
        int[] massiv = new int[razmer];
        for (int i = 0; i < razmer; i++)
        {
            massiv[i] = sluchaj.Next(-100, 100);
        }
        return massiv;
    }

    static int[] SkopirovatMassiv(int[] staryj)
    {
        int[] novyj = new int[staryj.Length];
        for (int i = 0; i < staryj.Length; i++)
        {
            novyj[i] = staryj[i];
        }
        return novyj;
    }

    static void VyvestiMassiv(int[] massiv)
    {
        if (massiv.Length > 10)
        {
            Console.WriteLine("Массив слишком длинный (больше 10) — вывод на экран не может быть.");
        }
        else
        {
            for (int i = 0; i < massiv.Length; i++)
            {
                Console.Write(massiv[i] + " ");
            }
            Console.WriteLine();
        }
    }

    static void SortirovkaVstavkami(int[] massiv)
    {
        for (int i = 1; i < massiv.Length; i++)
        {
            int vstavlyaemyj = massiv[i];
            int j = i - 1;

            while (j >= 0 && massiv[j] > vstavlyaemyj)
            {
                massiv[j + 1] = massiv[j];
                j = j - 1;
            }

            massiv[j + 1] = vstavlyaemyj;
        }
    }

    static void SortirovkaShellya(int[] massiv)
    {
        int n = massiv.Length;
        int shag = n / 2;

        while (shag > 0)
        {
            for (int i = shag; i < n; i++)
            {
                int vstavlyaemyj = massiv[i];
                int j = i;

                while (j >= shag && massiv[j - shag] > vstavlyaemyj)
                {
                    massiv[j] = massiv[j - shag];
                    j = j - shag;
                }

                massiv[j] = vstavlyaemyj;
            }

            shag = shag / 2;
        }
    }
}