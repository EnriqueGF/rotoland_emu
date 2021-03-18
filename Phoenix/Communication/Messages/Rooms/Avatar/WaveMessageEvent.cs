using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Avatar
{
	internal sealed class WaveMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null)
			{
				RoomUser class2 = @class.method_53(Session.GetHabbo().Id);
				if (class2 != null)
				{
					class2.method_0();
					class2.int_15 = 0;
					ServerMessage Message = new ServerMessage(481u);
					Message.AppendInt32(class2.VirtualId);
					@class.SendMessage(Message, null);
					if (Session.GetHabbo().CurrentQuestId == 8u)
					{
						Phoenix.GetGame().GetQuestManager().method_1(8u, Session);
					}
				}
			}
		}
	}
}
