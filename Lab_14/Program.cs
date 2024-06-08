using ClassLibraryLab10;

namespace Lab_14
{
    public class Program
    {
        #region Ввод пункта меню
        public static int InputOptionOrSize(string msg)
        {
            int number;
            bool isConvertNumber;
            do
            {
                Console.Write(msg);
                isConvertNumber = int.TryParse(Console.ReadLine(), out number);
                if (!isConvertNumber || number <= 0) Console.WriteLine("Введено отрицательное число, 0 или строка. Попробуйте еще раз");
            } while (!isConvertNumber || number <= 0);
            return number;
        }//Ввод пункта меню или размера таблицы
        #endregion

        #region Вывод консольного меню
        public static void PrintGeneralMenu()
        {
            Console.WriteLine("\nВыберите пункт меню:" +
                "\n1. Первая часть ЛР (коллекция Queue<List<T>>)" +
                "\n2. Вторая часть ЛР ()" +
                "\n3. Завершить работу");
        }//Вывод общего меню

        public static void PrintMenu_FirstPart()
        {
            Console.WriteLine("\nВыберите пункт меню:" +
                "\n1. Формировании колекции Queue, содержащей коллекции типа List" +
                "\n2. Печать коллекции" +
                "\n3. Запрос 1: Грузовики(Truck) с грузоподьемностью > 100 тонн (Where)" +
                "\n4. Запрос 2: Объединить легковые машины(PassengerCar) и внедорожники(Jeep) (Union)" +
                "\n5. Запрос 3: Средняя стоимость машин(PassengerCar) (Average)" +
                "\n6. Запрос 4: Группировка элементов коллекции по году выпуска (Group by)" +
                "\n7. Запрос 5: Соединить грузовики(Truck) и дату производства(ProductionDate) по году выпуска (Join)" +
                "\n8. Завершить работу");
        }//Вывод меню для первого задания

        public static void PrintMenu_SecondPart()
        {
            Console.WriteLine("\nВыберите пункт меню:" +
                "\n1. Формировании колекции MyCollection<PassengerCar>" +
                "\n2. Печать коллекции" +
                "\n3. Запрос 1: Легковые машины, бренд которых начинается на латинскую 'T' (Where) " +
                "\n4. Запрос 2: Количество легковых машин, стоимость которых превышает 4 млн. руб. (Count)" +
                "\n5. Запрос 3: Максимальная скорость среди легковых машин (Max)" +
                "\n6. Запрос 4: Группировка элементов коллекции по кол-ву пассажирских мест (Group by)" +
                "\n7. Завершить работу");
        }//Вывод меню для второго задания
        #endregion

        #region Создание коллекции для первой части
        public static void CreateCarFactory(ref Queue<List<object>> collection, int count)
        {
            collection = new Queue<List<object>>();
            List<Type> types = new List<Type> { typeof(Car), typeof(PassengerCar), typeof(Truck), typeof(Jeep) };
            for (int i = 0;i < count; i++)
            {
                int sizeOfList = InputOptionOrSize($"\nВведите размер {i+1} списка:");
                List<object> cars = new List<object>();
                FillListRandomly(cars, sizeOfList, types);
                collection.Enqueue(cars);
            }

        }//Метод для создания коллекции из первой части ЛР

