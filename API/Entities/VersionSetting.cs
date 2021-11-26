namespace API.Entities
{
  public class VersionSetting
  {
    public int Id { get; set; }
    public int StartYear { get; set; }
    public int NoYear { get; set; }
    public double VolumeDecrease { get; set; }
    public double InflationRate { get; set; }
    public FilesVersion Version { get; set; }
    public int FilesVersionId { get; set; }
  }
}