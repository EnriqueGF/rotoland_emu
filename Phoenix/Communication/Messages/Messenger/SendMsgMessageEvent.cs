using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Messenger
{
	internal sealed class SendMsgMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint num = Event.PopWiredUInt();
			string text = Phoenix.FilterString(Event.PopFixedString());
			if (Session.GetHabbo().GetMessenger() != null)
			{
				if (num == 0u && Session.GetHabbo().HasFuse("cmd_sa"))
				{
					ServerMessage Message = new ServerMessage(134u);
					Message.AppendUInt(0u);
					Message.AppendString(Session.GetHabbo().Username + ": " + text);
					Phoenix.GetGame().GetClientManager().method_17(Session, Message);
				}
				else
				{
					if (num == 0u)
					{
						ServerMessage Message2 = new ServerMessage(261u);
						Message2.AppendInt32(4);
						Message2.AppendUInt(0u);
						Session.SendMessage(Message2);
					}
					else
					{
						Session.GetHabbo().GetMessenger().method_18(num, text);
					}
				}
			}
		}
	}
}
