using System;

namespace API.DTOs
{
    public class FilesVersionDto
    {
        public int Id { get; set; }
        public string VersionName { get; set; }
        public DateTime Created { get; set; }
    }
}