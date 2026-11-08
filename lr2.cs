using System;
using System.IO;

namespace Lab2_Variant30
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==========================================================");
                Console.WriteLine("       ЛАБОРАТОРНА РОБОТА №2 — ВАРІАНТ 30 (Кураженко М.)");
                Console.WriteLine("==========================================================");
                Console.WriteLine("1. Завдання 1 (Варіант 30): Сер. арифм. додатних елементів вище головної діагоналі Y(4,4) та D(8,8)");
                Console.WriteLine("2. Завдання 2: Робота з двовимірним масивом N x M (Додатні елементи та сер. арифм.)");
                Console.WriteLine("3. Завдання 1 (Зчитування масиву з файлу dat.txt)");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("==========================================================");
                Console.Write("Виберіть пункт меню: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunTask1Auto();
                        break;
                    case "2":
                        RunTask2Custom();
                        break;
                    case "3":
                        RunTask1FromFile();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("\nНевірний вибір. Натисніть Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        #region Завдання 1

        /// <summary>
        /// Автоматичне виконання Завдання 1 з випадковими масивами Y(4,4) та D(8,8)
        /// </summary>
        static void RunTask1Auto()
        {
            Console.Clear();
            Console.WriteLine("--- ЗАВДАННЯ 1 (Варіант 30) ---");

            Random rnd = new Random();

            // Масив Y(4,4)
            double[,] Y = GenerateMatrix(4, 4, -10, 20, rnd);
            Console.WriteLine("\nМатриця Y(4,4):");
            PrintMatrix(Y);

            double avgY = GetAveragePositiveAboveDiagonal(Y);
            Console.WriteLine($"\nСереднє арифметичне додатних елементів вище головної діагоналі Y: {avgY:F2}");

            // Масив D(8,8)
            double[,] D = GenerateMatrix(8, 8, -10, 20, rnd);
            Console.WriteLine("\nМатриця D(8,8):");
            PrintMatrix(D);

            double avgD = GetAveragePositiveAboveDiagonal(D);
            Console.WriteLine($"\nСереднє арифметичне додатних елементів вище головної діагоналі D: {avgD:F2}");

            Console.WriteLine("\nНатисніть Enter для повернення в меню...");
            Console.ReadLine();
        }

        /// <summary>
        /// Виконання Завдання 1 зі зчитуванням масиву Y(4,4) з текстового файлу dat.txt
        /// </summary>
        static void RunTask1FromFile()
        {
            Console.Clear();
            Console.WriteLine("--- ЗАВДАННЯ 1 (Зчитування з файлу dat.txt) ---");

            string filePath = "dat.txt";

            if (!File.Exists(filePath))
            {
                // Якщо файлу немає, створимо приклад
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        sw.WriteLine(rndGlobal.Next(-5, 15));
                    }
                }
                Console.WriteLine($"Файл {filePath} створено з тестовими даними.");
            }

            try
            {
                double[,] Y = new double[4, 4];
                using (StreamReader sr = new StreamReader(filePath))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            string line = sr.ReadLine();
                            if (line != null)
                                Y[i, j] = Convert.ToDouble(line);
                        }
                    }
                }

                Console.WriteLine("\nМатриця Y(4,4), зчитана з файлу:");
                PrintMatrix(Y);

                double avgY = GetAveragePositiveAboveDiagonal(Y);
                Console.WriteLine($"\nСереднє арифметичне додатних елементів вище головної діагоналі: {avgY:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання файлу: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть Enter для повернення в меню...");
            Console.ReadLine();
        }

        /// <summary>
        /// Метод обчислення середнього арифметичного додатних елементів вище головної діагоналі (j > i)
        /// </summary>
        static double GetAveragePositiveAboveDiagonal(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            double sum = 0;
            int count = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = i + 1; j < cols; j++) // j > i означає елементи суворо вище головної діагоналі
                {
                    if (matrix[i, j] > 0)
                    {
                        sum += matrix[i, j];
                        count++;
                    }
                }
            }

            if (count == 0) return 0;
            return sum / count;
        }

        #endregion

        #region Завдання 2

        /// <summary>
        /// Виконання Завдання 2 з довільною розмірністю N x M
        /// </summary>
        static void RunTask2Custom()
        {
            Console.Clear();
            Console.WriteLine("--- ЗАВДАННЯ 2 (Матриця N x M) ---");

            Console.Write("Введіть кількість рядків N: ");
            int n = int.Parse(Console.ReadLine() ?? "3");

            Console.Write("Введіть кількість стовпців M: ");
            int m = int.Parse(Console.ReadLine() ?? "4");

            double[,] matrix = GenerateMatrix(n, m, -20, 30, rndGlobal);

            Console.WriteLine($"\nЗгенерована матриця A({n},{m}):");
            PrintMatrix(matrix);

            double sum = 0;
            int count = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        sum += matrix[i, j];
                        count++;
                    }
                }
            }

            Console.WriteLine("\n--- РЕЗУЛЬТАТИ ОБЧИСЛЕННЯ ---");
            Console.WriteLine($"Кількість додатних елементів: {count}");
            Console.WriteLine($"Сума додатних елементів: {sum:F2}");
            if (count > 0)
            {
                Console.WriteLine($"Середнє арифметичне додатних елементів: {(sum / count):F4}");
            }
            else
            {
                Console.WriteLine("Додатних елементів не знайдено.");
            }

            Console.WriteLine("\nНатисніть Enter для повернення в меню...");
            Console.ReadLine();
        }

        #endregion

        #region Допоміжні методи

        static Random rndGlobal = new Random();

        static double[,] GenerateMatrix(int rows, int cols, int minVal, int maxVal, Random rnd)
        {
            double[,] res = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    res[i, j] = rnd.Next(minVal, maxVal);
                }
            }
            return res;
        }

        static void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],7:F0}");
                }
                Console.WriteLine();
            }
        }

        #endregion
    }
}