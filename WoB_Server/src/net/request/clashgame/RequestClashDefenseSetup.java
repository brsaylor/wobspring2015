/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;
import java.util.Date;
import java.util.HashMap;

import core.GameServer;
import db.clashgame.DefenseConfigDAO;
import model.clashgame.DefenseConfig;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashDefenseSetup;
import util.DataReader;
import util.Log;
import util.Vector2;

/**
 *
 * @author lev
 */
public class RequestClashDefenseSetup extends GameRequest {

    private int setupTerrainID;
    private HashMap<Integer, Vector2<Float>> configMap
             = new HashMap<Integer, Vector2<Float>>();
   
    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        setupTerrainID = DataReader.readInt(dataInput);
        int defenseSpeciesCount = DataReader.readInt(dataInput);
        for(int i = 0; i < defenseSpeciesCount; i++){
            int speciesId = DataReader.readInt(dataInput);
            float x = DataReader.readFloat(dataInput);
            float y = DataReader.readFloat(dataInput);
            
            configMap.put(speciesId, new Vector2(x, y));
        }
    }

    @Override
    public void process() throws Exception {
        boolean valid = configMap.size() == 5; //more checks in the future

        DefenseConfig config = new DefenseConfig();
        config.createdAt = new Date();
        config.playerId = client.getPlayer().getID();
        config.terrainId = setupTerrainID;
        config.layout = configMap;

        DefenseConfigDAO.create(config);
        ResponseClashDefenseSetup response = new ResponseClashDefenseSetup();
        response.setValidSetup(valid);
        client.add(response);
    }
    
}
