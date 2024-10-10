using System;
using System.Collections.Generic;

namespace BackPTAlmaArchivo.Infrastructure.Data;

public partial class PersonasUsuario
{
    public int Id { get; set; }

    public int PersonaId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Persona Persona { get; set; } = null!;

    public virtual Usuarios Usuario { get; set; } = null!;
}
