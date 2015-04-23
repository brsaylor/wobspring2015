/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import db.ShopDAO;
import java.io.DataInputStream;
import java.io.IOException;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashSpeciesList;

/**
 *
 * @author lev
 */
public class RequestClashSpeciesList extends GameRequest{
        
    @Override
    public void parse(DataInputStream dataInput) throws IOException {
    }

    @Override
    public void process() throws Exception {
        //
        ResponseClashSpeciesList response = new ResponseClashSpeciesList();
        response.setSpeciesList(ShopDAO.getItems("level:0,99"));
        client.add(response);
    }
    
}
