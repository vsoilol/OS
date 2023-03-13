Thread thread1 = new Thread(ShowSequenceNumber!);
Thread thread2 = new Thread(ShowSequenceNumber!);
Thread thread3 = new Thread(ShowSequenceNumber!);

Console.WriteLine("Thread 1 created");
Console.WriteLine("Thread 2 created");
Console.WriteLine("Thread 3 created");

thread2.Priority = ThreadPriority.AboveNormal;
thread3.Priority = ThreadPriority.Highest;

Console.WriteLine("Thread 3 priority: " + thread3.Priority);
Console.WriteLine("Thread 2 priority: " + thread2.Priority);
Console.WriteLine("Thread 1 priority: " + thread1.Priority);

thread1.Start(1);
thread2.Start(2);
thread3.Start(3);

thread1.Join();
thread2.Join();
thread3.Join();

void ShowSequenceNumber(object sequenceNumberObj)
{
    int sequenceNumber = (int)sequenceNumberObj;

    Console.WriteLine($"Thread {sequenceNumber} started");

    Thread.Sleep(2000);

    Console.WriteLine($"Thread {sequenceNumber} finished");
}
