using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Users
{
	internal sealed class RespectUserMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && Session.GetHabbo().int_21 > 0)
			{
				RoomUser class2 = @class.method_53(Event.PopWiredUInt());
				if (class2 != null && class2.GetClient().GetHabbo().Id != Session.GetHabbo().Id && !class2.Boolean_4)
				{
					Session.GetHabbo().int_21--;
					Session.GetHabbo().RespectGiven++;
					class2.GetClient().GetHabbo().Respect++;
					using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
					{
						class3.ExecuteQuery("UPDATE user_stats SET Respect = respect + 1 WHERE Id = '" + class2.GetClient().GetHabbo().Id + "' LIMIT 1");
						class3.ExecuteQuery("UPDATE user_stats SET RespectGiven = RespectGiven + 1 WHERE Id = '" + Session.GetHabbo().Id + "' LIMIT 1");
						class3.ExecuteQuery("UPDATE user_stats SET dailyrespectpoints = dailyrespectpoints - 1 WHERE Id = '" + Session.GetHabbo().Id + "' LIMIT 1");
					}
					ServerMessage Message = new ServerMessage(440u);
					Message.AppendUInt(class2.GetClient().GetHabbo().Id);
					Message.AppendInt32(class2.GetClient().GetHabbo().Respect);
					@class.SendMessage(Message, null);
					if (Session.GetHabbo().RespectGiven == 100)
					{
						Phoenix.GetGame().GetAchievementManager().addAchievement(Session, 8u, 1);
					}
					int int_ = class2.GetClient().GetHabbo().Respect;
					if (int_ <= 166)
					{
						if (int_ <= 6)
						{
							if (int_ != 1)
							{
								if (int_ == 6)
								{
									Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 2);
								}
							}
							else
							{
								Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 1);
							}
						}
						else
						{
							if (int_ != 16)
							{
								if (int_ != 66)
								{
									if (int_ == 166)
									{
										Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 5);
									}
								}
								else
								{
									Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 4);
								}
							}
							else
							{
								Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 3);
							}
						}
					}
					else
					{
						if (int_ <= 566)
						{
							if (int_ != 366)
							{
								if (int_ == 566)
								{
									Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 7);
								}
							}
							else
							{
								Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 6);
							}
						}
						else
						{
							if (int_ != 766)
							{
								if (int_ != 966)
								{
									if (int_ == 1116)
									{
										Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 10);
									}
								}
								else
								{
									Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 9);
								}
							}
							else
							{
								Phoenix.GetGame().GetAchievementManager().addAchievement(class2.GetClient(), 14u, 8);
							}
						}
					}
					if (Session.GetHabbo().CurrentQuestId == 5u)
					{
						Phoenix.GetGame().GetQuestManager().method_1(5u, Session);
					}
				}
			}
		}
	}
}
