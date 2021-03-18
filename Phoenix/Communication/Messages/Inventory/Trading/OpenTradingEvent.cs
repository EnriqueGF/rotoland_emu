using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Inventory.Trading
{
	internal sealed class OpenTradingEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null)
			{
				if (!@class.Boolean_2)
				{
					Session.GetHabbo().method_28(PhoenixEnvironment.smethod_1("trade_error_roomdisabled"));
				}
				else
				{
					RoomUser class2 = @class.method_53(Session.GetHabbo().Id);
					RoomUser class3 = @class.method_52(Event.PopWiredInt32());
					if (class2 != null && class3 != null && class3.GetClient().GetHabbo().bool_2)
					{
						@class.method_77(class2, class3);
					}
					else
					{
						Session.GetHabbo().method_28(PhoenixEnvironment.smethod_1("trade_error_targetdisabled"));
					}
				}
			}
		}
	}
}
