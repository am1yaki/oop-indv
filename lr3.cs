using System;

namespace Lab3_Variant5
{
    // Власний інтерфейс IParent
    public interface IParent
    {
        string Info();
        double Metod();
    }

    // Клас Child1: Посібник з інвалідності
    public class Child1 : IParent, IComparable
    {
        private double pole1; // мінімальна допомога
        private int pole2;    // група інвалідності (1, 2, 3)
        private double allowance;

        public Child1(double minHelp, int group)
        {
            pole1 = minHelp;
            pole2 = group;
            allowance = Metod();
        }

        public string Info()
        {
            return "Мін. посібник=" + pole1 + " Група=" + pole2;
        }

        public double Metod()
        {
            if (pole2 == 1)
                return pole1 * 3;
            else
                return pole1 * 2;
        }

        public double GetAllowance()
        {
            return allowance;
        }

        // Сортування за групою інвалідності
        public int CompareTo(object obj)
        {
            if (obj is Child1 other)
            {
                return this.pole2.CompareTo(other.pole2);
            }
            return 0;
        }
    }

    // Клас Child2: Посібник багатодітним
    public class Child2 : IParent, IComparable
    {
        private double pole1; // мінімальна допомога
        private int pole3;    // кількість дітей
        private double allowance;

        public Child2(double minHelp, int childrenCount)
        {
            pole1 = minHelp;
            pole3 = childrenCount;
            allowance = Metod();
        }

        public string Info()
        {
            return "Мін. посібник=" + pole1 + " Дітей=" + pole3;
        }

        public double Metod()
        {
            if (pole3 >= 3 && pole3 <= 5)
                return pole1 * 1.3;
            else if (pole3 > 5)
                return pole1 * 1.5;
            else
                return pole1;
        }

        public double GetAllowance()
        {
            return allowance;
        }

        // Сортування за розміром допомоги
        public int CompareTo(object obj)
        {
            if (obj is Child2 other)
            {
                return this.allowance.CompareTo(other.allowance);
            }
            return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random rnd = new Random();

            double minHelp = 1863;

            Console.WriteLine("---- Посібник з інвалідності (Child1) ----");
            Child1[] group1 = new Child1[3];
            for (int i = 0; i < group1.Length; i++)
            {
                int group = rnd.Next(1, 4);
                group1[i] = new Child1(minHelp, group);
                
                Console.WriteLine(group1[i].Info());
                Console.WriteLine("Посібник=" + group1[i].Metod());
            }

            Array.Sort(group1);

            Console.WriteLine("\n---- Після сортування за групою інвалідності ----");
            foreach (var item in group1)
            {
                Console.WriteLine(item.Info() + " Посібник=" + item.Metod());
            }


            Console.WriteLine("\n---- Посібник багатодітним (Child2) ----");
            Child2[] group2 = new Child2[3];
            for (int i = 0; i < group2.Length; i++)
            {
                int children = rnd.Next(3, 8);
                group2[i] = new Child2(minHelp, children);

                Console.WriteLine(group2[i].Info());
                Console.WriteLine("Посібник=" + group2[i].Metod().ToString("F1"));
            }

            Array.Sort(group2);

            Console.WriteLine("\n---- Після сортування за величиною допомоги ----");
            foreach (var item in group2)
            {
                Console.WriteLine(item.Info() + " Посібник=" + item.GetAllowance().ToString("F1"));
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}