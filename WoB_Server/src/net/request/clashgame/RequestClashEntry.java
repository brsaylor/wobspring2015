/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import java.io.DataInputStream;
import java.io.IOException;

import model.clashgame.DefenseConfig;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashEntry;
import util.DataReader;
import db.clashgame.DefenseConfigDAO;
import util.Vector2;

/**
 *
 * @author lev
 */
public class RequestClashEntry extends GameRequest{
    private int playerID;
    boolean isNewClashPlayer;

    @Override
    public void parse(DataInputStream dataInput) throws IOException {
        playerID = DataReader.readInt(dataInput);
    }

    @Override
    public void process() throws Exception {
        DefenseConfig defense = DefenseConfigDAO.getConfig(playerID);
        if(defense == null){
            isNewClashPlayer = true;
        }else{
            isNewClashPlayer = false;
        }

        
        ResponseClashEntry response = new ResponseClashEntry();
        response.setNewClashPlayer(isNewClashPlayer);
        if(!isNewClashPlayer){
            //add existing defense setup
            if(defense.getSpecies_1() != 0){
                response.addNewSpecies(defense.getSpecies_1(),
                        new Vector2<Float>(defense.getSpecies_1_loc_x(),
                        defense.getSpecies_1_loc_y()));
            }

            if(defense.getSpecies_2() != 0){
                response.addNewSpecies(defense.getSpecies_2(),
                        new Vector2<Float>(defense.getSpecies_2_loc_x(),
                                defense.getSpecies_2_loc_y()));
            }

            if(defense.getSpecies_3() != 0){
                response.addNewSpecies(defense.getSpecies_3(),
                        new Vector2<Float>(defense.getSpecies_3_loc_x(),
                                defense.getSpecies_3_loc_y()));
            }

            if(defense.getSpecies_4() != 0){
                response.addNewSpecies(defense.getSpecies_4(),
                        new Vector2<Float>(defense.getSpecies_4_loc_x(),
                                defense.getSpecies_4_loc_y()));
            }

            if(defense.getSpecies_5() != 0){
                response.addNewSpecies(defense.getSpecies_5(),
                        new Vector2<Float>(defense.getSpecies_5_loc_x(),
                                defense.getSpecies_5_loc_y()));
            }
        }
        client.add(response);        
    }
    
}
