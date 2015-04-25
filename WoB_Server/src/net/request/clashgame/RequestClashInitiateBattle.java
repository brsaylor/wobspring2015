/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;

import db.clashgame.AttackConfigDAO;
import db.clashgame.BattleDAO;
import db.clashgame.DefenseConfigDAO;
import model.clashgame.AttackConfig;
import model.clashgame.Battle;
import model.clashgame.DefenseConfig;
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
        attackConfig = new ArrayList<Integer>();
        int count = DataReader.readInt(dataInput);
        for(int i = 0; i < count; i++){
            attackConfig.add(DataReader.readInt(dataInput));
        }
    }

    @Override
    public void process() throws Exception {
        ResponseClashInitiateBattle response = new ResponseClashInitiateBattle();

        if (attackConfig.size() > 5) {
            response.setValid(false);
        } else {
            response.setValid(true);

            DefenseConfig target = DefenseConfigDAO.findByPlayerId(playerToAttack);

            AttackConfig atk = new AttackConfig();
            atk.createdAt = new Date();
            atk.playerId = this.client.getPlayer().getID();
            atk.speciesIds = attackConfig;
            AttackConfigDAO.create(atk);

            Battle battle = new Battle();
            battle.defenseConfigId = target.id;
            battle.attackConfigId = atk.id;
            battle.battleStart = new Date();
            BattleDAO.create(battle);
        }
        client.add(response);
    }
    
}
