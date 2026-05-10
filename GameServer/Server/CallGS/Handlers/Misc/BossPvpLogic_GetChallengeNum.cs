using MikuSB.Proto;
using System.Text.Json.Nodes;

namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

[CallGSApi("BossPvpLogic_GetChallengeNum")]
public class BossPvpLogic_GetChallengeNum : ICallGSHandler
{
    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        // Force attempts = 8 whenever the client asks
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

        // Also respond to the actual request
        var rsp = new JsonObject
        {
            ["nNum"] = 8,
            ["nMaxNum"] = 8
        };

        await CallGSRouter.SendScript(
            connection,
            "BossPvpLogic_GetChallengeNum",
            rsp.ToJsonString()
        );
    }
}