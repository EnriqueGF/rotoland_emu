using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Items;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Marketplace
{
	internal sealed class MakeOfferMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().method_23() != null)
			{
				if (Session.GetHabbo().Boolean_0)
				{
					Room class14_ = Session.GetHabbo().Class14_0;
					RoomUser @class = class14_.method_53(Session.GetHabbo().Id);
					if (@class.Boolean_3)
					{
						return;
					}
				}
				int int_ = Event.PopWiredInt32();
				Event.PopWiredInt32();
				uint uint_ = Event.PopWiredUInt();
				UserItem class2 = Session.GetHabbo().method_23().method_10(uint_);
				if (class2 != null && class2.method_1().AllowTrade)
				{
					Phoenix.GetGame().GetCatalog().method_22().method_1(Session, class2.uint_0, int_);
				}
			}
		}
	}
}
