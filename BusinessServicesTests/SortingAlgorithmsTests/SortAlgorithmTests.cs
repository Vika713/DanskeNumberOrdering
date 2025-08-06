using BusinessServices.SortingAlgorithms;

namespace SortingAlgorithmsTests.BusinessServicesTests
{
    public class SortAlgorithmTests
    {
        public static IEnumerable<object[]> Algorithms =>
            new List<object[]>
            {
                new object[] { new ArraySortAlgorithm() },
                new object[] { new BubbleSortAlgorithm() },
                new object[] { new QuickSortAlgorithm() },
                new object[] { new MergeSortAlgorithm() }
            };

        [Theory]
        [MemberData(nameof(Algorithms))]
        public void Sort_ReturnsSortedArray(ISortAlgorithm algorithm)
        {
            // Arrange
            var input = new double[] { 5, 2, 8, 10, 1 };

            // Act
            var result = algorithm.Sort(input);

            // Assert
            Assert.Equal([1, 2, 5, 8, 10], result);
        }

        [Theory]
        [MemberData(nameof(Algorithms))]
        public void Sort_HandlesSingleElement(ISortAlgorithm algorithm)
        {
            // Arrange
            var input = new double[] { 1 };

            // Act
            var result = algorithm.Sort(input);

            // Assert
            Assert.Equal([1], result);
        }

        [Theory]
        [MemberData(nameof(Algorithms))]
        public void Sort_HandlesEmptyArray(ISortAlgorithm algorithm)
        {
            // Arrange
            var input = new double[0];

            // Act
            var result = algorithm.Sort(input);

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(Algorithms))]
        public void Sort_HandlesNegativeNumbers(ISortAlgorithm algorithm)
        {
            // Arrange
            var input = new double[] { -2.5, 0, 3.1, -1.0 };

            // Act
            var result = algorithm.Sort(input);

            // Assert
            Assert.Equal([-2.5, -1.0, 0, 3.1], result);
        }

        [Theory]
        [MemberData(nameof(Algorithms))]
        public void Sort_HandlesLargeArray(ISortAlgorithm algorithm)
        {
            // Arrange
            var size = 100000;
            var random = new Random();
            var input = new double[size];
            for (int i = 0; i < size; i++)
            {
                input[i] = random.NextDouble() * 10000;
            }

            // Act
            var result = algorithm.Sort(input);

            // Assert
            Assert.Equal(size, result.Length);

            // Assert the array is sorted
            for (int i = 1; i < result.Length; i++)
            {
                Assert.True(result[i - 1] <= result[i]);
            }
        }
    }
}
