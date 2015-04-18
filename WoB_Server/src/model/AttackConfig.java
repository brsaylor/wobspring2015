package model;



/**
*
* @author Abhijit
*/

public class AttackConfig {

    private int attack_config_id;
    private int species_1;
    private int species_1_loc_x;
    private int species_1_loc_y;
    private int species_2;
    private int species_2_loc_x;
    private int species_2_loc_y;
    private int species_3;
    private int species_3_loc_x;
    private int species_3_loc_y;
    private int species_4;
    private int species_4_loc_x;
    private int species_4_loc_y;
    private int species_5;
    private int species_5_loc_x;
    private int species_5_loc_y;
    private int player_id;
    private int terrain_id;
    /* 
     * private date creation_date; 
     * 
     */
	/**
	 * @param attack_config_id
	 * @param species_1
	 * @param species_1_loc_x
	 * @param species_1_loc_y
	 * @param species_2
	 * @param species_2_loc_x
	 * @param species_2_loc_y
	 * @param species_3
	 * @param species_3_loc_x
	 * @param species_3_loc_y
	 * @param species_4
	 * @param species_4_loc_x
	 * @param species_4_loc_y
	 * @param species_5
	 * @param species_5_loc_x
	 * @param species_5_loc_y
	 * @param player_id
	 * @param terrain_id
	 */
	public AttackConfig(int attack_config_id, 
			int species_1, int species_1_loc_x, int species_1_loc_y, 
			int species_2, int species_2_loc_x, int species_2_loc_y, 
			int species_3, int species_3_loc_x, int species_3_loc_y, 
			int species_4, int species_4_loc_x, int species_4_loc_y, 
			int species_5, int species_5_loc_x, int species_5_loc_y, 
			int player_id, int terrain_id) {
		super();
		this.attack_config_id = attack_config_id;
		this.species_1 = species_1;
		this.species_1_loc_x = species_1_loc_x;
		this.species_1_loc_y = species_1_loc_y;
		this.species_2 = species_2;
		this.species_2_loc_x = species_2_loc_x;
		this.species_2_loc_y = species_2_loc_y;
		this.species_3 = species_3;
		this.species_3_loc_x = species_3_loc_x;
		this.species_3_loc_y = species_3_loc_y;
		this.species_4 = species_4;
		this.species_4_loc_x = species_4_loc_x;
		this.species_4_loc_y = species_4_loc_y;
		this.species_5 = species_5;
		this.species_5_loc_x = species_5_loc_x;
		this.species_5_loc_y = species_5_loc_y;
		this.player_id = player_id;
		this.terrain_id = terrain_id;
	}    

