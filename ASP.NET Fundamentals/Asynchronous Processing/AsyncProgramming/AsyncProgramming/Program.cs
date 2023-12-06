namespace AsyncProgramming
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //Tasks allow us to work on multiple tasks at the same type.
            //For example if we see problem 3 and i use a regular for the UI is blocked and i cannot use it
            //When doing a Task async process i am able to see the progress of the sum and use other stuff.

            //Problem 1

            Task firstTask = Task.Run(() =>
            {
                Parallel.For(0, 10, number =>
                {
                    Console.WriteLine(number);
                });
            });

            await firstTask;           
            
            Console.WriteLine("Thread finished work");

            //Problem 2

            Console.WriteLine($"Sum of all the even numbers to a thousand: {SumAsync()}");

            //Problem 3

            long sum = 0;

            var secondTask = Task.Run(() => 
            {            
                for (int i = 0; i < 1000000000; i++)
                {
                    if (i % 2 == 0)
                    {
                        sum += i;
                        //Thread.Sleep(100);
                    }
                }
            });

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    return;
                }
                else if (line == "show")
                {
                    Console.WriteLine(sum);
                }
            }
        }
        private static long SumAsync()
        {
            return Task.Run(() =>
            {
                long sum = 0;

                for (int i = 0; i < 1000; i++)
                {
                    if (i % 2 == 0)
                    {
                        sum += i;
                    }
                }
                return sum;
            }).Result;
        }
    }
}