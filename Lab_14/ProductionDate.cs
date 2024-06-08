using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class ProductionDate
    {
        //Поля
        protected int year; //Год выпуска
        protected int month; //Месяц выпуска
        protected int day; //День выпуска(число)
        static Random rnd = new Random();

        //Свойства
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
        }//Свойство для года выпуска

        public int Month
        {
            get => month;
            set
            {
                if (value >= 1 && value <= 12)
                {
                    month = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! Месяц может быть в диапазоне [1, 12]. Число выходит за нужный диапазон!");
                }
            }
        }//Свойство для месяца выпуска

        public int Day
        {
            get => day;
            set
            {
                if (value >= 1 && value <= 28)
                {
                    day = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка! День может быть в диапазоне [1, 28]. Число выходит за нужный диапазон!");
                }
            }
        }//Свойство для дня выпуска

        //Конструкторы
        public ProductionDate()
        {
            Year = 2020;
            Month = 1;
            Day = 1;
        }//Конструктор по умолчанию
        public ProductionDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }//Конструктор с параметрами

        //Методы
        public override string ToString()
        {
            return $"Год выпуска: {year}; Месяц: {month}; День: {day}";
        }

        public virtual void RandomInit()
        {
            Year = rnd.Next(2020, 2025); // Год выпуска
            Month = rnd.Next(1, 12); // Месяц выпуска
            Day = rnd.Next(1, 28); // День выпуска
        } //Рандомная генерация даты производства

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is ProductionDate date)
                return date.Year == this.Year && date.Month == this.Month && date.Day == date.Day;
            else return false;
        }//Сравнение двух объектов класса Car
    }
}
