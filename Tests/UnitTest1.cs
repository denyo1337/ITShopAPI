using Application.DTOs;
using Application.Interfaces;
using Application.DTOs.ProductDtos.ProductDto;
using Domain.Entities;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using NSubstitute;

namespace Tests
{
    public class UnitTest1
    {
        private readonly AccountController _controller;
        private readonly IAccountService _accountService =
            Substitute.For<IAccountService>();
        public UnitTest1()
        {
            _controller = new AccountController(_accountService);
        }
        [Fact]
        public async Task GetProductById_Obj_Test()
        {

            var product = new ProductDto()
            {
                Name = "Some name",
                Description = "somedesc",
                Amount = 32,
                Price = 2,
                ProductType = "SomeType"
            };

            var mock = new Mock<IProductService>();

            var rs = mock.Setup(x => x.GetProductById(1)).ReturnsAsync(product);

            var controller = new ProductController(mock.Object);

            var result = await controller.GetProductbyId(1) as ObjectResult;

            var acutalResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ProductDto>(acutalResult);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);

        }
        //proper way
        [Fact]
        public async Task RegisterUserWithProperDto()
        {
            //Arrange
            var dto = new RegisterUserDto()
            {
                Email = "admin2@gmail.com",
                BirthDay = DateTime.Now.ToLocalTime(),
                Password = "password",
                ConfirmPassword = "password",
                PhoneNumber = 111222333,
                NickName = "admin2",
                Name = "teest",
                SurrName = "test",
                Nationality = "test"
            };
            _accountService.RegisterUser(dto).Returns(Task.CompletedTask);
            //Act

            var result = await _controller.Register(dto);

            var okObjectResult = (OkObjectResult)result;

            //Assert
            okObjectResult.StatusCode.Value.Equals(HttpStatusCode.OK);
        }
        
    }
}
