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
public class ResponseClashInitiateBattle extends GameResponse{

    private boolean valid;

    public void setValid(boolean valid) {
        this.valid = valid;
    }
    
    public ResponseClashInitiateBattle(){
        response_id = NetworkCode.CLASH_INITIATE_BATTLE;
    }
    
    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addBoolean(valid);
        return packet.getBytes();
    }
    
}
