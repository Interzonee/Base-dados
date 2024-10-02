using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("CategoriasMateriais", Schema = "dbo")]
  public partial class CategoriasMateriai
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoriaID
    {
      get;
      set;
    }

    public ICollection<MateriaisCategoria> MateriaisCategoria { get; set; }
    public string Nome
    {
      get;
      set;
    }
    public string Descricao
    {
      get;
      set;
    }
  }
}
