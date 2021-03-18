using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Messenger
{
	internal sealed class HabboSearchMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().GetMessenger() != null)
			{
				Session.SendMessage(Session.GetHabbo().GetMessenger().method_24(Event.PopFixedString()));
			}
		}
	}
}
