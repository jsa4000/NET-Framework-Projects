using System;

public static class Program
{
    public static void Main() {
        var l = new System.Collections.Generic.List<byte[]>();
        while (true) {
            Console.WriteLine("Allocating 10MB...");
            l.Add(new byte[10 * 1024 * 1024]);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            l.RemoveAt(0);
        }
    }
}
