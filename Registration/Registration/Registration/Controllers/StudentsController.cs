using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Registration.Data;
using Registration.Models;
using System.Web.Helpers;

namespace Registration.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return _context.Students_Reg != null ?
                        View(await _context.Students_Reg.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Students_Reg'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students_Reg == null)
            {
                return NotFound();
            }

            var student = await _context.Students_Reg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.States = new SelectList(GetStates(), "StateCode", "StateName");
            ViewBag.Cities = new SelectList(GetCities(), "CityName", "StateCode");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    student.PhotoPath = await SaveImageAsync(photo);
                    student.PhotoType = photo.ContentType;
                    student.PhotoData = await GetDataAsBase64String(photo);
                }
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Students_Reg == null)
            {
                return NotFound();
            }

            var student = await _context.Students_Reg.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.States = new SelectList(GetStates(), "StateCode", "StateName");
            ViewBag.Cities = new SelectList(GetCities(student.State), "StateCode", "CityName");
            //setting image
            var image = new ImageModel
            {
                ImagePath = student.PhotoPath,
                //ImageName = student.Photo.Name
            };
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student, IFormFile photo)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (photo != null && photo.Length > 0)
                    {
                        student.PhotoPath = await SaveImageAsync(photo);
                    }
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students_Reg == null)
            {
                return NotFound();
            }

            var student = await _context.Students_Reg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students_Reg == null)
            {
                return Problem("Entity set 'AppDbContext.Students_Reg'  is null.");
            }
            var student = await _context.Students_Reg.FindAsync(id);
            if (student != null)
            {
                _context.Students_Reg.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students_Reg?.Any(std => std.Id == id)).GetValueOrDefault();
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images"));
            var fileName = DateTime.Now.ToFileTime() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            string imgPath = @"~/images/" + "/" + fileName;
            if (file.Length > 0)
            {
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
            return imgPath;
        }

        private List<State> GetStates()
        {
            var statesTbl = _context.StatesTbl.ToList();
            return statesTbl;
        }

        public ActionResult CityJson(string state)
        {
            var states = _context.StatesTbl.FirstOrDefault(x => x.StateCode == state);
            var cities = _context.CitiesTbl.Where(x => x.StateCode == state).Select(x => new { x.CityName }).ToList();
            var cityList = new List<string>();
            foreach(var city in cities)
            {
                cityList.Add(city.CityName);
            }
            return Json(cityList);
        }

        public List<City> GetCities(string state = null!)
        {
            if(state != null)
            {
                return _context.CitiesTbl.Where(x => x.StateCode == state).ToList();
            }
            var citiesTbl = _context.CitiesTbl.ToList();
            return citiesTbl;
        }

        private async Task<string> GetDataAsBase64String(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                var bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
    }
}