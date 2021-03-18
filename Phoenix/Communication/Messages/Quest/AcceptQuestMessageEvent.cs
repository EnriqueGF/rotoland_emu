using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Quest
{
	internal sealed class AcceptQuestMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint uint_ = Event.PopWiredUInt();
			Phoenix.GetGame().GetQuestManager().method_7(uint_, Session);
		}
	}
}
