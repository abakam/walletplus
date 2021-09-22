using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Models.Common;
using WalletPlus.Api.Services.Common;
using WalletPlus.Api.Services.Helpers.Constants;

namespace WalletPlus.Api.Services.Wallet
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public WalletService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<BaseResponse<TopupWalletResponseDto>> TopUp(TopupWalletRequestDto topupWalletRequestDto)
        {
            try
            {
               var user = await _unitOfWork.Users.GetByEmail(topupWalletRequestDto.Email);

                if (user == null)
                    return BaseResponse<TopupWalletResponseDto>.WithError(ErrorMessages.RECORD_DO_EXISTS, StatusCodes.RECORD_DO_NOT_EXISTS);

                var mainWallet = (await _unitOfWork.Wallets.Find(w => w.UserId == user.Id && w.Type == Models.Enums.WalletType.Main)).FirstOrDefault();

                var bonusWallet = (await _unitOfWork.Wallets.Find(w => w.UserId == user.Id && w.Type == Models.Enums.WalletType.Bonus)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError("WalletService-TopUp", ex);
                return BaseResponse<TopupWalletResponseDto>.WithError(ErrorMessages.INTERNAL_ERROR_MESSAGE, StatusCodes.INTERNAL_ERROR);
            }
        }

        public Task<BaseResponse<TopupWalletResponseDto>> Transfer(TransferToWalletRequestDto transferToWalletRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
