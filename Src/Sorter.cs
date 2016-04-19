namespace _0014
{
    public class Sorter
    {
        // O(n^2)
        #region Insertion

        public void Insertion(int[] a)
        {
            var lenght = a.Length;

            for (int i = 0; i < lenght; i++)
                for (int j = i; j > 0; j--)
                    if (a[j] < a[j - 1]) Swap(a, j, j - 1);
        }

        #endregion

        #region BubbleSort

        public void BubbleSort(int[] a)
        {
            var lenght = a.Length;
            for (int i = 0; i < lenght - 1; i++)
                for (int j = 0; j < lenght - 1; j++)
                    if (a[j] > a[j + 1]) Swap(a, j, j + 1);
        }

        #endregion

        // O(nlogn)
        #region QucikSort

        public static void QuickSort(int[] a, int left, int right)
        {
            if (left >= right) return;

            var pivot = a[left];

            var l = left + 1; var r = right - 1;

            while (l <= r)
            {
                while ( l <= r && a[l] <= pivot) l += 1;
                while (l <= r && a[r] >= pivot) r -= 1;
                if (l < r)
                {
                    Swap(a, l, r);
                    l += 1; r -= 1;
                }
            }

            Swap(a, left, r);
            QuickSort(a, left, r);
            QuickSort(a, r + 1, right);
        }

        public void QuickSort(int[] a)
        {
            QuickSort(a, 0, a.Length);
        }

        #endregion

        #region MergeSort

        public void MergeSort(int[] a)
        {
            MergeSort(a, 0, a.Length - 1);
        }

        public void MergeSort(int[]a, int left, int right)
        {
            if (left < right)
            {
                var mid = (left + right) / 2;
                MergeSort(a, left, mid);
                MergeSort(a, mid + 1, right);

                Merge(a, left, mid, right);
            }
        }

        private void Merge(int[] a, int left, int mid, int right)
        {
            var temp = new int[right - left + 1];
            
            var j = mid + 1;
            var i = left;
            var k = 0;

            while (i <= mid && j <= right)
            {
                if (a[i] < a[j])
                {
                    temp[k] = a[i];
                    i ++;
                    k ++;
                }
                else
                {
                    temp[k] = a[j];
                    j ++;
                    k ++;
                }
            }

            while (i <= mid)
            {
                temp[k] = a[i];
                k ++;
                i ++;
            }

            while (j <= right)
            {
                temp[k] = a[j];
                k ++;
                j ++;
            }

            k = 0;
            i = left;
            while (k < temp.Length)
            {
                a[i] = temp[k];
                k ++;
                i ++;
            }
        }

        #endregion

        #region HeapSort

        public void HeapSort(int[] a)
        {
            BuildHeap(a, 0, a.Length - 1); // build heap

            DeleteMin(a); // delete root and assign it to the arrray
        }

        private void BuildHeap(int[] a, int r, int n)
        {
            if(2*r + 1 > n) return;

            BuildHeap(a, 2 * r + 1, n); // build left sub-tree
            BuildHeap(a, 2 * r + 2, n); // build right sub-tree

            PushDown(a, r, n); // push down the inserted node to its correct position
        }

        private void DeleteMin(int[] a)
        {
            var temp = new int[a.Length];
            a.CopyTo(temp, 0);

            var lenght = a.Length - 1;

            for (int i = 0; i <= lenght; i++)
            {
                a[i] = temp[0]; // delete the root and pass it to the array
                Swap(temp, 0, lenght - i); // leaf element with root
                PushDown(temp, 0, lenght - i - 1); // apply pushdown on the new root, so it will get in the right location
            }
        }

        private void PushDown(int[] a, int r, int n)
        {
            // if the current position is on the leaf, return
            if (2*r + 1 > n) return;

            //find the smaller child
            int s;

            if (2*r + 1 == n || a[2*r + 1] <= a[2*r + 2])
                s = 2*r + 1;
            else
                s = 2*r + 2;

            // pushdown if child is smaller than parent
            if (a[r] > a[s])
            {
                Swap(a, r, s);
                PushDown(a, s, n);
            }
        }

        #endregion

        // O(n)
        #region BucketSorting



        #endregion

        #region RadixSorting



        #endregion

        #region Helper Functions
        private static void Swap(int[] a, int index1, int index2)
        {
            var temp = a[index1];
            a[index1] = a[index2];
            a[index2] = temp;
        }
        #endregion
    }
}
