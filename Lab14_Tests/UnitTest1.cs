using ClassLibraryLab10;
using Lab_14;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Collections.ObjectModel;

namespace Lab14_Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region ��������������� ������ ��� ������������
        static int CountElements(IEnumerable<object> collection)
        {
            int count = 0;
            foreach(var item in collection)
            {
                count++;
            }
            return count;
        }//������� ���������� ��������� � ������������ ����������

        //������ ��� ������������ �������, ����� �������� ������-���� ���� �����������
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
        }//����� ��� �������� ������ ���(��� ������� Join)
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
        }//����� ��� �������� ��������� �� ������ ����� �� (��� ����)

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
        }//����� ��� �������� ��������� �� ������ ����� �� (��� ���� PassengerCar)

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
        }//����� ��� �������� ��������� �� ������ ����� �� (��� ���� Truck � PassengerCar)

        static void FillListRandomly<T>(List<object> list, int count, T sample) where T : IInit, new()
        {
            for (int i = 0; i < count; i++)
            {
                T element = new T();
                element.RandomInit();
                list.Add(element);
            }
        }//��������� ���������� ������ ������������� ���� ����������

        private string CaptureConsoleOutput(Action action)
        {
            // ��������� ����� StringWriter, ������� ����� �������������� ��� ��������� ������ �������
            // StringWriter - ��� ������� ��� StringBuilder ��� ������ �������� � ����� �����
            using (var consoleOutput = new StringWriter())
            {
                // ��������������� consoleOutput ��� ����� ������ �������, ����� ����������� ����� ���� �������
                Console.SetOut(consoleOutput);
                // ����������� ���������� �������� (action), ������� �������� �������� ������ ���������� � �������
                action.Invoke();

                return consoleOutput.ToString();
            }
        }

        #endregion

        #region ����� ��� ������� ������
        [TestMethod]
        public void PrintGeneralMenu_ExpectedAndActualStringsAreEqual()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.PrintGeneralMenu();

                string expected = "�������� ����� ����:" +
                "\n1. ������ ����� �� (��������� Queue<List<T>>)" +
                "\n2. ������ ����� �� ()" +
                "\n3. ��������� ������";

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

                string expected = "�������� ����� ����:" +
                "\n1. ������������ �������� Queue, ���������� ��������� ���� List" +
                "\n2. ������ ���������" +
                "\n3. ������ 1: ���������(Truck) � ����������������� > 100 ���� (Where)" +
                "\n4. ������ 2: ���������� �������� ������(PassengerCar) � ������������(Jeep) (Union)" +
                "\n5. ������ 3: ������� ��������� �����(PassengerCar) (Average)" +
                "\n6. ������ 4: ����������� ��������� ��������� �� ���� ������� (Group by)" +
                "\n7. ������ 5: ��������� ���������(Truck) � ���� ������������(ProductionDate) �� ���� ������� (Join)" +
                "\n8. ��������� ������";

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

                string expected = "�������� ����� ����:" +
                "\n1. ������������ �������� MyCollection<PassengerCar>" +
                "\n2. ������ ���������" +
                "\n3. ������ 1: �������� ������, ����� ������� ���������� �� ��������� 'T' (Where) " +
                "\n4. ������ 2: ���������� �������� �����, ��������� ������� ��������� 4 ���. ���. (Count)" +
                "\n5. ������ 3: ������������ �������� ����� �������� ����� (Max)" +
                "\n6. ������ 4: ����������� ��������� ��������� �� ���-�� ������������ ���� (Group by)" +
                "\n7. ��������� ������";

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

        #region ����� ��� ������� �������� ���������
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

        #region ������������ �������(��������) ��� 1 �����

        #region �������
        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_EM(collection));
        }// ����� SampleOfTrucks_CapacityMoreThan100_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_EM_WithoutTrucksAbove100Capacity_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("������", 202334, "����", 2022, 123,12,98);
            Truck truck2 = new Truck("������", 202334, "����", 2022, 123, 12, 99);
            Truck truck3 = new Truck("������", 202334, "����", 2022, 123, 12, 100);

            trucks.Add(truck1);
            trucks.Add(truck2);
            trucks.Add(truck3);

            collection.Enqueue(trucks);

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_EM(collection));
        }// ����� SampleOfTrucks_CapacityMoreThan100_EM, ���������� ��� ���������� ��������� � ������ �����������������

        [TestMethod]
        public void TestSampleOfTrucks_CapacityMoreThan100_EM_WithTrucksAbove100Capacity()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            Truck truck2 = new Truck("������", 202334, "����", 2022, 123, 12, 254);
            Truck truck3 = new Truck("������", 202334, "����", 2022, 123, 12, 80);
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
        }// ����� SampleOfTrucks_CapacityMoreThan100_EM, �������� �� ����������� ������ � ���������� ����������

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_LINQ_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_LINQ(emptyCollection));
        }// ����� SampleOfTrucks_CapacityMoreThan100_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_LINQ_WithoutTrucksAbove100Capacity_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 98);
            Truck truck2 = new Truck("������", 202334, "����", 2022, 123, 12, 99);
            Truck truck3 = new Truck("������", 202334, "����", 2022, 123, 12, 100);

            trucks.Add(truck1);
            trucks.Add(truck2);
            trucks.Add(truck3);

            collection.Enqueue(trucks);

            Assert.ThrowsException<Exception>(() => Program.SampleOfTrucks_CapacityMoreThan100_LINQ(collection));
        }// ����� SampleOfTrucks_CapacityMoreThan100_LINQ, ���������� ��� ���������� ��������� � ������ �����������������

        [TestMethod]
        public void SampleOfTrucks_CapacityMoreThan100_LINQ_WithTrucksAbove100Capacity()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> trucks = new List<object>();
            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            Truck truck2 = new Truck("������", 202334, "����", 2022, 123, 12, 254);
            Truck truck3 = new Truck("������", 202334, "����", 2022, 123, 12, 80);
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
        }// ����� SampleOfTrucks_CapacityMoreThan100_LINQ, �������� �� ����������� ������ � ���������� ����������
        #endregion

        #region �����������
        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_EM_WithEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_EM(emptyCollection));
        }// ����� UnionOfPassengersCarsAndJeeps_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_EM_WithoutPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            CreateFactory_NoPassengerCarAndJeep(ref collection);

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_EM(collection));
        }//����� UnionOfPassengersCarsAndJeeps_EM, ���������� ��� ���������� ��������� ������� ���� � ���������

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_EM_WithPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            CreateFactory_AllTypes(ref collection);

            IEnumerable<object> result = Program.UnionOfPassengersCarsAndJeeps_EM(collection);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, CountElements(result));
        }// ����� UnionOfPassengersCarsAndJeeps_EM, �������� �� ����������� ������ � ���������� ����������

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_LINQ_WithEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_LINQ(emptyCollection));
        }// ����� UnionOfPassengersCarsAndJeeps_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_LINQ_WithoutPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            CreateFactory_NoPassengerCarAndJeep(ref collection);

            Assert.ThrowsException<Exception>(() => Program.UnionOfPassengersCarsAndJeeps_LINQ(collection));
        }//����� UnionOfPassengersCarsAndJeeps_LINQ, ���������� ��� ���������� ��������� ������� ���� � ���������

        [TestMethod]
        public void UnionOfPassengersCarsAndJeeps_LINQ_WithPassengerCarsAndJeeps()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            CreateFactory_AllTypes(ref collection);

            IEnumerable<object> result = Program.UnionOfPassengersCarsAndJeeps_LINQ(collection);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, CountElements(result));
        }// ����� UnionOfPassengersCarsAndJeeps_LINQ, �������� �� ����������� ������ � ���������� ����������
        #endregion

        #region ������� ���������
        [TestMethod]
        public void AverageCostOfPassengerCars_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_EM(emptyCollection));
        }// ����� AverageCostOfPassengerCars_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void AverageCostOfPassengerCars_EM_WithoutPassengerCars_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            Truck truck2 = new Truck("������", 202334, "����", 2022, 123, 12, 254);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Car car2 = new Car("Kia", 2023389, "White", 2021, 224, 54);

            list1.Add(truck1);
            list2.Add(truck2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_EM(collection));
        }// ����� AverageCostOfPassengerCars_EM, ���������� ��� ���������� ��������� ������� ����

        [TestMethod]
        public void AverageCostOfPassengerCars_EM_WithPassengerCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("������", 202334, "����", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("������", 39489394, "����", 2022, 123, 12, 2, 56);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Jeep car2 = new Jeep("Kia", 2023389, "White", 2021, 224, 54, true, "�������");

            list1.Add(truck1);
            list2.Add(pc1);
            list2.Add(pc2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            double averageCost = Program.AverageCostOfPassengerCars_EM(collection);

            Assert.AreEqual((pc1.Cost+pc2.Cost)/2, averageCost);
        }// ����� AverageCostOfPassengerCars_EM, �������� �� ���������� ���������� ������� ��������� �������� �����

        [TestMethod]
        public void AverageCostOfPassengerCars_LINQ_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_LINQ(emptyCollection));
        }// ����� AverageCostOfPassengerCars_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void AverageCostOfPassengerCars_LINQ_WithoutPassengerCars_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            Truck truck2 = new Truck("������", 202334, "����", 2022, 123, 12, 254);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Car car2 = new Car("Kia", 2023389, "White", 2021, 224, 54);

            list1.Add(truck1);
            list2.Add(truck2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            Assert.ThrowsException<Exception>(() => Program.AverageCostOfPassengerCars_LINQ(collection));
        }// ����� AverageCostOfPassengerCars_LINQ, ���������� ��� ���������� ��������� ������� ����

        [TestMethod]
        public void AverageCostOfPassengerCars_LINQ_WithPassengerCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("������", 202334, "����", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("������", 39489394, "����", 2022, 123, 12, 2, 56);
            Car car1 = new Car("Kia", 2023389, "White", 2022, 234, 67);
            Jeep car2 = new Jeep("Kia", 2023389, "White", 2021, 224, 54, true, "�������");

            list1.Add(truck1);
            list2.Add(pc1);
            list2.Add(pc2);
            list1.Add(car1);
            list2.Add(car2);

            collection.Enqueue(list1);
            collection.Enqueue(list2);

            double averageCost = Program.AverageCostOfPassengerCars_LINQ(collection);

            Assert.AreEqual((pc1.Cost + pc2.Cost) / 2, averageCost);
        }// ����� AverageCostOfPassengerCars_LINQ, �������� �� ���������� ���������� ������� ��������� �������� �����
        #endregion

        #region �����������
        [TestMethod]
        public void GroupByYear_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.GroupByYear_EM(emptyCollection));
        }// ����� GroupByYear_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void GroupByYear_EM_WithCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            //��������� ��������� �������� � ����������
            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("������", 202334, "����", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("������", 39489394, "����", 2022, 123, 12, 2, 56);
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

            //���������� �������� ��������� �� ���� �������
            var result = Program.GroupByYear_EM(collection);

            Assert.AreEqual(2, result.Count());//��������� ���������� �����

            var group1 = result.FirstOrDefault(g => g.Key == 2022);
            Assert.IsNotNull(group1); // ���������, ��� ������ � 2022 ����� ������� ����������
            Assert.AreEqual(4, group1.Count()); // ���������, ��� ���������� ��������� � 2022 ����� ������� ����������

            var group2 = result.FirstOrDefault(g => g.Key == 2021);
            Assert.IsNotNull(group2); // ���������, ��� ������ � 2021 ����� ������� ����������
            Assert.AreEqual(2, group2.Count()); // ���������, ��� ���������� ��������� � 2021 ����� ������� ����������
        }// ����� GroupByYear_EM, �������� �� ���������� ������������ ������ ��������������� ���������

        [TestMethod]
        public void GroupByYear_LINQ_ExceptionWithEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();

            Assert.ThrowsException<Exception>(() => Program.GroupByYear_LINQ(emptyCollection));
        }// ����� GroupByYear_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void GroupByYear_LINQ_WithCars()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            //��������� ��������� �������� � ����������
            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            PassengerCar pc1 = new PassengerCar("������", 202334, "����", 2022, 123, 12, 2, 56);
            PassengerCar pc2 = new PassengerCar("������", 39489394, "����", 2022, 123, 12, 2, 56);
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

            //���������� �������� ��������� �� ���� �������
            var result = Program.GroupByYear_LINQ(collection);

            Assert.AreEqual(2, result.Count());//��������� ���������� �����

            var group1 = result.FirstOrDefault(g => g.Key == 2022);
            Assert.IsNotNull(group1); // ���������, ��� ������ � 2022 ����� ������� ����������
            Assert.AreEqual(4, group1.Count()); // ���������, ��� ���������� ��������� � 2022 ����� ������� ����������

            var group2 = result.FirstOrDefault(g => g.Key == 2021);
            Assert.IsNotNull(group2); // ���������, ��� ������ � 2021 ����� ������� ����������
            Assert.AreEqual(2, group2.Count()); // ���������, ��� ���������� ��������� � 2021 ����� ������� ����������
        }// ����� GroupByYear_LINQ, �������� �� ���������� ������������ ������ ��������������� ���������
        #endregion

        #region ����������
        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();
            //�������� ������ ���
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
        }//����� JoinTrucksAndProductionDateByYear_EM, ���������� ��� ������� ���������� ������� � ������ ���������

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_ExceptionForEmptyDatesList()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            List<ProductionDate> emptyDates = new List<ProductionDate>();

            CreateFactory_AllTypes(ref collection);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_EM(collection, emptyDates));
        }//����� JoinTrucksAndProductionDateByYear_EM, ���������� ��� ������� ���������� ������� ��� ������� ������ ���

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_WithoutTrucks_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //�������� ������ ���
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
        }//����� JoinTrucksAndProductionDateByYear_EM, ���������� ��� ������� ���������� ������� ��� ��������, � ������� ����������� �������� ������� ����

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_EM_WithTrucks()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //�������� ������ ���
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Ferrari", 202334, "����", 2022, 124, 12, 102);
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
        }//����� JoinTrucksAndProductionDateByYear_EM, ���������� ���������� ��������� ���� Truck � ProductionDate

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_ExceptionForEmptyCollection()
        {
            Queue<List<object>> emptyCollection = new Queue<List<object>>();
            //�������� ������ ���
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_LINQ(emptyCollection, dates));
        }//����� JoinTrucksAndProductionDateByYear_LINQ, ���������� ��� ������� ���������� ������� � ������ ���������

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_ExceptionForEmptyDatesList()
        {
            Queue<List<object>> collection = new Queue<List<object>>();
            List<ProductionDate> emptyDates = new List<ProductionDate>();

            CreateFactory_AllTypes(ref collection);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_LINQ(collection, emptyDates));
        }//����� JoinTrucksAndProductionDateByYear_LINQ, ���������� ��� ������� ���������� ������� ��� ������� ������ ���

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_WithoutTrucks_Exception()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //�������� ������ ���
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            CreateFactory_NoTruck(ref collection);

            Assert.ThrowsException<Exception>(() => Program.JoinTrucksAndProductionDateByYear_LINQ(collection, dates));
        }//����� JoinTrucksAndProductionDateByYear_LINQ, ���������� ��� ������� ���������� ������� ��� ��������, � ������� ����������� �������� ������� ����

        [TestMethod]
        public void JoinTrucksAndProductionDateByYear_LINQ_WithTrucks()
        {
            Queue<List<object>> collection = new Queue<List<object>>();

            //�������� ������ ���
            List<ProductionDate> dates = new List<ProductionDate>();
            CreateDatesList(ref dates);

            List<object> list1 = new List<object>();
            List<object> list2 = new List<object>();

            Truck truck1 = new Truck("������", 202334, "����", 2022, 123, 12, 102);
            Truck truck2 = new Truck("Ferrari", 202334, "����", 2022, 124, 12, 102);
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
        }//����� JoinTrucksAndProductionDateByYear_LINQ, ���������� ���������� ��������� ���� Truck � ProductionDate
        #endregion

        #endregion

        #region ������������ �������(��������) ��� 2 �����
        #region �������
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
        }// ����� SampleOfPassengerCars_BrandSrartsWithT_EM, �������� �� ��, ��� ����� �������� ��������� ��� ������� ������ ���������

        [TestMethod]
        public void SampleOfPassengerCars_BrandSrartsWithT_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfPassengerCars_BrandSrartsWithT_EM(collection));
        }// ����� SampleOfPassengerCars_BrandSrartsWithT_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

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
        }// ����� SampleOfPassengerCars_BrandSrartsWithT_LINQ, �������� �� ��, ��� ����� �������� ��������� ��� ������� ������ ���������

        [TestMethod]
        public void SampleOfPassengerCars_BrandSrartsWithT_LINQ_NedeedCarsExist()
        {
            MyCollection<PassengerCar> collection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.SampleOfPassengerCar_BrandSrartsWithT_LINQ(collection));
        }// ����� SampleOfTrucks_CapacityMoreThan100_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������
        #endregion

        #region ����������
        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.CountOfPassengerCar_CostMoreThan4Million_EM(emptyCollection));
        }// ����� CountOfPassengerCar_CostMoreThan4Million_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void CountOfPassengerCar_CostMoreThan4Million_LINQ_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.CountOfPassengerCar_CostMoreThan4Million_LINQ(emptyCollection));
        }// ����� CountOfPassengerCar_CostMoreThan4Million_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������

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
        }// ����� CountOfPassengerCar_CostMoreThan4Million_EM, ���������� ��� ������� ���������� ������� ��� ���������, � ������� ��� ���������� ���������

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
        }// ����� CountOfPassengerCar_CostMoreThan4Million_LINQ, ���������� ��� ������� ���������� ������� ��� ���������, � ������� ��� ���������� ���������

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
        }// ����� CountOfPassengerCar_CostMoreThan4Million_EM, �������� �� ������������ ������ ������� ��� ���������, � ������� ���� ���������� ��������

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
        }// ����� CountOfPassengerCar_CostMoreThan4Million_LINQ, �������� �� ������������ ������ ������� ��� ���������, � ������� ���� ���������� ��������
        #endregion

        #region ��������
        [TestMethod]
        public void MaxSpeedOfPassengerCars_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.MaxSpeedOfPassengerCars_EM(emptyCollection));
        }// ����� MaxSpeedOfPassengerCars_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void MaxSpeedOfPassengerCars_LINQ_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.MaxSpeedOfPassengerCars_LINQ(emptyCollection));
        }// ����� MaxSpeedOfPassengerCars_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������

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
        }// ����� MaxSpeedOfPassengerCars_EM, �������� �� ���������� ���������� ������������ �������� ����� ��������� ���������

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
        }// ����� MaxSpeedOfPassengerCars_LINQ, �������� �� ���������� ���������� ������������ �������� ����� ��������� ���������
        #endregion

        #region �����������
        [TestMethod]
        public void GroupByNumSeats_EM_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.GroupByNumSeats_EM(emptyCollection));
        }//����� GroupByNumSeats_EM, ���������� ��� ������� ���������� ������� ��� ������ ���������

        [TestMethod]
        public void GroupByNumSeats_LINQ_EmptyCollection()
        {
            MyCollection<PassengerCar> emptyCollection = new MyCollection<PassengerCar>();

            Assert.ThrowsException<Exception>(() => Program.GroupByNumSeats_LINQ(emptyCollection));
        }//����� GroupByNumSeats_LINQ, ���������� ��� ������� ���������� ������� ��� ������ ���������

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
            Assert.IsNotNull(group1); // ��������, ��� ������ � ����������, �������� 4 ����� ����������
            Assert.AreEqual(1, group1.Count()); // �������� �� ��, ��� ���������� ���������, � 4 �������, ��������� ���������

            var group2 = result.FirstOrDefault(g => g.Key == 6);
            Assert.IsNotNull(group2); // ��������, ��� ������ � ����������, �������� 6 ���� ����������
            Assert.AreEqual(3, group2.Count()); // �������� �� ��, ��� ���������� ���������, � 6 �������, ��������� ���������
        }//����� GroupByNumSeats_EM, �������� �� ������������ ������ �������

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
            Assert.IsNotNull(group1); // ��������, ��� ������ � ����������, �������� 4 ����� ����������
            Assert.AreEqual(1, group1.Count()); // �������� �� ��, ��� ���������� ���������, � 4 �������, ��������� ���������

            var group2 = result.FirstOrDefault(g => g.Key == 6);
            Assert.IsNotNull(group2);// ��������, ��� ������ � ����������, �������� 6 ���� ����������
            Assert.AreEqual(3, group2.Count()); // �������� �� ��, ��� ���������� ���������, � 6 �������, ��������� ���������
        }//����� GroupByNumSeats_LINQ, �������� �� ������������ ������ �������
        #endregion

        #endregion

        #region ������������ ������� �� 12 ������������
        //������������ ������ PointHT<T>

        [TestMethod]
        public void DefaultConstructor_DataIsNull_ClassPointHT()
        {
            PointHT<Car> point = new();
            Assert.IsNull(point.Data);
        }//����������� �� ���������, �������������� ���� data: null

        [TestMethod]
        public void ConstructorWithParameter_DataIsSet_ClassPointHT()
        {
            Car car = new Car();
            car.RandomInit();
            PointHT<Car> point = new PointHT<Car>(car);

            Assert.AreEqual(car, point.Data);
        }//����������� � ����������, ��������, ��� �������������� ���� data �� null

        [TestMethod]
        public void MakeData_ReturnsNewPoint_ClassPointHT()
        {
            Car car = new Car("������", 12000000, "Black", 2024, 120, 93);
            PointHT<Car> point = PointHT<Car>.MakeData(car);

            // Assert
            Assert.AreEqual(car, point.Data);
            Assert.IsNull(point.Next);
            Assert.IsNull(point.Prev);
        }//����� MakeData, �������� �� ��, ��� ����� ���������� ����� �������

        [TestMethod]
        public void ToString_DataIsNull_ClassPointHT()
        {
            PointHT<Car> point = new PointHT<Car>();
            string result = point.ToString();

            Assert.AreEqual("", result);
        }//�������� ������ ������ ������, ���� �������������� ���� data ������

        [TestMethod]
        public void ToString_DataIsNotNull_ClassPointHT()
        {
            Car car = new Car();
            car.RandomInit();
            PointHT<Car> point = new PointHT<Car>(car);

            string result = point.ToString();

            Assert.IsNotNull(result);
        }//�������� ������ ������ ������, ���� �������������� ���� data ������

        [TestMethod]
        public void GetHashCode_DataIsNull_ClassPointHT()
        {
            PointHT<Car> point = new PointHT<Car>();
            int result = point.GetHashCode();

            Assert.AreEqual(0, result);
        }//����� GetHashCode, �������� �� ����������� 0 � �������� ���-���� ��� ������ ������

        [TestMethod]
        public void GetHashCode_DataIsNotNull_ClassPointHT()
        {
            Car car = new Car();
            car.RandomInit();
            PointHT<Car> point = new PointHT<Car>(car);
            int result = point.GetHashCode();

            Assert.AreEqual(car.GetHashCode(), result);
        }//����� GetHashCode, �������� �� ����������� ����� � �������� ���-���� ��� �������� ������





        //������������ ������ MyCollection<T>
        //������������
        [TestMethod]
        public void DefaultConstructor_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>();

            Assert.AreEqual(0, collection.Count);
        }//����������� �� ���������, �������� �� ��, ��� �������� Count ����� 0

        [TestMethod]
        public void ConstructorWithParameter_SizeOfCollection_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Assert.IsNotNull(collection);
            Assert.AreEqual(3, collection.Count);
        }//����������� � ����������(������ ���������), �������� �� ��, ��� ��������� �� �������� ������� ������� 

        [TestMethod]
        public void ConstructorWithParameter_SizeOfCollection_ReturnsException_ClassMyCollection()
        {
            Assert.ThrowsException<Exception>(() => { MyCollection<Car> table = new MyCollection<Car>(0); });
        }//����������� � ����������(������ ���������),��������� ��� ������� ��������� �� 0 ���������

        [TestMethod]
        public void ConstructorWithParameter_OtherCollection_CollectionIsNotNull_ClassMyCollection()
        {
            MyCollection<Car> collection1 = new MyCollection<Car>(5);
            MyCollection<Car> collection2 = new MyCollection<Car>(collection1);

            for (int i = 0; i < collection1.Count; i++)
            {

                Assert.AreEqual(collection2[i], collection1[i]);
            }
        }//����������� � ����������(������ ��������� - �� �������), �������� �� ��, ��� ��������� ����� ���������� ��������

        [TestMethod]
        public void ConstructorWithParametr_OtherCollection_CollectionIsNull_ReturnsException_ClassMyCollection()
        {
            MyCollection<Car> otherCollection = null;

            Assert.ThrowsException<Exception>(() => { MyCollection<Car> collection2 = new MyCollection<Car>(otherCollection); });
        }//����������� � ����������(������ ��������� - �������), �������� �� ������������ ���������� ��� ������� ������� ��������� �� ������ null

        [TestMethod]
        public void ConstructorWithParametr_OtherCollection_CreatesChainWithValidLinks_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1); //������� ���������
            Car car1 = new Car("������", 12000000, "Black", 2021, 120, 93);
            Car car2 = new Car("������", 12000000, "White", 2022, 123, 95);
            //��������� � ��������� 2 ��������
            collection.Add(car1);
            collection.Add(car2);

            //� ������� ������������ � ���������� �� ������ ������ ��������� ������� ����� ���������
            MyCollection<Car> newCollection = new MyCollection<Car>(collection);

            //���������� �� ���� ��������� ��������� � ���������� ��
            for (int i = 0; i < collection.table.Length; i++)
            {
                PointHT<Car> originalCurrent = collection.table[i];
                PointHT<Car> newCurrent = newCollection.table[i];

                while (originalCurrent != null && newCurrent != null)
                {
                    //���������, ��� �������� ��������� � ����� � ��� �� �������� - ����������
                    Assert.AreEqual(originalCurrent.Data, newCurrent.Data);

                    //���� ��� �������� �� ��������� � ��������
                    if (originalCurrent.Next != null && newCurrent.Next != null)
                    {
                        // ��������� ����� ����� ����������
                        Assert.AreEqual(originalCurrent.Next.Data, newCurrent.Next.Data);
                        Assert.AreEqual(originalCurrent.Next.Prev.Data, originalCurrent.Data);
                        Assert.AreEqual(newCurrent.Next.Prev.Data, newCurrent.Data);
                    }

                    //������������� ������
                    originalCurrent = originalCurrent.Next;
                    newCurrent = newCurrent.Next;
                }
            }
        }//����������� � ����������(������ ���������), �������� ������ � �������������� ���������

        //������
        [TestMethod]
        public void Add_CountIncreasesAndContainsIsTrue_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);
            Car car = new Car("������", 12000000, "Black", 2022, 120, 93);

            collection.Add(car);

            Assert.IsTrue(collection.Contains(car));
            Assert.AreEqual(2, collection.Count);
        }//����� Add,  �������� �� ��, ��� ������� ������������� �������� � ���������

        [TestMethod]
        public void Add_AddsExistingElementToTable_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car = new Car("������", 12000000, "Black", 2023, 120, 93);
            table.Add(car);

            Assert.ThrowsException<Exception>(() => { table.Add(car); });
        }//����� Add, �������� �� ������������ ���������� ��� ������� ���������� ��� ������������� � ������� �������� 

        [TestMethod]
        public void PrintTable_TableIsNull_Exception_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>();


            Assert.ThrowsException<Exception>(() => { table.PrintTable(); });
        }//����� Contains, �������� �� ������������ ���������� ��� ������� ������ � ������ ���������

        [TestMethod]
        public void Contains_ElementExists_ReturnTrue_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(5);
            Car car = new Car("������", 12000000, "Black", 2021, 120, 93);
            table.Add(car);

            bool result = table.Contains(car);

            Assert.IsTrue(result);
        }//����� Contains, �������� �� ����������� �������� true, ���� ������� ������������� ���������� � ���������

        [TestMethod]
        public void Contains_ElementDoesntExist_Returnfalse_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(5);
            Car car = new Car("�����������", 12000000, "Black", 2022, 120, 93);

            bool result = table.Contains(car);

            Assert.IsFalse(result);
        }//����� Contains, �������� �� ����������� �������� false, ���� ������� �� ���������� � ���������

        [TestMethod]
        public void Contains_TableIsNull_NullReferenceException_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>();
            Car car = new Car("�����������", 12000000, "Black", 2023, 120, 93);

            Assert.ThrowsException<NullReferenceException>(() => { table.Contains(car); });
        }//����� Contains, �������� �� ������������ ���������� ��� ������� ������ � ������ ���������

        [TestMethod]
        public void Contains_DataIsFoundAndElementIsFirstInChain_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car1 = new Car("������1", 12000000, "Black", 2024, 120, 93);
            Car car2 = new Car("������2", 12000000, "Black", 2021, 120, 93);
            Car car3 = new Car("������3", 12000000, "Black", 2020, 120, 93);

            table.Add(car1);
            table.Add(car2);
            table.Add(car3);

            Car car = table.GetPointWithIndexZero().Data;

            bool result = table.Contains(car);

            Assert.IsTrue(result);
        }//����� Contains, ������� �������� ������������� ��������, ������� �������� ������ � �������

        [TestMethod]
        public void Clear_CountIsZero_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);
            Car car1 = new Car("������", 12000000, "Black", 2022, 120, 93);
            Car car2 = new Car("������", 12000000, "White", 2023, 123, 95);

            collection.Add(car1);
            collection.Add(car2);

            collection.Clear();

            Assert.AreEqual(0, collection.Count);
        }//����� Clear, �������� �� ��, ��� ����� �������� ������ ���-�� ��������� � ��������� ����� 0

        [TestMethod]
        public void CopyTo_NullArray_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>();

            Assert.ThrowsException<Exception>(() => { collection.CopyTo(null, 0); });
        }//����� CopeTo, ������� ����������� �������� � ������� ������ - ����������

        [TestMethod]
        public void CopyTo_IndexOutOfRange_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);

            Car[] array = new Car[1];

            Assert.ThrowsException<Exception>(() => { collection.CopyTo(array, -1); });
            Assert.ThrowsException<Exception>(() => { collection.CopyTo(array, 2); });
        }//����� CopeTo, ������, ������� � �������� ���������� �������� �������� � ������, ������� �� ������� ������� - ����������

        [TestMethod]
        public void CopyTo_NotEnoughSpaceInArray_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Car[] array = new Car[1];

            Assert.ThrowsException<Exception>(() => { collection.CopyTo(array, 0); });
        }//����� CopeTo, � ������� ������������ ����� ��� ����������� ��������� - ����������

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

        }//����� CopeTo, �������� �� ��, ��� �������� ��������� ���������� � ������

        [TestMethod]
        public void FindIndex_ReturnsNegative_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);
            Car newCar = new Car("������", 1500000, "White", 2022, 150, 21);
            int isFind = MyCollection<Car>.FindIndexInCollection(collection, newCar);

            Assert.AreEqual(isFind, -1);
        }//����� FindIndex, ����������� -1 ���� ������� �� ���������� � ���������

        [TestMethod]
        public void FindIndex_ReturnsIndex_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            int isFind = MyCollection<Car>.FindIndexInCollection(collection, collection[0]);

            Assert.AreEqual(isFind, 0);
        }//����� FindIndex, ����������� ������� �������� ���� ������� ���������� � ���������

        [TestMethod]
        public void IndexerSet_WrongIndex_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Car newCar = new Car("Toyota", 1500000, "White", 2022, 150, 21);

            Assert.ThrowsException<Exception>(() => { collection[-1] = newCar; });
        }//�������� ����� Set �����������, �� ������������ ���������� ��� ������� �������� ������� � �������� ��������

        [TestMethod]
        public void IndexerGet_WrongIndex_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(3);

            Car newCar = new Car("Toyota", 1500000, "White", 2022, 150, 21);
            collection[1] = newCar;

            Assert.ThrowsException<Exception>(() => { collection.Remove(collection[-1]); });
        }//�������� ����� Get �����������, �� ������������ ���������� ��� ������� �������� ������� � �������� ��������

        [TestMethod]
        public void IndexerSet_ValueIsSet_ClassMyObservableCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(5);
            Car oldCar = collection[3];
            Car newCar = new Car("Toyota", 1500000, "White", 2022, 150, 21);
            collection[3] = newCar;

            Assert.IsTrue(collection.Contains(newCar));
            Assert.IsFalse(collection.Contains(oldCar));
        }//���� Set �����������, ��������, ��� ������� � �������� �������� ������������� �������

        [TestMethod]
        public void RemoveData_DataIsNull_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(2);

            Assert.ThrowsException<Exception>(() => { table.Remove(null); });
        }//����� RemoveData, ������� �������� ������� ��������

        [TestMethod]
        public void Remove_TableIsNull_ThrowsException_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>();
            Car car = new Car("�����������", 12000000, "Black", 2022, 120, 93);

            Assert.ThrowsException<NullReferenceException>(() => table.Remove(car));
        }//����� Remove, ���������� ��� ������� �������� �������� �� ����������������������� �������

        [TestMethod]
        public void Remove_ReturnsFalse_ClassMyCollection()
        {
            MyCollection<Car> collection = new MyCollection<Car>(1);
            Car car0 = new Car("Lada", 18417495, "Salmon", 2024, 341, 36);

            collection.Remove(collection[0]);

            bool result = collection.Remove(car0);

            Assert.IsFalse(result);
        }//����� Remove, ������������ ������� if (table[index] == null) return false;(��������, ��� ����� �������� ��������� false)

        [TestMethod]
        public void Remove_DataDoesNotExist_ReturnsFalse_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(5);
            Car car = new Car("�����������", 12000000, "Green", 2022, 120, 93);

            bool result = table.Remove(car);

            Assert.IsFalse(result);
        }//����� Remove, ������� �������� ��������������� ��������

        [TestMethod]
        public void Remove_DataIsFoundAndNoNextElement_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car = new Car("�����������", 12000000, "Black", 2021, 120, 93);

            table.Add(car);

            bool result = table.Remove(car);

            Assert.IsTrue(result);
            Assert.IsFalse(table.Contains(car));
        }//����� RemoveData, ������� �������� ������������� ��������, ����� �������� ������ ��� ��������� � �������

        [TestMethod]
        public void RemoveData_DataIsFoundAndElementIsFirstInChain_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            Car car1 = new Car("������1", 12000000, "Black", 2021, 120, 93);
            Car car2 = new Car("������2", 12000000, "Black", 2022, 120, 93);
            Car car3 = new Car("������3", 12000000, "Black", 2023, 120, 93);

            table.Add(car1);
            table.Add(car2);
            table.Add(car3);

            Car car = table[0];

            bool result = table.Remove(car);

            Assert.IsTrue(result);
            Assert.IsFalse(table.Contains(car));
        }//����� RemoveData, ������� �������� ������������� ��������, ������� �������� ������ � �������

        [TestMethod]
        public void Remove_DataIsFoundAndIsInMiddle_ClassMyCollection()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);

            Car car1 = new Car("������1", 12000000, "Black", 2021, 120, 93);
            Car car2 = new Car("������2", 12000000, "Black", 2022, 120, 93);
            Car car3 = new Car("������3", 12000000, "Black", 2023, 120, 93);

            table.Add(car1);
            table.Add(car2);
            table.Add(car3);


            bool result = table.Remove(car2);

            Assert.IsTrue(result);
            Assert.IsFalse(table.Contains(car2));
            Assert.IsTrue(table.Contains(car1));
            Assert.IsTrue(table.Contains(car3));
        }//����� RemoveData, ������� �������� ������������� ��������, ������� ��������� � �������� �������

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

        }//����� GetEnumerator, �������� �� ��, ��� �������� ��������� ����� ��������� � ������� ����� foreach

        [TestMethod]
        public void GetPointWithIndexZero_Exception_ClassMyHashTable()
        {
            MyCollection<Car> table = new MyCollection<Car>(1);
            table.Remove(table.GetPointWithIndexZero().Data);
            Assert.ThrowsException<Exception>(() => { table.GetPointWithIndexZero(); });
        }//����� GetPointWithIndexZero, �������� �� ������������ ����������, ���� �������� � �������� 0 �� ����������

        [TestMethod]
        public void GetPointWithIndexZero_ElementIsNull_ClassMyHashTable()
        {
            MyCollection<Car> table = new MyCollection<Car>(10);

            if (table.GetPointWithIndexZero() != null) //���� ������� � �������� 0 �� ������
            {
                PointHT<Car>? current = table.GetPointWithIndexZero();
                while (current != null)//���������� �� ������ �������, ���� �� ������ �� �����
                {
                    table.Remove(table.GetPointWithIndexZero().Data);//������� ������� �� �������
                    current = current.Next;//���������� �� ���������
                }//������� ��� �������� �� ������� � �������� 0
            }
            if (table.Count == 0) Assert.ThrowsException<Exception>(() => { table.GetPointWithIndexZero(); }); //��� ������ ����� ��� 10 ��������� � ������� � �������� 0
            else Assert.IsNull(table.GetPointWithIndexZero()); //�������� ������� if (table[0] == null) return null;
        }//����� GetPointWithIndexZero, �������� �� null, ���� ������� � �������� 0 ������
        #endregion
    }
}