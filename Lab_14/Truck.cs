using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibraryLab10
{
    public class Truck :Car, IInit, ICloneable
    {
        private double capacity;
        static int objectCount;
        static Random rnd = new Random();
        public double Capacity
        {
            get => capacity;
            set
            {
                if (value >= 0 && value <= 300)
                {
                    capacity = value;
                }
                else
                {
                    throw new Exception("Ошибка! Значение должно быть в диапазоне от 0 до 300 тонн.");
                }
            }

        }//Свойство для грузоподъемности грузовика

        public Car GetBase
        {
            get => new Car(brand, cost, color, year, groundClearance, id.number);//возвращает объект базового класса
        }
        #region Конструкторы
        public Truck() : base()
        {
            Capacity = 0;
            objectCount++;
        }//Конструктор по умолчанию
        public Truck(string brand, int cost, string color, int year, int groundClearance, int number, double capacity) : base(brand, cost, color, year, groundClearance, number)
        {
            Capacity = capacity;
            objectCount++;
        }//Конструктор с параметрами
        #endregion

        #region Методы
        public new void Show()
        {
            Console.Write("Грузовик: ");
            base.Show();
            Console.WriteLine($"грузоподъемность: {capacity} тонн");
        }//Вывод данных в консоль(обычный метод)
        public override void VirtualShow()
        {
            Console.Write("Грузовик: ");
            base.Show();
            Console.WriteLine($"грузоподъемность: {capacity} тонн");
            //Console.WriteLine($"Грузовик: бренд: {brand}; стоимость: {cost} руб. ; цвет: {color}; год выпуска: {year}; просвет: {groundClearance} мм; ID: {id.number}; грузоподъемность: {capacity} тонн");
        }//Вывод данных в консоль(виртуальный метод)

        public override void RandomInit()
        {
            base.RandomInit();
            double min = 0;
            double max = 300;
            Capacity = Math.Round(min + (max - min) * rnd.NextDouble(), 1);
        } //Рандомный объект(грузовик)
        public override void Init()
        {
            base.Init();
            capacity = InputCapacity("Введите грузоподъемность грузовика: ");
        }//ввод данных для объекта(грузовика) с клавиатуры
        static double InputCapacity(string msg)
        {
            double result;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = double.TryParse(Console.ReadLine(), out result);
                if (!isConvert || result < 0 || result > 300)
                {
                    Console.WriteLine("Ошибка! Грузоподъемность варьируется от 0 до 300 тонн. Попробуйте еще раз.");
                }
            } while (!isConvert || result < 0 || result > 300);
            return result;
        }//проверка ввода для грузоподъемности

        public new static int ObjectCount()
        {
            return objectCount;
        }// статический метод для получения количества объектов класса

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Truck tr)
                return base.Equals(obj) && tr.Capacity == this.Capacity;
            else return false;
        }//Сравнение двух объектов класса Truck

        public new object Clone()
        {
            return new Truck(Brand, Cost, Color, Year, GroundClearance, id.number, Capacity);
        }//Метод клонирования

        

        public override string ToString()
        {
            return base.ToString() + $"; грузоподъемность: {capacity} тонн";
        }

        #endregion
    }
}
