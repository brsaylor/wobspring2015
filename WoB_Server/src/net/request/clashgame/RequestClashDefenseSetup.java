/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;
import java.util.ArrayList;
import net.request.GameRequest;
import clashofspecies.BattleElementData;
import net.response.clashgame.ResponseClashDefenseSetup;
import util.DataReader;
import util.Log;

/**
 *
 * @author lev
 */
public class RequestClashDefenseSetup extends GameRequest{

    private int setupTerrainID;
    private ArrayList<BattleElementData> defenseList = new ArrayList<BattleElementData>();

    
    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        setupTerrainID = DataReader.readInt(dataInput);
        int defenseSpeciesCount = DataReader.readInt(dataInput);
        for(int i = 0; i < defenseSpeciesCount; i++){
            int speciesID = DataReader.readInt(dataInput);
            float x = DataReader.readFloat(dataInput);
            float y = DataReader.readFloat(dataInput);
            
            defenseList.add(new BattleElementData(speciesID, x, y));
        }
    }

    @Override
    public void process() throws Exception {
        Log.println("received data ");
        
        boolean valid = defenseList.size() <= 5; //more checks in the future
        
        //TODO: process data, add to DB
        
        ResponseClashDefenseSetup response = new ResponseClashDefenseSetup();
        response.setValidSetup(valid);
        client.add(response);
    }
    
}
