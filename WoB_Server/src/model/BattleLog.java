package model;

/**
 *
 * @author Abhijit
 */
public class BattleLog {
    
	private int battle_id;
	private int species_id;
	private String team_flag;
		
	/**
	 * @param battle_id
	 * @param species_id
	 * @param team_flag
	 */
	public BattleLog(int battle_id, int species_id, String team_flag) {
		super();
		this.battle_id = battle_id;
		this.species_id = species_id;
		this.team_flag = team_flag;
	}
	public BattleLog() {
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
	 * @return the species_id
	 */
	public final int getSpecies_id() {
		return species_id;
	}
	/**
	 * @param species_id the species_id to set
	 */
	public final void setSpecies_id(int species_id) {
		this.species_id = species_id;
	}
	/**
	 * @return the team_flag
	 */
	public final String getTeam_flag() {
		return team_flag;
	}
	/**
	 * @param team_flag the team_flag to set
	 */
	public final void setTeam_flag(String team_flag) {
		this.team_flag = team_flag;
	}
	
	
    

}
