using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Support;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class GetCfhChatlogMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				SupportTicket @class = Phoenix.GetGame().GetModerationTool().method_5(Event.PopWiredUInt());
				if (@class != null)
				{
                    RoomData class2 = Phoenix.GetGame().GetRoomManager().method_11(@class.RoomId);
					if (class2 != null)
					{
                        Session.SendMessage(Phoenix.GetGame().GetModerationTool().method_21(@class, class2, @class.Timestamp));
					}
				}
			}
		}
	}
}
