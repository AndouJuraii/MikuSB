using MikuSB.Database;
using MikuSB.Database.Account;
using MikuSB.Enums.Player;
using MikuSB.Internationalization;
using System.Text;

namespace MikuSB.GameServer.Command.Commands;

[CommandInfo("account", "Game.Command.Account.Desc", "Game.Command.Account.Usage", [], [PermEnum.Admin, PermEnum.Support])]
public class CommandAccount : ICommands
{
    [CommandMethod("create")]
    public async ValueTask Create(CommandArg arg)
    {
        if (!await arg.CheckArgCnt(2))
            return;

        var email = arg.Args[0].Trim();
        if (!int.TryParse(arg.Args[1], out var uid) || uid <= 0)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        try
        {
            var account = AccountData.CreateAccount(email, uid, "");
            await arg.SendMsg(I18NManager.Translate("Game.Command.Account.Created", account.Username, account.Uid.ToString()));
        }
        catch (InvalidOperationException ex)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Account.CreateFailed", ex.Message));
        }
        catch (ArgumentException ex)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Account.CreateFailed", ex.Message));
        }
    }

    [CommandMethod("list")]
    public async ValueTask List(CommandArg arg)
    {
        var accounts = DatabaseHelper.GetAllInstance<AccountData>()?
            .OrderBy(account => account.Uid)
            .ToList();

        if (accounts == null || accounts.Count == 0)
        {
            await arg.SendMsg("No accounts found.");
            return;
        }

        var builder = new StringBuilder();
        builder.AppendLine("Accounts:");
        foreach (var account in accounts)
            builder.AppendLine($"{account.Username} -> UID {account.Uid}");

        await arg.SendMsg(builder.ToString().TrimEnd());
    }
}
