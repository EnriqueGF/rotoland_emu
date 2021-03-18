using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Users.Messenger;
namespace Phoenix.Communication.Messages.Messenger
{
	internal sealed class AcceptBuddyMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().GetMessenger() != null)
			{
				int num = Event.PopWiredInt32();
				for (int i = 0; i < num; i++)
				{
					uint uint_ = Event.PopWiredUInt();
					MessengerRequest @class = Session.GetHabbo().GetMessenger().method_4(uint_);
					if (@class != null)
					{
						if (@class.To != Session.GetHabbo().Id)
						{
							break;
						}
						if (!Session.GetHabbo().GetMessenger().method_9(@class.To, @class.From))
						{
							Session.GetHabbo().GetMessenger().method_12(@class.From);
						}
						Session.GetHabbo().GetMessenger().method_11(uint_);
					}
				}
			}
		}
	}
}
