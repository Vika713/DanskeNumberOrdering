namespace BusinessServices.SortingAlgorithms
{
    public class MergeSortAlgorithm : ISortAlgorithm
    {
        public double[] Sort(double[] array)
        {
            if (array.Length == 0)
            {
                return [];
            }

            var middle = array.Length / 2;

            double[] leftArray = array.Take(middle).ToArray();
            double[] rightArray = array.Skip(middle).Take(array.Length - middle).ToArray();

            if (leftArray.Length > 1)
            {
                Sort(leftArray);
            }

            if (rightArray.Length > 1)
            {
                Sort(rightArray);
            }

            MergeArray(leftArray, rightArray, array);

            return array;
        }

        private static void MergeArray(double[] leftArray, double[] rightArray, double[] sortedArray)
        {
            var leftIndex = 0;
            var rightIndex = 0;
            var sortedIndex = 0;

            while (leftIndex < leftArray.Length && rightIndex < rightArray.Length)
            {
                sortedArray[sortedIndex++] = leftArray[leftIndex] <= rightArray[rightIndex] ?
                    leftArray[leftIndex++] :
                    rightArray[rightIndex++];
            }

            while (leftIndex < leftArray.Length)
            {
                sortedArray[sortedIndex++] = leftArray[leftIndex++];
            }

            while (rightIndex < rightArray.Length)
            {
                sortedArray[sortedIndex++] = rightArray[rightIndex++];
            }
        }
    }
}
