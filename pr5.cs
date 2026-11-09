using System;

namespace PracticalWork5
{
    // Узагальнений клас "Трикутник" — TTriangle<T>
    // Накладаємо обмеження: тип T повинен бути структурою (або числовим типом) та підтримувати порівняння й арифметику
    public class TTriangle<T> : IComparable<TTriangle<T>> 
        where T : IComparable
    {
        public T A { get; set; }
        public T B { get; set; }
        public T C { get; set; }

        // Конструктор за замовчуванням
        public TTriangle() { }

        // Конструктор з параметрами
        public TTriangle(T a, T b, T c)
        {
            A = a;
            B = b;
            C = c;
        }

        // Конструктор копіювання
        public TTriangle(TTriangle<T> other)
        {
            A = other.A;
            B = other.B;
            C = other.C;
        }

        // Виведення даних
        public void Print()
        {
            Console.WriteLine($"Трикутник зі сторонами: a = {A}, b = {B}, c = {C}");
        }

        // Порівняння трикутників (за першою стороною для прикладу)
        public int CompareTo(TTriangle<T> other)
        {
            if (other == null) return 1;
            return A.CompareTo(other.A);
        }
    }

    // Клас-обгортка SideValue для забезпечення роботи з числовими значеннями сторін
    public struct SideValue : IComparable
    {
        public double Value;

        public SideValue(double val)
        {
            Value = val;
        }

        public int CompareTo(object obj)
        {
            if (obj is SideValue other)
            {
                return Value.CompareTo(other.Value);
            }
            return 0;
        }

        // Перевантаження операторів для зручної роботи зі сторонами
        public static SideValue operator +(SideValue s1, SideValue s2) => new SideValue(s1.Value + s2.Value);
        public static SideValue operator -(SideValue s1, SideValue s2) => new SideValue(s1.Value - s2.Value);
        public static SideValue operator *(SideValue s, double scalar) => new SideValue(s.Value * scalar);

        public override string ToString()
        {
            return Value.ToString("F2");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Встановлюємо кодування для коректного виведення українських літер
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("   ПРАКТИЧНА РОБОТА №5: УЗАГАЛЬНЕННЯ В C#         ");
            Console.WriteLine("==================================================\n");

            // Створюємо екземпляри узагальненого класу TTriangle з типом SideValue
            TTriangle<SideValue> t1 = new TTriangle<SideValue>(new SideValue(3), new SideValue(4), new SideValue(5));
            TTriangle<SideValue> t2 = new TTriangle<SideValue>(new SideValue(6), new SideValue(8), new SideValue(10));

            Console.WriteLine("--- Тестування узагальненого класу TTriangle<T> ---");
            Console.Write("t1: "); t1.Print();
            Console.Write("t2: "); t2.Print();

            // Перевірка порівняння
            int comparison = t1.CompareTo(t2);
            Console.WriteLine($"\nРезультат порівняння t1 та t2: {comparison}");

            Console.WriteLine("\n==================================================");
            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}