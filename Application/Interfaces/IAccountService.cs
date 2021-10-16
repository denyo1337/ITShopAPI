using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto dto);
        Task<string> GenerateJWT(LoginUserDto dto);
        Task<AccountDetailsDto> GetMyAccountDetails();
        Task<IEnumerable<Address>> GetAddresses();
        Task<Address> GetMyAddressById(int addressId);
    }
}
