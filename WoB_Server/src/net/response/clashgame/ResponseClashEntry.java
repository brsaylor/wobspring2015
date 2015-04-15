/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import metadata.NetworkCode;
import net.response.GameResponse;
import static net.response.ResponseLogin.SUCCESS;
import util.GamePacket;

/**
 *
 * @author lev
 */
public class ResponseClashEntry extends GameResponse{
    boolean isNewClashPlayer;

    public void setNewClashPlayer(boolean isNewClashPlayer) {
        this.isNewClashPlayer = isNewClashPlayer;
    }
    
    //TODO: when ClashSetup class implemented, pass it to the
    //constructor as well
    public ResponseClashEntry(){
        response_id = NetworkCode.CLASH_ENTRY;
    }

    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addBoolean(isNewClashPlayer);

        return packet.getBytes();
    }
    
}
