using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverKoepelendeOefening_LarsLauryssens.Data;
using OverKoepelendeOefening_LarsLauryssens.DTO;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    { 
        private readonly AppDbContext _context;

        public BankAccountsController( AppDbContext appDbContext)
        {
                _context = appDbContext;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return BadRequest("Geen bankaccount met dit id aanwezig");
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _context.BankAccounts.ToListAsync();
            if (result == null)
            {
                return Ok("[]");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddBankAccount([FromBody] BankAccountCreateDTO newAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Gelieve correcte data in te vullen");
            }

            // Maak een nieuw EF Core BankAccount-object aan
            var accountToAdd = new BankAccount()
            {
                // Laat Guid zelf genereren
                Id = Guid.NewGuid(),
                Name = newAccount.Name,
                Balance = newAccount.Balance
                // RowVersion laten we met rust, dat doet EF Core automatisch
            };
            try
            {
                _context.BankAccounts.Add(accountToAdd);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            // Retourneer de net aangemaakte resource
            // 201 Created + locatie: /api/bankaccounts/{newGuid}
            return CreatedAtAction(nameof(Get), new { id = accountToAdd.Id }, accountToAdd);

        }

        [HttpPut("/eigen/{id:guid}")]
        public async Task<ActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] BankAccount account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geen correcte data ingevuld");
            }
            //we halen het account op, of proberen dit toch
            var result = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound("Geen account met dit Id aanwezig");
            }
            _context.Entry(result).Property(r => r.RowVersion).OriginalValue = account.RowVersion;

            result.Balance = account.Balance;
            result.Name = account.Name;

            try
            {
                _context.BankAccounts.Update(result);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The record was updated by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAccountOri([FromRoute] Guid id, [FromBody] BankAccount account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geen correcte data ingevuld");
            }

            var result = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound("Geen account met dit Id aanwezig");
            }

            // Zeg tegen EF Core wat de 'OriginalValue' was volgens de client
            _context.Entry(result).Property(r => r.RowVersion).OriginalValue = account.RowVersion;

            // Schrijf de wijzigingen van de request-body over naar het tracked 'result'-object
            result.Balance = account.Balance;
            result.Name = account.Name;

            try
            {
                // In een connected scenario is 'Update(result)' niet noodzakelijk,
                // je zou ook direct 'await _context.SaveChangesAsync();' kunnen doen.
                _context.BankAccounts.Update(result);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The record was updated by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

    }
}
