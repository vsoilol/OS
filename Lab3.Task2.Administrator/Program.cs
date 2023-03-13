using System.Diagnostics;

Console.WriteLine("Administrator");

var readerAEvent = new EventWaitHandle(false, EventResetMode.ManualReset, "readerAEvent");
var readerBEvent = new EventWaitHandle(false, EventResetMode.ManualReset, "readerBEvent");
var readerEndSessionEvent = new EventWaitHandle(
    false,
    EventResetMode.ManualReset,
    "readerEndSessionEvent"
);

var writerAEvent = new EventWaitHandle(false, EventResetMode.ManualReset, "writerAEvent");
var writerBEvent = new EventWaitHandle(false, EventResetMode.ManualReset, "writerBEvent");
var writerEndSessionEvent = new EventWaitHandle(
    false,
    EventResetMode.ManualReset,
    "writerEndSessionEvent"
);

var writerProcessName = "Lab3.Task2.Writer.exe";
var readerProcessName = "Lab3.Task2.Reader.exe";

var semaphoreReader = new Semaphore(2, 2, "semaphoreReader");
var semaphoreWriter = new Semaphore(2, 2, "semaphoreWriter");

Console.Write("Input count: ");
var count = int.Parse(Console.ReadLine());

ProcessStartInfo writerProcessInfo = new ProcessStartInfo()
{
    FileName = writerProcessName,
    UseShellExecute = true,
};

ProcessStartInfo readerProcessInfo = new ProcessStartInfo()
{
    FileName = readerProcessName,
    UseShellExecute = true,
};

for (int i = 0; i < count; i++)
{
    Process.Start(writerProcessInfo);
    Process.Start(readerProcessInfo);
}

while (true)
{
    int eventIndex = WaitHandle.WaitAny(
        new WaitHandle[]
        {
            writerAEvent,
            writerBEvent,
            writerEndSessionEvent,
            readerEndSessionEvent
        }
    );

    switch (eventIndex)
    {
        case 0:
            Console.Write("A");
            writerAEvent.Reset();
            readerAEvent.Set();
            break;

        case 1:
            Console.Write("B");
            writerBEvent.Reset();
            readerBEvent.Set();
            break;

        case 2:
            Console.WriteLine("\nEnd of working one writer process");
            writerEndSessionEvent.Reset();
            break;

        case 3:
            Console.WriteLine("\nEnd of working one reader process");
            readerEndSessionEvent.Reset();
            break;
    }
}
