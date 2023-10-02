using comum;

namespace windows_reminder
{
	public class Program
	{
		private static void Main()
		{
			var reminders = new List<Reminder>();
			var fileData = FileManager.ReadFileData();

			if (fileData == null)
				return;

			foreach (var line in fileData)
			{
				if (!line.Any())
					break;

				var props = line.Split(',');

				if (props.Length == 3)
					reminders.Add(new Reminder(props));
			}

			var tasks = reminders.Select(r => Task.Run(() => RunReminder(r)));
			Task.WhenAll(tasks).Wait();
		}

		private static bool NextReminderInSameDay(TimeSpan nextReminderTime)
			=> (TimeSpan.FromDays(1) - nextReminderTime).Ticks > 0;

		private static void NotifyReminder(string message)
		{
			Console.WriteLine(message);
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