using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Pets
{
	internal sealed class RespectPetMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && !@class.Boolean_3)
			{
				uint uint_ = Event.PopWiredUInt();
				RoomUser class2 = @class.method_48(uint_);
				if (class2 != null && class2.PetData != null && Session.GetHabbo().int_22 > 0)
				{
					class2.PetData.OnRespect();
					Session.GetHabbo().int_22--;
					using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
					{
						class3.AddParamWithValue("userid", Session.GetHabbo().Id);
						class3.ExecuteQuery("UPDATE user_stats SET dailypetrespectpoints = dailypetrespectpoints - 1 WHERE Id = @userid LIMIT 1");
					}
				}
			}
		}
	}
}
