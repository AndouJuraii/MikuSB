using MikuSB.Proto;
using System.Text.Json.Nodes;

namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

[CallGSApi("BossPvpLogic_LevelSettlement")]
public class BossPvpLogic_LevelSettlement : ICallGSHandler
{
    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        // Parse the incoming param to get nID
        var paramObj = JsonNode.Parse(param)?.AsObject();
        var innerParam = paramObj?["tbParam"]?.AsObject();
        var nID = innerParam?["nID"]?.GetValue<int>() ?? 0;

        // Settlement success response
        var rsp = new JsonObject
        {
            ["Error"] = 0
        };

        // Force ChallengeNum (sid=1) = 8 so attempts never run out
        // gid=0 is the BossPvp attribute group
        var attr = new JsonObject
        {
            ["gid"] = 0,
            ["sid"] = 1,
            ["val"] = 8
        };

        // Send the attribute update FIRST so the client sees full attempts
        await CallGSRouter.SendScript(
            connection,
            "NTF_SETATTR",
            attr.ToJsonString()
        );

        // Then send the settlement result
        await CallGSRouter.SendScript(
            connection,
            "BossPvpLogic_LevelSettlement",
            rsp.ToJsonString()
        );
    }
}