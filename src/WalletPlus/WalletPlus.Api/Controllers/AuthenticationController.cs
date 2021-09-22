using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Services.Authentication;
using WalletPlus.Api.ViewModels;

namespace WalletPlus.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IWPAuthenticationService _authenticationService;

        public AuthenticationController(IWPAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [SwaggerOperation("Register a new user")]
        [ProducesResponseType(typeof(ApiResult<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResult<List<string>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResult<string> { IsError = true, Message = "A required field is empty" });
            }

            var registerUserDto = new RegisterRequestDto
            {
                Email = registerUserViewModel.Email,
                FirstName = registerUserViewModel.FirstName,
                LastName = registerUserViewModel.LastName,
                Password = registerUserViewModel.Password,
                Phone = registerUserViewModel.Phone
            };

            var registerUserResponse = await _authenticationService.Register(registerUserDto);

            if (registerUserResponse.Success)
            {
                return StatusCode(StatusCodes.Status201Created, new ApiResult<LoginResponseDto> { IsError = false, Data = registerUserResponse.ReturnValue });
            }
            else
            {
                return StatusCode(Int32.Parse(registerUserResponse.ErrorCode), new ApiResult<string> { IsError = true, Message = registerUserResponse.DisplayMessage });
            }
        }

        [HttpPost]
        [SwaggerOperation("Get access token")]
        [ProducesResponseType(typeof(ApiResult<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResult<List<string>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResult<string> { IsError = true, Message = "A required field is empty" });
            }

            var userLoginDto = new LoginRequestDto
            {
                Email = registerUserViewModel.Email,
                Password = registerUserViewModel.Password
            };

            var userLoginResponse = await _authenticationService.Login(userLoginDto);

            if (userLoginResponse.Success)
            {
                return StatusCode(StatusCodes.Status201Created, new ApiResult<LoginResponseDto> { IsError = false, Data = userLoginResponse.ReturnValue });
            }
            else
            {
                return StatusCode(Int32.Parse(userLoginResponse.ErrorCode), new ApiResult<string> { IsError = true, Message = userLoginResponse.DisplayMessage });
            }
        }
    }
}
