/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import javafx.util.Pair;
import metadata.NetworkCode;
import net.response.GameResponse;
import util.GamePacket;
import util.Vector2;


/**
 *
 * @author lev
 */
public class ResponseClashEntry extends GameResponse{
    private boolean isNewClashPlayer;
    private HashMap<Integer, Vector2<Float>> configMap;
    private int defenseTerrainID;

    public void setDefenseTerrainID(int defenseTerrainID) {
        this.defenseTerrainID = defenseTerrainID;
    }

    public void setNewClashPlayer(boolean isNewClashPlayer) {
        this.isNewClashPlayer = isNewClashPlayer;

    }
    
    public void addNewSpecies(int speciesId, Vector2<Float> position){
        this.configMap.put(speciesId, position);
    }
    
    //TODO: when ClashSetup class implemented, pass it to the
    //constructor as well
    public ResponseClashEntry(){
        this.response_id = NetworkCode.CLASH_ENTRY;
        this.configMap = new HashMap<Integer, Vector2<Float>>();
    }

    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addBoolean(isNewClashPlayer);
        if(!isNewClashPlayer){
            packet.addInt32(defenseTerrainID);
            packet.addInt32(configMap.size());
            for(Map.Entry<Integer, Vector2<Float>> d : configMap.entrySet()){
                packet.addInt32(d.getKey());
                packet.addFloat(d.getValue().getX());
                packet.addFloat(d.getValue().getY());
            }
        }
        return packet.getBytes();
    }
}

