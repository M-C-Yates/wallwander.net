using System;

namespace Wallwander.Domain.Models

{

  public class Image
  {
    public Guid id;
    public string path;
    public string title;
    public (int, int) dimensions;
    public string format;
    public bool nsfw = false;
    public int views;
    public User uploader { get; set; }
  }
}