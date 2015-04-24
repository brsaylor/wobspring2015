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

    public final static short INITIATED = 0;
    public final static short ALREADY_IN_BATTLE = 1;
    public final static short INVALID_ATTACK_CONFIG = 2;
    
    private short status;

    public void setStatus(short status) {
        this.status = status;
    }
    
    public ResponseClashInitiateBattle(){
        response_id = NetworkCode.CLASH_INITIATE_BATTLE;
    }
    
    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addShort16(status);
        
        return packet.getBytes();
    }
    
}
