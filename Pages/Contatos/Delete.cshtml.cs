using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AgendaTelefonica.Models;
using System.Text;
using System.Configuration;

namespace AgendaTelefonica.Pages.Contato
{
    public class DeleteModel : PageModel
    {
        private readonly AgendaTelefonicaContext _context;
        private readonly IConfiguration _configuration;

        public DeleteModel(AgendaTelefonicaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }
            var contato = await _context.Contatos.Include(c => c.Telefones).FirstOrDefaultAsync(c => c.Id == id);

            if (contato != null)
            {
                if (contato.Telefones != null && contato.Telefones.Any())
                {
                    _context.Telefones.RemoveRange(contato.Telefones);
                }

                _context.Contatos.Remove(contato);
                await _context.SaveChangesAsync();

                LogDeletion(Contato);
            }

            return RedirectToPage("./Index");
        }

        private void LogDeletion(Models.Contato contato)
        {
            string? logFilePath = $"{_configuration["LogOutputPath"]}";

            if (string.IsNullOrEmpty(logFilePath)) logFilePath = $"logs";

            logFilePath += $"\\delecao-contato-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt";

            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

            var logMessage = new StringBuilder();
            logMessage.AppendLine("Contato Excluído:");
            logMessage.AppendLine($"ID: {contato.Id}");
            logMessage.AppendLine($"Nome: {contato.Nome}");
            logMessage.AppendLine($"Idade: {contato.Idade}");
            logMessage.AppendLine($"Data da Exclusão: {DateTime.Now}");
            logMessage.AppendLine("------------");

            System.IO.File.AppendAllText(logFilePath, logMessage.ToString());
        }
    }
}
