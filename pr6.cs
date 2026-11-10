using System;

namespace PracticalWork6
{
    // ==========================================
    // ЗАВДАННЯ 1: Власний виняток для перевірки оцінки
    // ==========================================
    public class InvalidGradeException : Exception
    {
        public InvalidGradeException(string message) : base(message) { }
    }

    // ==========================================
    // ЗАВДАННЯ 2: Власний виняток для перевірки залишку товару
    // ==========================================
    public class InsufficientStockException : Exception
    {
        public int AvailableStock { get; }
        public int RequestedQuantity { get; }

        public InsufficientStockException(string message, int availableStock, int requestedQuantity) : base(message)
        {
            AvailableStock = availableStock;
            RequestedQuantity = requestedQuantity;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Встановлюємо кодування для коректного виведення українських літер
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("  ПРАКТИЧНА РОБОТА №6: ОБРОБЛЕННЯ ВИНЯТКІВ        ");
            Console.WriteLine("==================================================\n");

            // --- ТЕСТУВАННЯ ЗАВДАННЯ 1 ---
            Console.WriteLine("--- Завдання 1: Перевірка оцінки студента ---");
            TestGradeCheck();

            Console.WriteLine("\n--------------------------------------------------\n");

            // --- ТЕСТУВАННЯ ЗАВДАННЯ 2 ---
            Console.WriteLine("--- Завдання 2: Перевірка залишку товару на складі ---");
            TestStockCheck();

            Console.WriteLine("\n==================================================");
            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }

        // Метод для перевірки оцінки
        static void TestGradeCheck()
        {
            try
            {
                Console.Write("Введіть оцінку студента (від 0 до 100): ");
                int grade = Convert.ToInt32(Console.ReadLine());

                if (grade < 0 || grade > 100)
                {
                    // Генеруємо власний виняток, якщо оцінка поза межами
                    throw new InvalidGradeException($"Помилка: оцінка {grade} є неприпустимою. Вона має бути в межах від 0 до 100.");
                }

                Console.WriteLine($"Успіх! Введена коректна оцінка: {grade}");
            }
            catch (InvalidGradeException ex)
            {
                // Перехоплення нашого кастомного винятку
                Console.WriteLine($"[Спіймано власний виняток]: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("[Помилка]: Будь ласка, введіть ціле число.");
            }
        }

        // Метод для перевірки залишку товару
        static void TestStockCheck()
        {
            int stockQuantity = 25; // На складі є 25 одиниць товару
            Console.WriteLine($"На складі є в наявності: {stockQuantity} од. товару.");

            try
            {
                Console.Write("Введіть кількість товарів для продажу: ");
                int requested = Convert.ToInt32(Console.ReadLine());

                if (requested > stockQuantity)
                {
                    // Генеруємо власний виняток із передачею деталей
                    throw new InsufficientStockException(
                        "Помилка: недостатньо товару на складі для виконання замовлення.", 
                        stockQuantity, 
                        requested
                    );
                }

                stockQuantity -= requested;
                Console.WriteLine($"Успіх! Товар успішно продано. Залишок на складі: {stockQuantity} од.");
            }
            catch (InsufficientStockException ex)
            {
                // Обробка кастомного винятку з доступом до додаткових властивостей
                Console.WriteLine($"[Спіймано власний виняток]: {ex.Message}");
                Console.WriteLine($"   > Замовлено: {ex.RequestedQuantity}, доступно на складі: {ex.AvailableStock}");
            }
            catch (FormatException)
            {
                Console.WriteLine("[Помилка]: Будь ласка, введіть числове значення.");
            }
        }
    }
}