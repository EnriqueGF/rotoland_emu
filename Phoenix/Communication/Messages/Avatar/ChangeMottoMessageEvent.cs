using System;
using Phoenix.HabboHotel.Misc;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Avatar
{
	internal sealed class ChangeMottoMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			string text = Phoenix.FilterString(Event.PopFixedString());
			if (text.Length <= 50 && !(text != ChatCommandHandler.smethod_4(text)) && !(text == Session.GetHabbo().Motto))
			{
				Session.GetHabbo().Motto = text;
				using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
				{
					@class.AddParamWithValue("motto", text);
					@class.ExecuteQuery("UPDATE users SET motto = @motto WHERE Id = '" + Session.GetHabbo().Id + "' LIMIT 1");
				}
				if (Session.GetHabbo().CurrentQuestId == 17u)
				{
					Phoenix.GetGame().GetQuestManager().method_1(17u, Session);
				}
				ServerMessage Message = new ServerMessage(484u);
				Message.AppendInt32(-1);
				Message.AppendStringWithBreak(Session.GetHabbo().Motto);
				Session.SendMessage(Message);
				if (Session.GetHabbo().Boolean_0)
				{
					Room class14_ = Session.GetHabbo().Class14_0;
					if (class14_ == null)
					{
						return;
					}
					RoomUser class2 = class14_.method_53(Session.GetHabbo().Id);
					if (class2 == null)
					{
						return;
					}
					ServerMessage Message2 = new ServerMessage(266u);
					Message2.AppendInt32(class2.VirtualId);
					Message2.AppendStringWithBreak(Session.GetHabbo().Figure);
					Message2.AppendStringWithBreak(Session.GetHabbo().Gender.ToLower()); 
					Message2.AppendStringWithBreak(Session.GetHabbo().Motto);
					Message2.AppendInt32(Session.GetHabbo().AchievementScore);
					Message2.AppendStringWithBreak("");
					class14_.SendMessage(Message2, null);
				}
				Phoenix.GetGame().GetAchievementManager().addAchievement(Session, 5u, 1);
			}
		}
	}
}
