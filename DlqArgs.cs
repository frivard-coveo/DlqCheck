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
        HelpText = "Delete the messages in the DLQ.",
        SetName = "Delete"
    )]
    public bool Delete { get; set; } = false;

    [Option(
        'r',
        "redrive",
        Required = false,
        Default = false,
        HelpText = "Redrive the messages in the DLQ.",
        SetName = "Redrive"
    )]
    public bool Redrive { get; set; } = false;

    [Option(
        "waittime",
        Required = false,
        Default = 5,
        HelpText = "Wait time in seconds for the GetMessage call."
    )]
    public int WaitTimeSeconds { get; set; } = 5;

    [Option(
        "visibilitytimeout",
        Required = false,
        Default = 2,
        HelpText = "Visibility timeout in seconds for the GetMessage call."
    )]
    public int VisibilityTimeout { get; set; } = 2;
}
