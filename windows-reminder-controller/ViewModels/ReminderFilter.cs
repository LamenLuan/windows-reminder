using System.ComponentModel;

namespace windows_reminder_controller.ViewModels
{
  public class ReminderFilter
  {
    [DisplayName("Descrição")]
    public string Description { get; set; }

    [DisplayName("Repetir em quantas horas?")]
    public string RepeatTime { get; set; }

    public string StartHour { get; set; }
    public string StartMinute { get; set; }
  }
}