        public static void FillListRandomly(List<object> list, int count, List<Type> types)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                Type type = types[rand.Next(types.Count)];
                object element = Activator.CreateInstance(type);
                if (element is IInit) 
                {
                    ((IInit)element).RandomInit();
                    list.Add(element);
                } 
                
            }
        }//Метод для заполнения списка рандомными элементами

        //МЕТОДЫ ДЛЯ ДЕМОНСТРАЦИИ СЛУЧАЕВ, КОГДА ЭЛЕМЕНТЫ КАКОГО-ЛИБО ТИПА ОТСУТСТВУЮТ

        //static void CreateCarFactory(ref Queue<List<object>> collection)
        //{
        //    int count = 2;
        //    collection = new Queue<List<object>>();

        //    List<object> cars1 = new List<object>();
        //    FillListRandomly(cars1, count, new Car());
        //    List<object> cars2 = new List<object>();
        //    FillListRandomly(cars2, count, new Car());
        //    List<object> jeeps = new List<object>();
        //    FillListRandomly(jeeps, count, new Jeep());
        //    List<object> pasCars = new List<object>();
        //    FillListRandomly(pasCars, count, new PassengerCar());
        //    List<object> trucks = new List<object>();
        //    FillListRandomly(trucks, count, new Truck());

        //    collection.Enqueue(cars1);
        //    collection.Enqueue(cars2);
        //    collection.Enqueue(jeeps);
        //    collection.Enqueue(pasCars);
        //    collection.Enqueue(trucks);
        //}//Метод для создания коллекции из первой части ЛР

        //static void FillListRandomly<T>(List<object> list, int count, T sample) where T : IInit, new()
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        T element = new T();
        //        element.RandomInit();
        //        list.Add(element);
        //    }
        //}//Метод для заполнения списка рандомными элементами
        #endregion

        #region Методы печати для различных коллекций
        public static void PrintQueueCollection(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы");
            Console.WriteLine("\nАвтомобильный завод: ");
            int listNumber = 1;
            foreach (var list in collection)
            {
                Console.WriteLine($"\nЦех №{listNumber}");
                Console.WriteLine();
                foreach (var item in list)
                {
                    Console.WriteLine(item); 
                }
                listNumber++;
            }
        }//Метод печати для коллекции из первой части работы

        public static void PrintListOfResults(IEnumerable<object> collection)
        {
            if (!collection.Any())
            {
                throw new Exception("Элементы не найдены. Список результатов пуст");
            }
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }//Метод печати для результатов, полученных в запросах Join

        public static void PrintGroups(IEnumerable<IGrouping<int, object>> groups)
        {
            if (groups == null) throw new Exception("Список пуст");
            foreach (IGrouping<int, object> group in groups)
            {
                int count = 0;
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                    count++;
                }
                Console.WriteLine($"Количество элементов: {count}");
            }
        }//Метод печати для результатов, полученных в запросах группировки
        #endregion

        #region Запросы для первой части ЛР
        public static IEnumerable<object> SampleOfTrucks_CapacityMoreThan100_EM(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Получение грузовиков грузоподъемностью больше 100 тонн
            var res = collection
                    .SelectMany(item => item)
                    .Where(item2 => item2 is Truck && ((Truck)item2).Capacity > 100);

            if (!res.Any())
            {
                throw new Exception("Коллекция не содержит грузовики или элементы не найдены"); //ПРОВЕРЕНО
            }

            return res;
        }//Метод расширения: получение грузовиков грузоподъемностью больше 100 тонн

        public static IEnumerable<object> SampleOfTrucks_CapacityMoreThan100_LINQ(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Получение грузовиков грузоподъемностью больше 100 тонн
            var res = from item in collection
                      from item2 in item
                      where item2 is Truck && ((Truck)item2).Capacity > 100
                      select item2;

            if (!res.Any())
            {
                throw new Exception("Коллекция не содержит грузовики или элементы не найдены");//ПРОВЕРЕНО
            }

            return res;
        }//LINQ: получение грузовиков грузоподъемностью больше 100 тонн

        public static IEnumerable<object> UnionOfPassengersCarsAndJeeps_EM(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Объединение легковых машин и внедорожников
            var res = collection
                      .SelectMany(list => list)
                      .Where(item => item is PassengerCar)
                      .Union(collection
                            .SelectMany(list => list)
                            .Where(item => item is Jeep));
            if (!res.Any()) throw new Exception("Коллекция не содержит легковые машины и внедорожники"); //ПРОВЕРЕНО
            return res;
        }//Метод расширения: объединение легковых машин и внедорожников

        public static IEnumerable<object> UnionOfPassengersCarsAndJeeps_LINQ(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Объединение легковых машин и внедорожников
            var res = (from item in collection 
                       from item2 in item
                       where item2 is PassengerCar
                       select item2)
                       .Union(from item in collection
                              from item2 in item
                              where item2 is Jeep
                              select item2); 
            if (!res.Any()) throw new Exception("Коллекция не содержит легковые машины и внедорожники"); //ПРОВЕРЕНО
            return res;
        }//LINQ: объединение легковых машин и внедорожников

        public static double AverageCostOfPassengerCars_EM(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            if ((collection
                .SelectMany(item => item)
                .Where(item => item is PassengerCar))
                .ToList().Count == 0)
            {
                throw new Exception("Коллекция не содержит легковые машины"); //ПРОВЕРЕНО
            }//Проверка на то, что в коллекции есть легковые машины, чтобы не возникло деление на 0

            //Получение средней стоимости легковых машин
            var res = collection
                      .SelectMany(list => list)
                      .Where(item => item is PassengerCar)
                      .Select(item => ((PassengerCar)item).Cost)
                      .Average();

            return res;
        }//Метод расширения: получение средней стоимости легковых машин

        public static double AverageCostOfPassengerCars_LINQ(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО
            if ((collection.
                SelectMany(item => item)
                .Where(item => item is PassengerCar))
                .ToList().Count == 0)
            {
                throw new Exception("коллекция не содержит легковые машины"); //ПРОВЕРЕНО
            }//Проверка на то, что в коллекции есть легковые машины, чтобы не возникло деление на 0

            //Получение средней стоимости легковых машин
            var res = (from item in collection
                       from item2 in item
                       where item2 is PassengerCar
                       select ((PassengerCar)item2).Cost)
                       .Average();

            return res;
        }//LINQ: получение средней стоимости легковых машин

        public static IEnumerable<IGrouping<int, object>> GroupByYear_EM(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Группировка элементов коллекции по году выпуска
            var res = collection
                      .SelectMany(list => list)
                      .Where(item => item is Car)
                      .GroupBy(item => ((Car)item).Year);
            return res;
        }//Метод расширения: группировка элементов коллекции по году выпуска

        public static IEnumerable<IGrouping<int, object>> GroupByYear_LINQ(Queue<List<object>> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Группировка элементов коллекции по году выпуска
            var res = from item in collection
                      from item2 in item
                      where item2 is Car
                      group item2 by ((Car)item2).Year;

            return res;
        }//LINQ: группировка элементов коллекции по году выпуска

        public static IEnumerable<object> JoinTrucksAndProductionDateByYear_EM(Queue<List<object>> collection, List<ProductionDate> dates)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО
            if (dates.Count == 0) throw new Exception("Список дат не содержит элементы"); //ПРОВЕРЕНО

            //Соединение классов Truck и ProductionDate и создание нового элемента
            var res = collection
                      .SelectMany(list => list)
                      .Where(item => item is Truck)
                      .Join(dates,
                            item => ((Truck)item).Year,
                            t => t.Year,
                            (item, t) => new
                            {
                                ((Truck)item).Brand,
                                t.Year,
                                t.Month,
                                t.Day
                            });

            if (!res.Any())
            {
                throw new Exception("Коллекция не содержит грузовики"); //ПРОВЕРЕНО
            }

            return res;
        }//Метод расширения: соединение классов Truck и ProductionDate и создание нового элемента

        public static IEnumerable<object> JoinTrucksAndProductionDateByYear_LINQ(Queue<List<object>> collection, List<ProductionDate> dates)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО
            if (dates.Count == 0) throw new Exception("Список дат не содержит элементы"); //ПРОВЕРЕНО

            //Соединение классов Truck и ProductionDate и создание нового элемента
            var res = from item in collection
                      from item2 in item
                      where item2 is Truck
                      join t in dates on ((Truck)item2).Year equals t.Year
                      select new
                      {
                          ((Truck)item2).Brand,
                          t.Year,
                          t.Month,
                          t.Day
                      };

            if (!res.Any())
            {
                throw new Exception("Коллекция не содержит грузовики"); //ПРОВЕРЕНО
            }

            return res;
        }//LINQ: соединение классов Truck и ProductionDate и создание нового элемента
        #endregion

        #region Запросы для второй части ЛР
        public static IEnumerable<object> SampleOfPassengerCars_BrandSrartsWithT_EM(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Получение легковых машин, бренд которых начинается с латинской буквы 'T'
            var res = collection
                    .Where(item => item.Brand.StartsWith('T'));

            return res;
        }//Метод расширения: получение легковых машин, бренд которых начинается с латинской буквы 'T'

        public static IEnumerable<object> SampleOfPassengerCar_BrandSrartsWithT_LINQ(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Получение легковых машин, бренд которых начинается с латинской буквы 'T'
            var res = from item in collection
                       where item.Brand.StartsWith('T')
                       select item;
            return res;
        }//LINQ: получение легковых машин, бренд которых начинается с латинской буквы 'T'

        public static int CountOfPassengerCar_CostMoreThan4Million_EM(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Получение количества легковых автомобилей, стоимость которых больше 4 млн. руб.
            var res = collection
                    .Count(item => item.Cost > 4000000);

            if (res == 0)
            {
                throw new Exception("Коллекция не содержит машины(PassengerCar) или элементы не найдены"); 
            }

            return res;
        }//Метод расширения: получение количества легковых автомобилей, стоимость которых больше 4 млн. руб.

        public static int CountOfPassengerCar_CostMoreThan4Million_LINQ(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Получение количества легковых автомобилей, стоимость которых больше 4 млн. руб.
            var res = (from item in collection
                       where item.Cost > 4000000
                       select item).Count();

            if (res == 0)
            {
                throw new Exception("Коллекция не содержит машины(PassengerCar) или элементы не найдены");
            }

            return res;
        }//LINQ: получение количества легковых автомобилей, стоимость которых больше 4 млн. руб.

        public static double MaxSpeedOfPassengerCars_EM(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //if ((collection
            //    .Where(item => item is PassengerCar))
            //    .ToList().Count == 0)
            //{
            //    throw new Exception("Коллекция не содержит легковые машины"); 
            //}//Исключение при отсутствии легковых машин в коллекции

            //Получение максимальной скорости среди легковых автомобилей
            var res = collection
                      .Select(item => item.MaxSpeed)
                      .Max();

            return res;
        }//Метод расширения: получение максимальной скорости среди легковых автомобилей

        public static double MaxSpeedOfPassengerCars_LINQ(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО
            //if ((collection
            //    .Where(item => item is PassengerCar))
            //    .ToList().Count == 0)
            //{
            //    throw new Exception("Коллекция не содержит легковые машины"); 
            //}//Исключение при отсутствии легковых машин в коллекции

            //Получение максимальной скорости среди легковых автомобилей
            var res = (from item in collection
                       select item.MaxSpeed)
                       .Max();

            return res;
        }//LINQ: получение максимальной скорости среди легковых автомобилей

        public static IEnumerable<IGrouping<int, object>> GroupByNumSeats_EM(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Группировка элементов коллекции по количеству пассажирских мест
            var res = collection
                      .Where(item => item is PassengerCar)
                      .GroupBy(item => ((PassengerCar)item).NumSeats);
            return res;
        }//Метод расширения: группировка элементов коллекции по количеству пассажирских мест

        public static IEnumerable<IGrouping<int, object>> GroupByNumSeats_LINQ(MyCollection<PassengerCar> collection)
        {
            if (collection.Count == 0) throw new Exception("Коллекция не содержит элементы"); //ПРОВЕРЕНО

            //Группировка элементов коллекции по количеству пассажирских мест
            var res = from item in collection
                      where item is PassengerCar
                      group item by ((PassengerCar)item).NumSeats;

            return res;
        }//LINQ: группировка элементов коллекции по количеству пассажирских мест
        #endregion

        static void Main(string[] args)
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            int option1;
            do
            {
                PrintGeneralMenu();
                option1 = InputOptionOrSize("\nВыберите пунк меню: ");

                switch (option1)
                {
                    case 1:
                        {
                            int option;
                            do
                            {
                                PrintMenu_FirstPart();
                                option = InputOptionOrSize("\nВыберите пунк меню: ");

                                switch (option)
                                {
                                    case 1:
                                        {
                                            //Ввод размера коллекции
                                            int size = InputOptionOrSize("\nВведите размер коллекции:");
                                            //Создание коллекции и заполнение ее рандомными элементами
                                            CreateCarFactory(ref collection, size);
                                            Console.WriteLine("\nКоллекция сформирована!");
                                            break;
                                        }//Формировании колекции Queue, содержащей коллекции типа List
                                    case 2:
                                        {
                                            try
                                            {
                                                //печать коллекции
                                                PrintQueueCollection(collection);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            break;
                                        }//Печать коллекции
                                    case 3:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //Получение грузовиков грузоподъемностью больше 100 тонн
                                                IEnumerable<object> result1 = SampleOfTrucks_CapacityMoreThan100_EM(collection);
                                                PrintListOfResults(result1);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //Получение грузовиков грузоподъемностью больше 100 тонн
                                                IEnumerable<object> result2 = SampleOfTrucks_CapacityMoreThan100_LINQ(collection);
                                                PrintListOfResults(result2);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            break;
                                        }//Запрос 1: Грузовики(Truck) с грузоподьемностью > 100 тонн
                                    case 4:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //объединение легковых машин и внедорожников
                                                IEnumerable<object> result1 = UnionOfPassengersCarsAndJeeps_EM(collection);
                                                PrintListOfResults(result1);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //объединение легковых машин и внедорожников
                                                IEnumerable<object> result2 = UnionOfPassengersCarsAndJeeps_LINQ(collection);
                                                PrintListOfResults(result2);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            break;
                                        }//Запрос 2: Объединить легковые машины(PassengerCar) и внедорожники(Jeep)
                                    case 5:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //Получение средней стоимости легковых автомобилей
                                                double result1 = AverageCostOfPassengerCars_EM(collection);
                                                Console.WriteLine($"Средняя стоимость легковых автомобилей(тип PassengerCar): {result1}");
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //Получение средней стоимости легковых автомобилей
                                                double result2 = AverageCostOfPassengerCars_LINQ(collection);
                                                Console.WriteLine($"Средняя стоимость легковых автомобилей(тип PassengerCar): {result2}");
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            break;
                                        }//Запрос 3: Средняя стоимость машин(Car) (Average)
                                    case 6:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //группировка элементов коллекции по году выпуска
                                                IEnumerable<IGrouping<int, object>> result1 = GroupByYear_EM(collection);
                                                //печать результата
                                                PrintGroups(result1);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //группировка элементов коллекции по году выпуска
                                                IEnumerable<IGrouping<int, object>> result2 = GroupByYear_LINQ(collection);
                                                //печать результата
                                                PrintGroups(result2);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            break;
                                        }//Запрос 4: Группировка элементов коллекции по году выпуска
                                    case 7:
                                        {
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

                                            //Печать списка дат
                                            Console.WriteLine("\nСписок дат:");
                                            PrintListOfResults(dates);

                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //Соединение классов Truck и ProductionDate
                                                IEnumerable<object> result1 = JoinTrucksAndProductionDateByYear_EM(collection, dates);
                                                PrintListOfResults(result1);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            
                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //Соединение классов Truck и ProductionDate
                                                IEnumerable<object> result2 = JoinTrucksAndProductionDateByYear_LINQ(collection, dates);
                                                PrintListOfResults(result2);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            break;
                                        }//Запрос 5: Соединить грузовики(Truck) и дату производства(ProductionDate) по году выпуска
                                    case 8:
                                        {
                                            Console.WriteLine("Работа завершена!");
                                            break;
                                        }//Завершение работы
                                    default:
                                        {
                                            Console.WriteLine("Неправильно задан пункт меню. Попробуйте еще раз.");
                                            break;
                                        }
                                }
                            } while (option != 8);
                            break;
                        }//Первая часть работы
                    case 2:
                        {
                            //Создание коллекции из 12 лабораторной работы
                            MyCollection<PassengerCar> table = new MyCollection<PassengerCar>();

                            int option;
                            do
                            {
                                PrintMenu_SecondPart(); //Печать меню для второй части
                                option = InputOptionOrSize("\nВыберите пунк меню: ");

                                switch (option)
                                {
                                    case 1:
                                        {
                                            //Ввод размера коллекции
                                            int size = InputOptionOrSize("\nВведите размер таблицы:");
                                            //Создание коллекции и заполнение ее рандомными элементами
                                            table = new MyCollection<PassengerCar>(size);
                                            Console.WriteLine("Таблица создана");
                                            break;
                                        }//Формировании колекции MyCollection из 12 ЛР
                                    case 2:
                                        {
                                            Console.WriteLine("\nХеш-таблица:");
                                            try
                                            {
                                                //if (table.Count == 0) Console.WriteLine("Таблица пустая");
                                                //else table.PrintTable();
                                                table.PrintTable();
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Возникло исключение: {e.Message}");
                                                Console.WriteLine("Хеш-таблица не создана или не содержит элементы. Попробуйте еще раз");
                                            }
                                            break;
                                        }//Печать коллекции
                                    case 3:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //получение легковых машин, бренд которых начинается с латинской буквы 'T'
                                                IEnumerable<object> result1 = SampleOfPassengerCars_BrandSrartsWithT_EM(table);
                                                //печать результата
                                                PrintListOfResults(result1);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //получение легковых машин, бренд которых начинается с латинской буквы 'T'
                                                IEnumerable<object> result2 = SampleOfPassengerCar_BrandSrartsWithT_LINQ(table);
                                                //печать результата
                                                PrintListOfResults(result2);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            break;
                                        }//Запрос 1: Легковые машины, бренд которых начинается на латинскую 'T'
                                    case 4:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //Получение количества машин, стоимостью более 4 млн.руб.
                                                int result1 = CountOfPassengerCar_CostMoreThan4Million_EM(table);
                                                Console.WriteLine($"Кол-во машин стоимостью более 4 млн. руб. : {result1} ");
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //Получение количества машин, стоимостью более 4 млн.руб.
                                                int result2 = CountOfPassengerCar_CostMoreThan4Million_LINQ(table);
                                                Console.WriteLine($"Кол-во машин стоимостью более 4 млн. руб. : {result2} ");
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            break;
                                        }//Запрос 2: Количество легковых машин, стоимость которых превышает 4 млн. руб. (Count)
                                    case 5:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //Получение максимальной скорости среди легковых автомобилей
                                                double result1 = MaxSpeedOfPassengerCars_EM(table);
                                                Console.WriteLine($"Максимальная скорость автомобилей(тип PassengerCar): {result1}");
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //Получение максимальной скорости среди легковых автомобилей
                                                double result2 = MaxSpeedOfPassengerCars_LINQ(table);
                                                Console.WriteLine($"Максимальная скорость автомабилей(тип PassengerCar): {result2}");
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }
                                            break;
                                        }//Запрос 3: Максимальная скорость среди легковых машин (Max)
                                    case 6:
                                        {
                                            Console.WriteLine("\n-----Метод расширения-----");
                                            try
                                            {
                                                //Группировка элементов коллекции по кол-ву пассажирских мест
                                                IEnumerable<IGrouping<int, object>> result1 = GroupByNumSeats_EM(table);
                                                PrintGroups(result1);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            Console.WriteLine("\n-----LINQ-----");
                                            try
                                            {
                                                //Группировка элементов коллекции по кол-ву пассажирских мест
                                                IEnumerable<IGrouping<int, object>> result2 = GroupByNumSeats_LINQ(table);
                                                PrintGroups(result2);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Выброшено исключение: " + e.Message);
                                            }

                                            break;
                                        }//Запрос 4: Группировка элементов коллекции по кол-ву пассажирских мест (Group by)
                                    case 7:
                                        {
                                            Console.WriteLine("Работа завершена!");
                                            break;
                                        }//Завершение работы
                                    default:
                                        {
                                            Console.WriteLine("Неправильно задан пункт меню. Попробуйте еще раз.");
                                            break;
                                        }
                                }
                            } while (option != 7);
                            break;
                        }//Вторая часть работы
                    case 3:
                        {
                            Console.WriteLine("Работа завершена!");
                            break;
                        }//Завершение работы
                    default:
                        {
                            Console.WriteLine("Неправильно задан пункт меню. Попробуйте еще раз.");
                            break;
                        }
                }
            } while (option1 != 3);
        }
    }
}