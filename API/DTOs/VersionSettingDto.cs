namespace API.DTOs
{
  public class VersionSettingDto
  {
    public int Id { get; set; }
    public int StartYear { get; set; }
    public int NoYear { get; set; }
    public double VolumeDecrease { get; set; }
    public double InflationRate { get; set; }
  }
}