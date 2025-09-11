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
    public class DetailsModel : PageModel
    {
        private readonly EmployCrud.Models.EFCoreDbContext _context;

        public DetailsModel(EmployCrud.Models.EFCoreDbContext context)
        {
            _context = context;
        }

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
    }
}
