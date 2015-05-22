/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;


import metadata.NetworkCode;
import model.clashgame.DefenseConfig;
import model.clashgame.Player;
import net.response.GameResponse;
import util.GamePacket;
import util.Vector2;

/**
 * Container for the Clash of Species-related data on a specific
 * player to be sent back to the client
 * @author lev
 */
public class ResponseClashPlayerView extends GameResponse{

    /**
     * The defense setup of the requested player
     */
    private DefenseConfig defenseConfig = null;

    //private Player player = null;

    public ResponseClashPlayerView(){
        response_id = NetworkCode.CLASH_PLAYER_VIEW;
    }

    public void setDefenseConfig(DefenseConfig dc) {
        this.defenseConfig = dc;
    }

    /*
    public void setPlayer(Player pl) {
        this.player = pl;
    }//*/

    /**
     * <p>
     * Generates a byte array in the following format:
     * </p>
     * <p>
     * id of this response (short)
     * </p>
     * <p>
     * id of defense config (int)
     *  </p>
     * <p>
     *  name of terrain in defense config (string)
     *  </p>
     * <p>
     *  id of player requested (int)
     *  </p>
     * <p>
     *  timestamp for the defense config (string)
     *  </p>
     * <p>
     *  # of species in config (int)
     *  </p>
     * <p>
     *     for each species in config
     *      </p>
     * <p>
     *      species id (int)
     *      </p>
     * <p>
     *      instance count(int)
     *      </p>
     * <p>
     *      for each instance
     *    </p>
     * <p>
     *    x-coordinate (float)
     *    </p>
     * <p>
     *    y-coordinate (float)
     *    </p>
     * @return the byte array
     */
    @Override
    public byte[] getBytes() {
        GamePacket packet = new GamePacket(response_id);
        if (defenseConfig != null) {
            packet.addInt32(defenseConfig.id);
            packet.addString(defenseConfig.terrain);
            packet.addInt32(defenseConfig.playerId);
            //System.out.println("xxx " + defenseConfig.createdAt.getTime());
            packet.addString("" + defenseConfig.createdAt.getTime());
            packet.addInt32(defenseConfig.layout.size());
            for (Map.Entry<Integer, ArrayList<Vector2<Float>>> ent : defenseConfig.layout.entrySet()) {
                packet.addInt32(ent.getKey());
                ArrayList<Vector2<Float>> positions = ent.getValue();
                packet.addInt32(positions.size());
                for(Vector2<Float> v : positions) {
                    packet.addFloat(v.getX());
                    packet.addFloat(v.getY());
                }
            }
        }
        return packet.getBytes();
    }
    
}
