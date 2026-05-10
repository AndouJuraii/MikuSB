using MikuSB.Proto;
using System.Text.Json.Nodes;

namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

[CallGSApi("Chapter_DealLevelSettlement")]
public class Chapter_DealLevelSettlement : ICallGSHandler
{
    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        var paramObj = JsonNode.Parse(param)?.AsObject();
        var sCmd = paramObj?["sCmd"]?.GetValue<string>() ?? "";

        if (sCmd == "BossPvpLogic_LevelSettlement")
        {
            // Force ChallengeNum back to 8 so attempts never deplete
            var attr = new JsonObject
            {
                ["gid"] = 0,
                ["sid"] = 1,
                ["val"] = 8
            };

            await CallGSRouter.SendScript(
                connection,
                "NTF_SETATTR",
                attr.ToJsonString()
            );

            var rsp = new JsonObject
            {
                ["Error"] = 0
            };

            await CallGSRouter.SendScript(
                connection,
                "BossPvpLogic_LevelSettlement",
                rsp.ToJsonString()
            );
        }
    }
}