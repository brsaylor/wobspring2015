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
import java.util.Date;

import db.clashgame.BattleDAO;
import model.clashgame.Battle;

/**
 *
 * @author lev
 */
public class RequestClashEndBattle extends GameRequest {
    
    Battle.Outcome outcome;
    
    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        int value = DataReader.readInt(dataInput);
        
        if (value == 0) {
            outcome = Battle.Outcome.WIN;
        } else if (value == 1) {
            outcome = Battle.Outcome.LOSE;
        } else {
            outcome = Battle.Outcome.DRAW;
        }
    }

    @Override
    public void process() throws Exception {
        //record state in db
        Battle battle = BattleDAO.findActiveByPlayer(client.getPlayer().getID());
        battle.outcome = outcome;
        battle.timeEnded = new Date();
        BattleDAO.save(battle);
        
        ResponseClashEndBattle response = new ResponseClashEndBattle();
        client.add(response);
    }
    
}
