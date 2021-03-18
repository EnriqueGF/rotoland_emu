using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.Storage;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Navigator
{
	internal sealed class RateFlatMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && !Session.GetHabbo().list_4.Contains(@class.Id) && !@class.method_27(Session, true))
			{
				switch (Event.PopWiredInt32())
				{
				case -1:
					@class.Score--;
					break;
				case 0:
					return;
				case 1:
					@class.Score++;
					break;
				default:
					return;
				}
				using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
				{
					class2.ExecuteQuery(string.Concat(new object[]
					{
						"UPDATE rooms SET score = '",
						@class.Score,
						"' WHERE Id = '",
						@class.Id,
						"' LIMIT 1"
					}));
				}
				Session.GetHabbo().list_4.Add(@class.Id);
				ServerMessage Message = new ServerMessage(345u);
				Message.AppendInt32(@class.Score);
				Session.SendMessage(Message);
			}
		}
	}
}
