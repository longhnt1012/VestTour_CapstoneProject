using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public interface IBankingAccountRepository
    {

        public Task<List<BankingAccountModel>> GetAllAccount();

        public Task<BankingAccountModel> GetAccountbyId(int Accoountid);

        public Task<int> AddNewAccount (BankingAccountModel model);

        public Task   UpdateBankingAccount(int Accountid, BankingAccountModel model);

        public Task DeleteBankingAccount(int id);

    }
}
