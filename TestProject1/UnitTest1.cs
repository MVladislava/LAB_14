using Cars;
using LAB_14;
using static LAB_14.Method;
using LAB_12_4;
using LAB_12_3;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSumExtensionMethod()
        {
            // Arrange
            var collection = Method.MakeColl(5);

            // Act
            var result = collection.Sum(car => car.Price);

            // Assert
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        public void Where()
        {
            // Arrange
            var collection = new Queue<List<Car>>();
            var list1 = new List<Car>
            {
            new Car { Brand = "BMW", Price = 800000 },
            new LightCar { Brand = "Audi", Price = 1000000 }
            };
            var list2 = new List<Car>
            {
            new Car { Brand = "Mercedes", Price = 950000 },
            new LightCar { Brand = "Toyota", Price = 850000 }
            };
            collection.Enqueue(list1);
            collection.Enqueue(list2);

            // Act
            var result = Method.Where(collection);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.All(car => car.Price > 900000));
        }
        [TestMethod]
        public void TestWhereMyCollMethod()
        {
            // Arrange
            var collection = new MyCollection<Car>(5); 
            collection.TransformToFindTree(); 
            var methodResult = Method.WhereMyColl(collection); 

            // Act
            var queryResult = from item in collection
                              where item.GroundClearance > 150
                              select item;

            // Assert
            Assert.IsNotNull(methodResult);
            Assert.AreEqual(methodResult.Count(), queryResult.Count()); 
            Assert.IsTrue(methodResult.All(car => car.GroundClearance > 150));
        }
        [TestMethod]
        public void TestContainsMethod()
        {
            // Arrange
            var collection = new MyCollection<Car>(5);
            collection.AddPoint(new Car { Color = "Белый" });
            collection.AddPoint(new Car { Color = "Синий" });
            collection.AddPoint(new Car { Color = "Красный" });

            // Act
            var result = Method.Contains(collection);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestSumMax12Method()
        {
            // Arrange
            var collection = new MyCollection<Car>(10); // Создаем коллекцию с 10 элементами
            double expectedSum = collection.Sum(car => car.Price); // Ожидаемая сумма цен
            double expectedMax = collection.Max(car => car.Price); // Ожидаемое максимальное значение цены

            // Act
            double result = Method.SumMax12(collection); // Вызываем метод

            // Assert
            Assert.AreEqual(expectedSum, result); // Проверяем, что результат суммирования соответствует ожидаемому
        }
        [TestMethod]
        public void TestSumMax()
        {
            // Arrange
            var collection = new Queue<List<Car>>();
            var list1 = new List<Car>
                {
            new Car { Brand = "BMW", Price = 800000 },
            new LightCar { Brand = "Audi", Price = 1000000 }
                };
            var list2 = new List<Car>
                {
            new Car { Brand = "Mercedes", Price = 950000 },
            new LightCar { Brand = "Toyota", Price = 850000 }
                };
            collection.Enqueue(list1);
            collection.Enqueue(list2);

            // Act
            var result = Method.Sum(collection);
            var result2 = Method.Max(collection);

            // Assert
            Assert.AreEqual(3600000, result); // 800000 + 1000000 + 950000 + 850000
            Assert.AreEqual(1000000, result2);

        }
        [TestMethod]
        public void TestCount()
        {
            // Arrange
            var collection = new MyCollection<Car>(10);

            // Act
            var res = Method.CountQueue(collection);

            // Assert
            Assert.AreEqual(10, res); 
        }
        [TestMethod]
        public void TestUnionMethod()
        {
            // Arrange
            var collection = new Queue<List<Car>>();
            var list1 = new List<Car>
        {
            new Car { Brand = "BMW", Price = 800000 },
            new LightCar { Brand = "Audi", Price = 1000000 }
        };
            var list2 = new List<Car>
        {
            new Car { Brand = "Mercedes", Price = 950000 },
            new LightCar { Brand = "Toyota", Price = 850000 }
        };
            collection.Enqueue(list1);
            collection.Enqueue(list2);

            // Act
            var result = Method.Union(collection);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(list1.Count + list2.Count, result.Count());
            Assert.IsTrue(result.Contains(list1[0]));
            Assert.IsTrue(result.Contains(list1[1]));
            Assert.IsTrue(result.Contains(list2[0]));
            Assert.IsTrue(result.Contains(list2[1]));
        }
    }
}