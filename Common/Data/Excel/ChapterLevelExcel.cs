namespace MikuSB.Data.Excel;

[ResourceEntity("chapter/level.json")]
public class ChapterLevelExcel : ExcelResource
{
    public uint ID { get; set; }

    public override uint GetId() => ID;

    public override void Loaded()
    {
        GameData.ChapterLevelData.Add(ID, this);
    }
}
