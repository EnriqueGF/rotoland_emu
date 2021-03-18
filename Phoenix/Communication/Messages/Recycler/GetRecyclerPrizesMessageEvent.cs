using System;
using System.Collections.Generic;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.HabboHotel.Catalogs;
namespace Phoenix.Communication.Messages.Recycler
{
	internal sealed class GetRecyclerPrizesMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			ServerMessage Message = new ServerMessage(506u);
			Message.AppendInt32(5);
			for (uint num = 5u; num >= 1u; num -= 1u)
			{
				Message.AppendUInt(num);
				if (num <= 1u)
				{
					Message.AppendInt32(0);
				}
				else
				{
					if (num == 2u)
					{
						Message.AppendInt32(4);
					}
					else
					{
						if (num == 3u)
						{
							Message.AppendInt32(40);
						}
						else
						{
							if (num == 4u)
							{
								Message.AppendInt32(200);
							}
							else
							{
								if (num >= 5u)
								{
									Message.AppendInt32(2000);
								}
							}
						}
					}
				}
				List<EcotronReward> list = Phoenix.GetGame().GetCatalog().method_16(num);
				Message.AppendInt32(list.Count);
				foreach (EcotronReward current in list)
				{
					Message.AppendStringWithBreak(current.method_0().Type.ToString().ToLower());
					Message.AppendUInt(current.uint_1);
				}
			}
			Session.SendMessage(Message);
		}
	}
}
