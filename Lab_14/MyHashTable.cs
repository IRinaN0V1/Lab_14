using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace Lab_14
{
    public class MyHashTable<T> where T : IInit, ICloneable, new()
    {
        public PointHT<T>?[] table; //создаем таблицу
        public int Capacity => table.Length;//свойство для получения размера таблицы


        int count = 0;
        public int Count => count;//свойства для кол-ва элементов в таблице
        public MyHashTable() { }//Конструктор по умолчанию

        public MyHashTable(int length)
        {
            if (length <= 0) throw new Exception("Размер коллекции меньше или равен 0");
            else
            {
                table = new PointHT<T>[length];
                for (int i = 0; i < length; i++)
                {
                    AddPoint(PointHT<T>.MakeRandomItem());
                }
            }
        }//Конструктор с параметром для создания таблицы с заданным размером

        public MyHashTable(MyHashTable<T>? collection)
        {
            if (collection == null || collection.Count == 0) throw new Exception("Исходная коллекция не может быть равна null"); // Проверяем, что переданная коллекция не является null

            // Инициализация новой коллекции с тем же размером, что и у переданной коллекции
            table = new PointHT<T>[collection.table.Length];
            count = collection.count;

            for (int i = 0; i < table.Length; i++)
            {
                PointHT<T> current = collection.table[i];
                PointHT<T> newChain = null;
                PointHT<T> previous = null;

                while (current != null)
                {
                    PointHT<T> clonedPoint = new PointHT<T>((T)current.Data.Clone()); // Создаем глубокую копию текущего элемента 

                    if (newChain == null) newChain = clonedPoint;
                    else
                    {
                        previous.Next = clonedPoint; // Устанавливаем ссылку на следующий элемент
                        clonedPoint.Prev = previous; // Устанавливаем ссылку на предыдущий элемент
                    }

                    previous = clonedPoint;
                    current = current.Next; //переходим к следующему элементу
                }

                table[i] = newChain; // Устанавливаем новую цепочку в таблицу
            }
        }

        public void PrintTable()
        {
            if (count <= 0 || table == null) throw new Exception("Размер коллекции меньше или равен 0");
            else
            {
                for (int i = 0; i < table.Length; i++) //проходимся по каждой строке-элементу таблицы
                {
                    Console.WriteLine($"{i}: ");
                    if (table[i] != null) //если строка не пустая
                    {
                        Console.WriteLine(table[i].Data); //печатаем элемент
                        if (table[i].Next != null) //если следующий элемент в строке существует
                        {
                            PointHT<T>? current = table[i].Next; // следующий элемент становится текущим
                            while (current != null) //пока текущий элемент не пустой
                            {
                                Console.WriteLine(current.Data); //печатаем текущий элемент
                                current = current.Next; //двигаемся дальше по строке
                            }
                        }
                    }
                }
            }
        }//печать хеш-таблицы

        public void AddPoint(T data)
        {
            if (Contains(data)) throw new Exception("Элемент уже существует в хеш-таблице");
            else
            {
                int index = GetIndex(data); //получаем индекс(хеш-код) добавляемого элемента
                if (table[index] == null) // если строка с этим индексом пустая 
                {
                    table[index] = new PointHT<T>(data); //создаем новый элемент
                                                         //table[index].Data = data;
                }
                else //иначе, если строка с нужным индексом не пустая
                {
                    PointHT<T>? current = table[index]; //строка(элемент таблицы) с нужным индексом становится текущей
                    while (current.Next != null) //пока следующий элемент в строке существует
                    {
                        //if (current.Equals(data)) //если в таблице уже существует элемент с полем data - добавление не происходит(тк элементы не должны повторяться)
                        //{
                        //    return;
                        //}
                        current = current.Next; //сдвигаемся дальше по цепочке
                    }
                    //дошли до конца цепочки и добавляем элемент в ее конец
                    current.Next = new PointHT<T>(data); //создаем новый элемент
                    current.Next.Prev = current;
                }
                count++;
            }
        } //добавление элемента в таблицу

        public void ClearMemory()
        {
            table = null;
            count = 0;
            GC.Collect();
        }//Метод для полного удаления таблицы из памяти

        public bool Contains(T data)
        {
            int index = GetIndex(data); //получаем индекс(хеш-код) искомого элемента
            
            if (table[index] == null) return false; //если элемент с таким индексом пустой - элемент не найден
            if (table[index].Data.Equals(data)) return true; //если элемент с таким индексом не пустой и инф.поля совпадают - элемент найден
            else
            {
                PointHT<T>? current = table[index]; //цепочка с данным индексом становится текущей
                while (current != null) //пока текущий элемент не пустой
                {
                    if (current.Data.Equals(data)) return true; //если инф.поля совпадают - элемент найден
                    current = current.Next; //иначе сдвигаемся по цепочке дальше
                }
            }
            return false;
        }//поиск элемента в таблице

        public bool RemoveData(T data)
        {
            PointHT<T>? current;
            if (data == null) throw new Exception("Элемент для удаления пустой");
            int index = GetIndex(data);
            if (table[index] == null) return false;
            if (table[index].Data.Equals(data))
            {
                if (table[index].Next == null) table[index] = null;
                else
                {
                    table[index] = table[index].Next;
                    table[index].Prev = null;
                }
                count--;
                return true;
            }
            else
            {
                current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        PointHT<T>? prev = current.Prev;
                        PointHT<T>? next = current.Next;
                        prev.Next = next;
                        current.Prev = null;
                        if (next != null)
                            next.Prev = prev;
                        count--;
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }

        int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode()) % Capacity;//по модулю, так как GHC может вернуть отрицательное число
        }//получение хеш-кода элемента

        public PointHT<T>? GetPointWithIndexZero() 
        {
            if (count <= 0 || table == null) throw new Exception("Размер коллекции меньше или равен 0");
            if (table[0] == null) return null;
            else return table[0];
        }
    }
}
