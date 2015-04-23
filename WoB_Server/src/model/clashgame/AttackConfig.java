package model.clashgame;

import java.util.List;
import java.util.ArrayList;
import java.util.Date;

/**
 *
 * @author Abhijit, Ben
 */
public class AttackConfig {

    public int attackConfigId;
    public int playerId;
    public int terrainId;
    public ArrayList<Integer> speciesList;
    public Date dateCreated;

    public AttackConfig() {
    }
}