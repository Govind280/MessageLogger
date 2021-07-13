using LoggerMicroService.Controllers;
using LoggerMicroService.Helpers;
using LoggerMicroService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace LoggerMicroService.Test
{
    [TestClass]
    public class LoggerControllerTest
    {
        private Mock<IServiceHelper> _mockServiceHelper;
        private Mock<ILogger> _mockLogger;
        private Mock<IMessageLogger> _mockMessageLogger;
        private LoggerController _loggerController;

        [TestInitialize]
        public void Init()
        {
            _mockServiceHelper = new Mock<IServiceHelper>();
            _mockLogger = new Mock<ILogger>();
            _mockMessageLogger = new Mock<IMessageLogger>();
            _loggerController = new LoggerController(_mockServiceHelper.Object, _mockLogger.Object, _mockMessageLogger.Object);
        }

        [TestMethod]
        public async Task ProcessTransaction_ValidMessage()
        {
            // Arrange
            Guid refGuid = Guid.NewGuid();

            _mockServiceHelper.Setup(a => a.GetAsync()).ReturnsAsync(new ServiceResponse()
            {
                ID = refGuid,
                MessageDate = DateTime.Now,
                Message = "Test Message with Valid date and message length less than 255 characters"
            });

            // Act
            var result = await _loggerController.ProcessTransaction();

            // Assert
            OkObjectResult actionResult = result as OkObjectResult;
            ServiceResponse actualResultValue = (ServiceResponse)actionResult.Value;
            Assert.AreEqual(200, actionResult.StatusCode);
            Assert.AreEqual(refGuid, actualResultValue.ID);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ProcessTransaction_ValidMessage_Message_GreaterThan255()
        {
            // Arrange
            Guid refGuid = Guid.NewGuid();
            string message = @"Test Message with Valid date and message length greater than 255 characters.
                            Test Message with Valid date and message length greater than 255 characters.
                            Test Message with Valid date and message length greater than 255 characters.
                            Test Message with Valid date and message length greater than 255 characters.";

            _mockServiceHelper.Setup(a => a.GetAsync()).ReturnsAsync(new ServiceResponse()
            {
                ID = refGuid,
                MessageDate = DateTime.Now,
                Message = message
            });

            // Act
            var result = await _loggerController.ProcessTransaction();

            // Assert
            OkObjectResult actionResult = result as OkObjectResult;
            ServiceResponse actualResultValue = (ServiceResponse)actionResult.Value;
            Assert.AreEqual(200, actionResult.StatusCode);
            Assert.AreEqual(refGuid, actualResultValue.ID);
            Assert.AreEqual(message.Substring(0, 255), actualResultValue.Message);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ProcessTransaction_InValidMessage_Null_Message()
        {
            // Arrange
            Guid refGuid = Guid.NewGuid();

            _mockServiceHelper.Setup(a => a.GetAsync()).ReturnsAsync(new ServiceResponse()
            {
                ID = refGuid,
                MessageDate = DateTime.Now,
                Message = null
            });

            // Act
            var result = await _loggerController.ProcessTransaction();

            // Assert
            UnprocessableEntityObjectResult actionResult = result as UnprocessableEntityObjectResult;
            Assert.AreEqual(422, actionResult.StatusCode);            
            Assert.AreEqual("Unable to Process Request at this time. Please contact System Admin!!", actionResult.Value);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnprocessableEntityObjectResult));
        }
    }
}
