using System;
using System.Collections.Generic;

namespace HTLConsole.Models;

public partial class Raum
{
    public uint Id { get; set; }

    public uint Gid { get; set; }

    public string Bez { get; set; } = null!;

    public virtual Gebäude GidNavigation { get; set; } = null!;
}
