/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import metadata.NetworkCode;
import net.response.GameResponse;
import util.GamePacket;

/**
 *
 * @author lev
 */
public class ResponseClashEndBattle extends GameResponse{
    
    
    public ResponseClashEndBattle(){
        response_id = NetworkCode.CLASH_END_BATTLE;
    }

    public void setCredits(int credits) {
        this.credits = credits;
    }

    private int credits;


    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addInt32(credits);
        return packet.getBytes();
    }
    
}
