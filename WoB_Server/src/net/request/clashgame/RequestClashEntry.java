/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;

import model.clashgame.DefenseConfig;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashEntry;
import util.DataReader;
import db.clashgame.DefenseConfigDAO;
import util.Vector2;

/**
 *
 * @author lev
 */
public class RequestClashEntry extends GameRequest{
    private int playerID;
    boolean isNewClashPlayer;

    @Override
    public void parse(DataInputStream dataInput) throws IOException {
    }

    @Override
    public void process() throws Exception {
        DefenseConfig defense = DefenseConfigDAO.findByPlayerId(this.client.getPlayer().getID());
        if(defense == null){
            isNewClashPlayer = true;
        }else{
            isNewClashPlayer = false;
        }

        ResponseClashEntry response = new ResponseClashEntry();
        response.setNewClashPlayer(isNewClashPlayer);
        if(!isNewClashPlayer){
            //add existing defense setup
            response.setDefenseTerrainID(defense.terrainId);
            for (HashMap.Entry<Integer, Vector2<Float>> en : defense.layout.entrySet()) {
                response.addSpecies(en.getKey(), en.getValue());
            }
        }
        client.add(response);        
    }
    
}
