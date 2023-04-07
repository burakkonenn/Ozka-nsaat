using Application.Abstractions.Wrappers;
using Application.DTOs.User;
using Domain.Entities;
using Persistance.Token;

namespace Persistance.TokenService
{
    public interface IJWTTokenServices
    {
        JWTToken Authenticate(User users);
    }
}
