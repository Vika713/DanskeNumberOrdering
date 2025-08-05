using BusinessServices;

namespace BusinessServicesTests
{
    public class SortingServiceTests
    {
        [Fact]
        public void SortNumbers_ReturnsSortedArray()
        {
            // Arrange
            var service = new SortingService();
            var input = new double[] { 5, 2, 8, 10, 1 };

            // Act
            var result = service.SortNumbers(input);

            // Assert
            Assert.Equal([1, 2, 5, 8, 10], result);
        }

        [Fact]
        public void SortNumbers_HandlesSingleElement()
        {
            // Arrange
            var service = new SortingService();
            var input = new double[] { 1 };

            // Act
            var result = service.SortNumbers(input);

            // Assert
            Assert.Equal([1], result);
        }

        [Fact]
        public void SortNumbers_HandlesEmptyArray()
        {
            // Arrange
            var service = new SortingService();
            var input = new double[0];

            // Act
            var result = service.SortNumbers(input);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SortNumbers_HandlesNegativeNumbers()
        {
            // Arrange
            var service = new SortingService();
            var input = new double[] { -2.5, 0, 3.1, -1.0 };

            // Act
            var result = service.SortNumbers(input);

            // Assert
            Assert.Equal([-2.5, -1.0, 0, 3.1], result);
        }

        [Fact]
        public void SortNumbers_HandlesLargeArray()
        {
            // Arrange
            var service = new SortingService();

            var size = 100000;
            var random = new Random();
            var input = new double[size];
            for (int i = 0; i < size; i++)
            {
                input[i] = random.NextDouble() * 10000;
            }

            // Act
            var result = service.SortNumbers(input);

            // Assert
            Assert.Equal(size, result.Length);

            // Assert the array is sorted
            for (int i = 1; i < result.Length; i++)
            {
                Assert.True(result[i - 1] <= result[i]);
            }
        }

        [Fact]
        public void SortNumbers_OriginalArrayNotModified()
        {
            // Arrange
            var service = new SortingService();
            var input = new double[] { 5, 2, 8, 10, 1 };

            // Act
            service.SortNumbers(input);

            // Assert
            Assert.Equal([5, 2, 8, 10, 1], input);
        }
    }
}
