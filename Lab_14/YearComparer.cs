using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class YearComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null || y == null) return -1;

            if (x is Car && y is Car)
            {
                Car car1 = x as Car;
                Car car2 = y as Car;

                if (car1.Year < car2.Year) return -1;
                else if (car1.Year == car2.Year) return 0;
                else return 1;
            }

            return -1; 
        }
    }
}
