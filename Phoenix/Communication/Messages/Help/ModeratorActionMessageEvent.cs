using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Help
{
	internal sealed class ModeratorActionMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().HasFuse("acc_supporttool"))
			{
				Event.PopWiredInt32();
				int num = Event.PopWiredInt32();
				string text = Event.PopFixedString();
				string text2 = "";
				if (num.Equals(3))
				{
					text2 += "Room Cautioned.";
				}
				text2 = text2 + " Message: " + text;
				Phoenix.GetGame().GetClientManager().method_31(Session, "ModTool - Room Alert", text2);
				Phoenix.GetGame().GetModerationTool().method_13(Session.GetHabbo().CurrentRoomId, !num.Equals(3), text);
			}
		}
	}
}
