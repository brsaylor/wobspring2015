/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import metadata.NetworkCode;
import net.response.GameResponse;
import util.GamePacket;

import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author lev
 */
public class ResponseClashPlayerList extends GameResponse{
    private HashMap<Integer, String> playerNames;

    public ResponseClashPlayerList(){
        response_id = NetworkCode.CLASH_PLAYER_LIST;
        playerNames = new HashMap<Integer, String>();
    }

    public void addPlayer(int _id, String _name){
        playerNames.put(_id, _name);
    }

    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addInt32(playerNames.size());
        for(Map.Entry<Integer, String> p : playerNames.entrySet()){
            packet.addInt32(p.getKey());
            packet.addString(p.getValue());
        }

        return packet.getBytes();
    }
}
