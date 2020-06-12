using System;

namespace Domain.Entities
{
  public class Image
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Path { get; set; }
    public int Views { get; set; }
    public int Favorites { get; set; }
    public virtual AppUser Author { get; set; }

  }
}