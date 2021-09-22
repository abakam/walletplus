using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Models.Users;
using WalletPlus.Api.Services.Common;
using WalletPlus.Api.Services.Helpers;
using WalletPlus.Api.Services.Helpers.Constants;

namespace WalletPlus.Api.Services.Authentication
{
    public class WPAuthenticationService : IWPAuthenticationService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        public WPAuthenticationService(ITokenHelper tokenHelper, IUserRepository userRepository, ILogger logger)
        {
            _tokenHelper = tokenHelper;
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<BaseResponse<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            try
            {
                var user = (await _userRepository.Find(u => u.IsActive && u.Email == loginRequest.Email)).FirstOrDefault();

                if (user == null)
                {
                    return BaseResponse<LoginResponseDto>.WithError(ErrorMessages.INVALID_EMAIL_PASSWORD, StatusCodes.UNAUTHORISED_USER);
                }

                var passwordHash = HashingHelper.HashUsingPbkdf2
                                   (loginRequest.Password, user.PasswordSalt);

                if (user.Password != passwordHash)
                {
                    return BaseResponse<LoginResponseDto>.WithError(ErrorMessages.INVALID_EMAIL_PASSWORD, StatusCodes.UNAUTHORISED_USER);
                }

                var token = await Task.Run(() => _tokenHelper.GenerateToken(user));

                var loginResponse = new LoginResponseDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AccessToken = token
                };

                return BaseResponse<LoginResponseDto>.WithSuccess(loginResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError("AuthenicationService-Login", ex);
                return BaseResponse<LoginResponseDto>.WithError(ErrorMessages.INTERNAL_ERROR_MESSAGE, StatusCodes.INTERNAL_ERROR);
            }
            
        }

        public async Task<BaseResponse<LoginResponseDto>> Register(RegisterRequestDto registerRequest)
        {
            try
            {
                var newUser = await _userRepository.GetByEmail(registerRequest.Email);

                if (newUser != null)
                {
                    return BaseResponse<LoginResponseDto>.WithError(ErrorMessages.USER_ALREADY_EXISTS, StatusCodes.ALREADY_EXISTS);
                }

                string passwordSalt = Guid.NewGuid().ToString();

                var passwordHash = HashingHelper.HashUsingPbkdf2
                                   (registerRequest.Password, passwordSalt);

                newUser = new User
                {
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    Phone = registerRequest.Phone,
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = registerRequest.Email,
                    CreatedDate = DateTime.UtcNow
                };

                await _userRepository.Add(newUser);

                var token = await Task.Run(() => _tokenHelper.GenerateToken(newUser));

                var registerResponse = new LoginResponseDto
                {
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    AccessToken = token
                };

                return BaseResponse<LoginResponseDto>.WithSuccess(registerResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError("AuthenicationService-Register", ex);
                return BaseResponse<LoginResponseDto>.WithError(ErrorMessages.INTERNAL_ERROR_MESSAGE, StatusCodes.INTERNAL_ERROR);
            }
        }
    }
}
