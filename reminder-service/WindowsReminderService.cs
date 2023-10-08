using comum;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace reminder_service
{
	public partial class WindowsReminderService : ServiceBase
	{
		public WindowsReminderService()
		{
#if DEBUG
			OnStart(null);
#else
			InitializeComponent();
#endif

		}

		protected override void OnStart(string[] args)
		{
			var reminders = new List<Reminder>();
			var fileData = FileManager.ReadFileData();

			if (!fileData.Any())
				return;

			foreach (var line in fileData)
			{
				if (!line.Any())
					break;

				var props = line.Split(',');

				if (props.Length == 3)
					reminders.Add(new Reminder(props));
			}

			var tasks = reminders.Select(r => Task.Run(() => RunReminder(r))).ToArray();
			Task.WhenAll(tasks).Wait();
		}

		protected override void OnStop()
		{
		}

		private static bool NextReminderInSameDay(TimeSpan nextReminderTime)
			=> (TimeSpan.FromDays(1) - nextReminderTime).Ticks > 0;

		private static void NotifyReminder(string message)
		{
			new ToastContentBuilder()
				.AddText("Lembrete")
				.AddText(message)
				.Show();
		}

		private static void RunReminder(Reminder reminder)
		{
			var timeNow = DateTime.Now.TimeOfDay;
			if (timeNow <= reminder.StartTime)
			{
				var timeToStart = (int)(reminder.StartTime - timeNow).TotalMilliseconds;
				Thread.Sleep(timeToStart);
				NotifyReminder(reminder.Description);

				if (reminder.HoursToRepeat > 0)
					RunReminder(reminder);
			}
			else if (reminder.HoursToRepeat > 0)
			{
				var hoursInscrease = TimeSpan.FromHours(reminder.HoursToRepeat);
				var nextReminderTime = reminder.StartTime + hoursInscrease;
				while (NextReminderInSameDay(nextReminderTime))
				{
					if (nextReminderTime < timeNow)
						nextReminderTime += hoursInscrease;
					else if (nextReminderTime > timeNow)
					{
						reminder.StartTime = nextReminderTime;
						RunReminder(reminder);
					}
					else
						NotifyReminder(reminder.Description);
				}
			}
		}
	}
}
