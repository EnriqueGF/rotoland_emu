using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Chat
{
	internal sealed class CancelTypingMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null)
			{
				RoomUser class2 = @class.method_53(Session.GetHabbo().Id);
				if (class2 != null)
				{
					ServerMessage Message = new ServerMessage(361u);
					Message.AppendInt32(class2.VirtualId);
					Message.AppendBoolean(false);
					@class.SendMessage(Message, null);
				}
			}
		}
	}
}
