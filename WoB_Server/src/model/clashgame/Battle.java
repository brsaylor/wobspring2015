package model.clashgame;

import java.sql.Date;

/**
 *
 * @author Abhijit
 */
public class Battle {
	public int id;
	public int attackConfigId;
	public int defenseConfigId;
	public Date battleStart;
	public String outcome;
}
