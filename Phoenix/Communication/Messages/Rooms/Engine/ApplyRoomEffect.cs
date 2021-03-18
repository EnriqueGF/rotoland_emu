using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Items;
using Phoenix.Messages;
using Phoenix.Storage;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class ApplyRoomEffect : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
			if (@class != null && @class.method_27(Session, true))
			{
				UserItem class2 = Session.GetHabbo().method_23().method_10(Event.PopWiredUInt());
				if (class2 != null)
				{
					string text = "floor";
					if (class2.method_1().Name.ToLower().Contains("wallpaper"))
					{
						text = "wallpaper";
					}
					else
					{
						if (class2.method_1().Name.ToLower().Contains("landscape"))
						{
							text = "landscape";
						}
					}
					string text2 = text;
					if (text2 != null)
					{
						if (!(text2 == "floor"))
						{
							if (!(text2 == "wallpaper"))
							{
								if (text2 == "landscape")
								{
									@class.Landscape = class2.string_0;
								}
							}
							else
							{
								@class.Wallpaper = class2.string_0;
								if (Session.GetHabbo().CurrentQuestId == 11u)
								{
									Phoenix.GetGame().GetQuestManager().method_1(11u, Session);
								}
							}
						}
						else
						{
							@class.Floor = class2.string_0;
							if (Session.GetHabbo().CurrentQuestId == 13u)
							{
								Phoenix.GetGame().GetQuestManager().method_1(13u, Session);
							}
						}
					}
					using (DatabaseClient class3 = Phoenix.GetDatabase().GetClient())
					{
						class3.AddParamWithValue("extradata", class2.string_0);
						class3.ExecuteQuery(string.Concat(new object[]
						{
							"UPDATE rooms SET ",
							text,
							" = @extradata WHERE Id = '",
							@class.Id,
							"' LIMIT 1"
						}));
					}
					Session.GetHabbo().method_23().method_12(class2.uint_0, 0u, false);
					ServerMessage Message = new ServerMessage(46u);
					Message.AppendStringWithBreak(text);
					Message.AppendStringWithBreak(class2.string_0);
					@class.SendMessage(Message, null);
					Phoenix.GetGame().GetRoomManager().method_18(@class.Id);
				}
			}
		}
	}
}
