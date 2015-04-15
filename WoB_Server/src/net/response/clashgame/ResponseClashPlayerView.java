/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import metadata.NetworkCode;
import model.Player;
import net.response.GameResponse;
import util.GamePacket;

/**
 *
 * @author lev
 */
public class ResponseClashPlayerView extends GameResponse{

    private Player player;

    public void setPlayer(Player player) {
        this.player = player;
    }
    
    public ResponseClashPlayerView(){
        response_id = NetworkCode.CLASH_PLAYER_VIEW;
    }
    
    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addInt32(player.getID());
        //add other stuff relevant to clash game
        return packet.getBytes();
    }
    
}
