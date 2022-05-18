using Hangfire;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HangFireWork.App.Services
{
    public class HangfireService
    {

        public static void AddOnceWork()
        {
            Hangfire.BackgroundJob.Enqueue(()=> AddLog2File("a Single Work"));
            //Hangfire.RecurringJob.AddOrUpdate("Mission İmposible", () => AddText2File(atxt), Cron.Minutely);

        }

        public static void AddScheduleWork()
        {
            Hangfire.BackgroundJob.Schedule(() => AddLog2File("Schedule Work Delay 10sec"),TimeSpan.FromSeconds(10));
        }


        public static void AddRecurringWork()
        {
            Hangfire.RecurringJob.AddOrUpdate("Recurring Work", () => AddLog2File("Recurring 1 Minute Work"), Cron.Minutely);

        }



        public static  void AddLog2File(string aTxt)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "WorkerNotes", "Notes.txt");
            if (!File.Exists(file))
            {
                using (StreamWriter swr = File.CreateText(file))
                {
                    
                };
            }

            StreamWriter sw = File.AppendText(file);
            sw.WriteLine($"<tr><td><b>{DateTime.Now}</b></td><td>[{aTxt}]</td></tr>");
            sw.Close();
        }


        public static async Task<string> ReadLogFileAsync()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "WorkerNotes", "Notes.txt");
            if (!File.Exists(file))
            {
                return "<tr><td>No Log Record</td></tr>";
            }

            using (StreamReader sr = new StreamReader(file)){
                return await sr.ReadToEndAsync();
            }
            


        }
        public static void  ClearLogs()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "WorkerNotes", "Notes.txt");
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            



        }
    }
}