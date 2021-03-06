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
        Task<IEnumerable<GetAddressesDto>> GetAddresses();
        Task<GetAddressesDto> GetMyAddressById(int addressId);
        Task<int> AddAddress(AddressDto dto);
        Task EditProfileDetails(ProfileDetailsDto dto);
        Task DeactivateAccount(DeactivateAccountDto dto);
    }
}
