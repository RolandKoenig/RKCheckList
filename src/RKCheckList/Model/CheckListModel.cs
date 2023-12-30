using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RKCheckList.Model;

public class CheckListModel
{
    // public IReadOnlyList<CheckListItemModel> Items { get; } = new List<CheckListItemModel>();
    public CheckListItemModel[] Items { get; set; } = Array.Empty<CheckListItemModel>();

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
