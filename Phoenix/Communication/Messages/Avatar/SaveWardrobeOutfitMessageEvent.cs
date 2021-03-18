using System;
using Phoenix.HabboHotel.Misc;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Avatar
{
	internal sealed class SaveWardrobeOutfitMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint num = Event.PopWiredUInt();
			string text = Event.PopFixedString();
			string text2 = Event.PopFixedString();
			//if (AntiMutant.smethod_0(text, text2))
			{
				using (DatabaseClient dbClient = Phoenix.GetDatabase().GetClient())
				{
					dbClient.AddParamWithValue("userid", Session.GetHabbo().Id);
					dbClient.AddParamWithValue("slotid", num);
					dbClient.AddParamWithValue("look", text);
					dbClient.AddParamWithValue("gender", text2.ToUpper());
					if (dbClient.ReadDataRow("SELECT null FROM user_wardrobe WHERE user_id = @userid AND slot_id = @slotid LIMIT 1") != null)
					{
						dbClient.ExecuteQuery("UPDATE user_wardrobe SET look = @look, gender = @gender WHERE user_id = @userid AND slot_id = @slotid LIMIT 1;");
					}
					else
					{
						dbClient.ExecuteQuery("INSERT INTO user_wardrobe (user_id,slot_id,look,gender) VALUES (@userid,@slotid,@look,@gender)");
					}
				}
			}
		}
	}
}
