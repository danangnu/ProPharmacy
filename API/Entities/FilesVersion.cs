using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class FilesVersion
    {
        public int Id { get; set; }
        public string VersionName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public AppUser Creator { get; set; }
        public int AppUserId { get; set; }
        public ICollection<PrescriptionSummary> PrescriptionSummary { get; set; }
        public ICollection<Mur> Mur { get; set; }
        public ICollection<SalesSummary> SalesSummary { get; set; }
        public ICollection<ExpenseSummary> ExpenseSummary { get; set; }
        public ICollection<Docs> Documents { get; set; }
    }
}