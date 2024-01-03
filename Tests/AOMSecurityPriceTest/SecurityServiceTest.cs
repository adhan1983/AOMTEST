using AOMTEST.Entity;
using AOMTEST.Exceptions;
using AOMTEST.HttpRequest.Interfaces;
using AOMTEST.Logger.Interfaces;
using AOMTEST.Repository.Interfaces;
using AOMTEST.Response;
using AOMTEST.Services;
using AOMTEST.Validator;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace AOMSecurityPriceTest
{
    public class SecurityServiceTest
    {
        private Mock<ISecurityPriceHttp> _securityPriceHttp;
        private Mock<ISecurityRepository> _securityRepository;
        private Mock<ILoggerSecurityService> _loggerSecurityService;
        private Mock<SecurityPriceValidator> _securityPriceValidator;

        [SetUp]
        public void Setup()
        {
            _securityPriceHttp = new Mock<ISecurityPriceHttp>();
            _securityRepository = new Mock<ISecurityRepository>();
            _loggerSecurityService = new Mock<ILoggerSecurityService>();
            _securityPriceValidator = new Mock<SecurityPriceValidator>();
        }

        [Test]
        public async Task SecurityService_InsertListIsin_Success()
        {
            //Arrange
            List<string> lstISIn;            

            //Act            
            lstISIn = new List<string>() { "123456789AEF" };
            
            _securityPriceHttp.Setup(x => x.GetSecurityResponse(It.IsAny<string>())).ReturnsAsync(new SecutiryResponse
            {
                Price = 569
            });

            _loggerSecurityService.Setup(x => x.LogAsync(It.IsAny<string>()));

            _securityRepository.Setup(x => x.InsertSecutiryPriceAsync(It.IsAny<List<PriceData>>())).ReturnsAsync(true);

            var service = new SecurityService(_securityPriceHttp.Object, _securityRepository.Object, _loggerSecurityService.Object, new SecurityPriceValidator());

            //Assert
            Assert.DoesNotThrowAsync(() => service.InsertListSecutiryService(lstISIn));

            _securityRepository.Verify(x => x.InsertSecutiryPriceAsync(It.IsAny<List<PriceData>>()), Times.Exactly(1));

            _securityPriceHttp.Verify(x => x.GetSecurityResponse(It.IsAny<string>()), Times.Exactly(1));

            _loggerSecurityService.Verify(x => x.LogAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public async Task SecurityService_InsertListIsin_Throws_Invalid_ISIN_Fail() 
        {
            //Arrange
            List<string> lstISIn;

            //Act
            lstISIn = new List<string>() { "1234" };

            _securityPriceHttp.Setup(x => x.GetSecurityResponse(It.IsAny<string>())).ReturnsAsync(new SecutiryResponse
            {
                Price = 123
            });

            var service = new SecurityService(_securityPriceHttp.Object, _securityRepository.Object, _loggerSecurityService.Object, new SecurityPriceValidator());

            //Assert
            Assert.ThrowsAsync<ValidationException>(() => service.InsertListSecutiryService(lstISIn));
            
        }

        [Test]
        public async Task SecurityService_InsertListIsin_Throws_Database_Exception_Failure()
        {
            //Arrange
            List<string> lstISIn;

            //Act            
            lstISIn = new List<string>() { "123456789AEF" };

            _securityPriceHttp.Setup(x => x.GetSecurityResponse(It.IsAny<string>())).ReturnsAsync(new SecutiryResponse
            {
                Price = 569
            });

            _loggerSecurityService.Setup(x => x.LogAsync(It.IsAny<string>()));

            _securityRepository.Setup(x => x.InsertSecutiryPriceAsync(It.IsAny<List<PriceData>>())).ThrowsAsync(new DatabaseException { });

            var service = new SecurityService(_securityPriceHttp.Object, _securityRepository.Object, _loggerSecurityService.Object, new SecurityPriceValidator());
            
            //Assert
            Assert.ThrowsAsync<DatabaseException>(async () => await service.InsertListSecutiryService(lstISIn));            

        }
    }
}