using BusinessServices;
using BusinessServices.SortingAlgorithms;
using Microsoft.Extensions.Logging;
using Moq;

namespace BusinessServicesTests
{
    public class SortAlgorithmTests
    {
        [Fact]
        public void SortNumbers_OriginalArrayNotModified()
        {
            // Arrange
            var service = CreateSortingService();
            var input = new double[] { 5, 2, 8, 10, 1 };

            // Act
            service.SortNumbers(input);

            // Assert
            Assert.Equal([5, 2, 8, 10, 1], input);
        }

        [Fact]
        public void SortNumbers_UsesAllAlgorithms()
        {
            // Arrange
            var mockAlgorithm1 = new Mock<ISortAlgorithm>();
            mockAlgorithm1.Setup(a => a.Sort(It.IsAny<double[]>()))
                .Returns<double[]>(arr => arr.OrderBy(x => x).ToArray());
            var mockAlgorithm2 = new Mock<ISortAlgorithm>();
            mockAlgorithm1.Setup(a => a.Sort(It.IsAny<double[]>()))
                .Returns<double[]>(arr => arr.OrderBy(x => x).ToArray());

            var service = CreateSortingService(new List<ISortAlgorithm> { mockAlgorithm1.Object, mockAlgorithm2.Object });

            // Act
            var result = service.SortNumbers(new double[] { 1, 2, 3} );

            // Assert
            mockAlgorithm1.Verify(a => a.Sort(It.IsAny<double[]>()), Times.Once);
            mockAlgorithm2.Verify(a => a.Sort(It.IsAny<double[]>()), Times.Once);

        }

        private SortingService CreateSortingService(IEnumerable<ISortAlgorithm> algorithms = null)
        {
            var mockLogger = new Mock<ILogger<SortingService>>();

            if (algorithms == null)
            {
                var mockAlgorithm = new Mock<ISortAlgorithm>();
                mockAlgorithm.Setup(a => a.Sort(It.IsAny<double[]>()))
                    .Returns<double[]>(arr => arr.OrderBy(x => x).ToArray());

                algorithms = new List<ISortAlgorithm> { mockAlgorithm.Object };
            }

            return new SortingService(algorithms, mockLogger.Object);
        }
    }
}
