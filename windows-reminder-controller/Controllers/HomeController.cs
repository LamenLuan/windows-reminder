using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using windows_reminder_controller.Models;
using windows_reminder_controller.ViewModels;

namespace windows_reminder_controller.Controllers
{
  public class HomeController : Controller
  {
    public const string PATH = "data.dt";

    public IActionResult Index()
    {
      var alarms = new List<ReminderViewModel>();

      if (!System.IO.File.Exists(PATH))
        System.IO.File.Create(PATH);
      else
      {
        foreach (var line in System.IO.File.ReadAllLines(PATH))
        {
          if (!line.Any())
            break;

          var props = line.Split(',');

          if (props.Length == 3)
          {
            alarms.Add(new ReminderViewModel
            {
              Description = props[0],
              StartTime = props[1],
              RepeatsIn = props[2]
            });
          }
        }
      }

      return View(alarms);
    }

    [HttpPost]
    public IActionResult Index(ReminderFilter filter)
    {
      var reminder = $"{filter.Description},{filter.StartHour}:{filter.StartMinute},{filter.RepeatTime}";

      var sw = new StreamWriter(PATH);
      sw.WriteLine(reminder);
      sw.Close();

      return Index();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}