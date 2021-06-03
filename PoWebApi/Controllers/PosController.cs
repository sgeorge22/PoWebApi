using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoWebApi.Data;
using PoWebApi.Models;

namespace PoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosController : ControllerBase
    {
        private readonly PoContext _context;

        public PosController(PoContext context)
        {
            _context = context;
        }

        // GET: api/Pos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrders()
        {
            return await _context.PurchaseOrders
                .Include(x => x.Employee)
                .ToListAsync();
        }

        // GET: api/Pos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders
                
                .SingleOrDefaultAsync(x => x.Id == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return purchaseOrder;
        }
        //the below code is doing the same thing as the above but has the Include so that it can include in the getbypk the employee as well
        // GET: api/Pos/5/empl
        [HttpGet("{id}/empl")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderandEmpl(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders
                .Include(x => x.Employee)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return purchaseOrder;
        }
        [HttpPut("{id}/edit")]
        public async Task<IActionResult> PutPoToEdit(int id)
        {
            var po = await _context.PurchaseOrders.FindAsync(id);
            if(po == null)
            {
                return NotFound();
            }
            po.Status = "EDIT";
           

            return await PutPurchaseOrder(id, po);
        }

        [HttpPut("{id}/review")]
        public async Task<IActionResult> PutPoToReviewOrApprove(int id)
        {
            var po = await _context.PurchaseOrders.FindAsync(id);
            if(po == null)
            {
                return NotFound();
            }
            po.Status = (po.Total <= 100 && po.Total > 0) ? "APPROVED" : "REVIEW";

            return await PutPurchaseOrder(id, po);

        }


        // PUT: api/Pos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseOrder", new { id = purchaseOrder.Id }, purchaseOrder);
        }

        // DELETE: api/Pos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseOrder>> DeletePurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();

            return purchaseOrder;
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrders.Any(e => e.Id == id);
        }
    }
}
