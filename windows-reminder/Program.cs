using comum;
using windows_reminder;

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