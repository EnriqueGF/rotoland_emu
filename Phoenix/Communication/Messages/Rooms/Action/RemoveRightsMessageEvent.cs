using System;
using System.Text;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Action
{
	internal sealed class RemoveRightsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = Event.PopWiredInt32();
				for (int i = 0; i < num; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(" OR ");
					}
					uint num2 = Event.PopWiredUInt();
					@class.list_1.Remove(num2);
					stringBuilder.Append(string.Concat(new object[]
					{
						"room_id = '",
						@class.Id,
						"' AND user_id = '",
						num2,
						"'"
					}));
					RoomUser class2 = @class.method_53(num2);
					if (class2 != null && !class2.Boolean_4)
					{
						class2.GetClient().SendMessage(new ServerMessage(43u));
						class2.method_12("flatctrl");
						class2.bool_7 = true;
					}
					ServerMessage Message = new ServerMessage(511u);
					Message.AppendUInt(@class.Id);
					Message.AppendUInt(num2);
					Session.SendMessage(Message);
				}
				using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
				{
					class3.ExecuteQuery("DELETE FROM room_rights WHERE " + stringBuilder.ToString());
				}
			}
		}
	}
}
