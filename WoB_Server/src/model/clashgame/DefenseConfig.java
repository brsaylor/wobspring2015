package model.clashgame;

import util.Vector2;

import java.util.Date;
import java.util.HashMap;

/**
*
* @author Abhijit
*/

public class DefenseConfig {
    public int id;
    public int playerId;
    public int terrainId;
    public HashMap<Integer, Vector2<Float>> layout = new HashMap<Integer, Vector2<Float>>();
    public Date createdAt = new Date();
}
