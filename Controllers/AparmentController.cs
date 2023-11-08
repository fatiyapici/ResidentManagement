using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResidentManagement.Data;
using ResidentManagement.ViewModels;

namespace ResidentManagement.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Apartment
        public async Task<IActionResult> Index()
        {
            var apartments = await _context.Apartments
                            .Include(a => a.User)
                            .ToListAsync();

            return _context.Apartments != null ?
                        View(await _context.Apartments.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Apartments'  is null.");
        }

        // GET: Apartment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                            .Include(a => a.User)
                            .FirstOrDefaultAsync(m => m.ID == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Number,Floor,Block,ApartmentType,Status,OwnerOrTenant")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            var viewModel = new ApartmentDetailViewModel();
            viewModel.ID = apartment.ID;
            viewModel.IdentityNo = apartment.User != null ? apartment.User.IdentityNo : null;
            viewModel.Status = apartment.Status;
            viewModel.ApartmentType = apartment.ApartmentType;
            viewModel.OwnerOrTenant = apartment.OwnerOrTenant;
            return View(viewModel);
        }

        // POST: Apartment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdentityNo,ApartmentType,Status,OwnerOrTenant")] ApartmentDetailViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var apartment = await _context.Apartments.FindAsync(viewModel.ID);
                if (apartment == null)
                {
                    return NotFound();
                }
                if (!string.IsNullOrEmpty(viewModel.IdentityNo))
                {
                    var user = _context.Users.FirstOrDefault(x => x.IdentityNo == viewModel.IdentityNo);
                    if (user == null)
                    {
                        ModelState.AddModelError("IdentityNo", "User not found");
                        return View(viewModel);
                    }
                    apartment.UserId = user.Id;
                }
                else
                {
                    apartment.UserId = null;
                }
                apartment.ApartmentType = viewModel.ApartmentType;
                apartment.Status = viewModel.Status;
                apartment.OwnerOrTenant = viewModel.OwnerOrTenant;
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ID))
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
            return View(viewModel);
        }

        // GET: Apartment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Apartments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Apartments'  is null.");
            }
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return (_context.Apartments?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
