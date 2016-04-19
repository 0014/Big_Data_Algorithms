using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0014
{
    public class Selection
    {
        // O(n)
        #region FindMax

        public int FindMax(int[] a)
        {
            return FindMax_DC(a, 0, a.Length);
        }

        private int FindMax_DC(int[] a, int s, int n)
        {
            if (n == 1) return a[s];

            var max1 = FindMax_DC(a, s, n / 2);
            var max2 = FindMax_DC(a, n/2 + s, n - n/2);

            if (max1 > max2)
                return max1;
            else 
                return max2;
        }

    #endregion

        //O(logn)
        #region BinarySearch

        public int BinarySearch(int[] a, int startIndex, int endIndex, int key)
        {
            if (startIndex > endIndex) return -1;

            var mid = (startIndex + endIndex)/2;

            if (key == a[mid])
                return mid;
            if (key < a[mid])
                return BinarySearch(a, startIndex, mid - 1, key);
            
            return BinarySearch(a, mid + 1, endIndex, key);
        }

        #endregion
    }
}
