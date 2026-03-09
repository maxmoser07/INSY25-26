using System;
using System.Collections.Generic;

namespace HTLConsole.Models;

public partial class Klasse
{
    public uint Id { get; set; }

    public string Bez { get; set; } = null!;

    public virtual ICollection<Fach> Fids { get; set; } = new List<Fach>();
}
