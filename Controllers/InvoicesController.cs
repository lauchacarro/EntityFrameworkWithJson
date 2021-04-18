using EntityFrameworkWithJson.Data;
using EntityFrameworkWithJson.Entity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;
using System.Threading.Tasks;

namespace EntityFrameworkWithJson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoicesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _dbContext.Invoices.ToListAsync();
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();

            return Ok(invoice);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Webhook(int id, [FromBody] JsonElement json)
        {
            var invoice = await _dbContext.Invoices.FindAsync(id);

            invoice.IsPayed = true;
            invoice.WebhookData = json;

            _dbContext.Invoices.Update(invoice);
            await _dbContext.SaveChangesAsync();

            return Ok(invoice);
        }
    }
}
