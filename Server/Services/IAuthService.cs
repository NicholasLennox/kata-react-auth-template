using System;
using Server.Models.Dtos;

namespace Server.Services
{
public interface IAuthService
    {
        Task<AuthResponseDto?> Register(RegisterRequestDto dto);
        Task<AuthResponseDto?> Login(LoginRequestDto dto);
    }
}
