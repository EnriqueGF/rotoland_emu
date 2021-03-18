using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Items;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.HabboHotel.Items.Interactors
{
	internal sealed class InteractorBanzaiScoreCounter : FurniInteractor
	{
		public override void OnPlace(GameClient Session, RoomItem RoomItem_0)
		{
		}
		public override void OnRemove(GameClient Session, RoomItem RoomItem_0)
		{
		}
		public override void OnTrigger(GameClient Session, RoomItem RoomItem_0, int int_0, bool bool_0)
		{
			if (bool_0)
			{
				int num = 0;
				if (RoomItem_0.ExtraData.Length > 0)
				{
					num = int.Parse(RoomItem_0.ExtraData);
				}
				if (int_0 == 0)
				{
					if (num <= -1)
					{
						num = 0;
					}
					else
					{
						if (num >= 0)
						{
							num = -1;
						}
					}
				}
				else
				{
					if (int_0 >= 1)
					{
						if (int_0 == 1)
						{
							if (!RoomItem_0.bool_0)
							{
								RoomItem_0.bool_0 = true;
								RoomItem_0.ReqUpdate(1);
								if (Session != null)
								{
									RoomUser RoomUser_ = Session.GetHabbo().Class14_0.method_53(Session.GetHabbo().Id);
									RoomItem_0.method_8().method_14(RoomUser_);
								}
							}
							else
							{
								RoomItem_0.bool_0 = false;
							}
						}
						else
						{
							if (int_0 == 2)
							{
								num += 60;
								if (num >= 600)
								{
									num = 0;
								}
							}
						}
					}
				}
				RoomItem_0.ExtraData = num.ToString();
				RoomItem_0.UpdateState(true, true);
			}
		}
	}
}
