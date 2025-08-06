using BusinessServices.SortingAlgorithms;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BusinessServices
{
    public class SortingService : ISortingService
    {
        private readonly IEnumerable<ISortAlgorithm> _sortAlgorithms;
        private readonly ILogger<SortingService> _logger;

        public SortingService(IEnumerable<ISortAlgorithm> sortAlgorithms, ILogger<SortingService> logger)
        {
            _sortAlgorithms = sortAlgorithms;
            _logger = logger;
        }

        public double[] SortNumbers(double[] numbers)
        {
            var stopwatch = new Stopwatch();
            var sortedNumbers = new double[numbers.Length];

            foreach (var sortAlgorithm in _sortAlgorithms)
            {
                var copyOfNumbers = (double[])numbers.Clone();

                stopwatch.Restart();
                sortedNumbers = sortAlgorithm.Sort(copyOfNumbers);
                stopwatch.Stop();

                _logger.LogInformation($"{sortAlgorithm.GetType().Name} took {stopwatch.Elapsed} ms");
            }

            return sortedNumbers;
        }
    }
}
