package model.clashgame;

import java.sql.Date;

/**
 *
 * @author Abhijit
 */
public class Battle {
    
	private int battleId;
	private int attackConfigId;
	private int defenseConfigId;
	private Date battleStart;
	private String outcome;

    public Battle(int battleId, int attackConfigId, int defenseConfigId, Date battleStart, String outcome) {
        this.battleId = battleId;
        this.attackConfigId = attackConfigId;
        this.defenseConfigId = defenseConfigId;
        this.battleStart = battleStart;
        this.outcome = outcome;
    }

    int getBattleId() { return battleId; }
    int getAttackConfigId() { return attackConfigId; }
    int getDefenseConfigId() { return defenseConfigId; }
    Date getBattleStart() { return battleStart; }
    String getOutcome() { return outcome; }
}
