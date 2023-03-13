Console.WriteLine("Reader");
var semaphore = Semaphore.OpenExisting("semaphoreReader");

EventWaitHandle readerAEvent = EventWaitHandle.OpenExisting("readerAEvent");
EventWaitHandle readerBEvent = EventWaitHandle.OpenExisting("readerBEvent");
EventWaitHandle readerEndSessionEvent = EventWaitHandle.OpenExisting("readerEndSessionEvent");

Console.WriteLine("Wait message... (to exit press 'Esc')");

Console.CursorVisible = false;

Task readSymbols = Task.Run(() =>
{
    ConsoleKeyInfo keyInfo;

    do
    {
        keyInfo = Console.ReadKey(true);
    } while (keyInfo.Key != ConsoleKey.Escape);

    readerEndSessionEvent.Set();
});

Task inputTask = Task.Run(() =>
{
    while (true)
    {
        semaphore.WaitOne();

        var handles = new WaitHandle[] { readerAEvent, readerBEvent };
        int eventIndex = WaitHandle.WaitAny(handles);

        if (eventIndex == 0)
        {
            Console.Write("A");
            readerAEvent.Reset();
        }
        else if (eventIndex == 1)
        {
            Console.Write("B");
            readerBEvent.Reset();
        }

        semaphore.Release();
    }
});

await Task.WhenAny(readSymbols, inputTask);
