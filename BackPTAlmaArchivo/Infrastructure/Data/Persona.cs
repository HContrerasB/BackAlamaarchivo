using System;
using System.Collections.Generic;

namespace BackPTAlmaArchivo.Infrastructure.Data;

public partial class Persona
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string NumeroIdentificacion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string TipoIdentificacion { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<PersonasUsuario> PersonasUsuarios { get; set; } = new List<PersonasUsuario>();
}
