package com.eu.habbo.messages.outgoing.users;

import com.eu.habbo.Emulator;
import com.eu.habbo.habbohotel.users.Habbo;
import com.eu.habbo.messages.ServerMessage;
import com.eu.habbo.messages.outgoing.MessageComposer;
import com.eu.habbo.messages.outgoing.Outgoing;

public class UserClubComposer extends MessageComposer {
    private final Habbo habbo;

    public UserClubComposer(Habbo habbo) {
        this.habbo = habbo;
    }

    @Override
    protected ServerMessage composeInternal() {
        this.response.init(Outgoing.UserClubComposer);

        this.response.appendString("club_habbo");

        int endTimestamp = this.habbo.getHabboStats().getClubExpireTimestamp();
        int now = Emulator.getIntUnixTimestamp();

        if (endTimestamp >= now) {


            int days = ((endTimestamp - Emulator.getIntUnixTimestamp()) / 86400);
            int years = (int) Math.floor(days / 365);

            //if(years > 0)


            int months = 0;

            if (days > 31) {
                months = (int) Math.floor(days / 31);
                days = days - (months * 31);
            }

            this.response.appendInt(days);
            this.response.appendInt(1);
            this.response.appendInt(months);
            this.response.appendInt(years);
        } else {
            this.response.appendInt(0);
            this.response.appendInt(7);
            this.response.appendInt(0);
            this.response.appendInt(1);
        }
        this.response.appendBoolean(true);
        this.response.appendBoolean(true);
        this.response.appendInt(0);
        this.response.appendInt(0);

        long remaining = (endTimestamp - Emulator.getIntUnixTimestamp()) * 1000;

        if (remaining > Integer.MAX_VALUE || remaining <= 0) {
            this.response.appendInt(Integer.MAX_VALUE);
        } else {
            this.response.appendInt((int) remaining);
        }

        return this.response;
    }
}
