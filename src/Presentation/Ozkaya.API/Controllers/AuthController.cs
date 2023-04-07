using Application.Abstractions.Repositories;
using Application.Abstractions.Wrappers;
using Application.DTOs.User;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistance.Token;
using Persistance.TokenService;
using System.Security.Claims;

namespace Ozkaya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IJWTTokenServices _jwttokenservice;
        readonly IHttpContextAccessor _httpContextAccessor;
        ICitiesReadRepository citiesReadRepository;
        public AuthController(IJWTTokenServices jWTTokenServices, IHttpContextAccessor httpContextAccessor, ICitiesReadRepository citiesReadRepository)
        {
            _jwttokenservice = jWTTokenServices;
            _httpContextAccessor = httpContextAccessor;
            this.citiesReadRepository = citiesReadRepository;
        }


        [HttpPost]
        [Route("Authenticate")]
        public async Task<ActionResult> Authenticate()
        {

            ServiceResponse<JWTToken> serviceResponse = new ServiceResponse<JWTToken>();
            var user = this.citiesReadRepository.Get();

            var userClaims = new List<Claim>();
            userClaims.Add(new(ClaimTypes.Name, user.UserName));
            var userClaimIdentity = new ClaimsIdentity(userClaims, "userClaimIdentity");
            var userPrincipal = new ClaimsPrincipal(new[] { userClaimIdentity });
            await _httpContextAccessor.HttpContext.SignInAsync(userPrincipal);
            var token = _jwttokenservice.Authenticate(user);
            return Ok(token);

        }
    }
}
