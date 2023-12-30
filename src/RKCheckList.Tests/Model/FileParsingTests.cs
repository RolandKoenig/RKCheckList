using RKCheckList.Model;

namespace RKCheckList.Tests.Model;

public class FileParsingTests
{
    [Fact]
    public async Task Parse_AllLowercase()
    {
        // Arrange
        var sampleFileContent = """
                                items:
                                 - text: "item1"
                                 - text: "item2"
                                 - text: "item3"
                                """;
        var textReader = new StringReader(sampleFileContent);

        // Act
        var parsedModel = await CheckListModel.FromYamlAsync(textReader);

        // Assert
        Assert.NotNull(parsedModel);
        Assert.Equal(3, parsedModel.Items.Length);
        Assert.Equal("item1", parsedModel.Items[0].Text);
        Assert.Equal("item2", parsedModel.Items[1].Text);
        Assert.Equal("item3", parsedModel.Items[2].Text);
    }
}
