using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Messenger
{
	internal sealed class RemoveBuddyMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().GetMessenger() != null)
			{
				int num = Event.PopWiredInt32();
				for (int i = 0; i < num; i++)
				{
					Session.GetHabbo().GetMessenger().method_13(Event.PopWiredUInt());
				}
			}
		}
	}
}
