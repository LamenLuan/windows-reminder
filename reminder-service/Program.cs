#if DEBUG
using System;
#else
using System.ServiceProcess;
#endif

namespace reminder_service
{
  internal static class Program
  {
#if DEBUG

    static void Main()
    {

      new WindowsReminderService();
      Console.ReadKey();
    }

#else

    static void Main()
    {
      ServiceBase.Run(new WindowsReminderService());
    }

#endif
  }
}
