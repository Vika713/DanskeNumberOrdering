namespace BusinessServices
{
    public class FileService : INumberStorageService
    {
        private readonly string _filePath;

        public FileService(string filePath)
        {
            _filePath = filePath;
        }

        public async Task SaveNumbers(double[] numbers)
        {
            using (StreamWriter file = new StreamWriter(_filePath))
            {
                foreach (var number in numbers)
                {
                    await file.WriteLineAsync(number.ToString());
                }
            }
        }

        public async Task<double[]> ReadNumbers()
        {
            var lines = await File.ReadAllLinesAsync(_filePath);
            var numbers = new double[lines.Length];

            for (var i = 0; i < lines.Length; i += 1)
            {
                if (double.TryParse(lines[i], System.Globalization.CultureInfo.InvariantCulture, out var number))
                {
                    numbers[i] = number;
                }
                else
                {
                    throw new InvalidDataException($"Invalid number format in file {lines[i]}");
                }
            }

            return numbers;
        }
    }
}
