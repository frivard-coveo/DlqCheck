using System.Net;
using Amazon.SQS;
using CommandLine;
using DlqCheck;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        var parserResult = Parser.Default.ParseArguments<DlqArgs>(args);

        var parsed = parserResult as Parsed<DlqArgs>;
        if (parsed != null)
        {
            Console.WriteLine(parsed.Value);
            IAmazonSQS amazonSqs = new AmazonSQSClient(); // use default credentials
            Console.WriteLine("Receiving messages...");
            Amazon.SQS.Model.ReceiveMessageResponse messages = await amazonSqs.ReceiveMessageAsync(
                new Amazon.SQS.Model.ReceiveMessageRequest
                {
                    QueueUrl = parsed.Value.Queue,
                    MaxNumberOfMessages = parsed.Value.Count,
                    WaitTimeSeconds = parsed.Value.WaitTimeSeconds,
                    VisibilityTimeout = parsed.Value.VisibilityTimeout
                }
            );
            if (!messages.HttpStatusCode.IsSuccess())
            {
                Console.WriteLine($"Failed to receive messages from queue {parsed.Value.Queue}.");
                Console.WriteLine($"HTTP Status Code: {messages.HttpStatusCode}");
                return 2;
            }
            var msgCount = messages.Messages.Count;
            Console.WriteLine($"Received {msgCount} messages.");
            Console.WriteLine("-----------------");
            foreach (var message in messages.Messages)
            {
                Console.WriteLine(message.MessageId);
                Console.WriteLine(message.Body);
                Console.WriteLine("-----------------");
            }
            if (parsed.Value.Delete && msgCount > 0)
            {
                Console.WriteLine("Deleting messages...");
                await amazonSqs.DeleteMessageBatchAsync(
                    new Amazon.SQS.Model.DeleteMessageBatchRequest()
                    {
                        QueueUrl = parsed.Value.Queue,
                        Entries = messages
                            .Messages.Select(
                                m => new Amazon.SQS.Model.DeleteMessageBatchRequestEntry
                                {
                                    Id = m.MessageId,
                                    ReceiptHandle = m.ReceiptHandle
                                }
                            )
                            .ToList()
                    }
                );
                Console.WriteLine($"{msgCount} messages deleted.");
            }
            if (parsed.Value.Redrive && msgCount > 0)
            {
                var attr = await amazonSqs.GetQueueAttributesAsync(
                    parsed.Value.Queue,
                    new List<string> { "QueueArn" }
                );
                Console.WriteLine($"Redriving {msgCount} messages in {attr.QueueARN}...");
                await amazonSqs.StartMessageMoveTaskAsync(
                    new Amazon.SQS.Model.StartMessageMoveTaskRequest { SourceArn = attr.QueueARN, }
                );
            }
            return 0;
        }
        else
        {
            return 1;
        }
    }
}

public static class HttpStatusCodeExtensions
{
    public static bool IsSuccess(this HttpStatusCode statusCode)
    {
        return ((int)statusCode >= 200) && ((int)statusCode <= 299);
    }
}
