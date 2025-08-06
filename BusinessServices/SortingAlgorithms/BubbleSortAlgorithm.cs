namespace BusinessServices.SortingAlgorithms
{
    public class BubbleSortAlgorithm : ISortAlgorithm
    {
        public double[] Sort(double[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            var maxSwapCount = array.Length - 1;

            for (var i = 0; i < maxSwapCount; i++)
            {
                for (var j = 0; j < maxSwapCount - i; j++)
                {
                    var currentNumber = array[j];

                    var nextIndex = j + 1;
                    var nextNumber = array[nextIndex];

                    if (currentNumber > nextNumber)
                    {
                        array[j] = nextNumber;
                        array[nextIndex] = currentNumber;
                    }

                }
            }

            return array;
        }
    }
}
