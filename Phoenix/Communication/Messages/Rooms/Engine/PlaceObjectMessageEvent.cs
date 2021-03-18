using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Util;
using Phoenix.HabboHotel.Items;
using Phoenix.Messages;
using Phoenix.Storage;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class PlaceObjectMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_26(Session) && (LicenseTools.Boolean_1 || !(@class.Owner != Session.GetHabbo().Username)))
			{
				string text = Event.PopFixedString();
				string[] array = text.Split(new char[]
				{
					' '
				});
				if (array[0].Contains("-"))
				{
					array[0] = array[0].Replace("-", "");
				}
				uint uint_ = 0u;
				try
				{
					uint_ = uint.Parse(array[0]);
				}
				catch
				{
					return;
				}
				UserItem class2 = Session.GetHabbo().method_23().method_10(uint_);
				if (class2 != null)
				{
					string text2 = class2.method_1().InteractionType.ToLower();
					if (text2 != null && text2 == "dimmer" && @class.method_72("dimmer") >= 1)
					{
						Session.SendNotif("You can only have one moodlight in a room.");
					}
					else
					{
						RoomItem RoomItem_;
						if (array[1].StartsWith(":"))
						{
							string text3 = @class.method_98(":" + text.Split(new char[]
							{
								':'
							})[1]);
							if (text3 == null)
							{
								ServerMessage Message = new ServerMessage(516u);
								Message.AppendInt32(11);
								Session.SendMessage(Message);
								return;
							}
							RoomItem_ = new RoomItem(class2.uint_0, @class.Id, class2.uint_1, class2.string_0, 0, 0, 0.0, 0, text3, @class);
							if (!@class.method_82(Session, RoomItem_, true, null))
							{
								goto IL_32C;
							}
							Session.GetHabbo().method_23().method_12(uint_, 1u, false);
							using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
							{
								class3.ExecuteQuery(string.Concat(new object[]
								{
									"UPDATE items SET room_id = '",
									@class.Id,
									"' WHERE Id = '",
									class2.uint_0,
									"' LIMIT 1"
								}));
								goto IL_32C;
							}
						}
						int int_ = int.Parse(array[1]);
						int int_2 = int.Parse(array[2]);
						int int_3 = int.Parse(array[3]);
						RoomItem_ = new RoomItem(class2.uint_0, @class.Id, class2.uint_1, class2.string_0, 0, 0, 0.0, 0, "", @class);
						if (@class.method_79(Session, RoomItem_, int_, int_2, int_3, true, false, false))
						{
							Session.GetHabbo().method_23().method_12(uint_, 1u, false);
							using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
							{
								class3.ExecuteQuery(string.Concat(new object[]
								{
									"UPDATE items SET room_id = '",
									@class.Id,
									"' WHERE Id = '",
									class2.uint_0,
									"' LIMIT 1"
								}));
							}
						}
						IL_32C:
						if (Session.GetHabbo().CurrentQuestId == 14u)
						{
							Phoenix.GetGame().GetQuestManager().method_1(14u, Session);
						}
					}
				}
			}
		}
	}
}
