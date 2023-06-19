using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingCore;
using Xunit;

namespace TicketBookingRequestProcessorTests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly TicketBookingRequestProcessor _processor;
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
        public TicketBookingRequestProcessorTests()
        {
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {   

            var request = new TicketBookingRequest
            {
                FirstName = "Abdul",
                LastName = "Rahman",
                Email = "abdulrahman@demo.com"
            };

            // Act
            TicketBookingResponse response = _processor.Book(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {     

            // Act  
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));

            // Assert  
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveToDatabase()
        {
            // Arrange              
            TicketBooking savedTicketBooking = null;

            _ticketBookingRepositoryMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
                .Callback<TicketBooking>((TicketBooking) =>
                {
                    savedTicketBooking = TicketBooking;
                });

            var request = new TicketBookingRequest
            {
                FirstName = "Abdul",
                LastName = "Rahman",
                Email = "abdulrahman@demo.com"
            };

            // Act  
            TicketBookingResponse response = _processor.Book(request);

            // Assert  
            _ticketBookingRepositoryMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once);

            Assert.NotNull(savedTicketBooking);
            Assert.Equal(request.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(request.LastName, savedTicketBooking.LastName);
            Assert.Equal(request.Email, savedTicketBooking.Email);
        }

    }
}
