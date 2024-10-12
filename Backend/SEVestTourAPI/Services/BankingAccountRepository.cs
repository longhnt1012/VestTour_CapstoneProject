using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public class BankingAccountRepository: IBankingAccountRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public BankingAccountRepository(VestTourDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddNewAccount(BankingAccountModel model)
        {
            var newAccount= _mapper.Map<BankingAccount>(model);
            _context.BankingAccounts!.Add(newAccount);
            await _context.SaveChangesAsync();
            return newAccount.BankingAccountId;

        }

        public async Task DeleteBankingAccount(int id)
        {
            var deleteAccount = await _context.BankingAccounts!.FindAsync(id);
            if (deleteAccount != null)
            {
                _context.Remove(deleteAccount);
                await _context.SaveChangesAsync();

            }
        }
        public async Task<BankingAccountModel> GetAccountbyId(int Accoountid)
        {
            var account = await _context.BankingAccounts.FirstOrDefaultAsync(ba => ba.BankingAccountId == Accoountid);
            return _mapper.Map<BankingAccountModel>(account);
        }

        public async Task<List<BankingAccountModel>> GetAllAccount()
        {
            var accounts = await _context.BankingAccounts!.ToListAsync();
            return _mapper.Map<List<BankingAccountModel>>(accounts);
        }

        public async Task UpdateBankingAccount(int Accountid, BankingAccountModel model)
        {
            if (Accountid == model.BankingAccountId)
            {
                var updateBankingAccount = _mapper.Map<BankingAccount>(model);
                _context.BankingAccounts!.Update(updateBankingAccount);
                await _context.SaveChangesAsync();
            }

        }
    }
}
