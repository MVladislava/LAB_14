using System;
using System.Text.RegularExpressions;
using Cars;
using LAB_14;
using LAB_12_4;
using LAB_12_3;
class Program
{
    static void Main(string[] args)
    {
        List<Car> f1WSh = Method.MakeColl(5);
        List<Car> f2WSh = Method.MakeColl(5);
        Queue<List<Car>> carsFactory = new Queue<List<Car>>();
        carsFactory.Enqueue(f1WSh);
        carsFactory.Enqueue(f2WSh);
        int lenght = 10;
        MyCollection<Car> myColl = new MyCollection<Car>(lenght);

        Menu choise = new Menu();
        while (true)
        {
            Console.WriteLine("\n--- Главное меню ---");
            Console.WriteLine("1. Запросы с использованием LINQ");
            Console.WriteLine("2. ЛАБ12 Запросы с использованием  LINQ ");
            Console.WriteLine("8. Выйти из программы");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    LinqMenu();
                    break;
                case 2:
                    Lab12Menu();
                    break;
                case 8:
                    return;
            }
        }
        void LinqMenu()
        {
            int factoryNumber = 1;

            foreach (var factory in carsFactory)
            {
                var workshops = factory;
                Console.WriteLine($"Завод 1, Цех {factoryNumber}:");
                foreach (var car in workshops)
                {
                    Console.WriteLine(car);
                }
                factoryNumber++;
            }
            while (true)
            {
                Console.WriteLine();
                choise.MenuChoise();
                var str = Console.ReadLine();
                int l = int.Parse(str);
                Console.Clear();
                switch (l)
                {
                    case 0:
                        int factoryNumber1 = 1;
                        foreach (var factory in carsFactory)
                        {
                            var workshops = factory;
                            Console.WriteLine($"Завод 1, Цех {factoryNumber1}:");
                            foreach (var car in workshops)
                            {
                                Console.WriteLine(car);
                            }
                            factoryNumber1++;
                        }
                        break;
                    case 1:
                        Method.Where(carsFactory);
                        break;
                    case 2:
                        Method.Union(carsFactory);
                        break;
                    case 3:
                        Method.Sum(carsFactory);
                        Method.Max(carsFactory);
                        break;
                    case 4:
                        Method.GroupBy(carsFactory);
                        break;
                    case 5:
                        Method.Let(carsFactory);
                        break;
                    case 8:
                        return;
                }
            }
        }
        void Lab12Menu()
        {
            myColl.ShowTree();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($@" Меню:
1. Where авто. дорожный просвет свыше 150.
2. Количество
3. Сумма цен и максимальная цена автомобиля
4. Группировка
8. Выйти в главное меню");
                var str = Console.ReadLine();
                int l = int.Parse(str);
                Console.Clear();
                switch (l)
                {
                    case 0:
                        myColl.ShowTree();
                        break;
                    case 1:
                        Method.WhereMyColl(myColl);
                        break;
                    case 2:
                        Method.CountQueue(myColl);
                        break;
                    case 3:
                        Method.SumMax12(myColl);
                        break;
                    case 4:
                        Method.GroupBy12(myColl);
                        break;
                    case 5:
                        Method. Contains(myColl);
                        break;
                    case 8:
                        return;
                }
            }
        }
    }
}
