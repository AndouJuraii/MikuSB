using MikuSB.Util;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MikuSB.GameServer.Server.CallGS.Handlers.House;

[CallGSApi("House_Request")]
public class House_Request : ICallGSHandler
{
    private static readonly Logger Logger = new("House_Request");
    private static readonly Dictionary<string, IHouseFuncHandler> Handlers = [];

    static House_Request()
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            foreach (var attr in type.GetCustomAttributes<HouseFuncAttribute>())
                Handlers[attr.FuncName] = (IHouseFuncHandler)Activator.CreateInstance(type)!;
        }
    }

    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        var req = JsonSerializer.Deserialize<HouseRequestParam>(param);
        var root = HouseJson.ParseObject(param);
        if (root == null) return;

        if (!string.IsNullOrEmpty(req?.FuncName))
        {
            if (Handlers.TryGetValue(req.FuncName, out var handler))
            {
                await handler.Handle(connection, param);
                return;
            }

            Logger.Warn($"Unknown House_Request FuncName: {req.FuncName}. Sending default response.");
        }

        await CallGSRouter.SendScript(connection, "House_Request", HouseRequestScript.Synthesize(root));
    }
}

internal sealed class HouseRequestParam
{
    [JsonPropertyName("FuncName")]
    public string? FuncName { get; set; }
}
