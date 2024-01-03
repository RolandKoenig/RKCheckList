using System.IO;
using RKCheckList.Messages;
using RolandK.InProcessMessaging;

namespace RKCheckList.Services;

public class RKCheckListArgumentsContainer : IRKCheckListArgumentsContainer 
{
    private readonly IInProcessMessagePublisher _messagePublisher;
    
    /// <inheritdoc />
    public string? InitialFile { get; private set; }

    public RKCheckListArgumentsContainer(IInProcessMessagePublisher messagePublisher, string[] args)
    {
        _messagePublisher = messagePublisher;
        
        if ((args.Length > 0) &&
            (File.Exists(args[0])))
        {
            this.InitialFile = args[0];
        }
    }

    public void NotifyFileOpened(string filePath)
    {
        this.InitialFile = filePath;
        _messagePublisher.Publish<InitialFileChangedMessage>();
    }
}