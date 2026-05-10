using Newtonsoft.Json;

namespace MikuSB.Data.Excel;

[ResourceEntity("item/templates/support_card.json")]
public class SupportCardExcel : ExcelResource
{
    public uint Genre { get; set; }
    public uint Detail { get; set; }
    public uint Particular { get; set; }
    public uint Level { get; set; }
    public uint Icon { get; set; }
    public uint ProvideExp { get; set; }
    [JsonProperty("LevelLimitID")] public int LevelLimitId { get; set; }

    public uint MaxLevel => LevelLimitId switch
    {
        1007 => 10,
        1008 => 13,
        1009 => 16,
        _ => 10
    };

    public ulong TemplateId => GameResourceTemplateId.FromGdpl(Genre, Detail, Particular, Level);

    public override uint GetId() => Icon;

    public override void Loaded()
    {
        GameData.SupportCardData.Add(this);
    }
}
