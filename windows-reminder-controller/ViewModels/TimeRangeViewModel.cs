namespace windows_reminder_controller.ViewModels
{
  public class TimeRangeViewModel
  {
    public const string CLASS = "time-range";
    public string HoursId { get; set; }
    public string MinutesId { get; set; }
    public bool IsRequired { get; set; }
  }
}
