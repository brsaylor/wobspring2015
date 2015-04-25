/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import core.GameServer;
import java.io.DataInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import db.clashgame.ClashPlayerDAO;
import db.clashgame.DefenseConfigDAO;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashPlayerList;
import model.clashgame.Player;

/**
 *
 * @author lev
 */
public class RequestClashPlayerList extends GameRequest{

    @Override
    public void parse(DataInputStream dataInput) throws IOException {
    }

    @Override
    public void process() throws Exception {
        ResponseClashPlayerList response = new ResponseClashPlayerList();

        List<Player> eligible = ClashPlayerDAO.findEligiblePlayers();
        for(Player pl : eligible){
            // Players don't need to be active to initialize a battle.
            response.addPlayer(pl);
        }
        client.add(response);
    }
    
}
