using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace Lab_14
{
    public class MyCollection<T> : MyHashTable<T>, IEnumerable<T>, ICollection<T> where T : IInit, ICloneable, new()
    {
        public MyCollection(): base() { } //конструктор по умолчанию
        public MyCollection(int size) : base(size) { } //конструктор с параметром(размер коллекции)
        public MyCollection(MyCollection<T>? collection) : base(collection) { }//конструктор с параметром(другая коллекция)

        public new int Count => base.Count; //Свойство для получения количества элементов в коллекции
        public bool IsReadOnly => false; //означает что коллекция доступна не только для чтения, т.е ее можно изменять(не используется)

        public void Add(T item)
        {
            T car = (T)item.Clone();
            AddPoint(car);
        }//Метод для добавления элементов в коллекцию

        public void Clear()
        {
            ClearMemory();
        }//Метод для очищения памяти

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new Exception("Пустой массив");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new Exception("В массиве недостаточно места для копирования элементов");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new Exception($"Индекс, с которого неорбходимо начать копирование, выходит за пределы массива: [0, {array.Length}]");
            }

            int i = arrayIndex; //Индекс, начиная с которого элементы коллекции будут добавлены в массив
            foreach (T item in this) // проходимся по элементам коллекции циклом foreach(возможно за счет нумератора)
            {
                array[i] = (T)item.Clone(); // Присваиваем в массив глубокую копию элемента
                i++;
            }
        }//Метод для копирования элементов в массив, начиная с заданного индекса

        public new bool Contains(T item)
        {
            return base.Contains(item);
        }//Метод для проверки на то, содержится элемент в коллекции или нет

        public bool Remove(T item)
        {
            return RemoveData(item);
        }//Метод для удаления элемента из коллекции



        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < table.Length; i++) //проходимся по каждой строке-элементу таблицы
            {
                if (table[i] != null) //если строка не пустая
                {
                    PointHT<T> current = table[i];
                    while (current != null) //если  элемент в строке существует
                    {
                        yield return current.Data; //возвращаем инф.поле элемента
                        current = current.Next;//сдвигаемся дальше по цепочке
                    }
                }
            }
        }//Нумератор, необходимый для того, чтобы использовать цикл foreach для коллекции

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }//не используется

        public static int FindIndexInCollection(MyCollection<T> collection, T obj)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Equals(obj))
                {
                    return i;
                }
            }
            return -1;
        }//Метод для получения индекса по значению объекта в коллекции


        public T this[int index]
        {
            get //Получение элемента по индексу
            {
                if (index < 0 || index >= Count)
                {
                    throw new Exception("Элемент не найден в коллекции");
                }//Если индекс выходит за пределы коллекции, выбрасываем исключение

                int count = 0;
                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                    {
                        PointHT<T> current = table[i];
                        while (current != null)
                        {
                            if (count == index)
                            {
                                return current.Data; //Возвращаем элемент коллекции с заданным индексом
                            }
                            count++;
                            current = current.Next;
                        }
                    }
                }

                throw new IndexOutOfRangeException("Элемент не найден в коллекции");
            }

            set //Установка нового значения элементу с указанным индексом
            {
                if (index < 0 || index >= Count)
                {
                    throw new Exception("Элемент не найден в коллекции");
                }//Если индекс выходит за пределы коллекции, выбрасываем исключение

                int count = 0;
                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                    {
                        PointHT<T> current = table[i];
                        while (current != null)
                        {
                            if (count == index)
                            {

                                T tekData = current.Data; // Сохраняем старые данные перед изменением

                                if (!tekData.Equals(value))
                                {
                                    Remove(tekData); // Удаляем старый элемент из таблицы
                                    Add(value);      // Добавляем новый элемент с учетом измененного хеш-кода
                                }
                                return; // Завершаем метод после обновления
                            }
                            count++;
                            current = current.Next;
                        }
                    }
                }

                throw new IndexOutOfRangeException("Элемент не найден в коллекции");
            }
        }
    }
}
