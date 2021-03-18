using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Messenger
{
	internal sealed class RequestBuddyMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().GetMessenger() != null)
			{
				if (Session.GetHabbo().CurrentQuestId == 4u)
				{
					Phoenix.GetGame().GetQuestManager().method_1(4u, Session);
				}
				Session.GetHabbo().GetMessenger().method_16(Event.PopFixedString());
			}
		}
	}
}
