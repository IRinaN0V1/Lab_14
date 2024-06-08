using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class Jeep :Car, IInit, ICloneable
    {
        private bool isWheelDrive;//полный ли привод
        private string offRoad;// тип бездорожья
        static int objectCount;
        static Random rnd = new Random();
        static string[] types = { "гравий", "песок", "грязь", "снег", "лес", "скалы", "болото", "равнина", "горы", "пустыня" }; // Массив возможных типов бездорожья

        #region Свойства
        public bool IsWheelDrive
        {
            get => isWheelDrive;
            set
            {
                if (value == true || value == false) isWheelDrive = value;
                else throw new Exception("Ошибка! Должно быть указано значение true или false.");
            }
        }//Свойства для привода
        public string OffRoad
        {
            get => offRoad;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    offRoad = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Тип бездорожья не может быть пустой строкой!");
                }
            }
        }//Свойства для типа бездорожья
        #endregion

        #region Конструкторы
        public Jeep(string brand, int cost, string color, int year, int groundClearance, int number, bool isWheelDrive, string offRoad) : base(brand, cost, color, year, groundClearance, number)
        {
            IsWheelDrive = isWheelDrive;
            OffRoad = offRoad;
            objectCount++;
        }
        public Jeep() : base()
        {
            isWheelDrive = true;
            offRoad = "Пустыня";
            objectCount++;
        }
        #endregion

        #region Методы
        public new void Show()
        {
            Console.Write("Внедорожник: ");
            base.Show();
            Console.WriteLine($"полный привод: {isWheelDrive}; тип бездорожья: {offRoad}");
        }//Вывод данных в консоль(обычный метод)
        public override void VirtualShow()
        {
            Console.Write("Внедорожник: ");
            base.Show();
            Console.WriteLine($"полный привод: {isWheelDrive}; тип бездорожья: {offRoad}");
            //Console.WriteLine($"Внедорожник: бренд: {brand}; стоимость: {cost} руб. ; цвет: {color}; год выпуска: {year}; просвет: {groundClearance} мм; ID: {id.number}; полный привод: {isWheelDrive}; тип бездорожья: {offRoad}");
        }//Вывод данных в консоль(виртуальный метод)

        public override void RandomInit()
        {
            base.RandomInit();
            IsWheelDrive = rnd.Next(0, 2) == 0;
            OffRoad = types[rnd.Next(types.Length)]; 
        } //Рандомный объект(внедорожник)
        public override void Init()
        {
            base.Init();
            isWheelDrive = InputWheelDrive("Внедорожник полноприводный?(Да/нет) Ответ: ");
            offRoad = InputOffRoad("Выберите тип бездорожья из указанных(Гравий, песок, грязь,  снег, скалы, болото, равнина, горы, пустыня): ");
        }//ввод данных для объекта(внедорожника) с клавиатуры
        static bool InputWheelDrive(string msg)
        {
            bool result = false;
            string str;
            bool isCorrect = false;
            do
            {
                Console.WriteLine(msg);
                str = Console.ReadLine().Trim().ToLower();
                if (str == "да")
                {
                    result = true;
                    isCorrect = true;
                }
                else if (str == "нет")
                {
                    result = false;
                    isCorrect = true;
                }
                else Console.WriteLine("Ошибка! Ответ должен содержать 'Да' или 'Нет'. Попробуйте еще раз.");
            } while (!isCorrect);
            return result;
        }//проверка на полный привод
        static string InputOffRoad(string msg)
        {
            string str;
            bool isMatch;
            do
            {
                Console.Write(msg);
                str = Console.ReadLine().Trim().ToLower();
                isMatch = false;
                foreach (string road in types)
                {
                    if (string.Equals(str, road))
                    {
                        isMatch = true;
                        break;
                    }
                }
                if (str.Trim() == "" || String.IsNullOrEmpty(str))
                {
                    Console.WriteLine("Ошибка! Строка не может быть пустой. Попробуйте еще раз.");
                }
                else if (!isMatch) Console.WriteLine("Ошибка! Введенный тип бездорожья не соответствует указанным. Попробуйте еще раз.");
            } while (!isMatch || str.Trim() == "" || String.IsNullOrEmpty(str));
            return str;
        }//проверка ввода типа бездорожья

        public new static int ObjectCount()
        {
            return objectCount;
        }// статический метод для получения количества объектов класса

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Jeep jp)
                return base.Equals(obj)  && jp.isWheelDrive == this.isWheelDrive && jp.OffRoad == this.OffRoad;
            else return false;
        }//Сравнение двух объектов класса Jeep

        public override string ToString()
        {
            return base.ToString() + $"; полный привод: {isWheelDrive}; тип бездорожья: {offRoad}";
        }

        public new object Clone()
        {
            return new Jeep(Brand, Cost, Color, Year, GroundClearance, id.number, IsWheelDrive, OffRoad);
        }//Метод клонирования
        #endregion
    }
}
