using System.Collections.Generic;

namespace API.Entities
{
    public class Docs
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public FilesVersion Version { get; set; }
        public int FilesVersionId { get; set; }
        public ICollection<Prescriptions> Prescriptions { get; set; }
        public ICollection<ScheduleOfPayment> ScheduleOfPayment { get; set; }
        public ICollection<PriceListHistory> PriceListHistory { get; set; }
    }
}