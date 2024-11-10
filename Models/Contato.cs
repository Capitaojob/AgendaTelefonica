using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaTelefonica.Models;

public partial class Contato
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = null!;

    public int? Idade { get; set; }

    public virtual List<Telefone> Telefones { get; set; } = new List<Telefone>(); 
}
