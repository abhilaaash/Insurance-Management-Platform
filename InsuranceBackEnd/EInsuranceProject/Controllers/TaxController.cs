using EInsuranceProject.Data;
using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private InsuranceContext _context;

        public TaxController(InsuranceContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        public async Task<IActionResult> getTax()
        {
            var tax = await _context.Tax.Where(t => t.Id == 1).FirstOrDefaultAsync();
            return Ok(tax);

           

        }
        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTax(Tax tax)
        {

            var taxToUpdate = await _context.Tax.Where(tax => tax.Id == 1).FirstOrDefaultAsync();
            taxToUpdate.TaxPercent = tax.TaxPercent;
            _context.Tax.Update(taxToUpdate);
            await _context.SaveChangesAsync();

            return Ok(taxToUpdate);

        }
        [HttpGet("getAll")]
        public async Task<IActionResult> getAllTax()
        {
           

            var tax = await _context.Tax.Where(t => t.Id == 1).FirstOrDefaultAsync();

            var taxList = new List<Tax> { tax };

            return Ok(taxList);

        }


    }
}

