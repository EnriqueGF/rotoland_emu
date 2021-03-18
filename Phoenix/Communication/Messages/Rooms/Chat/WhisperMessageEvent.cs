using System;
using Phoenix.HabboHotel.Misc;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Util;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Rooms.Chat
{
	internal sealed class WhisperMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && Session != null)
			{
				if (Session.GetHabbo().bool_3)
				{
					Session.SendNotif(PhoenixEnvironment.smethod_1("error_muted"));
				}
				else
				{
					if (Session.GetHabbo().HasFuse("ignore_roommute") || !@class.bool_4)
					{
						string text = Phoenix.FilterString(Event.PopFixedString());
						string text2 = text.Split(new char[]
						{
							' '
						})[0];
						string text3 = text.Substring(text2.Length + 1);
						text3 = ChatCommandHandler.smethod_4(text3);
						RoomUser class2 = @class.method_53(Session.GetHabbo().Id);
						RoomUser class3 = @class.method_56(text2);
						if (Session.GetHabbo().method_4() > 0)
						{
							TimeSpan timeSpan = DateTime.Now - Session.GetHabbo().dateTime_0;
							if (timeSpan.Seconds > 4)
							{
								Session.GetHabbo().int_23 = 0;
							}
							if (timeSpan.Seconds < 4 && Session.GetHabbo().int_23 > 5 && !class2.Boolean_4)
							{
								ServerMessage Message = new ServerMessage(27u);
								Message.AppendInt32(Session.GetHabbo().method_4());
								Session.SendMessage(Message);
								Session.GetHabbo().bool_3 = true;
								Session.GetHabbo().int_4 = Session.GetHabbo().method_4();
								return;
							}
							Session.GetHabbo().dateTime_0 = DateTime.Now;
							Session.GetHabbo().int_23++;
						}
						ServerMessage Message2 = new ServerMessage(25u);
						Message2.AppendInt32(class2.VirtualId);
						Message2.AppendStringWithBreak(text3);
						Message2.AppendBoolean(false);
						if (class2 != null && !class2.Boolean_4)
						{
							class2.GetClient().SendMessage(Message2);
						}
						class2.method_0();
						if (class3 != null && !class3.Boolean_4 && (class3.GetClient().GetHabbo().list_2.Count <= 0 || !class3.GetClient().GetHabbo().list_2.Contains(Session.GetHabbo().Id)))
						{
							class3.GetClient().SendMessage(Message2);
							if (LicenseTools.Boolean_4 && !Session.GetHabbo().isAaron)
							{
								using (DatabaseClient class4 = Phoenix.GetDatabase().GetClient())
								{
									class4.AddParamWithValue("message", "<Whisper to " + class3.GetClient().GetHabbo().Username + ">: " + text3);
									class4.ExecuteQuery(string.Concat(new object[]
									{
										"INSERT INTO chatlogs (user_id,room_id,hour,minute,timestamp,message,user_name,full_date) VALUES ('",
										Session.GetHabbo().Id,
										"','",
										@class.Id,
										"','",
										DateTime.Now.Hour,
										"','",
										DateTime.Now.Minute,
										"',UNIX_TIMESTAMP(),@message,'",
										Session.GetHabbo().Username,
										"','",
										DateTime.Now.ToLongDateString(),
										"')"
									}));
								}
							}
						}
					}
				}
			}
		}
	}
}
