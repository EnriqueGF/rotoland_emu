using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Furniture
{
	internal sealed class UseFurnitureMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				if (@class != null)
				{
					RoomItem class2 = @class.method_28(Event.PopWiredUInt());
					if (class2 != null)
					{
						bool bool_ = false;
						if (@class.method_26(Session))
						{
							bool_ = true;
						}
						class2.Class69_0.OnTrigger(Session, class2, Event.PopWiredInt32(), bool_);
						if (Session.GetHabbo().CurrentQuestId == 12u)
						{
							Phoenix.GetGame().GetQuestManager().method_1(12u, Session);
						}
						else
						{
							if (Session.GetHabbo().CurrentQuestId == 18u && class2.GetBaseItem().Name == "bw_lgchair")
							{
								Phoenix.GetGame().GetQuestManager().method_1(18u, Session);
							}
							else
							{
								if (Session.GetHabbo().CurrentQuestId == 20u && class2.GetBaseItem().Name.Contains("bw_sboard"))
								{
									Phoenix.GetGame().GetQuestManager().method_1(20u, Session);
								}
								else
								{
									if (Session.GetHabbo().CurrentQuestId == 21u && class2.GetBaseItem().Name.Contains("bw_van"))
									{
										Phoenix.GetGame().GetQuestManager().method_1(21u, Session);
									}
									else
									{
										if (Session.GetHabbo().CurrentQuestId == 22u && class2.GetBaseItem().Name.Contains("party_floor"))
										{
											Phoenix.GetGame().GetQuestManager().method_1(22u, Session);
										}
										else
										{
											if (Session.GetHabbo().CurrentQuestId == 23u && class2.GetBaseItem().Name.Contains("party_ball"))
											{
												Phoenix.GetGame().GetQuestManager().method_1(23u, Session);
											}
											else
											{
												if (Session.GetHabbo().CurrentQuestId == 24u && class2.GetBaseItem().Name.Contains("jukebox"))
												{
													Phoenix.GetGame().GetQuestManager().method_1(24u, Session);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
