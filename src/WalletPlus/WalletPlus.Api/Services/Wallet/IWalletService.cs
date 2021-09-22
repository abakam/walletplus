using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Services.Common;

namespace WalletPlus.Api.Services.Wallet
{
    public interface IWalletService
    {
        Task<BaseResponse<TopupWalletResponseDto>> TopUp(TopupWalletRequestDto topupWalletRequestDto);
        Task<BaseResponse<TopupWalletResponseDto>> Transfer(TransferToWalletRequestDto transferToWalletRequestDto);
    }
}
