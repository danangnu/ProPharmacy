using System;
using System.Collections.Generic;

namespace API.Entities
{
  public class UserReport
  {
     public int Id { get; set; }
      public string ReportName { get; set; }
      public DateTime Created { get; set; } = DateTime.Now;
      public AppUser Creator { get; set; }
      public int AppUserId { get; set; }
      public ICollection<FilesVersion> VersionCreated { get; set; }
  }
}