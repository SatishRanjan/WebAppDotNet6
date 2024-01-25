namespace PeriodicGreeter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string greetingMsg = $"Current UTC datetime is = ";
            while (true)
            {
                Console.WriteLine(greetingMsg + DateTime.UtcNow.ToString("o")) ;
                await Task.Delay(3000);
            }
        }
    }
}