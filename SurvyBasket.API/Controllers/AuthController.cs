﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.API.Contracts.Authentacations;
using SurveyBasket.API.Services;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService )
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterViewModel  model, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(model, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPost("Login")]
        public async Task <ActionResult> Login(AuthViewModel model , CancellationToken cancellationToken )
        {
            var result = await _authService.GetTokenForUserAsync(model.Email, model.Password, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        } 
    
    
    }
}
