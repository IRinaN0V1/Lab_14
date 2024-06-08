using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ClassLibraryLab10
{
    public class IdNumber
    {
        public int number;
        public int Number
        {
            get => number;
            set
            {
                if (value >= 1)
                {
                    number = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Номер не может быть отрицательным!");
                }
            }
        }//Свойство для номера
        public IdNumber(int number)
        {
            this.number = number;
        }//Конструктор с параметром

        //МЕТОДЫ
        public override string ToString()
        {
            return number.ToString();
        }//Преобразование номера в строку
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is IdNumber n)
                return n.Number == this.Number;
            else return false;
        }
    }
    public class Car :IInit, IComparable, ICloneable
    {
        public IdNumber id;
        protected string brand;
        protected int cost;
        protected string color;
        protected int year;
        protected int groundClearance;
        static int objectCount;
        static Random rnd = new Random();
        static string[] brands = { "Toyota", "Honda", "Ford", "BMW", "Mercedes", "Kia", "Volkswagen", "Renault", "Lada", "Mazda", "Nissan", "Hyundai",
            "Lexus", "Cadillac", "Tesla", "Москвич", "Porsche", "Audi", "Lamborghini", "Ferrari", "Bentley", "Maserati", "Chery", "Chevrolet", "Haval", "Skoda", "Subaru", "Aurus", "Bugatti", "Eagle", "Fiat", "Hummer", "Jaguar", "Mercury", "Nissan" }; // Массив возможных марок автомобилей
        static string[] colors = { "Red", "Blue", "Black", "White", "Silver", "Green", "Yellow", "Brown", "Pink", "Orange", "Violet", "Gold", "Salmon", "Tomato", "Olive", "Lavender" }; // Массив возможных цветов

        #region Свойства полей
        public string Brand
        {
            get => brand;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    brand = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Бренд не может быть пустой строкой!");
                }
            }
        }//Свойства на бренд
        public int Cost
        {
            get => cost;
            set
            {
                if (value >= 0)
                {
                    cost = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Стоимость автомобиля не может быть отрицательной!");
                }
            }
        }//Свойства для стоимости автомобиля
        public string Color
        {
            get => color;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    color = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Цвет не может быть пустой строкой!");
                }
            }
        }//Свойства для цвета автомобиля
        public int Year
        {
            get => year;
            set
            {
                if (value >= 2020 && value <= 2024)
                {
                    year = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Первый современный автомобиль был выпущен в 1908 году, на данный момент 2024. Число выходит за нужный диапазон!");
                }
            }
        }//Свойства для года выпуска автомобиля
        public int GroundClearance
        {
            get => groundClearance;
            set
            {
                if (value >= 70 && value <= 400)
                {
                    groundClearance = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Дорожный просвет у автомобилей варьируется от 70 до 400 мм!");
                }
            }
        }//Свойства для дорожного просвета автомобиля
        #endregion

        #region Конструкторы
        public Car()
        {
            Brand = "Отсутствует";
            Cost = 0;
            Color = "Не определен";
            Year = 2024;
            GroundClearance = 70;
            id = new IdNumber(1);
            objectCount++;
        }//Конструктор по умолчанию(автомобиль не задан)
        public Car(string brand, int cost, string color, int year, int groundClearance, int number)
        {
            Brand = brand;
            Cost = cost;
            Color = color;
            Year = year;
            GroundClearance = groundClearance;
            id = new IdNumber(number);
            objectCount++;
        }//Конструктор с параметрами
      
        #endregion

        #region Методы класса
        public override string ToString()
        {
            return $"Бренд: {brand}; Стоимость: {cost} руб. ; Цвет: {color}; Год выпуска: {year}; Просвет: {groundClearance} мм; ID: {id.number}";
        }
        public virtual void VirtualShow()
        {
            Console.WriteLine($"Бренд: {brand}; Стоимость: {cost} руб. ; Цвет: {color}; Год выпуска: {year}; Просвет: {groundClearance} мм; ID: {id.number}");
        }//Вывод данных в консоль(виртуальный метод)
        public void Show()
        {
            Console.WriteLine($"Бренд: {brand}; Стоимость: {cost} руб. ; Цвет: {color}; Год выпуска: {year}; Просвет: {groundClearance} мм; ID: {id.number}");
        }//Вывод данных в консоль(обычнй метод)
        public virtual void RandomInit()
        {
            Brand = brands[rnd.Next(brands.Length)]; //Марка машины
            Cost = rnd.Next(500000, 100000000); //Стоимость автомобиля
            Color = colors[rnd.Next(colors.Length)];//Цвет машины
            Year = rnd.Next(2020, 2024); // Год выпуска
            GroundClearance = rnd.Next(70, 400); //Дорожный просвет
            id.number = rnd.Next(1, 100);//ID номер
        } //Рандомная генерация машины
        public virtual void Init()
        {
            brand = InputColorOrBrand("Введите марку машины: ");
            cost = InputCost("Введите стоимость автомобиля в рублях: ");
            color = InputColorOrBrand("Введите цвет машины: ");
            year = InputYear("Введите год выпуска автомобиля: ");
            groundClearance = InputGroundClearance("Введите дорожный просвет автомобиля в мм: ");
            id.number = InputID("Введите ID номер: ");
        } //Ввод данных об объекте(машине) с клавиатуры
        static int InputCost(string msg)
        {
            int result;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out result);
                if (!isConvert || result < 0) Console.WriteLine("Введено неверное значение. Стоимость должна быть неотрицательной. Попробуйте еще раз.");
            }
            while (!isConvert || result < 0);
            return result;
        }//проверка ввода для стоимости машины
        static int InputYear(string msg)
        {
            int result;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out result);
                if (!isConvert || result < 2020 || result > 2024) Console.WriteLine("Ошибка! Год выпуска машины может варьироваться от 1908 до 2024. Попробуйте еще раз.");
            }
            while (!isConvert || result < 2020 || result > 2024);
            return result;
        }//проверка ввода для года выпуска машины
        static int InputGroundClearance(string msg)
        {
            int result;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out result);
                if (!isConvert || result < 70 || result > 400) Console.WriteLine("Ошибка! Дорожны просвет у автомобилей варьируется от 70 до 400 мм. Попробуйте еще раз.");
            }
            while (!isConvert || result < 70 || result > 400);
            return result;
        }//проверка ввода для дорожного просвета машины
        static int InputID(string msg)
        {
            int result;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out result);
                if (!isConvert || result < 1) Console.WriteLine("Введено неверное значение. Номер должен быть не меньше 1. Попробуйте еще раз.");
            }
            while (!isConvert || result < 1);
            return result;
        }//проверка ввода для ID номера машины
        static string InputColorOrBrand(string msg)
        {
            string str;
            bool isMatch;
            do
            {
                Console.Write(msg);
                str = Console.ReadLine();
                isMatch = Regex.IsMatch(str, @"^[\p{L}\s]+$");
                if (str.Trim() == "" || String.IsNullOrEmpty(str)) Console.WriteLine("Ошибка! Строка не может быть пустой. Попробуйте еще раз.");
                else if (!isMatch) Console.WriteLine("Ошибка! Название бренда или цвета машины должно состоять из слов(русские, латинские), может содержать пробелы. Попробуйте еще раз.");
            }
            while (!isMatch || str.Trim() == "" || String.IsNullOrEmpty(str));
            return str.Trim();
        }//проверка ввода бренда и цвета машины

        public static int ObjectCount()
        {
            return objectCount;
        }// статический метод для получения количества объектов класса

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Car c)
                return c.Brand == this.Brand && c.Cost == this.Cost && c.Color == this.Color && c.Year == this.Year && c.GroundClearance == this.GroundClearance && this.id.number == c.id.number;
            else return false;
        }//Сравнение двух объектов класса Car

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is Car)) return -1;
            Car car = obj as Car;
            return this.Year.CompareTo(car.Year);
        }//Сравнение двух объектов по году выпуска машины

        public object Clone()
        {
            return new Car(Brand, Cost, Color, Year, GroundClearance, id.number);
        }//Метод клонирования

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }//Метод копирования

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + brand.GetHashCode();
                hash = hash * 23 + cost.GetHashCode();
                hash = hash * 23 + color.GetHashCode();
                hash = hash * 23 + year.GetHashCode();
                hash = hash * 23 + groundClearance.GetHashCode();
                hash = hash * 23 + id.Number.GetHashCode();
                return hash;
            }
        }
        #endregion
    }
}
