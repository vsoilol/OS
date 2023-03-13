Console.WriteLine("Writer");
var semaphore = Semaphore.OpenExisting("semaphoreWriter");

EventWaitHandle writerAEvent = EventWaitHandle.OpenExisting("writerAEvent");
EventWaitHandle writerBEvent = EventWaitHandle.OpenExisting("writerBEvent");
EventWaitHandle writerEndSessionEvent = EventWaitHandle.OpenExisting("writerEndSessionEvent");

Console.WriteLine("Input message A or B (to exit enter 'Esc')");

var isWorking = true;

while (isWorking)
{
    semaphore.WaitOne();

    var key = Console.ReadKey();

    switch (key.Key)
    {
        case ConsoleKey.A:
            writerAEvent.Set();
            break;
        case ConsoleKey.B:
            writerBEvent.Set();
            break;
        case ConsoleKey.Escape:
            isWorking = false;
            writerEndSessionEvent.Set();
            break;
    }

    semaphore.Release();
}
