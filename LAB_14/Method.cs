using Cars;
using LAB_12_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_14
{
    public class Method
    {
        public static List<Car> MakeColl(int lenght)
        {
            var list = new List<Car>();
            for (int i = 0; i < lenght; i++)
            {
                LightCar lc = new LightCar();
                lc.RandomInit();
                list.Add(lc);
            }
            return list;
        }
        public static void PrintColl(List<Car> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Empty Collection");
                return;
            }
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }

        // a)	На выборку данных (Where).
        public static IEnumerable<Car> Where(Queue<List<Car>> coll)
        {
            Console.WriteLine("Запрос на выборку данных Where:");
            var res = from car in coll
                      from item in car
                      where item is Car && item.Price > 900000
                      select item;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nАвтомобили с ценой свыше 900К. (LINQ):");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var car in res)
                Console.WriteLine(car);

            var res2 = coll.SelectMany(car => car)
                .Where(item => item is LightCar && item.Price > 900000);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nАвтомобили с ценой свыше 900К. (Extension Methods):");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var car in res2)
                Console.WriteLine(car);
            return res.Concat(res2);
        }
        // a)	На выборку данных (Where). - лаб.12
        public static IEnumerable<Car> WhereMyColl(MyCollection<Car> coll)
        {
            var res = from item in coll
                      where item.GroundClearance > 150 // Замените SomeProperty на нужное свойство
                      select item;

            var res2 = coll.Where(item => item.GroundClearance > 150);

            Console.WriteLine("Where Query (LINQ):");
            foreach (var car in res)
                Console.WriteLine(car);

            Console.WriteLine("Where Query (Extension Methods):");
            foreach (var car in res2)
                Console.WriteLine(car);
            return res;
        }
        //b)	Использование операций над множествами (Union,Except, Intersect).
        public static IEnumerable<Car> Union(Queue<List<Car>> coll)
        {
            Console.WriteLine("Запрос на операции над множествами (Union):");
            var res = (from car in coll
                       from item in car
                       select item)
                       .Union(from car in coll
                              from item in car
                              select item);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n(LINQ):");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var car in res)
                Console.WriteLine(car);

            var res2 = coll.SelectMany(car => car)
                .Union(coll.SelectMany(car => car));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n(Extension Methods):");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var car in res2)
                Console.WriteLine(car);
            return res2;
        }
        //b)	Использование операций над множествами (Union,Except, Intersect) - 12
        public static int CountQueue(MyCollection<Car> coll)
        {
            Console.WriteLine("Получение счётчика (Count):");
            var res = (from item in coll
                       select item).Count();
            var res2 = coll.Count();

            Console.WriteLine($"Count Query (LINQ): {res}");
            Console.WriteLine($"Count Query (Extension Methods): {res}");
            return res;
        }

        //c)	Агрегирование данных (Sum, Max, Min, Average).
        public static double Sum(Queue<List<Car>> coll)
        {
            var res = (from car in coll
                       from item in car
                       select item.Price)
                       .Sum();
            Console.WriteLine("Запрос на операции над множествами (Union):");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nСумма цен автомобилей (LINQ): {res}");
            Console.ForegroundColor = ConsoleColor.White;

            var res2 = coll.SelectMany(carList => carList)
                              .Sum(car => car.Price);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nСумма цен автомобилей (Extension Methods): {res2}");
            Console.ForegroundColor = ConsoleColor.White;
            return (res);

        }
        public static double Max(Queue<List<Car>> coll)
        {
            var resMax = (from carList in coll
                          from car in carList
                          select car.Price).Max();

            Console.WriteLine($"\nМаксимальная цена автомобиля (LINQ): {resMax}");

            var resMax2 = coll.SelectMany(carList => carList)
                              .Max(car => car.Price);

            Console.WriteLine($"\nМаксимальная цена автомобиля (Extension Methods): {resMax2}");
            return (resMax);

        }
        //c)	Агрегирование данных (Sum, Max, Min, Average) - 12
        public static double SumMax12(MyCollection<Car> coll)
        {
            var res = (from car in coll
                       select car.Price)
                       .Sum();

            Console.WriteLine("Запрос на операции над множествами (Union):");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nСумма цен автомобилей (LINQ): {res}");
            Console.ForegroundColor = ConsoleColor.White;
            var res2 = coll.Sum(car => car.Price);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nСумма цен автомобилей (Extension Methods): {res2}");
            Console.ForegroundColor = ConsoleColor.White;

            var resMax = (from car in coll
                          select car.Price).Max();

            Console.WriteLine($"\nМаксимальная цена автомобиля (LINQ): {resMax}");
            var resMax2 = coll.Max(car => car.Price);
            Console.WriteLine($"\nМаксимальная цена автомобиля (Extension Methods): {resMax2}");
            return res;
        }

        // d)	Группировка данных(Group by)
        public static void GroupBy(Queue<List<Car>> coll)
        {
            Console.WriteLine("Запрос на группировку данных (Group by):");

            var res = from carList in coll
                      from car in carList
                      group car by car.Brand into carGroup
                      select new { Brand = carGroup.Key, Cars = carGroup, Count = carGroup.Count() };

            Console.WriteLine("\nГруппировка автомобилей по бренду (LINQ):");
            foreach (var group in res)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Бренд: {group.Brand}, Количество: {group.Count}");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var car in group.Cars)
                {
                    Console.WriteLine(car);
                }
            }

            var res2 = coll.SelectMany(carList => carList)
                           .GroupBy(car => car.Brand)
                           .Select(carGroup => new { Brand = carGroup.Key, Cars = carGroup, Count = carGroup.Count() });


            Console.WriteLine("\nГруппировка автомобилей по бренду (Extension Methods):");
            foreach (var group in res2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Бренд: {group.Brand}, Количество: {group.Count}");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var car in group.Cars)
                {
                    Console.WriteLine(car);
                }
            }
        }
        // d)	Группировка данных(Group by) - 12
        public static void GroupBy12(MyCollection<Car> coll)
        {
            var res = from item in coll
                      group item by item.Brand into g
                      select new { Key = g.Key, Items = g, Count = g.Count() };

            var res2 = coll.GroupBy(item => item.Brand)
                                   .Select(g => new { Key = g.Key, Items = g, Count = g.Count() });

            Console.WriteLine("Group By Query (LINQ):");
            foreach (var group in res)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nБренд: {group.Key}");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var item in group.Items)
                {
                    Console.WriteLine(item);
                }
            }

            Console.WriteLine("Group By Query (Extension Methods):");
            foreach (var group in res2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nБренд: {group.Key}");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var item in group.Items)
                {
                    Console.WriteLine(item);
                }
            }
        }
        // e)	Получение нового типа (Let).
        public static void Let(Queue<List<Car>> coll)
        {
            Console.WriteLine("Запрос на получение нового типа (Let):");

            var res = from car in coll
                      from item in car
                      let priceInDollars = Math.Round(item.Price / 75, 2)
                      select new { item.Brand, item.Price, PriceInDollars = priceInDollars };
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nАвтомобили с ценой в долларах (LINQ):");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in res)
            {
                Console.WriteLine($"{item.Brand}: {item.Price}руб.,   {item.PriceInDollars} $");
            }

            var res2 = coll.SelectMany(carList => carList)
                           .Select(item => new { item.Brand, item.Price, PriceInDollars = Math.Round(item.Price / 75, 2) });

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nАвтомобили с ценой в долларах (Extension Methods):");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var car in res2)
            {
                Console.WriteLine($"{car.Brand}: {car.Price}руб.,   {car.PriceInDollars}$");
            }
        }
        public static bool Contains(MyCollection<Car> coll) //своё расширение
        {
            Console.WriteLine($"Запрос на проверку наличия элемента с SomeProperty :");

            var res = (from car in coll
                       where car.Color == "Белый"
                       select car)
                       .Any();

            Console.WriteLine($"\nРезультат проверки (LINQ): {(res ? "Элемент найден" : "Элемент не найден")}");

            var res2 = coll.Any(item => item.Color == "Белый");

            Console.WriteLine($"\nРезультат проверки (Extension Methods): {(res2 ? "Элемент найден" : "Элемент не найден")}");
            return res;
        }
    }
}
