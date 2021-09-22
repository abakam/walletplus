using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Services.Common;

namespace WalletPlus.Api.Services.Wallet
{
    public class WalletService : IWalletService
    {

        public Task<BaseResponse<TopupWalletResponseDto>> TopUp(TopupWalletRequestDto topupWalletRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TopupWalletResponseDto>> Transfer(TransferToWalletRequestDto transferToWalletRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
