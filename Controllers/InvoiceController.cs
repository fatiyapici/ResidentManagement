using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResidentManagement.Data;

namespace ResidentManagement.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public InvoiceController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoices.Include(i => i.Apartment).ThenInclude(x => x.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices
                .Include(i => i.Apartment)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // GET: Invoice/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userApartments = await _context.Apartments
                .Where(x => x.UserId == user.Id)
                .Select(x => new ApartmentDropdownOptions(x.ID, "Number: " + x.Number + " Floor: " + x.Floor + " Block: " + x.Block))
                .ToListAsync();

            if (userApartments == null || userApartments.Count == 0)
            {
                return NotFound("User apartments not found.");
            }
            var viewModel = new InvoiceViewModel();
            viewModel.Session = DateTime.Now.ToString("yyyy-MM");
            ViewData["UserApartments"] = new SelectList(userApartments, "ID", "NumberFloorBlockInfo");
            return View(viewModel);
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ApartmentId,Session,Amount,Description")] InvoiceViewModel invoiceViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userApartment = await _context.Apartments.FirstOrDefaultAsync(x => x.ID == invoiceViewModel.ApartmentId);

                if (userApartment != null)
                {
                    DateTime sessionDate;
                    if (DateTime.TryParse(invoiceViewModel.Session, out sessionDate))
                    {
                        var invoice = new Invoice
                        {
                            ApartmentId = userApartment.ID,
                            Session = sessionDate,
                            Amount = invoiceViewModel.Amount,
                            Description = invoiceViewModel.Description
                        };

                        _context.Add(invoice);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid date format.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "You don't have any assigned apartment. Please contact the administrator.");
                }
            }
            var userApartments = await _context.Apartments
                .Where(x => x.UserId == user.Id)
                .Select(x => new ApartmentDropdownOptions(x.ID, "Number: " + x.Number + " Floor: " + x.Floor + " Block: " + x.Block))
                .ToListAsync();

            ViewData["UserApartments"] = new SelectList(userApartments, "ID", "NumberFloorBlockInfo", invoiceViewModel.ApartmentId);
            return View(invoiceViewModel);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ID", "ID", invoice.ApartmentId);
            var viewModel = new InvoiceViewModel();
            viewModel.Session = DateTime.Now.ToString("yyyy-MM");
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ApartmentId,Session,Amount,Description")] Invoice invoice)
        {
            if (id != invoice.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.ID))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ID", "ID", invoice.ApartmentId);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Apartment)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return (_context.Invoices?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
