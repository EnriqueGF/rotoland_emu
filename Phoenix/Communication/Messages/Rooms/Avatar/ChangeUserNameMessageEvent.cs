using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Phoenix.HabboHotel.Misc;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Messages;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Avatar
{
	internal sealed class ChangeUserNameMessageEvent : Interface
	{
		[CompilerGenerated]
		private static Func<Room, int> func_0;
		public void Handle(GameClient Session, ClientMessage Event)
		{
			string text = Phoenix.DoFilter(Event.PopFixedString(), false, true);
			if (text.Length < 3)
			{
				ServerMessage Message = new ServerMessage(571u);
				Message.AppendString("J");
				Session.SendMessage(Message);
			}
			else
			{
				if (text.Length > 15)
				{
					ServerMessage Message = new ServerMessage(571u);
					Message.AppendString("K");
					Session.SendMessage(Message);
				}
				else
				{
					if (text.Contains(" ") || !Session.method_1().method_8(text) || text != ChatCommandHandler.smethod_4(text))
					{
						ServerMessage Message = new ServerMessage(571u);
						Message.AppendString("QA");
						Session.SendMessage(Message);
					}
					else
					{
						if (Event.Header == "GW")
						{
							ServerMessage Message = new ServerMessage(571u);
							Message.AppendString("H");
							Message.AppendString(text);
							Session.SendMessage(Message);
						}
						else
						{
							if (Event.Header == "GV")
							{
								ServerMessage Message2 = new ServerMessage(570u);
								Message2.AppendString("H");
								Session.SendMessage(Message2);
								ServerMessage Message3 = new ServerMessage(572u);
								Message3.AppendUInt(Session.GetHabbo().Id);
								Message3.AppendString("H");
								Message3.AppendString(text);
								Session.SendMessage(Message3);
								if (Session.GetHabbo().CurrentRoomId > 0u)
								{
									Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
									RoomUser class2 = @class.method_53(Session.GetHabbo().Id);
									ServerMessage Message4 = new ServerMessage(28u);
									Message4.AppendInt32(1);
									class2.method_14(Message4);
									@class.SendMessage(Message4, null);
								}
								Dictionary<Room, int> dictionary = Phoenix.GetGame().GetRoomManager().method_22();
								IEnumerable<Room> arg_204_0 = dictionary.Keys;
								if (ChangeUserNameMessageEvent.func_0 == null)
								{
									ChangeUserNameMessageEvent.func_0 = new Func<Room, int>(ChangeUserNameMessageEvent.smethod_0);
								}
								IOrderedEnumerable<Room> orderedEnumerable = arg_204_0.OrderByDescending(ChangeUserNameMessageEvent.func_0);
								foreach (Room current in orderedEnumerable)
								{
									if (current.Owner == Session.GetHabbo().Username)
									{
										current.Owner = text;
										Phoenix.GetGame().GetRoomManager().method_16(Phoenix.GetGame().GetRoomManager().GetRoom(current.Id));
									}
								}
								using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
								{
									class3.ExecuteQuery(string.Concat(new string[]
									{
										"UPDATE rooms SET owner = '",
										text,
										"' WHERE owner = '",
										Session.GetHabbo().Username,
										"'"
									}));
									class3.ExecuteQuery(string.Concat(new object[]
									{
										"UPDATE users SET username = '",
										text,
										"' WHERE Id = '",
										Session.GetHabbo().Id,
										"' LIMIT 1"
									}));
									Phoenix.GetGame().GetClientManager().method_31(Session, "flagme", "OldName: " + Session.GetHabbo().Username + " NewName: " + text);
									Session.GetHabbo().Username = text;
									Session.GetHabbo().method_1(class3);
                                    foreach (RoomData current2 in Session.GetHabbo().list_6)
									{
										current2.Owner = text;
									}
								}
								Phoenix.GetGame().GetAchievementManager().addAchievement(Session, 9u, 1);
							}
						}
					}
				}
			}
		}
		[CompilerGenerated]
		private static int smethod_0(Room class14_0)
		{
			return class14_0.Int32_0;
		}
	}
}
