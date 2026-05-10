using MikuSB.Proto;
using System.Text.Json.Nodes;

namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

[CallGSApi("BossPvpLogic_GetProgress")]
public class BossPvpLogic_GetProgress : ICallGSHandler
{
    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        var rsp = new JsonObject
        {
            ["nChallengeNum"] = 8,   // Current attempts remaining
            ["nMaxChallengeNum"] = 8, // Max attempts
            ["nSeasonID"] = 1,
            ["Error"] = 0
        };

        await CallGSRouter.SendScript(
            connection,
            "BossPvpLogic_GetProgress",
            rsp.ToJsonString()
        );
    }
}