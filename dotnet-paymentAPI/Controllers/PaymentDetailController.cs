using dotnet_paymentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_paymentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentDetailController : ControllerBase
	{
		private readonly PaymentDetailContext _context;
		public PaymentDetailController(PaymentDetailContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
		{
			return await _context.PaymentDetails.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
		{
			var paymentDetail = await _context.PaymentDetails.FindAsync(id);

			if(paymentDetail == null)
			{
				return NotFound();
			}

			return paymentDetail;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
		{
			if(id != paymentDetail.PaymentDetailID)
			{
				return BadRequest();
			}

			_context.Entry(paymentDetail).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PaymentDetailExists(id))
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

		[HttpPost]
		public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
		{
			_context.PaymentDetails.Add(paymentDetail);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetPaymentDetail", new {id = paymentDetail.PaymentDetailID}, paymentDetail);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePaymentDetail(int id)
		{
			var paymentDetail = await _context.PaymentDetails.FindAsync(id);
			if(paymentDetail == null)
			{
				return NotFound();
			}
			_context.PaymentDetails.Remove(paymentDetail);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool PaymentDetailExists(int id)
		{
			return _context.PaymentDetails.Any(e => e.PaymentDetailID == id);
		}
	}
}
