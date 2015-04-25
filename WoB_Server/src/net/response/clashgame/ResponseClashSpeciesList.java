/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import metadata.NetworkCode;
import model.clashgame.Species;
import net.response.GameResponse;
import net.response.ResponseSpeciesList;
import util.GamePacket;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author lev
 */
public class ResponseClashSpeciesList extends GameResponse {
    private List<Species> speciesList;

    public ResponseClashSpeciesList() {
        response_id = NetworkCode.CLASH_SPECIES_LIST;
        speciesList = new ArrayList<Species>();
    }

    public void setSpeciesList(List<Species> list) {
        speciesList = list;
    }

    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        packet.addInt32(speciesList.size());
        for (Species sp : speciesList) {
            packet.addInt32(sp.speciesId);
            packet.addString(sp.name);
            packet.addInt32(sp.price);
            packet.addInt32(sp.type.getValue());
            packet.addString(sp.description);
            packet.addInt32(sp.attackPoints);
            packet.addInt32(sp.hitPoints);
            packet.addInt32(sp.movementSpeed);
            packet.addInt32(sp.attackSpeed);
        }
        return packet.getBytes();
    }
}
