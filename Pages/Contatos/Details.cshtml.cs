using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AgendaTelefonica.Models;

namespace AgendaTelefonica.Pages.Contato
{
    public class DetailsModel : PageModel
    {
        private readonly AgendaTelefonica.Models.AgendaTelefonicaContext _context;

        public DetailsModel(AgendaTelefonica.Models.AgendaTelefonicaContext context)
        {
            _context = context;
        }

      public Models.Contato Contato { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos.Include(c => c.Telefones).FirstOrDefaultAsync(m => m.Id == id);
            if (contato == null)
            {
                return NotFound();
            }
            else 
            {
                Contato = contato;
            }
            return Page();
        }
    }
}
