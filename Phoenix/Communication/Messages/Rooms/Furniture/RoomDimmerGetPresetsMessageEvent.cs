using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Items;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Furniture
{
	internal sealed class RoomDimmerGetPresetsMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				if (@class != null && @class.method_27(Session, true) && @class.class67_0 != null)
				{
					ServerMessage Message = new ServerMessage(365u);
					Message.AppendInt32(@class.class67_0.Presets.Count);
					Message.AppendInt32(@class.class67_0.CurrentPreset);
					int num = 0;
					foreach (MoodlightPreset current in @class.class67_0.Presets)
					{
						num++;
						Message.AppendInt32(num);
						Message.AppendInt32(int.Parse(Phoenix.smethod_4(current.BackgroundOnly)) + 1);
						Message.AppendStringWithBreak(current.ColorCode);
						Message.AppendInt32(current.ColorIntensity);
					}
					Session.SendMessage(Message);
				}
			}
			catch
			{
			}
		}
	}
}
