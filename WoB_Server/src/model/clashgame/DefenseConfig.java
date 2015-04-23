package model.clashgame;

import util.Vector2;

import java.sql.Timestamp;
import java.util.HashMap;

/**
*
* @author Abhijit
*/

public class DefenseConfig {
    public int id;
    public int playerId;
    public int terrainId;
    public HashMap<Integer, Vector2<Float>> layout;
    public Timestamp createdAt;
}
