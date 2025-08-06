namespace BusinessServices
{
    public interface INumberStorageService
    {
        public Task SaveNumbers(double[] numbers);
        public Task<double[]> ReadNumbers();
    }
}
