using CommandLine;

namespace DlqCheck;

internal record DlqArgs
{
    [Option('q', "queue", Required = true, HelpText = "The message queue to check.")]
    public string Queue { get; set; } = string.Empty;

    [Option(
        'c',
        "count",
        Required = false,
        Default = 10,
        HelpText = "The maximum number of messages to retrieve."
    )]
    public int Count { get; set; } = 10;

    [Option(
        'd',
        "delete",
        Required = false,
        Default = false,
        HelpText = "Delete the messages in the DLQ."
    )]
    public bool Delete { get; set; } = false;
}
