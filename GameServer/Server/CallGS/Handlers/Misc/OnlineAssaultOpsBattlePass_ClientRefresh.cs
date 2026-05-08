namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

// Client refresh for Online Assault Ops Battle Pass. Stub implementation.
// param: unknown
[CallGSApi("OnlineAssaultOpsBattlePass_ClientRefresh")]
public class OnlineAssaultOpsBattlePass_ClientRefresh : ICallGSHandler
{
    public Task Handle(Connection connection, string param, ushort seqNo)
        => Task.CompletedTask;
}