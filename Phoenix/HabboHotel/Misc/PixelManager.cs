using System;
using System.Threading;
using Phoenix.Core;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Util;
using Phoenix.HabboHotel.Rooms;
namespace Phoenix.HabboHotel.Misc
{
	internal sealed class PixelManager
	{
		public bool KeepAlive;
		private Thread WorkerThread;
		public PixelManager()
		{
			this.KeepAlive = true;
			this.WorkerThread = new Thread(new ThreadStart(this.method_1));
			this.WorkerThread.Name = "Pixel Manager";
			this.WorkerThread.Priority = ThreadPriority.Lowest;
		}
		public void method_0()
		{
			Logging.smethod_0("Starting Reward Timer..");
			this.WorkerThread.Start();
			Logging.WriteLine("completed!");
		}
		private void method_1()
		{
			try
			{
				while (this.KeepAlive)
				{
					if (Phoenix.GetGame() != null && Phoenix.GetGame().GetClientManager() != null)
					{
						Phoenix.GetGame().GetClientManager().method_29();
					}
					Thread.Sleep(15000);
				}
			}
			catch (ThreadAbortException)
			{
			}
		}
		public bool method_2(GameClient Session)
		{
			double num = (Phoenix.GetUnixTimestamp() - Session.GetHabbo().LastActivityPointsUpdate) / 60.0;
			return num >= (double)LicenseTools.Int32_0;
		}
		public void method_3(GameClient Session)
		{
			try
			{
				if (Session.GetHabbo().Boolean_0)
				{
					RoomUser @class = Session.GetHabbo().Class14_0.method_53(Session.GetHabbo().Id);
					if (@class.int_1 <= LicenseTools.int_14)
					{
						double double_ = Phoenix.GetUnixTimestamp();
						Session.GetHabbo().LastActivityPointsUpdate = double_;
						if (LicenseTools.Int32_3 > 0 && (Session.GetHabbo().ActivityPoints < LicenseTools.int_3 || LicenseTools.int_3 == 0))
						{
							Session.GetHabbo().ActivityPoints += LicenseTools.Int32_3;
							Session.GetHabbo().method_16(LicenseTools.Int32_3);
						}
						if (LicenseTools.Int32_1 > 0 && (Session.GetHabbo().Credits < LicenseTools.int_5 || LicenseTools.int_5 == 0))
						{
							Session.GetHabbo().Credits += LicenseTools.Int32_1;
							if (Session.GetHabbo().Vip)
							{
								Session.GetHabbo().Credits += LicenseTools.Int32_1;
							}
							Session.GetHabbo().method_13(true);
						}
						if (LicenseTools.Int32_2 > 0 && (Session.GetHabbo().VipPoints < LicenseTools.int_4 || LicenseTools.int_4 == 0))
						{
							Session.GetHabbo().VipPoints += LicenseTools.Int32_2;
							Session.GetHabbo().method_14(false, true);
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
