using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Furniture
{
	internal sealed class DiceOffMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				if (@class != null)
				{
					RoomItem class2 = @class.method_28(Event.PopWiredUInt());
					if (class2 != null)
					{
						bool bool_ = false;
						if (@class.method_26(Session))
						{
							bool_ = true;
						}
						class2.Class69_0.OnTrigger(Session, class2, -1, bool_);
					}
				}
			}
			catch
			{
			}
		}
	}
}
