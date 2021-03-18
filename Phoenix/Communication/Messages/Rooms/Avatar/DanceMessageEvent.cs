using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Avatar
{
	internal sealed class DanceMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			//if (user != null)
			{
				RoomUser class2 = @class.method_53(Session.GetHabbo().Id);
				//if (class2 != null)
				{
				    class2.method_0();
					int num = Event.PopWiredInt32();

					if (num < 0 || num > 4 || (!Session.GetHabbo().method_20().method_2("habbo_club") && num > 1))
					{
						num = 0;
					}
					if (num > 0 && class2.int_5 > 0)
					{
						class2.method_8(0);
					}
					class2.int_15 = num;
					ServerMessage Message = new ServerMessage(480u);
					Message.AppendInt32(class2.VirtualId);
					Message.AppendInt32(num);
					@class.SendMessage(Message, null);
					
                    if (Session.GetHabbo().CurrentQuestId == 6u)
					{
						Phoenix.GetGame().GetQuestManager().method_1(6u, Session);
					}
				}
			}
		}
	}
}