	public AttackConfig() {
		// TODO Auto-generated constructor stub
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
	 * @return the species_1
	 */
	public final int getSpecies_1() {
		return species_1;
	}

	/**
	 * @param species_1 the species_1 to set
	 */
	public final void setSpecies_1(int species_1) {
		this.species_1 = species_1;
	}

	/**
	 * @return the species_1_loc_x
	 */
	public final int getSpecies_1_loc_x() {
		return species_1_loc_x;
	}

	/**
	 * @param species_1_loc_x the species_1_loc_x to set
	 */
	public final void setSpecies_1_loc_x(int species_1_loc_x) {
		this.species_1_loc_x = species_1_loc_x;
	}

	/**
	 * @return the species_1_loc_y
	 */
	public final int getSpecies_1_loc_y() {
		return species_1_loc_y;
	}

	/**
	 * @param species_1_loc_y the species_1_loc_y to set
	 */
	public final void setSpecies_1_loc_y(int species_1_loc_y) {
		this.species_1_loc_y = species_1_loc_y;
	}

	/**
	 * @return the species_2
	 */
	public final int getSpecies_2() {
		return species_2;
	}

	/**
	 * @param species_2 the species_2 to set
	 */
	public final void setSpecies_2(int species_2) {
		this.species_2 = species_2;
	}

	/**
	 * @return the species_2_loc_x
	 */
	public final int getSpecies_2_loc_x() {
		return species_2_loc_x;
	}

	/**
	 * @param species_2_loc_x the species_2_loc_x to set
	 */
	public final void setSpecies_2_loc_x(int species_2_loc_x) {
		this.species_2_loc_x = species_2_loc_x;
	}

	/**
	 * @return the species_2_loc_y
	 */
	public final int getSpecies_2_loc_y() {
		return species_2_loc_y;
	}

	/**
	 * @param species_2_loc_y the species_2_loc_y to set
	 */
	public final void setSpecies_2_loc_y(int species_2_loc_y) {
		this.species_2_loc_y = species_2_loc_y;
	}

	/**
	 * @return the species_3
	 */
	public final int getSpecies_3() {
		return species_3;
	}

	/**
	 * @param species_3 the species_3 to set
	 */
	public final void setSpecies_3(int species_3) {
		this.species_3 = species_3;
	}

	/**
	 * @return the species_3_loc_x
	 */
	public final int getSpecies_3_loc_x() {
		return species_3_loc_x;
	}

	/**
	 * @param species_3_loc_x the species_3_loc_x to set
	 */
	public final void setSpecies_3_loc_x(int species_3_loc_x) {
		this.species_3_loc_x = species_3_loc_x;
	}

	/**
	 * @return the species_3_loc_y
	 */
	public final int getSpecies_3_loc_y() {
		return species_3_loc_y;
	}

	/**
	 * @param species_3_loc_y the species_3_loc_y to set
	 */
	public final void setSpecies_3_loc_y(int species_3_loc_y) {
		this.species_3_loc_y = species_3_loc_y;
	}

	/**
	 * @return the species_4
	 */
	public final int getSpecies_4() {
		return species_4;
	}

	/**
	 * @param species_4 the species_4 to set
	 */
	public final void setSpecies_4(int species_4) {
		this.species_4 = species_4;
	}

	/**
	 * @return the species_4_loc_x
	 */
	public final int getSpecies_4_loc_x() {
		return species_4_loc_x;
	}

	/**
	 * @param species_4_loc_x the species_4_loc_x to set
	 */
	public final void setSpecies_4_loc_x(int species_4_loc_x) {
		this.species_4_loc_x = species_4_loc_x;
	}

	/**
	 * @return the species_4_loc_y
	 */
	public final int getSpecies_4_loc_y() {
		return species_4_loc_y;
	}

	/**
	 * @param species_4_loc_y the species_4_loc_y to set
	 */
	public final void setSpecies_4_loc_y(int species_4_loc_y) {
		this.species_4_loc_y = species_4_loc_y;
	}

	/**
	 * @return the species_5
	 */
	public final int getSpecies_5() {
		return species_5;
	}

	/**
	 * @param species_5 the species_5 to set
	 */
	public final void setSpecies_5(int species_5) {
		this.species_5 = species_5;
	}

	/**
	 * @return the species_5_loc_x
	 */
	public final int getSpecies_5_loc_x() {
		return species_5_loc_x;
	}

	/**
	 * @param species_5_loc_x the species_5_loc_x to set
	 */
	public final void setSpecies_5_loc_x(int species_5_loc_x) {
		this.species_5_loc_x = species_5_loc_x;
	}

	/**
	 * @return the species_5_loc_y
	 */
	public final int getSpecies_5_loc_y() {
		return species_5_loc_y;
	}

	/**
	 * @param species_5_loc_y the species_5_loc_y to set
	 */
	public final void setSpecies_5_loc_y(int species_5_loc_y) {
		this.species_5_loc_y = species_5_loc_y;
	}

	/**
	 * @return the player_id
	 */
	public final int getPlayer_id() {
		return player_id;
	}

	/**
	 * @param player_id the player_id to set
	 */
	public final void setPlayer_id(int player_id) {
		this.player_id = player_id;
	}

	/**
	 * @return the terrain_id
	 */
	public final int getTerrain_id() {
		return terrain_id;
	}

	/**
	 * @param terrain_id the terrain_id to set
	 */
	public final void setTerrain_id(int terrain_id) {
		this.terrain_id = terrain_id;
	}



    
}
