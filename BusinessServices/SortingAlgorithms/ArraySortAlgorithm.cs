namespace BusinessServices.SortingAlgorithms
{
    public class ArraySortAlgorithm : ISortAlgorithm
    {
        public double[] Sort(double[] array)
        {
            Array.Sort(array);

            return array;
        }
    }
}
