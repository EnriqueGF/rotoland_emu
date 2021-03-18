using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Users
{
	internal sealed class GetUserTagsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null)
			{
				RoomUser class2 = @class.method_53(Event.PopWiredUInt());
				if (class2 != null && !class2.Boolean_4)
				{
					ServerMessage Message = new ServerMessage(350u);
					Message.AppendUInt(class2.GetClient().GetHabbo().Id);
					Message.AppendInt32(class2.GetClient().GetHabbo().list_3.Count);
					using (TimedLock.Lock(class2.GetClient().GetHabbo().list_3))
					{
						foreach (string current in class2.GetClient().GetHabbo().list_3)
						{
							Message.AppendStringWithBreak(current);
						}
					}
					Session.SendMessage(Message);
				}
			}
		}
	}
}
