using System;
using System.Collections.Generic;

namespace TaskInd
{
    // Клас Пасажир
    public class Passenger
    {
        public int Number { get; }

        public Passenger(int number)
        {
            Number = number;
        }
    }

    // Клас Водій
    public class Driver
    {
        public string Name { get; }

        public Driver(string name)
        {
            Name = name;
        }

        // Обробник події Event1: Маршрутка заповнена
        public void DriveRoute()
        {
            Console.WriteLine($"Водій {Name}: їду за маршрутом");
        }

        // Обробник події Event2: Маршрутка порожня
        public void DriveToPark()
        {
            Console.WriteLine($"Водій {Name}: їду до парку");
        }
    }

    // Клас Маршрутка
    public class Route
    {
        // Оголошення делегатів та подій
        public delegate void RouteHandler();
        public static event RouteHandler Event1;
        public static event RouteHandler Event2;

        private int capacity;
        private List<Passenger> passengers;
        private bool hasPlaces;

        public bool HasPlaces => hasPlaces;

        public Route(int capacity)
        {
            this.capacity = capacity;
            this.passengers = new List<Passenger>();
            this.hasPlaces = true;
        }

        // Метод 1: Посадка пасажира
        public void Metod1(Passenger passenger)
        {
            if (passengers.Count < capacity)
            {
                passengers.Add(passenger);
                Console.WriteLine($"Пасажир {passenger.Number} сів");

                if (passengers.Count == capacity)
                {
                    hasPlaces = false;
                    Event1?.Invoke(); // Генерація події заповнення
                }
            }
        }

        // Метод 2: Вихід пасажира
        public void Metod2(int index)
        {
            if (passengers.Count > 0)
            {
                Passenger exitingPassenger = passengers[passengers.Count - 1];
                passengers.RemoveAt(passengers.Count - 1);
                Console.WriteLine($"Пасажир {exitingPassenger.Number} вийшов");

                if (passengers.Count == 0)
                {
                    Event2?.Invoke(); // Генерація події, коли всі вийшли
                }
            }
        }

        // Статичні обробники подій від самого класу Route
        public static void OnEvent1()
        {
            Console.WriteLine("Усі місця зайняті");
        }

        public static void OnEvent2()
        {
            Console.WriteLine("Усі місця вільні");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Встановлюємо кодування для коректного виведення українських літер
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int seats = 10;
            Console.WriteLine($"Кількість місць у маршрутці: {seats}\n");

            // Створення об'єктів маршрутки та водія
            Route route = new Route(seats);
            Driver driver = new Driver("Вася");

            // Підписка на події
            Route.Event1 += Route.OnEvent1;
            Route.Event1 += driver.DriveRoute;

            Route.Event2 += Route.OnEvent2;
            Route.Event2 += driver.DriveToPark;

            // Цикл 1: Поки є місця в маршрутці – садимо пасажирів
            int i = 1;
            while (route.HasPlaces)
            {
                Passenger p = new Passenger(i);
                route.Metod1(p);
                i++;
            }

            // Цикл 2: Поки в маршрутці є пасажири – вони виходять
            int currentPassengersCount = seats;
            while (currentPassengersCount > 0)
            {
                route.Metod2(currentPassengersCount);
                currentPassengersCount--;
            }

            Console.ReadKey();
        }
    }
}