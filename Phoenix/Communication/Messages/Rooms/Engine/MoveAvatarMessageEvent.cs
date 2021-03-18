using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Engine
{
	internal sealed class MoveAvatarMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Room class14_ = Session.GetHabbo().Class14_0;
			if (class14_ != null)
			{
				RoomUser @class = class14_.method_53(Session.GetHabbo().Id);
				if (@class != null && @class.bool_0)
				{
					int num = Event.PopWiredInt32();
					int num2 = Event.PopWiredInt32();
					if (num != @class.int_3 || num2 != @class.int_4)
					{
						if (@class.RoomUser_0 != null)
						{
							try
							{
								if (@class.RoomUser_0.Boolean_4)
								{
									@class.method_0();
								}
								@class.RoomUser_0.MoveTo(num, num2);
								return;
							}
							catch
							{
								@class.RoomUser_0 = null;
								@class.class34_1 = null;
								@class.MoveTo(num, num2);
								Session.GetHabbo().method_24().method_2(-1, true);
								return;
							}
						}
						if (@class.TeleportMode)
						{
							@class.int_3 = num;
							@class.int_4 = num2;
							@class.bool_7 = true;
						}
						else
						{
							@class.MoveTo(num, num2);
						}
					}
				}
			}
		}
	}
}
