using Microsoft.EntityFrameworkCore;
using Rodnie.API.Data;
using Rodnie.API.Models;

namespace Rodnie.API.Repositories
{
	public class PinRepository : IPinRepository
	{
		private readonly ApplicationDbContext _context;

		public PinRepository(ApplicationDbContext context)
		{	_context = context;}

		public async Task<Pin> CreateAsync(Pin pin)
		{
			_context.Pin.Add(pin);
			await _context.SaveChangesAsync();
			return pin;
		}

		public async Task<List<Pin>> GetByUserIdAsync(Guid userId)
		{
			return await _context.Pin
				.Where(p => p.owner_user_id == userId)
				.ToListAsync();
		}

		public async Task<Pin> GetByIdAsync(Guid id)
		{
			return await _context.Pin
				.FirstOrDefaultAsync(p => p.id == id);
		}

		public async Task<Pin> GetByIdAndUserIdAsync(Guid id, Guid userId)
		{
			return await _context.Pin
				.FirstOrDefaultAsync(p => p.id == id && p.owner_user_id == userId);
		}

		public async Task<Pin> UpdateAsync(Pin pin)
		{
			_context.Pin.Update(pin);
			await _context.SaveChangesAsync();
			return pin;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var pin = await _context.Pin.FindAsync(id);
			if (pin == null)
				return false;

			_context.Pins.Remove(pin);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}