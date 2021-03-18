using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class AddFavouriteRoomMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint num = Event.PopWiredUInt();
            RoomData @class = Phoenix.GetGame().GetRoomManager().method_12(num);
			if (@class == null || Session.GetHabbo().list_1.Count >= 30 || Session.GetHabbo().list_1.Contains(num) || @class.Type == "public")
			{
				ServerMessage Message = new ServerMessage(33u);
				Message.AppendInt32(-9001);
				Session.SendMessage(Message);
			}
			else
			{
				ServerMessage Message2 = new ServerMessage(459u);
				Message2.AppendUInt(num);
				Message2.AppendBoolean(true);
				Session.SendMessage(Message2);
				Session.GetHabbo().list_1.Add(num);
				using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
				{
					class2.ExecuteQuery(string.Concat(new object[]
					{
						"INSERT INTO user_favorites (user_id,room_id) VALUES ('",
						Session.GetHabbo().Id,
						"','",
						num,
						"')"
					}));
				}
			}
		}
	}
}
