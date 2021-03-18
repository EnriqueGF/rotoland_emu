using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Users.Badges;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Users
{
	internal sealed class GetSelectedBadgesMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session != null && Session.GetHabbo() != null)
			{
				Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				if (@class != null)
				{
					RoomUser class2 = @class.method_53(Event.PopWiredUInt());
					if (class2 != null && !class2.Boolean_4 && class2.GetClient() != null)
					{
						ServerMessage Message = new ServerMessage(228u);
						Message.AppendUInt(class2.GetClient().GetHabbo().Id);
						Message.AppendInt32(class2.GetClient().GetHabbo().method_22().Int32_1);
						using (TimedLock.Lock(class2.GetClient().GetHabbo().method_22().List_0))
						{
							foreach (Badge current in class2.GetClient().GetHabbo().method_22().List_0)
							{
								if (current.Slot > 0)
								{
									Message.AppendInt32(current.Slot);
									Message.AppendStringWithBreak(current.Code);
								}
							}
						}
						Session.SendMessage(Message);
					}
				}
			}
		}
	}
}
