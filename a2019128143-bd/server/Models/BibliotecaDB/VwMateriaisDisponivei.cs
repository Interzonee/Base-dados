using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2019128143.Models.BibliotecaDb
{
  [Table("vw_MateriaisDisponiveis", Schema = "dbo")]
  public partial class VwMateriaisDisponivei
  {
    public int MaterialID
    {
      get;
      set;
    }
    public string Titulo
    {
      get;
      set;
    }
    public string Tipo
    {
      get;
      set;
    }
    public string Autor
    {
      get;
      set;
    }
    public string Editora
    {
      get;
      set;
    }
    public int? AnoPublicacao
    {
      get;
      set;
    }
    public string ISBN
    {
      get;
      set;
    }
    public string Localizacao
    {
      get;
      set;
    }
    public int? ExemplaresDisponiveis
    {
      get;
      set;
    }
  }
}
