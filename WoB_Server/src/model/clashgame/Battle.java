package model.clashgame;

import java.sql.Date;

/**
 *
 * @author Abhijit
 */
public class Battle {
    
	private int battle_id;
	private int attack_config_id;
	private int defence_config_id;
	private Date battle_start;
	private String battle_outcome;
    
	/**
	 * @param battle_id
	 * @param attack_config_id
	 * @param defence_config_id
	 * @param battle_start
	 * @param battle_outcome
	 */
	public Battle(int battle_id, int attack_config_id, int defence_config_id,
			Date battle_start, String battle_outcome) {
		super();
		this.battle_id = battle_id;
		this.attack_config_id = attack_config_id;
		this.defence_config_id = defence_config_id;
		this.battle_start = battle_start;
		this.battle_outcome = battle_outcome;
	}

	public Battle() {
		// TODO Auto-generated constructor stub
	}

	/**
	 * @return the battle_id
	 */
	public final int getBattle_id() {
		return battle_id;
	}
	
	/**
	 * @param battle_id the battle_id to set
	 */
	public final void setBattle_id(int battle_id) {
		this.battle_id = battle_id;
	}
	/**
	 * @return the attack_config_id
	 */
	public final int getAttack_config_id() {
		return attack_config_id;
	}
	/**
	 * @param attack_config_id the attack_config_id to set
	 */
	public final void setAttack_config_id(int attack_config_id) {
		this.attack_config_id = attack_config_id;
	}
	/**
	 * @return the defence_config_id
	 */
	public final int getDefence_config_id() {
		return defence_config_id;
	}
	/**
	 * @param defence_config_id the defence_config_id to set
	 */
	public final void setDefence_config_id(int defence_config_id) {
		this.defence_config_id = defence_config_id;
	}

	/**
	 * @return the battle_start
	 */
	public final Date getBattle_start() {
		return battle_start;
	}

	/**
	 * @param battle_start the battle_start to set
	 */
	public final void setBattle_start(Date battle_start) {
		this.battle_start = battle_start;
	}

	/**
	 * @return the battle_outcome
	 */
	public final String getBattle_outcome() {
		return battle_outcome;
	}

	/**
	 * @param battle_outcome the battle_outcome to set
	 */
	public final void setBattle_outcome(String battle_outcome) {
		this.battle_outcome = battle_outcome;
	}

	
	
	
	
    
    

}
