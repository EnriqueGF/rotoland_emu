using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.RoomBots;
using Phoenix.HabboHotel.Rooms;
using Phoenix.HabboHotel.Pathfinding;
namespace Phoenix.Communication.Messages.Rooms.Action
{
	internal sealed class CallGuideBotMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				for (int i = 0; i < @class.RoomUser_0.Length; i++)
				{
					RoomUser class2 = @class.RoomUser_0[i];
					if (class2 != null && (class2.Boolean_4 && class2.class34_0.AiType == AIType.const_1))
					{
						ServerMessage Message = new ServerMessage(33u);
						Message.AppendInt32(4009);
						Session.SendMessage(Message);
						return;
					}
				}
				if (Session.GetHabbo().bool_10)
				{
					ServerMessage Message = new ServerMessage(33u);
					Message.AppendInt32(4010);
					Session.SendMessage(Message);
				}
				else
				{
					RoomUser class3 = @class.method_3(Phoenix.GetGame().GetBotManager().method_3(2u));
					class3.method_7(@class.Class28_0.int_0, @class.Class28_0.int_1, @class.Class28_0.double_0);
					class3.bool_7 = true;
					RoomUser class4 = @class.method_56(@class.Owner);
					if (class4 != null)
					{
						class3.method_4(class4.GStruct1_0);
						class3.method_9(Class107.smethod_0(class3.int_3, class3.int_4, class4.int_3, class4.int_4));
					}
					Phoenix.GetGame().GetAchievementManager().addAchievement(Session, 6u, 1);
					Session.GetHabbo().bool_10 = true;
				}
			}
		}
	}
}
