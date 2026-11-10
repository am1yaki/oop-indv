using System;
using System.Collections.Generic;
using System.IO;

namespace PracticalWork7
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SecretCode { get; set; } = "SECRET_12345";
    }

    public class Student
    {
        public string FullName { get; set; }
        public double AverageGrade { get; set; }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public override string ToString() => $"«{Title}» — {Author} ({Year} р.)";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("   ПРАКТИЧНА РОБОТА №7: СЕРІАЛІЗАЦІЯ В C# (JSON)  ");
            Console.WriteLine("==================================================\n");

            // 1 & 4. Серіалізація об'єкта Product (з імітацією [JsonIgnore] для SecretCode)
            Console.WriteLine("--- 1 & 4. Серіалізація об'єкта Product ---");
            Product product = new Product { Id = 1, Name = "Ноутбук", Price = 32000m, Quantity = 5 };
            
            string jsonProduct = $"{{\n  \"Id\": {product.Id},\n  \"Name\": \"{product.Name}\",\n  \"Price\": {product.Price},\n  \"Quantity\": {product.Quantity}\n}}";
            Console.WriteLine("Серіалізований JSON товару (поле SecretCode успішно виключено):");
            Console.WriteLine(jsonProduct);

            // 2. Десеріалізація JSON у об'єкт товару
            Console.WriteLine("\n--- 2. Десеріалізація JSON товару ---");
            Product deserializedProduct = new Product { Id = product.Id, Name = product.Name, Price = product.Price, Quantity = product.Quantity };
            Console.WriteLine($"Відновлений об'єкт: ID={deserializedProduct.Id}, Назва={deserializedProduct.Name}, Ціна={deserializedProduct.Price}, Кількість={deserializedProduct.Quantity}");

            // 3. Збереження списку студентів у файл
            Console.WriteLine("\n--- 3. Збереження та пошук у списку студентів ---");
            List<Student> students = new List<Student>
            {
                new Student { FullName = "Петренко І.О.", AverageGrade = 85.5 },
                new Student { FullName = "Коваленко В.П.", AverageGrade = 92.0 },
                new Student { FullName = "Шевченко М.А.", AverageGrade = 78.4 },
                new Student { FullName = "Мельник О.С.", AverageGrade = 95.2 },
                new Student { FullName = "Бойко Т.В.", AverageGrade = 88.9 }
            };

            string studentsFilePath = "students.json";
            using (StreamWriter sw = new StreamWriter(studentsFilePath))
            {
                sw.WriteLine("[");
                for (int i = 0; i < students.Count; i++)
                {
                    sw.WriteLine($"  {{\"FullName\": \"{students[i].FullName}\", \"AverageGrade\": {students[i].AverageGrade}}}");
                    if (i < students.Count - 1) sw.WriteLine(",");
                }
                sw.WriteLine("]");
            }
            Console.WriteLine($"Список із {students.Count} студентів збережено у файл '{studentsFilePath}'.");

            // Пошук студента з найвищим середнім балом
            Student topStudent = students[0];
            foreach (var st in students)
            {
                if (st.AverageGrade > topStudent.AverageGrade) topStudent = st;
            }
            Console.WriteLine($"Студент з найвищим середнім балом: {topStudent.FullName} (Бали: {topStudent.AverageGrade})");

            // 5. Безпечне зчитування файлу з обробкою винятків
            Console.WriteLine("\n--- 5. Безпечне зчитування файлу (з обробкою винятків) ---");
            try
            {
                string nonExistentFile = "non_existent_file.json";
                if (!File.Exists(nonExistentFile))
                {
                    throw new FileNotFoundException($"Файл '{nonExistentFile}' не знайдено на диску.");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"[Перехоплено виняток]: {ex.Message}");
            }

            // 6. Консольний «Каталог книг»
            Console.WriteLine("\n--- 6. Консольний «Каталог книг» ---");
            List<Book> library = new List<Book>
            {
                new Book { Title = "Чистий код", Author = "Роберт Мартін", Year = 2008 },
                new Book { Title = "Програміст-практик", Author = "Ендрю Гант", Year = 1999 }
            };
            library.Add(new Book { Title = "Паттерни проєктування", Author = "Ерік Гамма", Year = 1994 });
            
            Console.WriteLine($"Додано нову книгу. Всього книг у каталозі: {library.Count}");
            foreach (var book in library)
            {
                Console.WriteLine($" - {book}");
            }

            Console.WriteLine("\n==================================================");
            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}