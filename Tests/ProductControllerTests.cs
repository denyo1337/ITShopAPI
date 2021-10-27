using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI;
using System.Threading.Tasks;
using FluentAssertions;
namespace Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnsOkResultAsync()
        {
            //arrange
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            //act
            var response = await client.GetAsync("/api/Product?PageSize=5&pageNumber=1");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }
    }
}
