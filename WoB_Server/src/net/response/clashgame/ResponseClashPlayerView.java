/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import clashofspecies.BattleElementData;
import java.util.ArrayList;
import metadata.NetworkCode;
import model.Player;
import net.response.GameResponse;
import util.GamePacket;

/**
 *
 * @author lev
 */
public class ResponseClashPlayerView extends GameResponse{

    private int defenseConfigID;
    private int terrainID;

    public void setDefenseConfigID(int defenseConfigID) {
        this.defenseConfigID = defenseConfigID;
    }

    public void setTerrainID(int terrainID) {
        this.terrainID = terrainID;
    }
    private ArrayList<BattleElementData> defenseList;
    
    /*
    private Player player;

    public void setPlayer(Player player) {
        this.player = player;
    }//*/
    
    public void addNewDefenseSpecies(BattleElementData data){
        defenseList.add(data);
    }
    
    public ResponseClashPlayerView(){
        response_id = NetworkCode.CLASH_PLAYER_VIEW;
        defenseList = new ArrayList<>();
    }
    
    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        //packet.addInt32(player.getID());
        packet.addInt32(defenseConfigID);
        packet.addInt32(terrainID);
        packet.addInt32(defenseList.size());
        for(BattleElementData bed : defenseList){
            packet.addInt32(bed.speciesID);
            packet.addFloat(bed.x);
            packet.addFloat(bed.y);
        }
        
        return packet.getBytes();
    }
    
}
