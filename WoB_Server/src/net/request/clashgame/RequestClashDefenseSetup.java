/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashDefenseSetup;
import util.DataReader;
import util.Log;

/**
 *
 * @author lev
 */
public class RequestClashDefenseSetup extends GameRequest{

    private String setupData; //actual format will likely be different
    
    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        setupData = DataReader.readString(dataInput);
    }

    @Override
    public void process() throws Exception {
        Log.println("received data " + setupData);
        
        //TODO: process data, add to DB
        
        ResponseClashDefenseSetup response = new ResponseClashDefenseSetup();
        client.add(response);
    }
    
}
