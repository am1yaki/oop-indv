using System;

namespace DesignPatternsApp
{
    // ==========================================
    // 1. ПОРОДЖУВАЛЬНИЙ ПАТЕРН: Singleton (Одинак)
    // ==========================================
    public sealed class AppSettings
    {
        private static AppSettings _instance = null;
        public string Theme { get; set; }

        private AppSettings() 
        { 
            Theme = "Світла"; 
        }

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettings();
                }
                return _instance;
            }
        }
    }

    // ==========================================
    // 2. СТРУКТУРНИЙ ПАТЕРН: Adapter (Адаптер)
    // ==========================================
    public interface IModernPayment
    {
        string ProcessPayment(decimal amount);
    }

    // Існуючий клас із застарілим інтерфейсом
    public class LegacyBillingSystem
    {
        public string MakeOldPayment(double sum)
        {
            return $"[Стара система] Оплачено через застарілий шлюз суму: {sum} грн.";
        }
    }

    // Адаптер для сумісності
    public class PaymentAdapter : IModernPayment
    {
        private readonly LegacyBillingSystem _legacySystem;

        public PaymentAdapter(LegacyBillingSystem legacySystem)
        {
            _legacySystem = legacySystem;
        }

        public string ProcessPayment(decimal amount)
        {
            return _legacySystem.MakeOldPayment((double)amount);
        }
    }

    // ==========================================
    // 3. ПОВЕДІНКОВИЙ ПАТЕРН: Strategy (Стратегія)
    // ==========================================
    public interface IPaymentStrategy
    {
        string Pay(int amount);
    }

    public class CreditCardStrategy : IPaymentStrategy
    {
        public string Pay(int amount)
        {
            return $"Сплачено {amount} грн. за допомогою банківської картки.";
        }
    }

    public class PayPalStrategy : IPaymentStrategy
    {
        public string Pay(int amount)
        {
            return $"Сплачено {amount} грн. через систему PayPal.";
        }
    }

    public class ShoppingCart
    {
        private IPaymentStrategy _strategy;

        public void SetStrategy(IPaymentStrategy strategy)
        {
            _strategy = strategy;
        }

        public string Checkout(int amount)
        {
            if (_strategy == null) return "Стратегія оплати не обрана!";
            return _strategy.Pay(amount);
        }
    }

    // ==========================================
    // ГОЛОВНА ПРОГРАМА ДЛЯ ТЕСТУВАННЯ
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("   ПРАКТИЧНА РОБОТА №3: ШАБЛОНИ ПРОЄКТУВАННЯ GoF  ");
            Console.WriteLine("==================================================\n");

            // 1. Тест Singleton
            Console.WriteLine("--- 1. Породжувальний патерн: Singleton ---");
            AppSettings s1 = AppSettings.Instance;
            AppSettings s2 = AppSettings.Instance;

            s1.Theme = "Темна";
            Console.WriteLine($"Тема об'єкта s1: {s1.Theme}");
            Console.WriteLine($"Тема об'єкта s2 (змінилася автоматично): {s2.Theme}");
            Console.WriteLine($"Чи є s1 та s2 одним і тим самим об'єктом? {(s1 == s2 ? "Так" : "Ні")}\n");

            // 2. Тест Adapter
            Console.WriteLine("--- 2. Структурний патерн: Adapter ---");
            IModernPayment paymentService = new PaymentAdapter(new LegacyBillingSystem());
            string paymentResult = paymentService.ProcessPayment(1500.50m);
            Console.WriteLine(paymentResult + "\n");

            // 3. Тест Strategy
            Console.WriteLine("--- 3. Поведінковий патерн: Strategy ---");
            ShoppingCart cart = new ShoppingCart();

            cart.SetStrategy(new CreditCardStrategy());
            Console.WriteLine(cart.Checkout(500));

            cart.SetStrategy(new PayPalStrategy());
            Console.WriteLine(cart.Checkout(1200));

            Console.WriteLine("\n==================================================");
            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}