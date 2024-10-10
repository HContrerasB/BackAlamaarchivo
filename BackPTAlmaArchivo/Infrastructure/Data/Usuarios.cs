using System;
using System.Collections.Generic;

namespace BackPTAlmaArchivo.Infrastructure.Data;

public partial class Usuarios
{
    public int Id { get; set; }

    public string Usuario { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<PersonasUsuario> PersonasUsuarios { get; set; } = new List<PersonasUsuario>();
}
