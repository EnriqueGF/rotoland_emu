using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Action
{
	internal sealed class RemoveAllRightsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				foreach (uint current in @class.list_1)
				{
					RoomUser class2 = @class.method_53(current);
					if (class2 != null && !class2.Boolean_4)
					{
						class2.GetClient().SendMessage(new ServerMessage(43u));
					}
					ServerMessage Message = new ServerMessage(511u);
					Message.AppendUInt(@class.Id);
					Message.AppendUInt(current);
					Session.SendMessage(Message);
				}
				using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
				{
					class3.ExecuteQuery("DELETE FROM room_rights WHERE room_id = '" + @class.Id + "'");
				}
				@class.list_1.Clear();
			}
		}
	}
}
