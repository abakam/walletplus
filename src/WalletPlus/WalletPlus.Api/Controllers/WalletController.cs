using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Services.Wallet;
using WalletPlus.Api.ViewModels;

namespace WalletPlus.Api.Controllers
{
    [Route("api/v1/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {

        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        [SwaggerOperation("Topup a wallet")]
        [ProducesResponseType(typeof(ApiResult<TopupWalletResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResult<List<string>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Topup([FromBody] TopupWalletViewModel topupWalletViewModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResult<string> { IsError = true, Message = "A required field is empty" });
            }

            var topupWalletDto = new TopupWalletRequestDto
            {
                Email = topupWalletViewModel.Email,
                Amount = topupWalletViewModel.Amount
            };

            var topupWalletResponse = await _walletService.TopUp(topupWalletDto);

            if (topupWalletResponse.Success)
            {
                return StatusCode(StatusCodes.Status201Created, new ApiResult<TopupWalletResponseDto> { IsError = false, Data = topupWalletResponse.ReturnValue });
            }
            else
            {
                return StatusCode(Int32.Parse(topupWalletResponse.ErrorCode), new ApiResult<string> { IsError = true, Message = topupWalletResponse.DisplayMessage });
            }
        }
    }
}
