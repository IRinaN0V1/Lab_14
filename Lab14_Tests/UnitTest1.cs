using ClassLibraryLab10;
using Lab_14;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Collections.ObjectModel;

namespace Lab14_Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ДЛЯ ТЕСТИРОВАНИЯ
        static int CountElements(IEnumerable<object> collection)
        {
            int count = 0;
            foreach(var item in collection)
            {
                count++;
            }
            return count;
        }//Подсчет количества элементов в перечислимых коллекциях

        //МЕТОДЫ ДЛЯ ДЕМОНСТРАЦИИ СЛУЧАЕВ, КОГДА ЭЛЕМЕНТЫ КАКОГО-ЛИБО ТИПА ОТСУТСТВУЮТ
        static void CreateDatesList(ref List<ProductionDate> dates)
        {
            ProductionDate date1 = new ProductionDate(2020, 3, 6);
            ProductionDate date2 = new ProductionDate(2021, 5, 11);
            ProductionDate date3 = new ProductionDate(2022, 7, 17);
            ProductionDate date4 = new ProductionDate(2023, 9, 22);
            ProductionDate date5 = new ProductionDate(2024, 12, 28);
            dates.Add(date1);
            dates.Add(date2);
            dates.Add(date3);
            dates.Add(date4);
            dates.Add(date5);
        }//Метод для создания списка дат(для запроса Join)
        static void CreateFactory_AllTypes(ref Queue<List<object>> collection)
        {
            int count = 5;
            collection = new Queue<List<object>>();

            List<object> cars1 = new List<object>();
            FillListRandomly(cars1, count, new Car());
            List<object> jeeps = new List<object>();
            FillListRandomly(jeeps, count, new Jeep());
            List<object> pasCars = new List<object>();
            FillListRandomly(pasCars, count, new PassengerCar());
            List<object> trucks = new List<object>();
            FillListRandomly(trucks, count, new Truck());

            collection.Enqueue(cars1);
            collection.Enqueue(jeeps);
            collection.Enqueue(pasCars);
            collection.Enqueue(trucks);
        }//Метод для создания коллекции из первой части ЛР (все типы)

        static void CreateFactory_NoTruck(ref Queue<List<object>> collection)
        {
            int count = 5;
            collection = new Queue<List<object>>();

            List<object> cars1 = new List<object>();
            FillListRandomly(cars1, count, new Car());
            List<object> jeeps = new List<object>();
            FillListRandomly(jeeps, count, new Jeep());

            collection.Enqueue(cars1);
            collection.Enqueue(jeeps);
        }//Метод для создания коллекции из первой части ЛР (без типа PassengerCar)

        static void CreateFactory_NoPassengerCarAndJeep(ref Queue<List<object>> collection)
        {
            int count = 5;
            collection = new Queue<List<object>>();

            List<object> cars1 = new List<object>();
            FillListRandomly(cars1, count, new Car());
            List<object> trucks = new List<object>();
            FillListRandomly(trucks, count, new Truck());

            collection.Enqueue(cars1);
            collection.Enqueue(trucks);
        }//Метод для создания коллекции из первой части ЛР (без типа Truck и PassengerCar)

        static void FillListRandomly<T>(List<object> list, int count, T sample) where T : IInit, new()
        {
            for (int i = 0; i < count; i++)
            {
                T element = new T();
                element.RandomInit();
                list.Add(element);
            }
        }//Случайное заполнение списка определенного типа элементами

        private string CaptureConsoleOutput(Action action)
        {
            // Создается новый StringWriter, который будет использоваться для перехвата вывода консоли
            // StringWriter - это обертка над StringBuilder для записи символов в поток строк
            using (var consoleOutput = new StringWriter())
            {
                // Устанавливается consoleOutput как поток вывода консоли, чтобы перехватить вывод этой консоли
                Console.SetOut(consoleOutput);
                // Выполняется переданное действие (action), которое содержит операции вывода информации в консоль
                action.Invoke();

                return consoleOutput.ToString();
            }
        }

        #endregion

        #region Тесты для методов печати
        [TestMethod]
        public void PrintGeneralMenu_ExpectedAndActualStringsAreEqual()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.PrintGeneralMenu();

                string expected = "Выберите пункт меню:" +
                "\n1. Первая часть ЛР (коллекция Queue<List<T>>)" +
                "\n2. Вторая часть ЛР ()" +
                "\n3. Завершить работу";

                string actual = sw.ToString().Trim();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void PrintMenu_FirstPart_ExpectedAndActualStringsAreEqual()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.PrintMenu_FirstPart();

                string expected = "Выберите пункт меню:" +
                "\n1. Формировании колекции Queue, содержащей коллекции типа List" +
                "\n2. Печать коллекции" +
                "\n3. Запрос 1: Грузовики(Truck) с грузоподьемностью > 100 тонн (Where)" +
                "\n4. Запрос 2: Объединить легковые машины(PassengerCar) и внедорожники(Jeep) (Union)" +
                "\n5. Запрос 3: Средняя стоимость машин(PassengerCar) (Average)" +
                "\n6. Запрос 4: Группировка элементов коллекции по году выпуска (Group by)" +
                "\n7. Запрос 5: Соединить грузовики(Truck) и дату производства(ProductionDate) по году выпуска (Join)" +
                "\n8. Завершить работу";

                string actual = sw.ToString().Trim();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void PrintMenu_SecondPart_ExpectedAndActualStringsAreEqual()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.PrintMenu_SecondPart();

                string expected = "Выберите пункт меню:" +
                "\n1. Формировании колекции MyCollection<PassengerCar>" +
                "\n2. Печать коллекции" +
                "\n3. Запрос 1: Легковые машины, бренд которых начинается на латинскую 'T' (Where) " +
                "\n4. Запрос 2: Количество легковых машин, стоимость которых превышает 4 млн. руб. (Count)" +
                "\n5. Запрос 3: Максимальная скорость среди легковых машин (Max)" +
                "\n6. Запрос 4: Группировка элементов коллекции по кол-ву пассажирских мест (Group by)" +
                "\n7. Завершить работу";

                string actual = sw.ToString().Trim();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void PrintQueueCollection_EmptyCollection_ThrowsException()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>(0);

            Assert.ThrowsException<Exception>(() => Program.PrintQueueCollection(emptyCollection));
        }

        [TestMethod]
        public void PrintListOfResults_EmptyCollection_ThrowsException()
        {
            IEnumerable<object> list = new List<object>(0);

            Assert.ThrowsException<Exception>(() => Program.PrintListOfResults(list));
        }
        #endregion

        #region Тесты для методов создания коллекций
        [TestMethod]
        public void FillListRandomly_CollectionIsNotNull()
        {
            List<Type> types = new List<Type> { typeof(Car), typeof(PassengerCar), typeof(Truck), typeof(Jeep) };
            List<object> list = new List<object>();
            int count = 3;

            Lab_14.Program.FillListRandomly(list, count, types);

            Assert.IsNotNull(list);
            Assert.AreEqual(count, list.Count);
        }
        #endregion

        #region Тестирование методов(запросов) для 1 части

        #region Выборка
        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_EM(collection));
        }// Метод SampleOfTrucks_CapacityMoreThan100_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_EM_WithoutTrucksAbove100Capacity_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123,12,98);
            Truck truck2 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 99);
            Truck truck3 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 100);

            trucks.Add(truck1);
            trucks.Add(truck2);
            trucks.Add(truck3);

            collection.Enqueue(trucks);

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_EM(collection));
        }// Метод SampleOfTrucks_CapacityMoreThan100_EM, исключение при отсутствии элементов с нужной грузоподъемностью

        [TestMethod]
        public void TestSampleOfTrucks_CapacityMoreThan100_EM_WithTrucksAbove100Capacity()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 254);
            Truck truck3 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 80);
            Car car = new Car();

            trucks.Add(truck1);
            trucks.Add(truck2);
            trucks.Add(truck3);
            trucks.Add(car);

            collection.Enqueue(trucks);

            IEnumerable<object> result = Program.SampleOfTrucks_CapacityMoreThan100_EM(collection);
            var consoleOutput = CaptureConsoleOutput(() => Program.PrintListOfResults(result));

            Assert.IsNotNull(result);
            Assert.AreEqual(2, CountElements(result));
            Assert.IsTrue(consoleOutput.Contains(truck1.ToString()));
            Assert.IsTrue(consoleOutput.Contains(truck2.ToString()));
        }// Метод SampleOfTrucks_CapacityMoreThan100_EM, проверка на возвращение списка с найденными элементами

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_LINQ_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_LINQ(emptyCollection));
        }// Метод SampleOfTrucks_CapacityMoreThan100_LINQ, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_LINQ_WithoutTrucksAbove100Capacity_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 98);
            Truck truck2 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 99);
            Truck truck3 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 100);

            trucks.Add(truck1);
            trucks.Add(truck2);
            trucks.Add(truck3);

            collection.Enqueue(trucks);

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_LINQ(collection));
        }// Метод SampleOfTrucks_CapacityMoreThan100_LINQ, исключение при отсутствии элементов с нужной грузоподъемностью

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_LINQ_WithTrucksAbove100Capacity()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 254);
            Truck truck3 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 80);
            Car car = new Car();

            trucks.Add(truck1);
            trucks.Add(truck2);
            trucks.Add(truck3);
            trucks.Add(car);

            collection.Enqueue(trucks);

            IEnumerable<object> result = Program.SampleOfTrucks_CapacityMoreThan100_LINQ(collection);
            var consoleOutput = CaptureConsoleOutput(() => Program.PrintListOfResults(result));

            Assert.IsNotNull(result);
            Assert.AreEqual(2, CountElements(result));
            Assert.IsTrue(consoleOutput.Contains(truck1.ToString()));
            Assert.IsTrue(consoleOutput.Contains(truck2.ToString()));
        }// Метод SampleOfTrucks_CapacityMoreThan100_LINQ, проверка на возвращение списка с найденными элементами
        #endregion

        #region Объединение
        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_EM_WithEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_EM(emptyCollection));
        }// Метод UnionOfPassengersCarsAndJeeps_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_EM_WithoutPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            CreateFactory_NoPassengerCarAndJeep(ref collection);

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_EM(collection));
        }//Метод UnionOfPassengersCarsAndJeeps_EM, исключение при отсутствии элементов нужного типа в коллекции

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_EM_WithPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            CreateFactory_AllTypes(ref collection);

            IEnumerable<object> result = Program.UnionOfPassengersCarsAndJeeps_EM(collection);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, CountElements(result));
        }// Метод UnionOfPassengersCarsAndJeeps_EM, проверка на возвращение списка с найденными элементами

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_LINQ_WithEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_LINQ(emptyCollection));
        }// Метод UnionOfPassengersCarsAndJeeps_LINQ, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_LINQ_WithoutPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            CreateFactory_NoPassengerCarAndJeep(ref collection);

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_LINQ(collection));
        }//Метод UnionOfPassengersCarsAndJeeps_LINQ, исключение при отсутствии элементов нужного типа в коллекции

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_LINQ_WithPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            CreateFactory_AllTypes(ref collection);

            IEnumerable<object> result = Program.UnionOfPassengersCarsAndJeeps_LINQ(collection);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, CountElements(result));
        }// Метод UnionOfPassengersCarsAndJeeps_LINQ, проверка на возвращение списка с найденными элементами
        #endregion

        #region Средняя стоимость
        [TestMethod]
        public void AverageCostOfPassengerCars_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_EM(emptyCollection));
        }// Метод AverageCostOfPassengerCars_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void AverageCostOfPassengerCars_EM_WithoutPassengerCars_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 254);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Car car2 = new Car("Kia", 2023389, "White", 2021, 224, 54);

            list1.Add(truck1);
            list2.Add(truck2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_EM(collection));
        }// Метод AverageCostOfPassengerCars_EM, исключение при отсутствии элементов нужного типа

        [TestMethod]
        public void AverageCostOfPassengerCars_EM_WithPassengerCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("Машина", 202334, "цвет", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("Машина", 39489394, "цвет", 2022, 123, 12, 2, 56);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Jeep car2 = new Jeep("Kia", 2023389, "White", 2021, 224, 54, true, "пустыня");

            list1.Add(truck1);
            list2.Add(pc1);
            list2.Add(pc2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            double averageCost = Program.AverageCostOfPassengerCars_EM(collection);

            Assert.AreEqual((pc1.Cost+pc2.Cost)/2, averageCost);
        }// Метод AverageCostOfPassengerCars_EM, проверка на правильное вычисление средней стоимости легковых машин

        [TestMethod]
        public void AverageCostOfPassengerCars_LINQ_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_LINQ(emptyCollection));
        }// Метод AverageCostOfPassengerCars_LINQ, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void AverageCostOfPassengerCars_LINQ_WithoutPassengerCars_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 254);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Car car2 = new Car("Kia", 2023389, "White", 2021, 224, 54);

            list1.Add(truck1);
            list2.Add(truck2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_LINQ(collection));
        }// Метод AverageCostOfPassengerCars_LINQ, исключение при отсутствии элементов нужного типа

        [TestMethod]
        public void AverageCostOfPassengerCars_LINQ_WithPassengerCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("Машина", 202334, "цвет", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("Машина", 39489394, "цвет", 2022, 123, 12, 2, 56);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Jeep car2 = new Jeep("Kia", 2023389, "White", 2021, 224, 54, true, "пустыня");

            list1.Add(truck1);
            list2.Add(pc1);
            list2.Add(pc2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            double averageCost = Program.AverageCostOfPassengerCars_LINQ(collection);

            Assert.AreEqual((pc1.Cost + pc2.Cost) / 2, averageCost);
        }// Метод AverageCostOfPassengerCars_LINQ, проверка на правильное вычисление средней стоимости легковых машин
        #endregion

        #region Группировка
        [TestMethod]
        public void GroupByYear_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.GroupByYear_EM(emptyCollection));
        }// Метод GroupByYear_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void GroupByYear_EM_WithCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            //Заполняем коллекцию списками с элементами
            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("Машина", 202334, "цвет", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("Машина", 39489394, "цвет", 2022, 123, 12, 2, 56);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Car car2 = new Car("Kia", 2023389, "White", 2021, 224, 54);
            Car car3 = new Car("Kia", 2023389, "White", 2021, 224, 58);

            list1.Add(truck1);
            list2.Add(pc1);
            list2.Add(pc2);
            list1.Add(car1);
            list2.Add(car2);
            list1.Add(car3);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            //Группируем элементы коллекции по году выпуска
            var result = Program.GroupByYear_EM(collection);

            Assert.AreEqual(2, result.Count());//Проверяем количество групп

            var group1 = result.FirstOrDefault(g => g.Key == 2022);
            Assert.IsNotNull(group1); // Проверяем, что группа с 2022 годом выпуска существует
            Assert.AreEqual(4, group1.Count()); // Проверяем, что количество элементов с 2022 годом выпуска правильное

            var group2 = result.FirstOrDefault(g => g.Key == 2021);
            Assert.IsNotNull(group2); // Проверяем, что группа с 2021 годом выпуска существует
            Assert.AreEqual(2, group2.Count()); // Проверяем, что количество элементов с 2021 годом выпуска правильное
        }// Метод GroupByYear_EM, проверка на правильный возвращаемый список сгруппированных элементов

        [TestMethod]
        public void GroupByYear_LINQ_ExceptionWithEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.GroupByYear_LINQ(emptyCollection));
        }// Метод GroupByYear_LINQ, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void GroupByYear_LINQ_WithCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            //Заполняем коллекцию списками с элементами
            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("Машина", 202334, "цвет", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("Машина", 39489394, "цвет", 2022, 123, 12, 2, 56);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Car car2 = new Car("Kia", 2023389, "White", 2021, 224, 54);
            Car car3 = new Car("Kia", 2023389, "White", 2021, 224, 58);

            list1.Add(truck1);
            list2.Add(pc1);
            list2.Add(pc2);
            list1.Add(car1);
            list2.Add(car2);
            list1.Add(car3);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            //Группируем элементы коллекции по году выпуска
            var result = Program.GroupByYear_LINQ(collection);

            Assert.AreEqual(2, result.Count());//Проверяем количество групп

            var group1 = result.FirstOrDefault(g => g.Key == 2022);
            Assert.IsNotNull(group1); // Проверяем, что группа с 2022 годом выпуска существует
            Assert.AreEqual(4, group1.Count()); // Проверяем, что количество элементов с 2022 годом выпуска правильное

            var group2 = result.FirstOrDefault(g => g.Key == 2021);
            Assert.IsNotNull(group2); // Проверяем, что группа с 2021 годом выпуска существует
            Assert.AreEqual(2, group2.Count()); // Проверяем, что количество элементов с 2021 годом выпуска правильное
        }// Метод GroupByYear_LINQ, проверка на правильный возвращаемый список сгруппированных элементов
        #endregion

        #region Соединение
        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();
            //Создание списка дат
            List<ProductionDate> dates = new List<ProductionDate>();
            ProductionDate date1 = new ProductionDate(2020, 3, 6);
            ProductionDate date2 = new ProductionDate(2021, 5, 11);
            ProductionDate date3 = new ProductionDate(2022, 7, 17);
            ProductionDate date4 = new ProductionDate(2023, 9, 22);
            ProductionDate date5 = new ProductionDate(2024, 12, 28);
            dates.Add(date1);
            dates.Add(date2);
            dates.Add(date3);
            dates.Add(date4);
            dates.Add(date5);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_EM(emptyCollection, dates));
        }//Метод JoinTrucksAndProductionDateByYear_EM, исключение при попытке выполнения запроса в пустой коллекции

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_ExceptionForEmptyDatesList()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            List<ProductionDate> emptyDates = new List<ProductionDate>();

            CreateFactory_AllTypes(ref collection);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_EM(collection, emptyDates));
        }//Метод JoinTrucksAndProductionDateByYear_EM, исключение при попытке выполнения запроса для пустого списка дат

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_WithoutTrucks_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //Создание списка дат
            List<ProductionDate> dates = new List<ProductionDate>();
            ProductionDate date1 = new ProductionDate(2020, 3, 6);
            ProductionDate date2 = new ProductionDate(2021, 5, 11);
            ProductionDate date3 = new ProductionDate(2022, 7, 17);
            ProductionDate date4 = new ProductionDate(2023, 9, 22);
            ProductionDate date5 = new ProductionDate(2024, 12, 28);
            dates.Add(date1);
            dates.Add(date2);
            dates.Add(date3);
            dates.Add(date4);
            dates.Add(date5);

            CreateFactory_NoTruck(ref collection);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_EM(collection, dates));
        }//Метод JoinTrucksAndProductionDateByYear_EM, исключение при попытке выполнения запроса для колекции, в которой отсутствуют элементы нужного типа

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_WithTrucks()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //Создание списка дат
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Ferrari", 202334, "цвет", 2022, 124, 12, 102);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Truck truck3 = new Truck("Renault", 2023389, "White", 2021, 224, 54, 300);
            Car car3 = new Car("Kia", 2023389, "White", 2021, 225, 58);

            list2.Add(truck1);
            list2.Add(truck2);
            list1.Add(car1);
            list2.Add(truck3);
            list1.Add(car3);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            var result = Program.JoinTrucksAndProductionDateByYear_EM(collection, dates);
            var consoleOutput = CaptureConsoleOutput(() => Program.PrintListOfResults(result));

            Assert.IsNotNull(result);
            Assert.AreEqual(3, CountElements(result));
            Assert.IsTrue(consoleOutput.Contains("{ Brand = Ferrari, Year = 2022, Month = 7, Day = 17 }"));
            Assert.IsTrue(consoleOutput.Contains("{ Brand = Renault, Year = 2021, Month = 5, Day = 11 }"));
        }//Метод JoinTrucksAndProductionDateByYear_EM, правильное соединение элементов типа Truck и ProductionDate

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();
            //Создание списка дат
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_LINQ(emptyCollection, dates));
        }//Метод JoinTrucksAndProductionDateByYear_LINQ, исключение при попытке выполнения запроса в пустой коллекции

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_ExceptionForEmptyDatesList()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            List<ProductionDate> emptyDates = new List<ProductionDate>();

            CreateFactory_AllTypes(ref collection);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_LINQ(collection, emptyDates));
        }//Метод JoinTrucksAndProductionDateByYear_LINQ, исключение при попытке выполнения запроса для пустого списка дат

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_WithoutTrucks_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //Создание списка дат
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            CreateFactory_NoTruck(ref collection);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_LINQ(collection, dates));
        }//Метод JoinTrucksAndProductionDateByYear_LINQ, исключение при попытке выполнения запроса для колекции, в которой отсутствуют элементы нужного типа

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_WithTrucks()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //Создание списка дат
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("Машина", 202334, "цвет", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Ferrari", 202334, "цвет", 2022, 124, 12, 102);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Truck truck3 = new Truck("Renault", 2023389, "White", 2021, 224, 54, 300);
            Car car3 = new Car("Kia", 2023389, "White", 2021, 225, 58);

            list2.Add(truck1);
            list2.Add(truck2);
            list1.Add(car1);
            list2.Add(truck3);
            list1.Add(car3);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            var result = Program.JoinTrucksAndProductionDateByYear_LINQ(collection, dates);
            var consoleOutput = CaptureConsoleOutput(() => Program.PrintListOfResults(result));

            Assert.IsNotNull(result);
            Assert.AreEqual(3, CountElements(result));
            Assert.IsTrue(consoleOutput.Contains("{ Brand = Ferrari, Year = 2022, Month = 7, Day = 17 }"));
            Assert.IsTrue(consoleOutput.Contains("{ Brand = Renault, Year = 2021, Month = 5, Day = 11 }"));
        }//Метод JoinTrucksAndProductionDateByYear_LINQ, правильное соединение элементов типа Truck и ProductionDate
        #endregion

        #endregion

        #region Тестирование методов(запросов) для 2 части
        #region Выборка
        [TestMethod]
        public void SampleOfPassengerCars_BrandSrartsWithT_EM_NedeedCarsExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 3892389, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 3892389, "Red", 2021, 234, 13, 6, 100);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Remove(collection[0]);

            var result = Program.SampleOfPassengerCars_BrandSrartsWithT_EM(collection);

            Assert.AreEqual(2, CountElements(result));
            Assert.IsNotNull(result);
        }// Метод SampleOfPassengerCars_BrandSrartsWithT_EM, проверка на то, что метод работает корректно при наличии нужных элементов

        [TestMethod]
        public void SampleOfPassengerCars_BrandSrartsWithT_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfPassengerCars_BrandSrartsWithT_EM(collection));
        }// Метод SampleOfPassengerCars_BrandSrartsWithT_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void SampleOfPassengerCars_BrandSrartsWithT_LINQ_ValidCollection()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 3892389, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 3892389, "Red", 2021, 234, 13, 6, 100);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Remove(collection[0]);

          
            var result = Program.SampleOfPassengerCar_BrandSrartsWithT_LINQ(collection);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, CountElements(result));
        }// Метод SampleOfPassengerCars_BrandSrartsWithT_LINQ, проверка на то, что метод работает корректно при наличии нужных элементов

        [TestMethod]
        public void SampleOfPassengerCars_BrandSrartsWithT_LINQ_NedeedCarsExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfPassengerCar_BrandSrartsWithT_LINQ(collection));
        }// Метод SampleOfTrucks_CapacityMoreThan100_LINQ, исключение при попытке выполнения запроса для пустой коллекции
        #endregion

        #region Количество
        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.CountOfPassengerCar_CostMoreThan4Million_EM(emptyCollection));
        }// Метод CountOfPassengerCar_CostMoreThan4Million_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_LINQ_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.CountOfPassengerCar_CostMoreThan4Million_LINQ(emptyCollection));
        }// Метод CountOfPassengerCar_CostMoreThan4Million_LINQ, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_EM_NoMatchingCars()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 3892389, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 2892389, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3= new PassengerCar("Toyota", 1892389, "Red", 2021, 234, 13, 6, 100);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Remove(collection[0]);


            Assert.ThrowsException<Exception>(() => Program.CountOfPassengerCar_CostMoreThan4Million_EM(collection));
        }// Метод CountOfPassengerCar_CostMoreThan4Million_EM, исключение при попытке выполнения запроса для коллекции, в которой нет подходящих элементов

        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_LINQ_NoMatchingCars()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 3892389, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 2892389, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3 = new PassengerCar("Toyota", 1892389, "Red", 2021, 234, 13, 6, 100);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Remove(collection[0]);

            Assert.ThrowsException<Exception>(() => Program.CountOfPassengerCar_CostMoreThan4Million_LINQ(collection));
        }// Метод CountOfPassengerCar_CostMoreThan4Million_LINQ, исключение при попытке выполнения запроса для коллекции, в которой нет подходящих элементов

        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_EM_MatchingCarsExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 4000001, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 12000000, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3 = new PassengerCar("Toyota", 1892389, "Red", 2021, 234, 13, 6, 100);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Remove(collection[0]);

            var result = Program.CountOfPassengerCar_CostMoreThan4Million_EM(collection);

            Assert.AreEqual(2, result);
        }// Метод CountOfPassengerCar_CostMoreThan4Million_EM, проверка на правильность работы запроса для коллекции, в которой есть подходящие элементы

        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_LINQ_MatchingCarsExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 4000001, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 12000000, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3 = new PassengerCar("Toyota", 1892389, "Red", 2021, 234, 13, 6, 100);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Remove(collection[0]);

            var result = Program.CountOfPassengerCar_CostMoreThan4Million_LINQ(collection);

            Assert.AreEqual(2, result);
        }// Метод CountOfPassengerCar_CostMoreThan4Million_LINQ, проверка на правильность работы запроса для коллекции, в которой есть подходящие элементы
        #endregion

        #region Максимум
        [TestMethod]
        public void MaxSpeedOfPassengerCars_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.MaxSpeedOfPassengerCars_EM(emptyCollection));
        }// Метод MaxSpeedOfPassengerCars_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void MaxSpeedOfPassengerCars_LINQ_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.MaxSpeedOfPassengerCars_LINQ(emptyCollection));
        }// Метод MaxSpeedOfPassengerCars_LINQ, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void MaxSpeedOfPassengerCars_EM_PassengerCarsExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 4000001, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 12000000, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3 = new PassengerCar("Toyota", 1892389, "Red", 2021, 234, 13, 6, 54);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Remove(collection[0]);


            var result = Program.MaxSpeedOfPassengerCars_EM(collection);

            Assert.AreEqual(100, result);
        }// Метод MaxSpeedOfPassengerCars_EM, проверка на правильное нахождение максимальной скорочти среди элементов коллекции

        [TestMethod]
        public void MaxSpeedOfPassengerCars_LINQ_PassengerCarsExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota", 4000001, "Red", 2021, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota", 12000000, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3 = new PassengerCar("Toyota", 1892389, "Red", 2021, 234, 13, 6, 54);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Remove(collection[0]);

            var result = Program.MaxSpeedOfPassengerCars_LINQ(collection);

            Assert.AreEqual(100, result);
        }// Метод MaxSpeedOfPassengerCars_LINQ, проверка на правильное нахождение максимальной скорочти среди элементов коллекции
        #endregion

        #region Группировка
        [TestMethod]
        public void GroupByNumSeats_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.GroupByNumSeats_EM(emptyCollection));
        }//Метод GroupByNumSeats_EM, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void GroupByNumSeats_LINQ_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.GroupByNumSeats_LINQ(emptyCollection));
        }//Метод GroupByNumSeats_LINQ, исключение при попытке выполнения запроса для пустой коллекции

        [TestMethod]
        public void GroupByNumSeats_EM_PassengerCarExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota1", 4000001, "Red", 2023, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota2", 12000000, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3 = new PassengerCar("Toyota3", 1892389, "Red", 2021, 234, 13, 6, 54);
            PassengerCar pc4 = new PassengerCar("Toyota4", 1892389, "Red", 2022, 234, 13, 6, 54);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Add(pc4);
            collection.Remove(collection[0]);

            var result = Program.GroupByNumSeats_EM(collection);

            Assert.AreEqual(2, result.Count());

            var group1 = result.FirstOrDefault(g => g.Key == 4);
            Assert.IsNotNull(group1); // Проверка, что группа с элементами, имеющими 4 места существует
            Assert.AreEqual(1, group1.Count()); // Проверка на то, что количество элементов, с 4 местами, считается правильно

            var group2 = result.FirstOrDefault(g => g.Key == 6);
            Assert.IsNotNull(group2); // Проверка, что группа с элементами, имеющими 6 мест существует
            Assert.AreEqual(3, group2.Count()); // Проверка на то, что количество элементов, с 6 местами, считается правильно
        }//Метод GroupByNumSeats_EM, проверка на правильность работы запроса

        [TestMethod]
        public void GroupByNumSeats_LINQ_PassengerCarExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>(1);
            PassengerCar pc1 = new PassengerCar("Toyota1", 4000001, "Red", 2023, 234, 12, 4, 87);
            PassengerCar pc2 = new PassengerCar("Toyota2", 12000000, "Red", 2021, 234, 13, 6, 100);
            PassengerCar pc3 = new PassengerCar("Toyota3", 1892389, "Red", 2021, 234, 13, 6, 54);
            PassengerCar pc4 = new PassengerCar("Toyota4", 1892389, "Red", 2022, 234, 13, 6, 54);

            collection.Add(pc1);
            collection.Add(pc2);
            collection.Add(pc3);
            collection.Add(pc4);
            collection.Remove(collection[0]);

            var result = Program.GroupByNumSeats_LINQ(collection);

            Assert.AreEqual(2, result.Count()); 

            var group1 = result.FirstOrDefault(g => g.Key == 4);
            Assert.IsNotNull(group1); // Проверка, что группа с элементами, имеющими 4 места существует
            Assert.AreEqual(1, group1.Count()); // Проверка на то, что количество элементов, с 4 местами, считается правильно

            var group2 = result.FirstOrDefault(g => g.Key == 6);
            Assert.IsNotNull(group2);// Проверка, что группа с элементами, имеющими 6 мест существует
            Assert.AreEqual(3, group2.Count()); // Проверка на то, что количество элементов, с 6 местами, считается правильно
        }//Метод GroupByNumSeats_LINQ, проверка на правильность работы запроса
        #endregion

        #endregion

        #region Тестирование классов из 12 лабороторной
        //Тестирование класса PointHT<T>

        [TestMethod]
        public void DefaultConstructor_DataIsNull_ClassPointHT()
        {
            PointHT<Car> point = new();
            Assert.IsNull(point.Data);
        }//Конструктор по умолчанию, информационное поле data: null

        [TestMethod]
        public void ConstructorWithParameter_DataIsSet_ClassPointHT()
        {
            Car car = new Car();
            car.RandomInit();
            PointHT<Car> point = new PointHT<Car>(car);

            Assert.AreEqual(car, point.Data);
        }//Конструктор с параметром, проверка, что информационное поле data не null

        [TestMethod]
        public void MakeData_ReturnsNewPoint_ClassPointHT()
        {
            Car car = new Car("Машина", 12000000, "Black", 2024, 120, 93);
            PointHT<Car> point = PointHT<Car>.MakeData(car);

            // Assert
            Assert.AreEqual(car, point.Data);
            Assert.IsNull(point.Next);
            Assert.IsNull(point.Prev);
        }//метод MakeData, проверка на то, что метод возвращает новый элемент

        [TestMethod]
        public void ToString_DataIsNull_ClassPointHT()
        {
            PointHT<Car> point = new PointHT<Car>();
            string result = point.ToString();

            Assert.AreEqual("", result);
        }//Проверка вывода пустой строки, если информационное поле data пустое

        [TestMethod]
        public void ToString_DataIsNotNull_ClassPointHT()
        {
            Car car = new Car();
            car.RandomInit();
            PointHT<Car> point = new PointHT<Car>(car);

            string result = point.ToString();

            Assert.IsNotNull(result);
        }//Проверка вывода пустой строки, если информационное поле data пустое

        [TestMethod]
        public void GetHashCode_DataIsNull_ClassPointHT()
        {
            PointHT<Car> point = new PointHT<Car>();
            int result = point.GetHashCode();

            Assert.AreEqual(0, result);
        }//Метод GetHashCode, проверка на возвращение 0 в качестве хеш-кода для пустой строки

        [TestMethod]
        public void GetHashCode_DataIsNotNull_ClassPointHT()
        {
            Car car = new Car();
            car.RandomInit();
            PointHT<Car> point = new PointHT<Car>(car);
            int result = point.GetHashCode();

            Assert.AreEqual(car.GetHashCode(), result);
        }//Метод GetHashCode, проверка на возвращение числа в качестве хеш-кода для непустой строки





        //Тестирование класса MyCollection<T>
        //КОНСТРУКТОРЫ
        [TestMethod]
        public void DefaultConstructor_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>();

            Assert.AreEqual(0, collection.Count);
        }//Конструктор по умолчанию, проверка на то, что свойство Count равно 0

        [TestMethod]
        public void ConstructorWithParameter_SizeOfCollection_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Assert.IsNotNull(collection);
            Assert.AreEqual(3, collection.Count);
        }//Конструктор с параметром(размер коллекции), проверка на то, что коллекция не является нулевой ссылкой 

        [TestMethod]
        public void ConstructorWithParameter_SizeOfCollection_ReturnsException_ClassMyCollection()
        {
            Assert.ThrowsException<Exception>(() => { MyCollection<Car> table = new MyCollection<Car>(0); });
        }//Конструктор с параметром(размер коллекции),сключение при содании коллекции от 0 элементов

        [TestMethod]
        public void ConstructorWithParameter_OtherCollection_CollectionIsNotNull_ClassMyCollection()
        {
            MyCollection<Car> collection1 = new MyCollection<Car>(5);
            MyCollection<Car> collection2 = new MyCollection<Car>(collection1);

            for (int i = 0; i < collection1.Count; i++)
            {

                Assert.AreEqual(collection2[i], collection1[i]);
            }
        }//Конструктор с параметром(другая коллекция - не нулевая), проверка на то, что коллекции имеют одинаковые элементы

        [TestMethod]
        public void ConstructorWithParametr_OtherCollection_CollectionIsNull_ReturnsException_ClassMyCollection()
        {
            MyCollection<Car> otherCollection = null;

            Assert.ThrowsException<Exception>(() => { MyCollection<Car> collection2 = new MyCollection<Car>(otherCollection); });
        }//Конструктор с параметром(другая коллекция - нулевая), проверка на выбрасывание исключения при попытке создать коллекцию от ссылки null

        [TestMethod]
        public void ConstructorWithParametr_OtherCollection_CreatesChainWithValidLinks_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1); //Создаем коллекцию
            Car car1 = new Car("Машина", 12000000, "Black", 2021, 120, 93);
            Car car2 = new Car("Машина", 12000000, "White", 2022, 123, 95);
            //Добавляем в коллекцию 2 элемента
            collection.Add(car1);
            collection.Add(car2);

            //С помощью конструктора с параметром на основе первой коллекции создаем новую коллекцию
            MyCollection<Car> newCollection = new MyCollection<Car>(collection);

            //Проходимся по всем элементам коллекций и сравниваем их
            for (int i = 0; i < collection.table.Length; i++)
            {
                PointHT<Car> originalCurrent = collection.table[i];
                PointHT<Car> newCurrent = newCollection.table[i];

                while (originalCurrent != null && newCurrent != null)
                {
                    //Проверяем, что элементы коллекций с одним и тем же индексом - одинаковые
                    Assert.AreEqual(originalCurrent.Data, newCurrent.Data);

                    //Если эти элементы не последние в цепочках
                    if (originalCurrent.Next != null && newCurrent.Next != null)
                    {
                        // Проверяем связи между элементами
                        Assert.AreEqual(originalCurrent.Next.Data, newCurrent.Next.Data);
                        Assert.AreEqual(originalCurrent.Next.Prev.Data, originalCurrent.Data);
                        Assert.AreEqual(newCurrent.Next.Prev.Data, newCurrent.Data);
                    }

                    //Передвигаемся дальше
                    originalCurrent = originalCurrent.Next;
                    newCurrent = newCurrent.Next;
                }
            }
        }//Конструктор с параметром(другая коллекция), проверка связей в склонированной коллекции

        //МЕТОДЫ
        [TestMethod]
        public void Add_CountIncreasesAndContainsIsTrue_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);
            Car car = new Car("Машина", 12000000, "Black", 2022, 120, 93);

            collection.Add(car);

            Assert.IsTrue(collection.Contains(car));
            Assert.AreEqual(2, collection.Count);
        }//Метод Add,  проверка на то, что элемент действительно добавлен в коллекцию

        [TestMethod]
        public void Add_AddsExistingElementToTable_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car = new Car("Машина", 12000000, "Black", 2023, 120, 93);
            table.Add(car);

            Assert.ThrowsException<Exception>(() => { table.Add(car); });
        }//Метод Add, проверка на выбрасывание исключения при попытке добавления уже существующего в таблице элемента 

        [TestMethod]
        public void PrintTable_TableIsNull_Exception_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>();


            Assert.ThrowsException<Exception>(() => { table.PrintTable(); });
        }//Метод Contains, проверка на выбрасывание исключения при попытке поиска в пустой коллекции

        [TestMethod]
        public void Contains_ElementExists_ReturnTrue_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(5);
            Car car = new Car("Машина", 12000000, "Black", 2021, 120, 93);
            table.Add(car);

            bool result = table.Contains(car);

            Assert.IsTrue(result);
        }//Метод Contains, проверка на возвращение значения true, если элемент действительно содержится в коллекции

        [TestMethod]
        public void Contains_ElementDoesntExist_Returnfalse_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(5);
            Car car = new Car("Отсутствует", 12000000, "Black", 2022, 120, 93);

            bool result = table.Contains(car);

            Assert.IsFalse(result);
        }//Метод Contains, проверка на возвращение значения false, если элемент не содержится в коллекции

        [TestMethod]
        public void Contains_TableIsNull_NullReferenceException_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>();
            Car car = new Car("Отсутствует", 12000000, "Black", 2023, 120, 93);

            Assert.ThrowsException<NullReferenceException>(() => { table.Contains(car); });
        }//Метод Contains, проверка на выбрасывание исключения при попытке поиска в пустой коллекции

        [TestMethod]
        public void Contains_DataIsFoundAndElementIsFirstInChain_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car1 = new Car("Машина1", 12000000, "Black", 2024, 120, 93);
            Car car2 = new Car("Машина2", 12000000, "Black", 2021, 120, 93);
            Car car3 = new Car("Машина3", 12000000, "Black", 2020, 120, 93);

            table.Add(car1);
            table.Add(car2);
            table.Add(car3);

            Car car = table.GetPointWithIndexZero().Data;

            bool result = table.Contains(car);

            Assert.IsTrue(result);
        }//Метод Contains, попытка удаления существующего элемента, который является первым в цепочке

        [TestMethod]
        public void Clear_CountIsZero_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);
            Car car1 = new Car("Машина", 12000000, "Black", 2022, 120, 93);
            Car car2 = new Car("Машина", 12000000, "White", 2023, 123, 95);

            collection.Add(car1);
            collection.Add(car2);

            collection.Clear();

            Assert.AreEqual(0, collection.Count);
        }//Метод Clear, проверка на то, что после очищения памяти кол-во элементов в коллекции равно 0

        [TestMethod]
        public void CopyTo_NullArray_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>();

            Assert.ThrowsException<Exception>(() => { collection.CopyTo(null, 0); });
        }//Метод CopeTo, попытка скопировать элементы в нулевой массив - исключение

        [TestMethod]
        public void CopyTo_IndexOutOfRange_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);

            Car[] array = new Car[1];

            Assert.ThrowsException<Exception>(() => { collection.CopyTo(array, -1); });
            Assert.ThrowsException<Exception>(() => { collection.CopyTo(array, 2); });
        }//Метод CopeTo, индекс, начиная с которого необходимо добавить элементы в массив, выходит за границы массива - исключение

        [TestMethod]
        public void CopyTo_NotEnoughSpaceInArray_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Car[] array = new Car[1];

            Assert.ThrowsException<Exception>(() => { collection.CopyTo(array, 0); });
        }//Метод CopeTo, в массиве недостаточно места для копирования элементов - исключение

        [TestMethod]
        public void CopyTo_CopyItemsToArray_ElementsAreEqual_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(5);

            Car[] array = new Car[10];

            collection.CopyTo(array, 0);
            int i = 0;
            foreach (Car item in collection)
            {
                Assert.AreEqual(item, array[i]);
                i++;
            }

        }//Метод CopeTo, проверка на то, что элементы правильно копируются в массив

        [TestMethod]
        public void FindIndex_ReturnsNegative_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);
            Car newCar = new Car("Машина", 1500000, "White", 2022, 150, 21);
            int isFind = MyCollection<Car>.FindIndexInCollection(collection, newCar);

            Assert.AreEqual(isFind, -1);
        }//метод FindIndex, возвращение -1 если элемент не существует в коллекции

        [TestMethod]
        public void FindIndex_ReturnsIndex_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            int isFind = MyCollection<Car>.FindIndexInCollection(collection, collection[0]);

            Assert.AreEqual(isFind, 0);
        }//метод FindIndex, возвращение индекса элемента если элемент существует в коллекции

        [TestMethod]
        public void IndexerSet_WrongIndex_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Car newCar = new Car("Toyota", 1500000, "White", 2022, 150, 21);

            Assert.ThrowsException<Exception>(() => { collection[-1] = newCar; });
        }//Проверка блока Set индексатора, на выбрасывание исключения при попытке изменить элемент с неверным индексом

        [TestMethod]
        public void IndexerGet_WrongIndex_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Car newCar = new Car("Toyota", 1500000, "White", 2022, 150, 21);
            collection[1] = newCar;

            Assert.ThrowsException<Exception>(() => { collection.Remove(collection[-1]); });
        }//Проверка блока Get индексатора, на выбрасывание исключения при попытке получить элемент с неверным индексом

        [TestMethod]
        public void IndexerSet_ValueIsSet_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(5);
            Car oldCar = collection[3];
            Car newCar = new Car("Toyota", 1500000, "White", 2022, 150, 21);
            collection[3] = newCar;

            Assert.IsTrue(collection.Contains(newCar));
            Assert.IsFalse(collection.Contains(oldCar));
        }//Блок Set индексатора, проверка, что элемент с заданным индексом действительно изменен

        [TestMethod]
        public void RemoveData_DataIsNull_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(2);

            Assert.ThrowsException<Exception>(() => { table.Remove(null); });
        }//Метод RemoveData, попытка удаления пустого элемента

        [TestMethod]
        public void Remove_TableIsNull_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>();
            Car car = new Car("Отсутствует", 12000000, "Black", 2022, 120, 93);

            Assert.ThrowsException<NullReferenceException>(() => table.Remove(car));
        }//Метод Remove, исключение при попытке удаления элемента из непроинициализированной таблицы

        [TestMethod]
        public void Remove_ReturnsFalse_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);
            Car car0 = new Car("Lada", 18417495, "Salmon", 2024, 341, 36);

            collection.Remove(collection[0]);

            bool result = collection.Remove(car0);

            Assert.IsFalse(result);
        }//Метод Remove, тестирование условия if (table[index] == null) return false;(проверка, что после удаления результат false)

        [TestMethod]
        public void Remove_DataDoesNotExist_ReturnsFalse_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(5);
            Car car = new Car("Отсутствует", 12000000, "Green", 2022, 120, 93);

            bool result = table.Remove(car);

            Assert.IsFalse(result);
        }//Метод Remove, попытка удаления несуществующего элемента

        [TestMethod]
        public void Remove_DataIsFoundAndNoNextElement_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car = new Car("Отсутствует", 12000000, "Black", 2021, 120, 93);

            table.Add(car);

            bool result = table.Remove(car);

            Assert.IsTrue(result);
            Assert.IsFalse(table.Contains(car));
        }//Метод RemoveData, попытка удаления существующего элемента, после которого больше нет элементов в цепочке

        [TestMethod]
        public void RemoveData_DataIsFoundAndElementIsFirstInChain_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car1 = new Car("Машина1", 12000000, "Black", 2021, 120, 93);
            Car car2 = new Car("Машина2", 12000000, "Black", 2022, 120, 93);
            Car car3 = new Car("Машина3", 12000000, "Black", 2023, 120, 93);

            table.Add(car1);
            table.Add(car2);
            table.Add(car3);

            Car car = table[0];

            bool result = table.Remove(car);

            Assert.IsTrue(result);
            Assert.IsFalse(table.Contains(car));
        }//Метод RemoveData, попытка удаления существующего элемента, который является первым в цепочке

        [TestMethod]
        public void Remove_DataIsFoundAndIsInMiddle_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);

            Car car1 = new Car("Машина1", 12000000, "Black", 2021, 120, 93);
            Car car2 = new Car("Машина2", 12000000, "Black", 2022, 120, 93);
            Car car3 = new Car("Машина3", 12000000, "Black", 2023, 120, 93);

            table.Add(car1);
            table.Add(car2);
            table.Add(car3);


            bool result = table.Remove(car2);

            Assert.IsTrue(result);
            Assert.IsFalse(table.Contains(car2));
            Assert.IsTrue(table.Contains(car1));
            Assert.IsTrue(table.Contains(car3));
        }//Метод RemoveData, попытка удаления существующего элемента, который находится в середине цепочки

        [TestMethod]
        public void GetEnumerator_CollectionWithItems_InterfaceIEnumerable()
        {
            MyCollection<Car> collection = new MyCollection<Car>(5);

            Car[] cars = new Car[5];
            collection.CopyTo(cars, 0);

            int i = 0;
            foreach (Car item in collection)
            {
                Assert.AreEqual(cars[i], item);
                i++;
            }

        }//Метод GetEnumerator, проверка на то, что элементы коллекции можно перебрать с помощью цикла foreach

        [TestMethod]
        public void GetPointWithIndexZero_Exception_ClassMyHashTable()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            table.Remove(table.GetPointWithIndexZero().Data);
            Assert.ThrowsException<Exception>(() => { table.GetPointWithIndexZero(); });
        }//Метод GetPointWithIndexZero, проверка на выбрасывание исключения, если элемента с индексом 0 не существует

        [TestMethod]
        public void GetPointWithIndexZero_ElementIsNull_ClassMyHashTable()
        {
            MyCollection<Car> table = new MyCollection<Car>(10);

            if (table.GetPointWithIndexZero() != null) //Если элемент с индексом 0 не пустой
            {
                PointHT<Car>? current = table.GetPointWithIndexZero();
                while (current != null)//Проходимся по данной цепочке, пока не дойдем до конца
                {
                    table.Remove(table.GetPointWithIndexZero().Data);//Удаляем элемент из цепочки
                    current = current.Next;//Сдвигаемся на следующий
                }//Удаляем все элементы из цепочки с индексом 0
            }
            if (table.Count == 0) Assert.ThrowsException<Exception>(() => { table.GetPointWithIndexZero(); }); //Для случая когда все 10 элементов в цепочке с индексом 0
            else Assert.IsNull(table.GetPointWithIndexZero()); //Проверка условия if (table[0] == null) return null;
        }//Метод GetPointWithIndexZero, проверка на null, если элемент с индексом 0 пустой
        #endregion
    }
}