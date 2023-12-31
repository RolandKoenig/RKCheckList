using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RKCheckList.Model;

public class CheckListModel
{
    public CheckListItemModel[] Items { get; set; } = Array.Empty<CheckListItemModel>();

    public static async Task<CheckListModel> FromYamlFileAsync(string filePath)
    {
        await using var fileStream = File.OpenRead(filePath);
        using var fileStreamReader = new StreamReader(fileStream);

        return await FromYamlAsync(fileStreamReader);
    }
    
    public static async Task<CheckListModel> FromYamlAsync(TextReader textReader)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
        
        return await Task.Factory.StartNew(
            () => deserializer.Deserialize<CheckListModel>(textReader));
    }
}
