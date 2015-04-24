/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import metadata.NetworkCode;
import model.clashgame.Player;
import net.response.GameResponse;
import util.GamePacket;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 *
 * @author lev
 */
public class ResponseClashPlayerList extends GameResponse{
    private List<Player> players = new ArrayList<>();

    public ResponseClashPlayerList(){
        response_id = NetworkCode.CLASH_PLAYER_LIST;
    }

    public void addPlayer(Player pl) {
        players.add(pl);
    }

    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addInt32(players.size());
        for(Player pl : players) {
            packet.addInt32(pl.id);
            packet.addString(pl.name);
            // Also add level?
        }
        return packet.getBytes();
    }
}
