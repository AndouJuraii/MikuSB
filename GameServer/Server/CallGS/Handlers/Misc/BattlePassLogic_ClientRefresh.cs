namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

// Client refresh for Battle Pass Logic. Stub implementation.
// param: unknown
[CallGSApi("BattlePassLogic_ClientRefresh")]
public class BattlePassLogic_ClientRefresh : ICallGSHandler
{
    public Task Handle(Connection connection, string param, ushort seqNo)
        => Task.CompletedTask;
}