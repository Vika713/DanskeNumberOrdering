namespace BusinessServices
{
    public class SortingService : ISortingService
    {
        public double[] SortNumbers(double[] numbers)
        {
            var copyOfNumbers = (double[])numbers.Clone();

            Array.Sort(copyOfNumbers);

            return copyOfNumbers;
        }
    }
}
