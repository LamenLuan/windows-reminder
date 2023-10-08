using System;
using System.Linq;

namespace reminder_service
{
	public class Reminder
	{
		public Reminder(string[] dataLine)
		{
			Description = dataLine[0];
			var time = dataLine[1].Split(':').Select(x => int.Parse(x)).ToList();
			StartTime = new TimeSpan(time[0], time[1], 0);
			HoursToRepeat = byte.Parse(dataLine[2]);
		}

		public string Description { get; set; }
		public TimeSpan StartTime { get; set; }
		public byte HoursToRepeat { get; set; }
	}
}
