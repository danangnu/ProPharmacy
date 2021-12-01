namespace API.DTOs
{
  public class UpdateVersionSettingDto
  {
    public int StartYear { get; set; }
    public int NoYear { get; set; }
    public double VolumeDecrease { get; set; }
    public double InflationRate { get; set; }
  }
}