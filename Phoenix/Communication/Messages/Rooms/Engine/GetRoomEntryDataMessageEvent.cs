using System;
using System.Collections;
using System.Collections.Generic;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
using Phoenix.HabboHotel.Items;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class GetRoomEntryDataMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().uint_2 > 0u && Session.GetHabbo().bool_5)
			{
                RoomData @class = Phoenix.GetGame().GetRoomManager().method_12(Session.GetHabbo().uint_2);
				if (@class != null)
				{
					if (@class.Model == null)
					{
						Session.SendNotif("Error loading room, please try again soon! (Error Code: MdlData)");
						Session.SendMessage(new ServerMessage(18u));
						Session.method_1().method_7();
					}
					else
					{
						Session.SendMessage(@class.Model.method_1());
						Session.SendMessage(@class.Model.method_2());
						Room class2 = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().uint_2);
						if (class2 != null)
						{
							Session.method_1().method_7();
							ServerMessage Message = new ServerMessage(30u);
							if (class2.Class28_0.string_2 != "")
							{
								Message.AppendStringWithBreak(class2.Class28_0.string_2);
							}
							else
							{
								Message.AppendInt32(0);
							}
							Session.SendMessage(Message);
							if (class2.Type == "private")
							{
								Hashtable hashtable_ = class2.Hashtable_0;
								Hashtable hashtable_2 = class2.Hashtable_1;
								ServerMessage Message2 = new ServerMessage(32u);
								Message2.AppendInt32(hashtable_.Count);
								foreach (RoomItem class3 in hashtable_.Values)
								{
									class3.method_6(Message2);
								}
								Session.SendMessage(Message2);
								ServerMessage Message3 = new ServerMessage(45u);
								Message3.AppendInt32(hashtable_2.Count);
								foreach (RoomItem class3 in hashtable_2.Values)
								{
									class3.method_6(Message3);
								}
								Session.SendMessage(Message3);
							}
							class2.method_46(Session, Session.GetHabbo().bool_8);
							List<RoomUser> list = new List<RoomUser>();
							for (int i = 0; i < class2.RoomUser_0.Length; i++)
							{
								RoomUser class4 = class2.RoomUser_0[i];
								if (class4 != null && (!class4.bool_11 && class4.bool_12))
								{
									list.Add(class4);
								}
							}
							ServerMessage Message4 = new ServerMessage(28u);
							Message4.AppendInt32(list.Count);
							foreach (RoomUser class4 in list)
							{
								class4.method_14(Message4);
							}
							Session.SendMessage(Message4);
							ServerMessage Message5 = new ServerMessage(472u);
							Message5.AppendBoolean(class2.Hidewall);
							Message5.AppendInt32(class2.Wallthick);
							Message5.AppendInt32(class2.Floorthick);
							Session.SendMessage(Message5);
							if (class2.Type == "public")
							{
								ServerMessage Message6 = new ServerMessage(471u);
								Message6.AppendBoolean(false);
								Message6.AppendStringWithBreak(class2.ModelName);
								Message6.AppendBoolean(false);
								Session.SendMessage(Message6);
							}
							else
							{
								if (class2.Type == "private")
								{
									ServerMessage Message6 = new ServerMessage(471u);
									Message6.AppendBoolean(true);
									Message6.AppendUInt(class2.Id);
									if (class2.method_27(Session, true))
									{
										Message6.AppendBoolean(true);
									}
									else
									{
										Message6.AppendBoolean(false);
									}
									Session.SendMessage(Message6);
									ServerMessage Message7 = new ServerMessage(454u);
									Message7.AppendBoolean(false);
									@class.method_3(Message7, false, false);
									Session.SendMessage(Message7);
								}
							}
							ServerMessage Message8 = class2.method_67(true);
							if (Message8 != null)
							{
								Session.SendMessage(Message8);
							}
							for (int i = 0; i < class2.RoomUser_0.Length; i++)
							{
								RoomUser class4 = class2.RoomUser_0[i];
								if (class4 != null && !class4.bool_11)
								{
									if (class4.Boolean_1)
									{
										ServerMessage Message9 = new ServerMessage(480u);
										Message9.AppendInt32(class4.VirtualId);
										Message9.AppendInt32(class4.int_15);
										Session.SendMessage(Message9);
									}
									if (class4.bool_8)
									{
										ServerMessage Message10 = new ServerMessage(486u);
										Message10.AppendInt32(class4.VirtualId);
										Message10.AppendBoolean(true);
										Session.SendMessage(Message10);
									}
									if (class4.int_5 > 0 && class4.int_6 > 0)
									{
										ServerMessage Message11 = new ServerMessage(482u);
										Message11.AppendInt32(class4.VirtualId);
										Message11.AppendInt32(class4.int_5);
										Session.SendMessage(Message11);
									}
									if (!class4.Boolean_4)
									{
										try
										{
											if (class4.GetClient().GetHabbo() != null && class4.GetClient().GetHabbo().method_24() != null && class4.GetClient().GetHabbo().method_24().int_0 >= 1)
											{
												ServerMessage Message12 = new ServerMessage(485u);
												Message12.AppendInt32(class4.VirtualId);
												Message12.AppendInt32(class4.GetClient().GetHabbo().method_24().int_0);
												Session.SendMessage(Message12);
											}
											goto IL_5C5;
										}
										catch
										{
											goto IL_5C5;
										}
									}
									if (!class4.isPet && class4.class34_0.EffectId != 0)
									{
										ServerMessage Message12 = new ServerMessage(485u);
										Message12.AppendInt32(class4.VirtualId);
										Message12.AppendInt32(class4.class34_0.EffectId);
										Session.SendMessage(Message12);
									}
								}
								IL_5C5:;
							}
							if (class2 != null && Session != null && Session.GetHabbo().Class14_0 != null)
							{
								class2.method_8(Session.GetHabbo().Class14_0.method_53(Session.GetHabbo().Id));
							}
							if (class2.Achievement > 0u)
							{
								Phoenix.GetGame().GetAchievementManager().addAchievement(Session, class2.Achievement, 1);
							}
							if (Session.GetHabbo().bool_3 && Session.GetHabbo().int_4 > 0)
							{
								ServerMessage Message13 = new ServerMessage(27u);
								Message13.AppendInt32(Session.GetHabbo().int_4);
								Session.SendMessage(Message13);
							}
						}
					}
				}
			}
		}
	}
}
