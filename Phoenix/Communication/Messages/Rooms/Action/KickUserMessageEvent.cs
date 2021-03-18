using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Action
{
	internal sealed class KickUserMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_26(Session))
			{
				uint uint_ = Event.PopWiredUInt();
				RoomUser class2 = @class.method_53(uint_);
				if (class2 != null && !class2.Boolean_4 && (!@class.method_27(class2.GetClient(), true) && !class2.GetClient().GetHabbo().HasFuse("acc_unkickable")))
				{
					@class.method_78(Session.GetHabbo().Id);
					@class.method_47(class2.GetClient(), true, true);
				}
			}
		}
	}
}
