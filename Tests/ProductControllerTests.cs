using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http;

namespace Tests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        public ProductControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(10, 1)]
        [InlineData(20,1)]
        [InlineData(50,1)]
        public async Task GetAll_WithQueryParameters_ReturnsOkResultAsync(int pageSize, int pageNumber)
        {
            //act
            var response = await _client.GetAsync($"/api/Product?PageSize={pageSize}&pageNumber={pageNumber}");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(16, 1)]
        [InlineData(22, 1)]
        [InlineData(12, 1)]
        [InlineData(0,0)]
        public async Task GetAll_WithInvalidQueryParameters_ReturnsBadRequestStatus(int pageSize, int pageNumber)
        {
            //act
            var response = await _client.GetAsync($"/api/Product?PageSize={pageSize}&pageNumber={pageNumber}");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
