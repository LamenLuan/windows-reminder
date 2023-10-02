namespace comum
{
	public static class FileManager
	{
		private const string DIRECTORY_NAME = "WindowsReminder";
		private const string FILE_NAME = "data.dt";

		private static string DirectoryPath
			=> $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/{DIRECTORY_NAME}";
		private static string Path
			=> $"{DirectoryPath}/{FILE_NAME}";

		public static void CreateFile()
		{
			if (!Directory.Exists(DirectoryPath))
				Directory.CreateDirectory(DirectoryPath);
		}

		public static string[]? ReadFileData()
		{
			if (!File.Exists(Path))
				return null;

			return File.ReadAllLines(Path);
		}

		public static void WriteData(string reminder)
		{
			var sw = new StreamWriter(Path, true);
			sw.WriteLine(reminder);
			sw.Close();
		}
	}
}