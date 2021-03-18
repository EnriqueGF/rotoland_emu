using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Quest
{
	internal sealed class OpenQuestTrackerMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Phoenix.GetGame().GetQuestManager().method_4(Session);
		}
	}
}
