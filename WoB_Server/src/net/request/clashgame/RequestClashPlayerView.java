/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import db.PlayerDAO;
import java.io.DataInputStream;
import java.io.IOException;

import db.clashgame.ClashPlayerDAO;
import db.clashgame.DefenseConfigDAO;
import model.clashgame.DefenseConfig;
import model.clashgame.Player;
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
        ResponseClashPlayerView response = new ResponseClashPlayerView();

        Player target = ClashPlayerDAO.findById(playerID);
        DefenseConfig defcon = DefenseConfigDAO.findByPlayerId(playerID);

        if (target != null) {
            response.setDefenseConfig(defcon);
            response.setPlayer(target);
        }
        client.add(response);
    }
    
}
