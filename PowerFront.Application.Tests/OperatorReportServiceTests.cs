using Moq;
using PowerFront.DTO;
using PowerFront.Repository;

namespace PowerFront.Services.Tests
{
    public class OperatorReportServiceTests
    {
        private Mock<IOperatorReportRepository> _mockRepository;
        private OperatorReportService _service;

        public OperatorReportServiceTests()
        {
            _mockRepository = new Mock<IOperatorReportRepository>();
            _service = new OperatorReportService(_mockRepository.Object);
        }

        [Fact]
        public void GetOperatorReport_ValidParameters_ReturnsReport()
        {
            // Arrange
            var expectedReport = new OperatorReportItemsDTO();
            _mockRepository.Setup(repo => repo.GetOperatorReport(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<string>()))
                           .Returns(expectedReport);

            // Act
            var result = _service.GetOperatorReport("today", DateTime.Now, DateTime.Now, "website", "device");

            // Assert
            Assert.Equal(expectedReport, result);
            _mockRepository.Verify(repo => repo.GetOperatorReport(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetDistinctDevices_CallsRepository_ReturnsListOfDevices()
        {
            // Arrange
            var expectedDevices = new List<string> { "Device1", "Device2" };
            _mockRepository.Setup(repo => repo.GetDistinctDevices()).Returns(expectedDevices);

            // Act
            var result = _service.GetDistinctDevices();

            // Assert
            Assert.Equal(expectedDevices, result);
            _mockRepository.Verify(repo => repo.GetDistinctDevices(), Times.Once);
        }

        [Fact]
        public void GetDistinctWebsites_CallsRepository_ReturnsListOfWebsites()
        {
            // Arrange
            var expectedWebsites = new List<string> { "Website1", "Website2" };
            _mockRepository.Setup(repo => repo.GetDistinctWebsites()).Returns(expectedWebsites);

            // Act
            var result = _service.GetDistinctWebsites();

            // Assert
            Assert.Equal(expectedWebsites, result);
            _mockRepository.Verify(repo => repo.GetDistinctWebsites(), Times.Once);
        }
    }
}
