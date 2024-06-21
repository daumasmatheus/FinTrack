using FinTrack.Api.Data;
using FinTrack.Core.Common;
using FinTrack.Core.Enums;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Handlers
{
    public class TransactionHandler(AppDbContext dbContext) : ITransactionHandler
    {
        public async Task<BaseResponse<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { TransactionType: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= 1;

            Transaction transaction = new()
            {
                Title = request.Title,
                Amount = request.Amount,
                CreatedAt = DateTime.Now,
                TransactionType = request.TransactionType,
                CategoryId = request.CategoryId,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                UserId = request.UserId
            };

            try
            {
                await dbContext.Transactions.AddAsync(transaction);
                await dbContext.SaveChangesAsync();

                return new BaseResponse<Transaction?>(transaction, message: "Transação criada com sucesso");
            }
            catch (Exception)
            {
                return new BaseResponse<Transaction?>(transaction, 404, "Não foi possivel criar a transação");
                throw;
            }
        }

        public async Task<BaseResponse<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                Transaction? transaction = await dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new BaseResponse<Transaction?>(null, 404, "Transação nao encontrada");

                dbContext.Transactions.Remove(transaction);
                await dbContext.SaveChangesAsync();

                return new BaseResponse<Transaction?>(transaction, message: "Transação removida com sucesso");
            }
            catch (Exception)
            {
                return new BaseResponse<Transaction?>(null, message: "Não foi possivel remover a Transação");
                throw;
            }
        }

        public async Task<BaseResponse<Transaction?>> GetById(GetTransactionByIdRequest request)
        {
            try
            {
                Transaction? transaction = await dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null ?
                    new BaseResponse<Transaction?>(null, 404, "Transação nao encontrada") : new BaseResponse<Transaction?>(transaction);
            }
            catch (Exception)
            {
                return new BaseResponse<Transaction?>(null, 404, "Falha ao obter a Transação");
                throw;
            }
        }

        public async Task<PagedBaseReponse<List<Transaction>>> GetByPeriod(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch (Exception)
            {
                return new PagedBaseReponse<List<Transaction>>(null, 400, "Não foi possivel determinar a data inicio/data fim");
                throw;
            }

            try
            {
                var query = dbContext.Transactions.AsNoTracking()
                                                  .Where(x => x.PaidOrReceivedAt >= request.StartDate &&
                                                              x.PaidOrReceivedAt <= request.EndDate &&
                                                              x.UserId == request.UserId)
                                                  .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedBaseReponse<List<Transaction>>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {
                return new PagedBaseReponse<List<Transaction>>(null, 400, "Falha ao obter as transações");
                throw;
            }
        }

        public async Task<BaseResponse<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { TransactionType: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= 1;

            try
            {
                Transaction? transaction = await dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new BaseResponse<Transaction?>(null, 404, "Transação nao encontrada");

                transaction.Title = request.Title;
                transaction.CategoryId = request.CategoryId;
                transaction.TransactionType = request.TransactionType;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                dbContext.Transactions.Update(transaction);
                await dbContext.SaveChangesAsync();

                return new BaseResponse<Transaction?>(transaction, message: "Transação atualizada com sucesso");
            }
            catch (Exception)
            {
                return new BaseResponse<Transaction?>(null, 404, "Não foi possivel atualizar a Transação");
                throw;
            }
        }
    }
}
