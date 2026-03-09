using System;
using System.Collections.Generic;

namespace HTLConsole.Models;

public partial class Fach
{
    public uint Id { get; set; }

    public string Bez { get; set; } = null!;

    public virtual ICollection<Klasse> Kids { get; set; } = new List<Klasse>();
}
