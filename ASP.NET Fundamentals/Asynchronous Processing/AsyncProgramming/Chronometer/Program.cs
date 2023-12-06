namespace Chronometer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Chronometer chronometer = new Chronometer();

            string line;

            while ((line = Console.ReadLine()) != "exit")
            {
                if (line == "start")
                {
                    Task.Run(() =>
                    {
                        chronometer.Start();
                    });
                }
                else if (line == "stop")
                {
                    chronometer.Stop();
                }
                else if (line == "lap")
                {
                    Console.WriteLine(chronometer.Lap());
                }
                else if (line == "laps")
                {
                    if (chronometer.Laps.Count == 0)
                    {
                        Console.WriteLine("NO LAPS TO SHOW");
                        continue;
                    }
                    Console.WriteLine("Laps: ");
                    foreach (var lap in chronometer.Laps)
                    {
                        Console.WriteLine(lap);
                    }
                }
                else if (line == "reset")
                {
                    chronometer.Reset();
                }
                else if (line == "time")
                {
                    Console.WriteLine(chronometer.GetTime);
                }
            }

            chronometer.Stop();

        }
    }
}