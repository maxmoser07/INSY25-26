using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Teacher
{
    [Key]
    public int Id { get; set; }
    public string Abbr { get; set; }
    
    public virtual ICollection<T_has_S> TeacherSubjects { get; set; }
    public virtual ICollection<T_has_C> TeacherClasses { get; set; }
}

public class Subject
{
    [Key]
    public int Id { get; set; }
    public string Desc { get; set; }

    // Navigation properties
    public virtual ICollection<T_has_S> TeacherSubjects { get; set; }
    public virtual ICollection<C_has_S> ClassSubjects { get; set; }
}

public class Class
{
    [Key]
    public int Id { get; set; }
    public string Abbr { get; set; }
    
    public virtual ICollection<C_has_S> ClassSubjects { get; set; }
    public virtual ICollection<T_has_C> TeacherClasses { get; set; }
}

public class T_has_S
{
    [Key, Column(Order = 0)]
    public int Tid { get; set; }
    [ForeignKey("Tid")]
    public virtual Teacher Teacher { get; set; }

    [Key, Column(Order = 1)]
    public int Sid { get; set; }
    [ForeignKey("Sid")]
    public virtual Subject Subject { get; set; }
}


public class C_has_S
{
    [Key, Column(Order = 0)]
    public int Cid { get; set; }
    [ForeignKey("Cid")]
    public virtual Class Class { get; set; }

    [Key, Column(Order = 1)]
    public int Sid { get; set; }
    [ForeignKey("Sid")]
    public virtual Subject Subject { get; set; }
}

public class T_has_C
{
    [Key, Column(Order = 0)]
    public int Tid { get; set; } // Note: The image shows Tid/Cid usage
    [ForeignKey("Tid")]
    public virtual Teacher Teacher { get; set; }

    [Key, Column(Order = 1)]
    public int Cid { get; set; }
    [ForeignKey("Cid")]
    public virtual Class Class { get; set; }
}

public class Unterricht
{
    [Key]
    public int Id { get; set; }

    // Diese IDs bilden zusammen die Composite FKs
    public int Tid { get; set; }
    public int Sid { get; set; }
    public int Cid { get; set; }

    // Navigation Properties (Optional, können bleiben für Includes)
    [ForeignKey("Tid")]
    public virtual Teacher? Teacher { get; set; }
    
    [ForeignKey("Sid")]
    public virtual Subject? Subject { get; set; }
    
    [ForeignKey("Cid")]
    public virtual Class? Class { get; set; }

    public WeekDay Day { get; set; }
    public string Hour { get; set; }
}
public enum WeekDay
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5
}
