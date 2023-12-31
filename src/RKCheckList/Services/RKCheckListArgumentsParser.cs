using System.IO;

namespace RKCheckList.Services;

public class RKCheckListArgumentsParser : IRKCheckListArgumentParser
{
    /// <inheritdoc />
    public string? InitialFile { get; }

    public RKCheckListArgumentsParser(string[] args)
    {
        if ((args.Length > 0) &&
            (File.Exists(args[0])))
        {
            this.InitialFile = args[0];
        }
    }
}