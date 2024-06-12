using System.Linq;
using System.Xml.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using ClassLibrary;
using Lab_12_4;

namespace lab_14
{
    internal class Program
    {
        private static void CommandsForPart1()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Меню часть 1.\n" +
                              "----------------------------------------------------------------------------\n" +
                              "1. На выборку данных(Where).\n" +
                              "2. Использование операций над множествами (Union,Except, Intersect).\n" +
                              "3. Агрегирование данных (Sum, Max, Min, Average).\n" +
                              "4. Группировка данных (Group by).\n" +
                              "5. Получение нового типа (Let).\n" +
                              "6. Переход к части 2.\n" +
                              "----------------------------------------------------------------------------\n");
        }

        private static void CommandsForPart2()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Меню часть 2.\n" +
                              "----------------------------------------------------------------------------\n" +
                              "1. На выборку данных(Where).\n" +
                              "2. Получение счетчика(Count). \n" +
                              "3. Агрегирование данных (Sum, Max, Min, Average). \n" +
                              "4. Группировка данных (Group by).\n" +
                              "5. Завершение работы.\n" +
                              "----------------------------------------------------------------------------\n");
        }

        // Создание коллекции
        static List<MusicalInstrument> MakeCollection(int length)
        {
            var list = new List<MusicalInstrument>();
            for (int i = 0; i < length; i++)
            {
                Guitar g = new Guitar();
                g.RandomInit();
                list.Add(g);
            }
            return list;
        }

        // Печать коллекции
        static void PrintCollection(List<MusicalInstrument> list)
        {
            // Проверка коллекции на пустоту
            if (list.Count == 0)
            {
                Console.WriteLine("Empty collection");
                return;
            }

            // Вывод
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        // На выборку данных (Where) - часть 2
        public static void WhereFlightFender2(MyCollection<MusicalInstrument> collection)
        {
            // Запрос LINQ на вывод музыкальных инструментов с названием Flight с отложенным выполнеием
            Console.WriteLine("\nЗапрос LINQ на вывод музыкальных инструментов с названием Flight");
            var res1 = from mi in collection
                       where mi?.Title?.ToString() == "Flight"
                       select mi;

            // Вывод  
            Console.WriteLine("\nИнструменты с назвванием Flight");
            foreach (var item in res1)
                Console.WriteLine(item.GetType());


            // Методы расширения на вывод музыкальных инструментов с названием Flight с отложенным выполнеием
            Console.WriteLine("\nМетоды расширения на вывод музыкальных инструментов с названием Fender");
            var res2 = collection.Select(gr => gr)
                              .Where(mi => mi?.id?.number1 != null && mi?.Title == "Fender");

            // Вывод  
            Console.WriteLine("\nИнструменты с назвванием Fender");
            foreach (var item in res2)
                Console.WriteLine(item);
        }

        // Использование операций над множествами (Union,Except, Intersect) - часть 1
        public static void UnionGroup(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            // Запрос LINQ на объеденение музыкальных инструментов из двух групп
            Console.WriteLine("\nЗапрос LINQ на объеденение музыкальных инструментов из двух групп");
            var res1 = (from gr in collection
                        from mi in gr.Value
                        select mi)
                        .Union(from gr in collection
                               from mi in gr.Value
                               select mi);

            // Вывод  
            foreach (var item in res1)
                Console.WriteLine(item);

            // Методы расширения на объеденение музыкальных инструментов из двух групп
            Console.WriteLine("\nМетоды расширения на объеденение музыкальных инструментов из двух групп");
            var res2 = collection.SelectMany(gr => gr.Value)
                .Union(collection.SelectMany(gr => gr.Value));

            // Вывод  
            foreach (var item in res2)
                Console.WriteLine(item);
        }

        // Получение счетчика(Count) - часть 2
        public static void CountFlightFender(MyCollection<MusicalInstrument> collection)
        {
            // Запрос LINQ на вывод количества музыкальных инструментов с названием Flight с отложенным выполнеием
            Console.WriteLine("\nЗапрос LINQ на вывод количества музыкальных инструментов с названием Flight");
            var res1 = (from mi in collection
                        where mi?.Title?.ToString() == "Flight"
                        select mi).Count();

            // Вывод  
            Console.WriteLine($"Количество инструментов с названием Flight: {res1}");


            // Методы расширения на вывод музыкальных инструментов с названием Flight с отложенным выполнеием
            Console.WriteLine("\nМетоды расширения на вывод количества музыкальных инструментов с названием Fender");
            var res2 = collection.Select(gr => gr)
                              .Where(mi => mi?.Title == "Fender").Count();

            // Вывод  
            Console.WriteLine($"Количество инструментов с названием Fender: {res2}");
        }

        // Агрегирование данных (Sum, Max, Min, Average) - часть 1
        public static void MaxAverageId(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            // Запрос LINQ на вывод среднего значения показателя id всех инструментов
            Console.WriteLine("\nЗапрос LINQ на вывод среднего значения показателя id всех инструментов");
            var res1 = (from gr in collection
                        from mi in gr.Value
                        select mi.id.number1)
                       .Average();
            // Вывод  
            Console.WriteLine($"Среднее значение показателя id всех инструментов: {res1}");

            // Методы расширения на вывод максимального значения показателя id всех инструментов
            Console.WriteLine("\nМетоды расширения на вывод среднего значения показателя id всех инструментов");
            var res2 = collection.SelectMany(gr => gr.Value)
                              .Max(mi => mi.id.number1);

            // Вывод  
            Console.WriteLine($"Максимальное значение показателя id всех инструментов: {res2}");
        }

        // Агрегирование данных (Sum, Max, Min, Average) - часть 2
        public static void MaxAverageId2(MyCollection<MusicalInstrument> collection)
        {
            // Запрос LINQ на вывод среднего значения показателя id всех инструментов
            Console.WriteLine("\nЗапрос LINQ на вывод среднего значения показателя id всех инструментов");
            var res1 = (from mi in collection
                        select mi?.id?.number1)
                       .Average();
            // Вывод  
            Console.WriteLine($"Среднее значение показателя id всех инструментов: {res1}");

            // Методы расширения на вывод максимального значения показателя id всех инструментов
            Console.WriteLine("\nМетоды расширения на вывод среднего значения показателя id всех инструментов");
            var res2 = collection.Select(gr => gr)
                              .Max(mi => mi?.id?.number1);

            // Вывод  
            Console.WriteLine($"Максимальное значение показателя id всех инструментов: {res2}");
        }

        // Группировка данных (Group by) - часть 1
        public static void GroupTitleAndStringCount(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            // Запросы LINQ на группировку по Названию мукзыкальных инструментов
            Console.WriteLine("\nЗапросы LINQ на группировку по Названию мукзыкальных инструментов ");
            var res1 = from gr in collection
                       from mi in gr.Value
                       group mi by mi.Title;

            // Вывод  
            Console.WriteLine("Группировка ");
            foreach (IGrouping<string, MusicalInstrument> gr in res1)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }

            // Методы расширения на группировку по максимальному количеству струн гитар
            Console.WriteLine("\nМетоды расширения на группировку по максимальному количеству струн гитар ");
            var res2 = collection.SelectMany(gr => gr.Value)
                .Where(mi => mi is Guitar).GroupBy(x => ((Guitar)x).String_count1);

            // Вывод  
            foreach (var gr in res2)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }
        }

        // Группировка данных (Group by) - часть 2
        public static void GroupTitleAndStringCount2(MyCollection<MusicalInstrument> collection)
        {
            // Запросы LINQ на группировку по названию мукзыкальных инструментов
            Console.WriteLine("\nЗапросы LINQ на группировку по названию мукзыкальных инструментов ");
            var res1 = from mi in collection
                       group mi by mi?.Title;

            // Вывод  
            foreach (var gr in res1)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }

            // Методы расширения на группировку по максимальному количеству струн гитар
            Console.WriteLine("\nМетоды расширения на группировку по максимальному количеству струн гитар ");
            var res2 = collection
                .Select(gr => gr)
                .GroupBy(mi => mi?.Title);

            // Вывод  
            foreach (var gr in res2)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }
        }

        // Получение нового типа(Let) - часть 1
        public static void NewTitle(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            // Запросы LINQ на получение нового типа ID
            Console.WriteLine("\nЗапросы LINQ на получение нового типа ID");
            var res1 = from gr in collection
                       from mi in gr.Value
                       let newId = mi.id.number1 * 0.1
                       select new { mi.Title, OldId = mi.id.number1, NewID = mi.id.number1 * 0.1 };

            // Вывод  
            foreach (var item in res1)
                Console.WriteLine(item);

            // Методы расширения на получение нового типа
            Console.WriteLine("\nМетоды расширения на получение нового типа ID");
            var res2 = collection.SelectMany(gr => gr.Value)
                .Select(mi => new { mi.Title, OldId = mi.id.number1, NewID = mi.id.number1 * 0.5 });

            // Вывод  
            foreach (var item in res2)
                Console.WriteLine(item);
        }



        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЧАСТЬ 1");

            List<MusicalInstrument> group1 = MakeCollection(10);
            List<MusicalInstrument> group2 = MakeCollection(10);

            SortedDictionary<string, List<MusicalInstrument>> participants = new SortedDictionary<string, List<MusicalInstrument>>();

            participants.Add("Музыкальная группа 1", group1);
            participants.Add("Музыкальная группа 2", group2);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Музыкальные инструменты");

            foreach (var participant in participants)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nГруппа: {participant.Key}");
                foreach (var musicalInstrument in participant.Value)
                {
                    Console.WriteLine(musicalInstrument);
                }
            }

            // Меню часть 1
            int answer = 1;
            while (answer != 6)
            {
                try
                {
                    CommandsForPart1();
                    answer = int.Parse(Console.ReadLine());
                    switch (answer)
                    {
                        case 1:
                            {
                                WhereFlightFender(participants);
                                break;
                            }
                        case 2:
                            {
                                UnionGroup(participants);
                                break;
                            }
                        case 3:
                            {
                                MaxAverageId(participants);
                                break;
                            }
                        case 4:
                            {
                                GroupTitleAndStringCount(participants);
                                break;
                            }
                        case 5:
                            {
                                NewTitle(participants);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЧАСТЬ 2\n");
            MyCollection<MusicalInstrument> myCollection = new MyCollection<MusicalInstrument>(10); // Создание хеш-таблицы музыкальных инструментов

            for (int i = 0; i < 10; i++)
            {
                MusicalInstrument a = new();
                a.RandomInit();
                myCollection.Add(a);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            myCollection.Print();
            // Меню часть 2
            int answer2 = 1;
            while (answer2 != 5)
            {
                try
                {
                    CommandsForPart2();
                    answer2 = int.Parse(Console.ReadLine());
                    switch (answer2)
                    {
                        case 1:
                            {
                                WhereFlightFender2(myCollection);
                                break;
                            }
                        case 2:
                            {
                                CountFlightFender(myCollection);
                                break;
                            }
                        case 3:
                            {
                                MaxAverageId2(myCollection);
                                break;
                            }
                        case 4:
                            {
                                GroupTitleAndStringCount2(myCollection);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}