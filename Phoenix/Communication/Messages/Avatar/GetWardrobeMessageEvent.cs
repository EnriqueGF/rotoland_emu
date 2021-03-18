using System;
using System.Data;
using Phoenix.HabboHotel.GameClients;
using Phoenix.Messages;
using Phoenix.Storage;
namespace Phoenix.Communication.Messages.Avatar
{
    internal sealed class GetWardrobeMessageEvent : Interface
    {
        public void Handle(GameClient Session, ClientMessage Event)
        {
            ServerMessage Message = new ServerMessage(267u);
            Message.AppendBoolean(Session.GetHabbo().method_20().method_2("habbo_club"));
            if (Session.GetHabbo().method_20().method_2("habbo_club"))
            {
                using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
                {
                    @class.AddParamWithValue("userid", Session.GetHabbo().Id);
                    DataTable dataTable = @class.ReadDataTable("SELECT slot_id, look, gender FROM user_wardrobe WHERE user_id = @userid;");
                    if (dataTable == null)
                    {
                        Message.AppendInt32(0);
                    }
                    else
                    {
                        Message.AppendInt32(dataTable.Rows.Count);
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            Message.AppendUInt((uint)dataRow["slot_id"]);
                            Message.AppendStringWithBreak((string)dataRow["look"]);
                            Message.AppendStringWithBreak((string)dataRow["gender"]);
                        }
                    }
                }
                Session.SendMessage(Message);
            }
            else
            {
                using (DatabaseClient @class = Phoenix.GetDatabase().GetClient())
                {
                    @class.AddParamWithValue("userid", Session.GetHabbo().Id);
                    DataTable dataTable = @class.ReadDataTable("SELECT slot_id, look, gender FROM user_wardrobe WHERE user_id = @userid;");
                    if (dataTable == null)
                    {
                        Message.AppendInt32(0);
                    }
                    else
                    {
                        Message.AppendInt32(dataTable.Rows.Count);
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            Message.AppendUInt((uint)dataRow["slot_id"]);
                            Message.AppendStringWithBreak((string)dataRow["look"]);
                            Message.AppendStringWithBreak((string)dataRow["gender"]);
                        }
                    }
                }
                Session.SendMessage(Message);
            }
        }
    }
}