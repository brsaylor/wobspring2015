/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import metadata.NetworkCode;
import model.Player;
import net.response.GameResponse;
import util.GamePacket;
import util.Vector2;

/**
 *
 * @author lev
 */
public class ResponseClashPlayerView extends GameResponse{

    private int defenseConfigID;
    private int terrainID;
    private HashMap<Integer, Vector2<Float>> configMap;

    public ResponseClashPlayerView(){
        response_id = NetworkCode.CLASH_PLAYER_VIEW;
        configMap = new HashMap<Integer, Vector2<Float>>();
    }

    public void setDefenseConfigID(int defenseConfigID) {
        this.defenseConfigID = defenseConfigID;
    }

    public void setTerrainID(int terrainID) {
        this.terrainID = terrainID;
    }

    public void addNewDefenseSpecies(int speciesId, Vector2<Float> position){
        configMap.put(speciesId, position);
    }

    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        //packet.addInt32(player.getID());
        packet.addInt32(defenseConfigID);
        packet.addInt32(terrainID);
        packet.addInt32(configMap.size());
        for(Map.Entry<Integer, Vector2<Float>> bed : configMap.entrySet()) {
            packet.addInt32(bed.getKey());
            packet.addFloat(bed.getValue().getX());
            packet.addFloat(bed.getValue().getY());
        }
        return packet.getBytes();
    }
    
}
