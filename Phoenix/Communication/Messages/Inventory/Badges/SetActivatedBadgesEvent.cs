using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Users.Badges;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Inventory.Badges
{
	internal sealed class SetActivatedBadgesEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.GetHabbo().method_22().method_5();
			using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
			{
				@class.ExecuteQuery("UPDATE user_badges SET badge_slot = '0' WHERE user_id = '" + Session.GetHabbo().Id + "'");
				goto IL_131;
			}
			IL_52:
			int num = Event.PopWiredInt32();
			string text = Event.PopFixedString();
			if (text.Length != 0)
			{
				if (!Session.GetHabbo().method_22().method_1(text) || num < 1 || num > 5)
				{
					return;
				}
				if (Session.GetHabbo().CurrentQuestId == 16u)
				{
					Phoenix.GetGame().GetQuestManager().method_1(16u, Session);
				}
				Session.GetHabbo().method_22().method_0(text).Slot = num;
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.AddParamWithValue("slotid", num);
					@class.AddParamWithValue("badge", text);
					@class.AddParamWithValue("userid", Session.GetHabbo().Id);
					@class.ExecuteQuery("UPDATE user_badges SET badge_slot = @slotid WHERE badge_id = @badge AND user_id = @userid LIMIT 1");
				}
			}
			IL_131:
			if (Event.RemainingLength > 0)
			{
				goto IL_52;
			}
			ServerMessage Message = new ServerMessage(228u);
			Message.AppendUInt(Session.GetHabbo().Id);
			Message.AppendInt32(Session.GetHabbo().method_22().Int32_1);
			foreach (Badge current in Session.GetHabbo().method_22().List_0)
			{
				if (current.Slot > 0)
				{
					Message.AppendInt32(current.Slot);
					Message.AppendStringWithBreak(current.Code);
				}
			}
			if (Session.GetHabbo().Boolean_0 && Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId) != null)
			{
				Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId).SendMessage(Message, null);
			}
			else
			{
				Session.SendMessage(Message);
			}
		}
	}
}
