/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import java.util.HashMap;
import java.util.Map;

import metadata.NetworkCode;
import net.response.GameResponse;
import util.GamePacket;
import util.Vector2;


/**
 * Data container for information needed when first enterirng the
 * Clash of Species game
 * @author lev
 */
public class ResponseClashEntry extends GameResponse{
    /**
     * whether the user has a Clash of Species defense set up
     * true if no defense
     */
    private boolean isNewClashPlayer;

    /**
     * Stores the species and positions of the defense,
     * if it exists
     */
    private HashMap<Integer, Vector2<Float>> configMap;

    /**
     * Stores the terrain of hte defense, if it exists
     */
    private String defenseTerrain;


    public void setDefenseTerrain(String defenseTerrain) {
        this.defenseTerrain = defenseTerrain;
    }

    /**
     * Sets the boolean value for whether the user has a Clash of
     * Species defense set up
     * @param isNewClashPlayer true if no defense, false otherwise
     */
    public void setNewClashPlayer(boolean isNewClashPlayer) {
        this.isNewClashPlayer = isNewClashPlayer;

    }

    /**
     * Appends an element of the existing defense config: a species id
     * and its position on the terrain
     * @param speciesId the species id of the defense element
     * @param position the vector containing x and y-coordinates of
     *                 the defense element
     */
    public void addSpecies(int speciesId, Vector2<Float> position){
        this.configMap.put(speciesId, position);
    }
    
    //constructor as well
    public ResponseClashEntry(){
        this.response_id = NetworkCode.CLASH_ENTRY;
        this.configMap = new HashMap<Integer, Vector2<Float>>();
    }

    /**
     * Generates a byte array containing all the data to be sent to
     * back to the client
     * @return the byte array
     */
    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addBoolean(isNewClashPlayer);
        if(!isNewClashPlayer){
            packet.addString(defenseTerrain);
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

