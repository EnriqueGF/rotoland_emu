using System;
using System.Data;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Items;
using Phoenix.Messages;
using Phoenix.Storage;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.Communication.Messages.Rooms.Furniture
{
	internal sealed class CreditFurniRedeemMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				Room @class = Phoenix.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				if (@class != null && @class.method_27(Session, true))
				{
					RoomItem class2 = @class.method_28(Event.PopWiredUInt());
					UserItem class3 = Session.GetHabbo().method_23().method_10(class2.uint_0);
					if (class2 != null)
					{
						if (class2.GetBaseItem().Name.StartsWith("CF_") || class2.GetBaseItem().Name.StartsWith("CFC_") || class2.GetBaseItem().Name.StartsWith("PixEx_") || class2.GetBaseItem().Name.StartsWith("PntEx_"))
						{
							if (class3 != null)
							{
								@class.method_29(null, class3.uint_0, true, true);
							}
							else
							{
								DataRow dataRow = null;
								using (DatabaseClient class4 = Phoenix.GetDatabase().GetClient())
								{
									dataRow = class4.ReadDataRow("SELECT ID FROM items WHERE ID = '" + class2.uint_0 + "' LIMIT 1");
								}
								if (dataRow != null)
								{
									string[] array = class2.GetBaseItem().Name.Split(new char[]
									{
										'_'
									});
									int num = int.Parse(array[1]);
									if (num > 0)
									{
										if (class2.GetBaseItem().Name.StartsWith("CF_") || class2.GetBaseItem().Name.StartsWith("CFC_"))
										{
											Session.GetHabbo().Credits += num;
											Session.GetHabbo().method_13(true);
										}
										else
										{
											if (class2.GetBaseItem().Name.StartsWith("PixEx_"))
											{
												Session.GetHabbo().ActivityPoints += num;
												Session.GetHabbo().method_15(true);
											}
											else
											{
												if (class2.GetBaseItem().Name.StartsWith("PntEx_"))
												{
													Session.GetHabbo().VipPoints += num;
													Session.GetHabbo().method_14(false, true);
												}
											}
										}
									}
								}
								@class.method_29(null, class2.uint_0, true, true);
								ServerMessage Message5_ = new ServerMessage(219u);
								Session.SendMessage(Message5_);
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
