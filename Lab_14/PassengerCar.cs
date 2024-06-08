using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class PassengerCar :Car, IInit, ICloneable
    {
        private int numSeats;//как минимум 2
        private int maxSpeed;//как минимум 10
        static int objectCount;
        static Random rnd = new Random();
        #region Свойства
        public int NumSeats
        {
            get => numSeats;
            set
            {
                if (value > 1 && value <= 10)
                {
                    numSeats = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Количество мест в автомобиле должно быть от 2 до 10.");
                }
            }
        }//Свойство для кол-ва мест в легковом автомобиле
        public int MaxSpeed
        {
            get => maxSpeed;
            set
            {
                if (value >= 10 && value <= 300)
                {
                    maxSpeed = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Максимальная скорость должна быть в пределах от 10 до 300 км/ч.");
                }
            }
        }//Свойство для максимальной скорости легкового автомобиля
        #endregion

        #region Конструкторы
        public PassengerCar() : base()
        {
            NumSeats = 2;
            MaxSpeed = 10;
            objectCount++;
        }//Конструктор по умолчанию для легковых автомобилей
        public PassengerCar(string brand, int cost, string color, int year, int groundClearance, int number, int numSeats, int maxSpeed) : base(brand, cost, color, year, groundClearance, number)
        {
            NumSeats = numSeats;
            MaxSpeed = maxSpeed;
            objectCount++;
        }//Конструктор с параметрами для легковых автомобилей
        #endregion

        #region Методы
        public new void Show()
        {
            Console.Write("Легковая машина: ");
            base.Show();
            Console.WriteLine($"количество мест: {numSeats}; макс. скорость: {maxSpeed} км/ч");
        }//Вывод данных в консоль(обычный метод)
        public override void VirtualShow()
        {
            Console.Write("Легковая машина: ");
            base.Show();
            Console.WriteLine($"количество мест: {numSeats}; макс. скорость: {maxSpeed} км/ч");
            //Console.WriteLine($"Легковая машина: бренд: {brand}; стоимость: {cost} руб. ; цвет: {color}; год выпуска: {year}; просвет: {groundClearance} мм; ID: {id.number}; количество мест: {numSeats}; макс. скорость: {maxSpeed} км/ч");
        }//Вывод данных в консоль(виртуальный метод)

        public override void RandomInit()
        {
            base.RandomInit();
            NumSeats = rnd.Next(2, 11);
            MaxSpeed = rnd.Next(10, 301);
        } //Рандомный объект(машина)
        public override void Init()
        {
            base.Init();
            numSeats = InputNumSeats("Введите кол-во мест в машине: ");
            maxSpeed = InputMaxSpeed("Введите макс. скорость машины: ");
        }//ввод данных для объекта(машины) с клавиатуры
        static int InputNumSeats(string msg)
        {
            int result;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out result);
                if (!isConvert || result < 2 || result > 10)
                {
                    Console.WriteLine("Ошибка! допустимое кол-во мест: от 2 до 10. Попробуйте еще раз.");
                }
            }
            while (!isConvert || result < 2 || result > 10);
            return result;
        }//проверка ввода для кол-ва мест в машине
        static int InputMaxSpeed(string msg)
        {
            int result;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out result);
                if (!isConvert || result < 10 || result > 300)
                {
                    Console.WriteLine("Ошибка! Допустимые значения для макс. скорости: от 10 до 300 км/ч. Попробуйте еще раз.");
                }
            }
            while (!isConvert || result < 10 || result > 300);
            return result;
        }//проверка ввода для макс. скорости машины

        public new static int ObjectCount()
        {
            return objectCount;
        }// статический метод для получения количества объектов класса

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is PassengerCar pc)
                return base.Equals(obj) && pc.NumSeats == this.NumSeats && pc.MaxSpeed == this.MaxSpeed;
            else return false;
        }//Сравнение двух объектов класса PassengerCar

        public override string ToString()
        {
            return base.ToString() + $"; количество мест: {numSeats}; макс. скорость: {maxSpeed} км/ч";
        }

        public new object Clone()
        {
            return new PassengerCar(Brand, Cost, Color, Year, GroundClearance, id.number, NumSeats, MaxSpeed);
        }//Метод клонирования
        #endregion
    }
}
