using System.ComponentModel;

namespace windows_reminder_controller.ViewModels
{
  public class ReminderViewModel
  {
    [DisplayName("Descrição")]
    public string? Description { get; set; }

    [DisplayName("Início")]
    public string? StartTime { get; set; }

    [DisplayName("Repete em")]
    public string? RepeatsIn { get; set; }
  }
}
