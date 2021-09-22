using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Dtos;
using WalletPlus.Api.Models.Common;
using WalletPlus.Api.Models.WalletTransaction;
using WalletPlus.Api.Services.Common;
using WalletPlus.Api.Services.Helpers;
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

                mainWallet.CurrentBalance += topupWalletRequestDto.Amount;
                mainWallet.UpdatedBy = user.Id;
                mainWallet.UpdatedDate = DateTime.UtcNow;

                var mainWalletTransaction = new WalletTransaction
                {
                    Amount = topupWalletRequestDto.Amount,
                    Type = Models.Enums.WalletTransactionType.Bonus,
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.UtcNow
                };

                _unitOfWork.CreateTransaction();

                await _unitOfWork.Wallets.Update(mainWallet);
                await _unitOfWork.WalletTransactions.Add(mainWalletTransaction);

                var bonusAmount = BonusCalculator.GetBonus(topupWalletRequestDto.Amount);

                if(bonusAmount > 0)
                {
                    var bonusWallet = (await _unitOfWork.Wallets.Find(w => w.UserId == user.Id && w.Type == Models.Enums.WalletType.Bonus)).FirstOrDefault();

                    bonusWallet.CurrentBalance += bonusAmount;
                    bonusWallet.UpdatedBy = user.Id;
                    bonusWallet.UpdatedDate = DateTime.UtcNow;

                    var bonusWalletTransaction = new WalletTransaction
                    {
                        Amount = bonusAmount,
                        Type = Models.Enums.WalletTransactionType.Bonus,
                        CreatedBy = user.Id,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _unitOfWork.Wallets.Update(bonusWallet);
                    await _unitOfWork.WalletTransactions.Add(bonusWalletTransaction);
                }

                _unitOfWork.Save();

                var topupWalletResponseDto = new TopupWalletResponseDto
                {
                    Amount = topupWalletRequestDto.Amount,
                    Email = topupWalletRequestDto.Email,
                    TransactionReference = mainWalletTransaction.Id
                };

                return BaseResponse<TopupWalletResponseDto>.WithSuccess(topupWalletResponseDto);
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
