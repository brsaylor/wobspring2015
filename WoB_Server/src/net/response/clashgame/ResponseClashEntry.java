/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import clashofspecies.BattleElementData;
import java.util.ArrayList;
import metadata.NetworkCode;
import net.response.GameResponse;
import util.GamePacket;

/**
 *
 * @author lev
 */
public class ResponseClashEntry extends GameResponse{
    private boolean isNewClashPlayer;
    private ArrayList<BattleElementData> defenseList;
    private int defenseTerrainID;

    public void setDefenseTerrainID(int defenseTerrainID) {
        this.defenseTerrainID = defenseTerrainID;
    }

    public void setNewClashPlayer(boolean isNewClashPlayer) {
        this.isNewClashPlayer = isNewClashPlayer;
        this.defenseList = new ArrayList<>();
    }
    
    public void addNewDefenseSpecies(BattleElementData data){
        defenseList.add(data);
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
        if(!isNewClashPlayer){
            packet.addInt32(defenseTerrainID);
            packet.addInt32(defenseList.size());
            for(BattleElementData d : defenseList){
                packet.addInt32(d.speciesID);
                packet.addFloat(d.x);
                packet.addFloat(d.y);
            }
        }

        return packet.getBytes();
    }
}

