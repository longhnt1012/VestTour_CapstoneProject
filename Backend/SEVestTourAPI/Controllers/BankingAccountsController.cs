using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingAccountsController : ControllerBase
    {
        private readonly IBankingAccountRepository _AccountRepo;

        public BankingAccountsController(IBankingAccountRepository repo)
        {
            _AccountRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBankingAccount()
        {
            try
            {
                return  Ok(await _AccountRepo.GetAllAccount());    
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankingAccountByID(int id)
        {
            try
            {
                var bankingAccount= await _AccountRepo.GetAccountbyId(id);
                return bankingAccount ==null ? NotFound() : Ok(bankingAccount);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBankingAccount(BankingAccountModel model) {

            try
            {
                var newBankingAccount = await _AccountRepo.AddNewAccount(model);
                var bankingAccount = await _AccountRepo.GetAccountbyId(newBankingAccount);

                return bankingAccount ==null ? NotFound() : Ok(bankingAccount);
            }
            catch { 
                return BadRequest();
            }
         

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBankingAccount(int id, BankingAccountModel model)
        {
            try
            {
                if (id != model.BankingAccountId)
                {
                    return NotFound();
                }
                if (id != null)
                {
                    await _AccountRepo.UpdateBankingAccount(id, model);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankingAccount(int id)
        {
            try
            {
               if(id != null)
                {
                    await _AccountRepo.DeleteBankingAccount(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

    
    }
}
