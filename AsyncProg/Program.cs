// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


List<Task<string>> tasks = new List<Task<string>>();
HttpClient httpClient = new HttpClient();

List<string> urls = new List<string>
            {
                "https://github.com/",
                "https://github.com/explore",
                "https://github.com/marketplace",
                "https://github.com/NarminSH?tab=repositories"
            };

Stopwatch stopwatch = Stopwatch.StartNew();

foreach (var item in urls)
{
    stopwatch.Start();
    tasks.Add(httpClient.GetStringAsync(item));
    stopwatch.Stop();
    Console.WriteLine(stopwatch.ElapsedMilliseconds);
}

await Task.WhenAll(tasks);

foreach (var task in tasks)
    Console.WriteLine($"Length is : {task.Result.Length}, millisec is :{stopwatch.ElapsedMilliseconds}");

Task t = Task.WhenAll(tasks);

try
{
    t.Wait();
}
catch { }

if (t.Status == TaskStatus.RanToCompletion)
    Console.WriteLine("all succeeded.");
else if (t.Status == TaskStatus.Faulted)
    Console.WriteLine("failed");


