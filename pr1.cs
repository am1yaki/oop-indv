using System;

namespace Sample03
{
    class TTriangle
    {
        // Поля для зберігання довжин сторін трикутника
        public double a, b, c;

        // Конструктор без параметрів
        public TTriangle()
        {
            a = 3.0;
            b = 4.0;
            c = 5.0;
        }

        // Конструктор з параметрами
        public TTriangle(double sideA, double sideB, double sideC)
        {
            if (IsValidTriangle(sideA, sideB, sideC))
            {
                a = sideA;
                b = sideB;
                c = sideC;
            }
            else
            {
                Console.WriteLine("Попередження: трикутник з такими сторонами не існує! Встановлено значення за замовчуванням (3, 4, 5).");
                a = 3.0;
                b = 4.0;
                c = 5.0;
            }
        }

        // Конструктор копіювання
        public TTriangle(TTriangle other)
        {
            a = other.a;
            b = other.b;
            c = other.c;
        }

        // Перевірка нерівності трикутника
        private bool IsValidTriangle(double sideA, double sideB, double sideC)
        {
            return (sideA > 0 && sideB > 0 && sideC > 0) &&
                   (sideA + sideB > sideC) &&
                   (sideA + sideC > sideB) &&
                   (sideB + sideC > sideA);
        }

        // Введення даних
        public void Input()
        {
            Console.WriteLine("Введіть сторони трикутника:");
            Console.Write("Сторона a: ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Сторона b: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Сторона c: ");
            c = Convert.ToDouble(Console.ReadLine());

            if (!IsValidTriangle(a, b, c))
            {
                Console.WriteLine("Помилка: трикутник з такими сторонами не може існувати!");
            }
        }

        // Виведення даних
        public void Display()
        {
            Console.WriteLine($"Трикутник зі сторонами: a = {a}, b = {b}, c = {c}");
        }

        // Визначення периметру
        public double GetPerimeter()
        {
            return a + b + c;
        }

        // Визначення площі (за формулою Герона)
        public double GetArea()
        {
            double p = GetPerimeter() / 2.0;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        // Порівняння з іншим трикутником (за площею)
        public int CompareTo(TTriangle other)
        {
            double area1 = this.GetArea();
            double area2 = other.GetArea();

            if (area1 > area2) return 1;
            if (area1 < area2) return -1;
            return 0;
        }

        // Перевантаження оператора '+'
        public static TTriangle operator +(TTriangle t1, TTriangle t2)
        {
            return new TTriangle(t1.a + t2.a, t1.b + t2.b, t1.c + t2.c);
        }

        // Перевантаження оператора '-'
        public static TTriangle operator -(TTriangle t1, TTriangle t2)
        {
            return new TTriangle(t1.a - t2.a, t1.b - t2.b, t1.c - t2.c);
        }

        // Перевантаження оператора '*' (множення сторін на число)
        public static TTriangle operator *(TTriangle t, double number)
        {
            return new TTriangle(t.a * number, t.b * number, t.c * number);
        }

        public static TTriangle operator *(double number, TTriangle t)
        {
            return new TTriangle(t.a * number, t.b * number, t.c * number);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Тестування конструкторів та методів ---");
            TTriangle t1 = new TTriangle();
            t1.Display();
            Console.WriteLine($"Периметр: {t1.GetPerimeter():F2}");
            Console.WriteLine($"Площа: {t1.GetArea():F2}\n");

            TTriangle t2 = new TTriangle(6, 8, 10);
            t2.Display();
            Console.WriteLine($"Периметр: {t2.GetPerimeter():F2}");
            Console.WriteLine($"Площа: {t2.GetArea():F2}\n");

            TTriangle t3 = new TTriangle(t1);
            Console.WriteLine("Скопійований трикутник t3:");
            t3.Display();
            Console.WriteLine();

            Console.WriteLine("--- Тестування перевантаження операторів ---");
            TTriangle tSum = t1 + t2;
            Console.Write("Результат t1 + t2: ");
            tSum.Display();

            TTriangle tDiff = t2 - t1;
            Console.Write("Результат t2 - t1: ");
            tDiff.Display();

            TTriangle tMult = t1 * 2.0;
            Console.Write("Результат t1 * 2.0: ");
            tMult.Display();

            Console.WriteLine("\n--- Порівняння трикутників ---");
            int comparison = t1.CompareTo(t2);
            if (comparison > 0)
                Console.WriteLine("Площа t1 більша за площу t2");
            else if (comparison < 0)
                Console.WriteLine("Площа t1 менша за площу t2");
            else
                Console.WriteLine("Площі t1 та t2 рівні");

            Console.ReadKey();
        }
    }
}