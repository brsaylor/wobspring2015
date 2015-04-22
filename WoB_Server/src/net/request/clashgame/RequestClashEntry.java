/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashEntry;
import util.DataReader;

/**
 *
 * @author lev
 */
public class RequestClashEntry extends GameRequest{
    private int playerID;
    boolean isNewClashPlayer = true;

    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        playerID = DataReader.readInt(dataInput);
    }

    @Override
    public void process() throws Exception {
        //need to query db for current defense setup
        //something like
        /*
        ClashSetup cs = client.getPlayer().getClashSetup();
        if(cs != null){
            isNewClashPlayer = false;
        }*/
        
        
        
        ResponseClashEntry response = new ResponseClashEntry();
        response.setNewClashPlayer(isNewClashPlayer);
        if(!isNewClashPlayer){
            //add existing defense setup
        }
        client.add(response);        
    }
    
}
