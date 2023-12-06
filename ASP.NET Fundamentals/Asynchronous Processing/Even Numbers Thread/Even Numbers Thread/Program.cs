//Problem 1

Task task = Task.Run(() =>
{
    Parallel.For(0, 1000, number =>
    {
        Console.WriteLine(number);
    });
});

await task;

Console.WriteLine("Thread finished work");