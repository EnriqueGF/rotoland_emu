using System;
using System.Collections.Generic;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Pets;
using Phoenix.Util;
using Phoenix.Messages;
using Phoenix.HabboHotel.RoomBots;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Pets
{
	internal sealed class PlacePetMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && (@class.AllowPet || @class.method_27(Session, true)))
			{
				uint uint_ = Event.PopWiredUInt();
				Pet class2 = Session.GetHabbo().method_23().method_4(uint_);
				if (class2 != null && !class2.PlacedInRoom)
				{
					int num = Event.PopWiredInt32();
					int num2 = Event.PopWiredInt32();
					if (@class.method_30(num, num2, 0.0, true, false))
					{
						if (@class.Int32_2 >= LicenseTools.int_2)
						{
							Session.SendNotif(PhoenixEnvironment.smethod_1("error_maxpets") + LicenseTools.int_2);
						}
						else
						{
							class2.PlacedInRoom = true;
							class2.RoomId = @class.Id;
							List<RandomSpeech> list = new List<RandomSpeech>();
							List<BotResponse> list2 = new List<BotResponse>();
							@class.method_4(new RoomBot(class2.PetId, class2.RoomId, AIType.const_0, "freeroam", class2.Name, "", class2.Look, num, num2, 0, 0, 0, 0, 0, 0, ref list, ref list2, 0), class2);
							if (@class.method_27(Session, true))
							{
								Session.GetHabbo().method_23().method_6(class2.PetId, @class.Id);
							}
						}
					}
				}
			}
		}
	}
}
