namespace BusinessServices.SortingAlgorithms
{
    public class QuickSortAlgorithm : ISortAlgorithm
    {
        public double[] Sort(double[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            return Sort(array, 0, array.Length - 1);
        }

        private static double[] Sort(double[] array, int leftIndex, int rightIndex)
        {
            var pivot = array[leftIndex];
            var currentLeftIndex = leftIndex;
            var currentRightIndex = rightIndex;

            while (currentLeftIndex <= currentRightIndex)
            {
                while (array[currentLeftIndex] < pivot)
                {
                    currentLeftIndex++;
                }

                while (array[currentRightIndex] > pivot)
                {
                    currentRightIndex--;
                }

                if (currentLeftIndex <= currentRightIndex)
                {
                    (array[currentRightIndex], array[currentLeftIndex]) = (array[currentLeftIndex], array[currentRightIndex]);
                    currentLeftIndex++;
                    currentRightIndex--;
                }
            }

            if (leftIndex < currentRightIndex)
            {
                Sort(array, leftIndex, currentRightIndex);
            }

            if (rightIndex > currentLeftIndex)
            {
                Sort(array, currentLeftIndex, rightIndex);
            }

            return array;
        }
    }
}
