using System;
using System.Collections.Generic;

namespace API.DTOs
{
  public class UserReportDto
  {
    public int Id { get; set; }
    public string ReportName { get; set; }
    public DateTime Created { get; set; }
    public ICollection<FilesVersionDto> VersionCreated { get; set; }
  }
}