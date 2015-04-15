/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import db.PlayerDAO;
import java.io.DataInputStream;
import java.io.IOException;
import model.Player;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashPlayerView;
import util.DataReader;

/**
 *
 * @author lev
 */
public class RequestClashPlayerView extends GameRequest{
    
    private int playerID;

    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        playerID = DataReader.readInt(dataInput);
    }

    @Override
    public void process() throws Exception {
        Player player = PlayerDAO.getPlayerByAccount(playerID);

        ResponseClashPlayerView response = new ResponseClashPlayerView();
        if (player != null) {
            response.setPlayer(player);
        }
        
        client.add(response);
    }
    
}
