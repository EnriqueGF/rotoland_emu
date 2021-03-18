using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Items;
using Phoenix.Messages;
using Phoenix.HabboHotel.Catalogs;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Recycler
{
	internal sealed class RecycleItemsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().Boolean_0)
			{
				int num = Event.PopWiredInt32();
				if (num == 5)
				{
					for (int i = 0; i < num; i++)
					{
						UserItem @class = Session.GetHabbo().method_23().method_10(Event.PopWiredUInt());
						if (@class == null || !@class.method_1().AllowRecycle)
						{
							return;
						}
						Session.GetHabbo().method_23().method_12(@class.uint_0, 0u, false);
					}
					uint num2 = Phoenix.GetGame().GetCatalog().method_14();
					EcotronReward class2 = Phoenix.GetGame().GetCatalog().method_15();
					using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
					{
						class3.ExecuteQuery(string.Concat(new object[]
						{
							"INSERT INTO items (Id,user_id,base_item,extra_data,wall_pos) VALUES ('",
							num2,
							"','",
							Session.GetHabbo().Id,
							"','1478','",
							DateTime.Now.ToLongDateString(),
							"', '')"
						}));
						class3.ExecuteQuery(string.Concat(new object[]
						{
							"INSERT INTO user_presents (item_id,base_id,amount,extra_data) VALUES ('",
							num2,
							"','",
							class2.uint_2,
							"','1','')"
						}));
					}
					Session.GetHabbo().method_23().method_9(true);
					ServerMessage Message = new ServerMessage(508u);
					Message.AppendBoolean(true);
					Message.AppendUInt(num2);
					Session.SendMessage(Message);
				}
			}
		}
	}
}
