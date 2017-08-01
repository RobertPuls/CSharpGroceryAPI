using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GroceryApi.Models;

namespace GroceryApi
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<GroceryContext>(opt => opt.UseInMemoryDatabase());
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
		}
	}
}