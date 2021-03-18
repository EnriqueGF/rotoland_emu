using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Inventory.Trading
{
	internal sealed class AcceptTradingEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.Boolean_2)
			{
				Trade class2 = @class.method_76(Session.GetHabbo().Id);
				if (class2 != null)
				{
					class2.method_4(Session.GetHabbo().Id);
				}
			}
		}
	}
}
