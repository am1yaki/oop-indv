using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PracticalWork9
{
    class Program
    {
        // Довготривалий метод (наприклад, симуляція складних обчислень або завантаження даних)
        // який може генерувати виняток за певних умов
        static async Task<int> CalculateAsync(int number)
        {
            // Симуляція асинхронної роботи (наприклад, запит до бази даних або мережі)
            await Task.Delay(1500);

            // Перевірка на виняток (для демонстрації обробки помилок)
            if (number < 0)
            {
                throw new ArgumentException("Помилка: Число не може бути від'ємним для обчислень!");
            }

            // Корисне обчислення (наприклад, факторіал або складне множення)
            int result = number * 100;
            return result;
        }

        static async Task Main(string[] args)
        {
            // Встановлюємо кодування для коректного виведення українських символів
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("   ПРАКТИЧНА РОБОТА №9: АСИНХРОННА ОБРОБКА В C#   ");
            Console.WriteLine("   (Варіант 9: Синхронний та асинхронний запуски) ");
            Console.WriteLine("==================================================\n");

            int inputNumber = 25;
            Stopwatch stopwatch = new Stopwatch();

            // ==========================================
            // 1. СИНХРОННИЙ ЗАПУСК МЕТОДУ
            // ==========================================
            Console.WriteLine("--- 1. Синхронне виконання методу ---");
            stopwatch.Start();
            try
            {
                // Синхронний виклик асинхронного методу через .Result (блокує потік)
                int syncResult = CalculateAsync(inputNumber).Result;
                Console.WriteLine($"[Синхронно] Результат: {syncResult}");
            }
            catch (AggregateException ex)
            {
                // Task.Result загортає винятки в AggregateException
                Console.WriteLine($"[Синхронно] Перехоплено виняток: {ex.InnerException?.Message}");
            }
            stopwatch.Stop();
            Console.WriteLine($"Час синхронного виконання: {stopwatch.ElapsedMilliseconds} мс\n");

            // ==========================================
            // 2. АСИНХРОННИЙ ЗАПУСК МЕТОДУ
            // ==========================================
            Console.WriteLine("--- 2. Асинхронне виконання методу ---");
            stopwatch.Restart();
            try
            {
                // Асинхронний виклик через await (не блокує головний потік)
                int asyncResult = await CalculateAsync(inputNumber);
                Console.WriteLine($"[Асинхронно] Результат: {asyncResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Асинхронно] Перехоплено виняток: {ex.Message}");
            }
            stopwatch.Stop();
            Console.WriteLine($"Час асинхронного виконання: {stopwatch.ElapsedMilliseconds} мс\n");

            // ==========================================
            // 3. ДЕМОНСТРАЦІЯ ОБРОБКИ ВИНЯТКІВ
            // ==========================================
            Console.WriteLine("--- 3. Демонстрація обробки винятку (некоректні дані) ---");
            try
            {
                // Передаємо від'ємне число для виклику помилки всередині методу
                int errorResult = await CalculateAsync(-5);
                Console.WriteLine($"Результат: {errorResult}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Успішно оброблено виняток]: {ex.Message}");
            }

            Console.WriteLine("\n==================================================");
            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}