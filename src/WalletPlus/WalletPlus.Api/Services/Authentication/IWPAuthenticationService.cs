using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Services.Common;

namespace WalletPlus.Api.Services.Authentication
{
    public interface IWPAuthenticationService
    {
        public Task<BaseResponse<LoginResponseDto>> Login(LoginRequestDto loginRequest);
        public Task<BaseResponse<LoginResponseDto>> Register(RegisterRequestDto registerRequestDto);
    }
    
}
