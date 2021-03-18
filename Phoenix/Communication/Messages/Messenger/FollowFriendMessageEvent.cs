using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Messenger
{
	internal sealed class FollowFriendMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			uint uint_ = Event.PopWiredUInt();
			GameClient @class = Phoenix.GetGame().GetClientManager().method_2(uint_);
			if (@class != null && @class.GetHabbo() != null && @class.GetHabbo().Boolean_0)
			{
				Room class2 = Phoenix.GetGame().GetRoomManager().GetRoom(@class.GetHabbo().CurrentRoomId);
				if (class2 != null && class2 != Session.GetHabbo().Class14_0)
				{
					ServerMessage Message = new ServerMessage(286u);
					Message.AppendBoolean(class2.Boolean_3);
					Message.AppendUInt(class2.Id);
					Session.SendMessage(Message);
				}
			}
		}
	}
}
