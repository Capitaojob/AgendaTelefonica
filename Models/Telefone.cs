using System;
using System.Collections.Generic;

namespace AgendaTelefonica.Models;

public partial class Telefone
{
    public long? Id { get; set; }

    public long? IdContato { get; set; }

    public string Numero { get; set; } = null!;

    public virtual Contato? IdContatoNavigation { get; set; }
}
