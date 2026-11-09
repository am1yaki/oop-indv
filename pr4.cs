using System;

namespace PracticalWork4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("       ПРАКТИЧНА РОБОТА №4: РЕКУРСІЯ В C#          ");
            Console.WriteLine("==================================================\n");

            // --- ПРИКЛАД 1: Арифметична прогресія ---
            Console.WriteLine("--- Приклад 1: Арифметична прогресія (n перших членів) ---");
            uint n1 = 5;
            Console.WriteLine($"Перші {n1} членів прогресії (1, 2, 3...):");
            for (uint i = 0; i < n1; i++)
            {
                Console.Write(ArithmeticProgression(i) + (i + 1 == n1 ? "" : ", "));
            }
            Console.WriteLine("\n");

            // --- ПРИКЛАД 2: Сума членів арифметичної прогресії ---
            Console.WriteLine("--- Приклад 2: Сума n перших членів прогресії ---");
            uint n2 = 4;
            uint d = 2; // різниця прогресії
            uint sum = 0;
            for (uint i = 0; i < n2; i++)
            {
                sum += ArithmeticProgressionStep(i, d);
            }
            Console.WriteLine($"Сума {n2} перших членів прогресії з кроком {d} рівна {sum}\n");

            // --- ПРИКЛАД 3: Переворот рядка ---
            Console.WriteLine("--- Приклад 3: Рекурсивний переворот рядка ---");
            string originalStr = "абвгд";
            string reversedStr = ReverseString(originalStr);
            Console.WriteLine($"Оригінал: \"{originalStr}\" => Перевернутий: \"{reversedStr}\"\n");

            // --- ПРИКЛАД 4: Індекс максимального елемента масиву ---
            Console.WriteLine("--- Приклад 4: Індекс максимального елемента масиву ---");
            int[] array = { 0, 5, 6, 1, 9, 7, 8, 3, 2, 4 };
            Console.WriteLine($"Масив: [{string.Join(", ", array)}]");
            int maxIndex = IndexOfMax(array, array.Length - 1);
            Console.WriteLine($"Індекс максимального елемента: {maxIndex} (значення: {array[maxIndex]})\n");

            // --- ДОДАТКОВЕ ЗАВАНТАЖЕННЯ: Рекурсивна сума елементів масиву ---
            Console.WriteLine("--- Додаткове завдання: Рекурсивна сума елементів масиву ---");
            int[] numbers = { 2, 4, 6, 8, 10 };
            Console.WriteLine($"Масив: [{string.Join(", ", numbers)}]");
            int arraySum = RecursiveArraySum(numbers, numbers.Length - 1);
            Console.WriteLine($"Сума елементів масиву (рекурсивно): {arraySum}");

            Console.WriteLine("\n==================================================");
            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }

        // --- Реалізація Прикладу 1 ---
        static uint ArithmeticProgression(uint n)
        {
            if (n == 0)
            {
                return 1;
            }
            return ArithmeticProgression(n - 1) + 1;
        }

        // --- Реалізація Прикладу 2 ---
        static uint ArithmeticProgressionStep(uint n, uint d)
        {
            if (n == 0)
            {
                return d;
            }
            return ArithmeticProgressionStep(n - 1, d) + d;
        }

        // --- Реалізація Прикладу 3 ---
        static string ReverseString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            return ReverseString(s.Substring(1)) + s[0].ToString();
        }

        // --- Реалізація Прикладу 4 ---
        static int IndexOfMax(int[] array, int len)
        {
            if (len == 0)
            {
                return len;
            }
            var i = IndexOfMax(array, len - 1);
            return array[len] > array[i] ? len : i;
        }

        // --- Додаткове завдання: Рекурсивне обчислення суми елементів одновимірного масиву ---
        static int RecursiveArraySum(int[] arr, int index)
        {
            // Базовий випадок: якщо дійшли до початку масиву
            if (index == 0)
            {
                return arr[0];
            }
            // Рекурсивний випадок: поточний елемент + сума попередніх
            return arr[index] + RecursiveArraySum(arr, index - 1);
        }
    }
}