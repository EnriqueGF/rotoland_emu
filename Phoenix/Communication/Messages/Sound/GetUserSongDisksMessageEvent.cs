using System;
using System.Collections.Generic;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Items;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Sound
{
	internal sealed class GetUserSongDisksMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			List<UserItem> list = new List<UserItem>();
			foreach (UserItem current in Session.GetHabbo().method_23().list_0)
			{
				if (current != null && !(current.method_1().Name != "song_disk") && !Session.GetHabbo().method_23().list_1.Contains(current.uint_0))
				{
					list.Add(current);
				}
			}
			ServerMessage Message = new ServerMessage(333u);
			Message.AppendInt32(list.Count);
			foreach (UserItem current2 in list)
			{
				int int_ = 0;
				if (current2.string_0.Length > 0)
				{
					int_ = int.Parse(current2.string_0);
				}
				Soundtrack @class = Phoenix.GetGame().GetItemManager().method_4(int_);
				if (@class == null)
				{
					return;
				}
				Message.AppendUInt(current2.uint_0);
				Message.AppendInt32(@class.Id);
			}
			Session.SendMessage(Message);
		}
	}
}
