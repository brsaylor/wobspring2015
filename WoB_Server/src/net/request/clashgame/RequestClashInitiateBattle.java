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
import net.response.clashgame.ResponseClashInitiateBattle;
import util.DataReader;

/**
 *
 * @author lev
 */
public class RequestClashInitiateBattle extends GameRequest{

    private int playerToAttack;
    private ArrayList<Integer> attackConfig;
    
    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        playerToAttack = DataReader.readInt(dataInput);
        attackConfig = new ArrayList<>();
        int count = DataReader.readInt(dataInput);
        for(int i = 0; i < count; i++){
            attackConfig.add(DataReader.readInt(dataInput));
        }
    }

    @Override
    public void process() throws Exception {
        ResponseClashInitiateBattle response = new ResponseClashInitiateBattle();
        //check if player is already engaged in battle, if so
        //reponse.setStatus(ResponseClashInitiateBattle.ALREADY_IN_BATTLE);
        
        //record battle in db
        response.setStatus(ResponseClashInitiateBattle.INITIATED);
        
        client.add(response);
    }
    
}
