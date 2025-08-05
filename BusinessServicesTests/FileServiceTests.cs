using BusinessServices;

namespace BusinessServicesTests
{
    public class FileServiceTests : IDisposable
    {
        private const string TestFilePath = "test_result.txt";

        public void Dispose()
        {
            if (File.Exists(TestFilePath))
                File.Delete(TestFilePath);
        }

        [Fact]
        public async Task SaveNumbers_SavesNumbersCorrectly()
        {
            // Arrange
            var service = new FileService(TestFilePath);
            var numbers = new double[] { 1.1, 2.2, 3.3 };

            // Act
            await service.SaveNumbers(numbers);

            var result = await File.ReadAllLinesAsync(TestFilePath);

            // Assert
            Assert.Equal(["1.1", "2.2", "3.3"], result);
        }

        [Fact]
        public async Task SaveNumbers_WritesEmptyFile_ForEmptyArray()
        {
            // Arrange
            var service = new FileService(TestFilePath);
            var numbers = new double[0];

            // Act
            await service.SaveNumbers(numbers);

            var result = await File.ReadAllLinesAsync(TestFilePath);

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public async Task ReadNumbers_ThrowsInvalidDataException_OnInvalidContent()
        {
            // Arrange
            await File.WriteAllLinesAsync(TestFilePath, ["1.1", "not-a-number", "3.3"]);
            var service = new FileService(TestFilePath);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(service.ReadNumbers);
        }

        [Fact]
        public async Task ReadNumbers_ReadsDataCorrectly()
        {
            // Arrange
            await File.WriteAllLinesAsync(TestFilePath, ["1.1", "2.2", "3.3"]);
            var service = new FileService(TestFilePath);

            // Act 
            var result = await service.ReadNumbers();

            // Assert
            Assert.Equal([1.1, 2.2, 3.3], result);
        }

        [Fact]
        public async Task ReadNumbers_ReadsNegativeNumbersCorrectly()
        {
            // Arrange
            await File.WriteAllLinesAsync(TestFilePath, ["-3.3", "-2.2", "-1.1"]);
            var service = new FileService(TestFilePath);

            // Act 
            var result = await service.ReadNumbers();

            // Assert
            Assert.Equal([-3.3, -2.2, -1.1], result);
        }

        [Fact]
        public async Task SaveNumbers_HandlesLargeArray()
        {
            // Arrange
            var service = new FileService(TestFilePath);
            var size = 100000;
            var random = new Random();
            var numbers = new double[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = random.NextDouble() * 10000;
            }

            // Act
            await service.SaveNumbers(numbers);

            var result = await File.ReadAllLinesAsync(TestFilePath);

            // Assert
            Assert.Equal(numbers, result.Select(x => double.Parse(x)).ToArray());
        }
    }
}
