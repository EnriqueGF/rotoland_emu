using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Register
{
	internal sealed class UpdateFigureDataMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			string text = Event.PopFixedString().ToUpper();
			string text2 = Phoenix.FilterString(Event.PopFixedString());
			//if (Session.GetRoomUser().isPet && Class98.smethod_0(text2, text))
			{
				Room class14_ = Session.GetHabbo().Class14_0;
				if (class14_ != null)
				{
					RoomUser @class = class14_.method_53(Session.GetHabbo().Id);
					if (@class != null)
					{
						@class.string_0 = "";
						if (Session.GetHabbo().method_4() > 0)
						{
							TimeSpan timeSpan = DateTime.Now - Session.GetHabbo().dateTime_0;
							if (timeSpan.Seconds > 4)
							{
								Session.GetHabbo().int_23 = 0;
							}
							if (timeSpan.Seconds < 4 && Session.GetHabbo().int_23 > 5)
							{
								ServerMessage Message = new ServerMessage(27u);
								Message.AppendInt32(Session.GetHabbo().method_4());
								Session.SendMessage(Message);
								return;
							}
							Session.GetHabbo().dateTime_0 = DateTime.Now;
							Session.GetHabbo().int_23++;
						}
						if (Session.GetHabbo().CurrentQuestId == 2u)
						{
							Phoenix.GetGame().GetQuestManager().method_1(2u, Session);
						}
						Session.GetHabbo().Figure = text2;
						Session.GetHabbo().Gender = text.ToLower();
						using (DatabaseClient class2 = Phoenix.GetDatabase().GetClient())
						{
							class2.AddParamWithValue("look", text2);
							class2.AddParamWithValue("gender", text);
							class2.ExecuteQuery("UPDATE users SET look = @look, gender = @gender WHERE Id = '" + Session.GetHabbo().Id + "' LIMIT 1;");
						}
						ServerMessage Message2 = new ServerMessage(266u);
						Message2.AppendInt32(-1);
						Message2.AppendStringWithBreak(Session.GetHabbo().Figure);
						Message2.AppendStringWithBreak(Session.GetHabbo().Gender.ToLower());
						Message2.AppendStringWithBreak(Session.GetHabbo().Motto);
						Message2.AppendInt32(Session.GetHabbo().AchievementScore);
						Message2.AppendStringWithBreak("");
						Session.SendMessage(Message2);
						ServerMessage Message3 = new ServerMessage(266u);
						Message3.AppendInt32(@class.VirtualId);
						Message3.AppendStringWithBreak(Session.GetHabbo().Figure);
						Message3.AppendStringWithBreak(Session.GetHabbo().Gender.ToLower());
						Message3.AppendStringWithBreak(Session.GetHabbo().Motto);
						Message3.AppendInt32(Session.GetHabbo().AchievementScore);
						Message3.AppendStringWithBreak("");
						class14_.SendMessage(Message3, null);
						Phoenix.GetGame().GetAchievementManager().addAchievement(Session, 1u, 1);
					}
				}
			}
		}
	}
}
