using System;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
namespace Phoenix.Communication.Messages.Catalog
{
	internal sealed class PurchaseFromCatalogAsGiftEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			int int_ = Event.PopWiredInt32();
			uint uint_ = Event.PopWiredUInt();
			string string_ = Event.PopFixedString();
			string string_2 = Phoenix.FilterString(Event.PopFixedString());
			string string_3 = Phoenix.FilterString(Event.PopFixedString());
			Phoenix.GetGame().GetCatalog().method_6(Session, int_, uint_, string_, true, string_2, string_3, true);
		}
	}
}
