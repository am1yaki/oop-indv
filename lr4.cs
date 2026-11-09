using System;

namespace Lab4_Variant30
{
    class Program
    {
        // 1. Сортування вибором (Selection Sort)
        // Оцінка складності: O(n^2) у всіх випадках
        static void SelectionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                // Обмін елементів
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }

        // 2. Бінарний пошук та підрахунок кількості входжень ключа
        // Оцінка складності бінарного пошуку: O(log n)
        static int CountOccurrencesBinarySearch(int[] arr, int key)
        {
            int left = 0;
            int right = arr.Length - 1;
            int foundIndex = -1;

            // Знаходимо будь-яке входження елемента за допомогою бінарного пошуку
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] == key)
                {
                    foundIndex = mid;
                    break;
                }
                else if (arr[mid] < key)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            // Якщо елемент не знайдено
            if (foundIndex == -1)
                return 0;

            // Оскільки в масиві можуть бути дублікати, розширюємо пошук ліворуч і праворуч від знайненого індексу
            int count = 1;
            
            int l = foundIndex - 1;
            while (l >= 0 && arr[l] == key)
            {
                count++;
                l--;
            }

            int r = foundIndex + 1;
            while (r < arr.Length && arr[r] == key)
            {
                count++;
                r++;
            }

            return count;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random rnd = new Random();

            // Розрахунок розміру масиву за формулою: N = 20 + 0.6 * K (де K = 30)
            int K = 30;
            int N = (int)(20 + 0.6 * K); // 38

            int[] array = new int[N];

            // Заповнення масиву випадковими числами в діапазоні [10, 100]
            for (int i = 0; i < N; i++)
            {
                array[i] = rnd.Next(10, 101);
            }

            Console.WriteLine($"=== ВИХІДНИЙ МАСИВ (Розмір N = {N}) ===");
            Console.WriteLine(string.Join(", ", array));

            // Сортування масиву
            SelectionSort(array);

            Console.WriteLine("\n=== ВІДСОРТОВАНИЙ МАСИВ (Сортування вибором) ===");
            Console.WriteLine(string.Join(", ", array));

            // Введення ключового значення для пошуку
            Console.Write("\nВведіть число для пошуку (від 10 до 100): ");
            if (int.TryParse(Console.ReadLine(), out int searchKey))
            {
                int occurrences = CountOccurrencesBinarySearch(array, searchKey);
                Console.WriteLine($"Результат: число {searchKey} зустрічається в масиві {occurrences} раз(и).");
            }
            else
            {
                Console.WriteLine("Помилка: введено некоректне число.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}