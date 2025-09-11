using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmployCrud.Models;

namespace EmployCrud.Pages.EmploysNew
{
    public class DeleteModel : PageModel
    {
        private readonly EmployCrud.Models.EFCoreDbContext _context;

        public DeleteModel(EmployCrud.Models.EFCoreDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employ Employ { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employ = await _context.Employees.FirstOrDefaultAsync(m => m.Empno == id);

            if (employ == null)
            {
                return NotFound();
            }
            else
            {
                Employ = employ;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employ = await _context.Employees.FindAsync(id);
            if (employ != null)
            {
                Employ = employ;
                _context.Employees.Remove(Employ);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
