using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Action
{
	internal sealed class AssignRightsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint num = Event.PopWiredUInt();
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			RoomUser class2 = @class.method_53(num);
			if (@class != null && @class.method_27(Session, true) && class2 != null && !class2.Boolean_4 && !@class.list_1.Contains(num))
			{
				@class.list_1.Add(num);
				using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
				{
					class3.ExecuteQuery(string.Concat(new object[]
					{
						"INSERT INTO room_rights (room_id,user_id) VALUES ('",
						@class.Id,
						"','",
						num,
						"')"
					}));
				}
				ServerMessage Message = new ServerMessage(510u);
				Message.AppendUInt(@class.Id);
				Message.AppendUInt(num);
				Message.AppendStringWithBreak(class2.GetClient().GetHabbo().Username);
				Session.SendMessage(Message);
				class2.method_11("flatctrl", "");
				class2.bool_7 = true;
				class2.GetClient().SendMessage(new ServerMessage(42u));
			}
		}
	}
}
