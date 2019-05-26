using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quorum.Entities.Domain;
using Quorum.Entities.Identity;

namespace Quorum.DataProviders.IdentityDataProvider
{
	public sealed class IdentityDataContext : IdentityDbContext<QuorumUser>
	{
		public IdentityDataContext(DbContextOptions options) : base(options)
		{
		}
	}
}