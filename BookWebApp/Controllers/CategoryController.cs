using BookWebApp.Data;
using BookWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           IEnumerable<Category> objCategoryList = _db.Categories; 
            return View(objCategoryList);
        }
        //GET-Create
		public IActionResult Create()
		{
			
			return View();
		}	
        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(Category obj)
		{
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("Name","Display Order cannot exactly match with Name. ");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
				TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
		}

		//GET-Edit
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDb = _db.Categories.Find(id);
			//var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);

			if(categoryFromDb == null) { 
				return NotFound();
			}

			return View(categoryFromDb);
		}
		//POST-Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "Display Order cannot exactly match with Name. ");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category edited successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		//GET-Delete
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDb = _db.Categories.Find(id);
			//var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}
		//POST-Delete
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
				

			}

				_db.Categories.Remove(obj);
				_db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
			
		}

	}
}
