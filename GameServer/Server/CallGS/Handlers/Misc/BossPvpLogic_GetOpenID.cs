using MikuSB.Proto;
using System.Text.Json.Nodes;

namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

[CallGSApi("BossPvpLogic_GetOpenID")]
public class BossPvpLogic_GetOpenID : ICallGSHandler
{
    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        var rsp = new JsonObject
        {
            ["nID"] = 1,
            ["nChallengeNum"] = 8,      // Current attempts remaining
            ["nMaxChallengeNum"] = 8,   // Max attempts
            ["tbTimeCfg"] = new JsonArray
            {
                new JsonObject
                {
                    ["nStartTime"] = -1,
                    ["nEndTime"] = -1
                }
            }
        };

        await CallGSRouter.SendScript(
            connection,
            "BossPvpLogic_GetOpenID",
            rsp.ToJsonString()
        );
    }
}