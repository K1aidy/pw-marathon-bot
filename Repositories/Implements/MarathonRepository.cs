using Marathon.DataBase;
using Marathon.DataBase.Entities;
using Marathon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marathon.Repositories.Implements
{
	/// <inheritdoc />
	public class MarathonRepository : IMarathonRepository
	{
		private readonly MarathonContext _contex;

		public MarathonRepository(MarathonContext contex)
		{
			_contex = contex ?? throw new ArgumentNullException(nameof(contex));
		}

		/// <inheritdoc />
		public async Task<Result> AddResult(int accountId, string text)
		{
			var result = await _contex.Results.AsNoTracking()
				.Where(a => a.AccountId == accountId)
				.FirstOrDefaultAsync();

			if (result == null)
			{
				result = new Result
				{
					AccountId = accountId,
					Dt = DateTime.Now,
					Text = text
				};

				await _contex.AddAsync(result);
			}
			else
			{
				result.Text = text;
				result.Dt = DateTime.Now;
			}

			await _contex.SaveChangesAsync();

			return result;
		}
	}
}
