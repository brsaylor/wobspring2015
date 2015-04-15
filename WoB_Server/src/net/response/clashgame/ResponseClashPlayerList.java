/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.response.clashgame;

import metadata.NetworkCode;
import net.response.ResponsePlayers;

/**
 *
 * @author lev
 */
public class ResponseClashPlayerList extends ResponsePlayers{
    public ResponseClashPlayerList(){
        response_id = NetworkCode.CLASH_PLAYER_LIST;
    }
    
}
