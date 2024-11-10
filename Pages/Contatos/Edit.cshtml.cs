using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AgendaTelefonica.Models;
using System.ComponentModel.DataAnnotations;

namespace AgendaTelefonica.Pages.Contato
{
    public class EditModel : PageModel
    {
        private readonly AgendaTelefonicaContext _context;

        public EditModel(AgendaTelefonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Contato Contato { get; set; } = default!;

        [BindProperty]
        public List<Telefone> Telefones { get; set; } = new List<Telefone>();

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

            Contato = contato;
            Telefones = contato.Telefones.ToList();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contatoExistente = await _context.Contatos
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(c => c.Id == Contato.Id);

            if (contatoExistente == null)
            {
                return NotFound();
            }

            contatoExistente.Nome = Contato.Nome;
            contatoExistente.Idade = Contato.Idade;

            contatoExistente.Telefones.RemoveAll(t => !Telefones.Any(e => e.Id == t.Id));

            foreach (var telefoneEditado in Telefones)
            {
                if (telefoneEditado.Id != null)
                {
                    var telefoneExistente = contatoExistente.Telefones.FirstOrDefault(t => t.Id == telefoneEditado.Id);
                    if (telefoneExistente != null)
                    {
                        telefoneExistente.Numero = telefoneEditado.Numero;
                        continue;
                    }
                }
                
                if (!string.IsNullOrEmpty(telefoneEditado.Numero))
                {
                    contatoExistente.Telefones.Add(new Telefone
                    {
                        Numero = telefoneEditado.Numero,
                        IdContato = Contato.Id
                    });
                }   
            }

            _context.Attach(contatoExistente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContatoExists(Contato.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContatoExists(long id)
        {
          return (_context.Contatos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
