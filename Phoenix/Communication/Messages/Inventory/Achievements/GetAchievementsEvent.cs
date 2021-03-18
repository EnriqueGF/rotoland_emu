using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.HabboHotel.Achievements;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Inventory.Achievements
{
	internal sealed class GetAchievementsEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.SendMessage(AchievementManager.smethod_1(Session));
		}
	}
}
