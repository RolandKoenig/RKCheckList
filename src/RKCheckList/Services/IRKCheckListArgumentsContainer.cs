namespace RKCheckList.Services;

public interface IRKCheckListArgumentsContainer
{
    string? InitialFile { get; }

    void NotifyFileOpened(string filePath);
}