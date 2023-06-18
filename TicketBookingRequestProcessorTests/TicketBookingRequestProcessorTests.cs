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
        public TicketBookingRequestProcessorTests()
        {
            _processor = new TicketBookingRequestProcessor();
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

    }
}
