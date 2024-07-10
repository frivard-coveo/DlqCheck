using CommandLine;
using DlqCheck;

internal class Program
{
    private static int Main(string[] args)
    {
        Parser
            .Default.ParseArguments<DlqArgs>(args)
            .WithParsed(options =>
            {
                Console.WriteLine(options);
            });

        return 0;
    }
}
