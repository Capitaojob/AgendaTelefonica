using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgendaTelefonica.Models;

namespace AgendaTelefonica.Pages.Contato
{
    public class CreateModel : PageModel
    {
        private readonly AgendaTelefonica.Models.AgendaTelefonicaContext _context;

        public CreateModel(AgendaTelefonica.Models.AgendaTelefonicaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Contato Contato { get; set; } = default!;

        [BindProperty]
        public List<string> Telefones { get; set; } = new List<string>();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Contatos == null || Contato == null)
            {
                return Page();
            }

            foreach (var numero in Telefones)
            {
                if (!string.IsNullOrWhiteSpace(numero))
                {
                    Contato.Telefones.Add(new Telefone { Numero = numero });
                }
            }

            _context.Contatos.Add(Contato);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
