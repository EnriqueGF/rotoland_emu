using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Pets;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Pets
{
	internal sealed class RemovePetFromFlatMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && !@class.Boolean_3 && (@class.AllowPet || @class.method_27(Session, true)))
			{
				uint uint_ = Event.PopWiredUInt();
				RoomUser class2 = @class.method_48(uint_);
				if (class2 != null && class2.PetData != null && class2.PetData.OwnerId == Session.GetHabbo().Id)
				{
					using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
					{
						if (class2.PetData.DBState == DatabaseUpdateState.NeedsInsert)
						{
							class3.AddParamWithValue("petname" + class2.PetData.PetId, class2.PetData.Name);
							class3.AddParamWithValue("petcolor" + class2.PetData.PetId, class2.PetData.Color);
							class3.AddParamWithValue("petrace" + class2.PetData.PetId, class2.PetData.Race);
							class3.ExecuteQuery(string.Concat(new object[]
							{
								"INSERT INTO `user_pets` VALUES ('",
								class2.PetData.PetId,
								"', '",
								class2.PetData.OwnerId,
								"', '0', @petname",
								class2.PetData.PetId,
								", @petrace",
								class2.PetData.PetId,
								", @petcolor",
								class2.PetData.PetId,
								", '",
								class2.PetData.Type,
								"', '",
								class2.PetData.Expirience,
								"', '",
								class2.PetData.Energy,
								"', '",
								class2.PetData.Nutrition,
								"', '",
								class2.PetData.Respect,
								"', '",
								class2.PetData.CreationStamp,
								"', '",
								class2.PetData.X,
								"', '",
								class2.PetData.Y,
								"', '",
								class2.PetData.Z,
								"');"
							}));
						}
						else
						{
							class3.ExecuteQuery(string.Concat(new object[]
							{
								"UPDATE user_pets SET room_id = '0', expirience = '",
								class2.PetData.Expirience,
								"', energy = '",
								class2.PetData.Energy,
								"', nutrition = '",
								class2.PetData.Nutrition,
								"', respect = '",
								class2.PetData.Respect,
								"' WHERE Id = '",
								class2.PetData.PetId,
								"' LIMIT 1; "
							}));
						}
						class2.PetData.DBState = DatabaseUpdateState.Updated;
					}
					Session.GetHabbo().method_23().method_7(class2.PetData);
					@class.method_6(class2.VirtualId, false);
					class2.uint_1 = 0u;
				}
			}
		}
	}
}
