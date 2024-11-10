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
    public class IndexModel : PageModel
    {
        private readonly AgendaTelefonicaContext _context;

        public IndexModel(AgendaTelefonicaContext context)
        {
            _context = context;
        }

        public IList<Models.Contato> Contato { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Contatos != null)
            {
                Contato = await _context.Contatos.Include(c => c.Telefones).ToListAsync();
            }
        }
    }
}
