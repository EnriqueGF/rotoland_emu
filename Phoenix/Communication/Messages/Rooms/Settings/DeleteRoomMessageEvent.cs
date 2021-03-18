using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Settings
{
	internal sealed class DeleteRoomMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint num = Event.PopWiredUInt();
			Room class14_ = Session.GetHabbo().Class14_0;
			if (class14_ != null && (!(class14_.Owner != Session.GetHabbo().Username) || Session.GetHabbo().Rank == 7u))
			{
				Phoenix.GetGame().GetRoomManager().method_2(num);
                RoomData @class = Phoenix.GetGame().GetRoomManager().method_12(num);
				if (@class != null && (!(@class.Owner.ToLower() != Session.GetHabbo().Username.ToLower()) || Session.GetHabbo().Rank == 7u))
				{
					Room class2 = Phoenix.GetGame().GetRoomManager().GetRoom(@class.Id);
					if (class2 != null)
					{
						for (int i = 0; i < class2.RoomUser_0.Length; i++)
						{
							RoomUser class3 = class2.RoomUser_0[i];
							if (class3 != null && !class3.Boolean_4)
							{
								class3.GetClient().SendMessage(new ServerMessage(18u));
								class3.GetClient().GetHabbo().method_11();
							}
						}
						Phoenix.GetGame().GetRoomManager().method_16(class2);
					}
					using (DatabaseClient class4 = Phoenix.GetDatabase().GetClient())
					{
						class4.ExecuteQuery("DELETE FROM rooms WHERE Id = '" + num + "' LIMIT 1");
						class4.ExecuteQuery("DELETE FROM user_favorites WHERE room_id = '" + num + "'");
						class4.ExecuteQuery("UPDATE items SET room_id = '0' WHERE room_id = '" + num + "'");
						class4.ExecuteQuery("DELETE FROM room_rights WHERE room_id = '" + num + "'");
						class4.ExecuteQuery("UPDATE users SET home_room = '0' WHERE home_room = '" + num + "'");
						class4.ExecuteQuery("UPDATE user_pets SET room_id = '0' WHERE room_id = '" + num + "'");
						Session.GetHabbo().method_1(class4);
					}
					Session.GetHabbo().method_23().method_9(true);
					Session.GetHabbo().method_23().method_3(true);
					Session.SendMessage(Phoenix.GetGame().GetNavigator().method_12(Session, -3));
				}
			}
		}
	}
}
