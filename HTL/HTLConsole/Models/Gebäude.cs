using System;
using System.Collections.Generic;

namespace HTLConsole.Models;

public partial class Gebäude
{
    public uint Id { get; set; }

    public string Bez { get; set; } = null!;

    public virtual ICollection<Raum> Raums { get; set; } = new List<Raum>();
}
