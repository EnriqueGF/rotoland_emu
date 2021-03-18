using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Quest
{
	internal sealed class RejectQuestMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Phoenix.GetGame().GetQuestManager().method_7(0u, Session);
		}
	}
}
