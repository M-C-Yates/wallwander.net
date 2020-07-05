using System;

namespace Domain.Entities
{
  public class Wallpaper
  {
    public Guid Id { get; set; }
    public string PublicId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int Views { get; set; }
    public int Favorites { get; set; }
    public virtual AppUser Author { get; set; }

  }
}