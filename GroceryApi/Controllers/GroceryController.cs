using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GroceryApi.Models;
using System.Linq;

namespace GroceryApi.Controllers
{
	[Route("api/[controller]")]
	public class GroceryController : Controller
	{
		private readonly GroceryContext _context;

        public GroceryController(GroceryContext context)
		{
			_context = context;

			if (_context.GroceryItems.Count() == 0)
			{
                _context.GroceryItems.Add(new GroceryItem { Name = "Milk", Quantity = 1, Unit = "Gallon"});
				_context.SaveChanges();
			}
		}

		[HttpGet]
		public IEnumerable<GroceryItem> GetAll()
		{
			return _context.GroceryItems.ToList();
		}

		[HttpGet("{id}", Name = "GetGrocery")]
		public IActionResult GetById(long id)
		{
			var item = _context.GroceryItems.FirstOrDefault(t => t.Id == id);
			if (item == null)
			{
				return NotFound();
			}
			return new ObjectResult(item);
		}

		[HttpPost]
		public IActionResult Create([FromBody] GroceryItem item)
		{
			if (item == null)
			{
				return BadRequest();
			}

			_context.GroceryItems.Add(item);
			_context.SaveChanges();

			return CreatedAtRoute("GetGrocery", new { id = item.Id }, item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(long id, [FromBody] GroceryItem item)
		{
			if (item == null || item.Id != id)
			{
				return BadRequest();
			}

			var grocery = _context.GroceryItems.FirstOrDefault(t => t.Id == id);
			if (grocery == null)
			{
				return NotFound();
			}

            grocery.Name = item.Name;
            grocery.Quantity = item.Quantity;
            grocery.Unit = item.Unit;

			_context.GroceryItems.Update(grocery);
			_context.SaveChanges();
			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			var grocery = _context.GroceryItems.First(t => t.Id == id);
			if (grocery == null)
			{
				return NotFound();
			}

			_context.GroceryItems.Remove(grocery);
			_context.SaveChanges();
			return new NoContentResult();
		}
	}
}