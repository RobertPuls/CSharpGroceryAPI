using Microsoft.EntityFrameworkCore;

namespace GroceryApi.Models
{
	public class GroceryContext : DbContext
	{
		public GroceryContext(DbContextOptions<GroceryContext> options)
			: base(options)
		{
		}

		public DbSet<GroceryItem> GroceryItems { get; set; }

	}
}