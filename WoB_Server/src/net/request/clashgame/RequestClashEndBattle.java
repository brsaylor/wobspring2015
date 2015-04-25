/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashEndBattle;
import util.DataReader;

/**
 *
 * @author lev
 */
public class RequestClashEndBattle extends GameRequest{
    private boolean won;
    
    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        won = DataReader.readBoolean(dataInput);
    }

    @Override
    public void process() throws Exception {
        //record state in db
        
        ResponseClashEndBattle response = new ResponseClashEndBattle();
        client.add(response);
    }
    
}
