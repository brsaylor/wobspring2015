package model.clashgame;

/**
*
* @author Abhijit
*/

public class DefenseConfig {

    private int defence_config_id;
    private int species_1;
    private float species_1_loc_x;
    private float species_1_loc_y;
    private int species_2;
    private float species_2_loc_x;
    private float species_2_loc_y;
    private int species_3;
    private float species_3_loc_x;
    private float species_3_loc_y;
    private int species_4;
    private float species_4_loc_x;
    private float species_4_loc_y;
    private int species_5;
    private float species_5_loc_x;
    private float species_5_loc_y;
    private int player_id;
    private int terrain_id;
    /* 
     * private date creation_date; 
     * 
     */
	/**
	 * @param defence_config_id
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
	public DefenseConfig(int defence_config_id, 
			int species_1, float species_1_loc_x, float species_1_loc_y,
			int species_2, float species_2_loc_x, float species_2_loc_y,
			int species_3, float species_3_loc_x, float species_3_loc_y,
			int species_4, float species_4_loc_x, float species_4_loc_y,
			int species_5, float species_5_loc_x, float species_5_loc_y,
			int player_id, int terrain_id) {
		super();
		this.defence_config_id = defence_config_id;
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

	public DefenseConfig() {
		// TODO Auto-generated constructor stub
	}

	public void addSpecies(int _id, float x, float y){
		if(species_1 == 0){
			species_1 = _id;
			species_1_loc_x = x;
			species_1_loc_y = y;
		}else if(species_2 == 0){
			species_2 = _id;
			species_2_loc_x = x;
			species_2_loc_y = y;
		}if(species_3 == 0){
			species_3 = _id;
			species_3_loc_x = x;
			species_3_loc_y = y;
		}if(species_4 == 0){
			species_4 = _id;
			species_4_loc_x = x;
			species_4_loc_y = y;
		}if(species_5 == 0){
			species_5 = _id;
			species_5_loc_x = x;
			species_5_loc_y = y;
		}else{
			//can't add anymore
		}
	}

	/**
	 * @return the attack_config_id
	 */
	public final int getDefence_config_id() {
		return defence_config_id;
	}

	/**
	 * @param defence_config_id the attack_config_id to set
	 */
	public final void setDefence_config_id(int defence_config_id) {
		this.defence_config_id = defence_config_id;
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
	public final float getSpecies_1_loc_x() {
		return species_1_loc_x;
	}

	/**
	 * @param species_1_loc_x the species_1_loc_x to set
	 */
	public final void setSpecies_1_loc_x(float species_1_loc_x) {
		this.species_1_loc_x = species_1_loc_x;
	}

	/**
	 * @return the species_1_loc_y
	 */
	public final float getSpecies_1_loc_y() {
		return species_1_loc_y;
	}

	/**
	 * @param species_1_loc_y the species_1_loc_y to set
	 */
	public final void setSpecies_1_loc_y(float species_1_loc_y) {
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
	public final float getSpecies_2_loc_x() {
		return species_2_loc_x;
	}

	/**
	 * @param species_2_loc_x the species_2_loc_x to set
	 */
	public final void setSpecies_2_loc_x(float species_2_loc_x) {
		this.species_2_loc_x = species_2_loc_x;
	}

	/**
	 * @return the species_2_loc_y
	 */
	public final float getSpecies_2_loc_y() {
		return species_2_loc_y;
	}

	/**
	 * @param species_2_loc_y the species_2_loc_y to set
	 */
	public final void setSpecies_2_loc_y(float species_2_loc_y) {
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
	public final float getSpecies_3_loc_x() {
		return species_3_loc_x;
	}

	/**
	 * @param species_3_loc_x the species_3_loc_x to set
	 */
	public final void setSpecies_3_loc_x(float species_3_loc_x) {
		this.species_3_loc_x = species_3_loc_x;
	}

	/**
	 * @return the species_3_loc_y
	 */
	public final float getSpecies_3_loc_y() {
		return species_3_loc_y;
	}

	/**
	 * @param species_3_loc_y the species_3_loc_y to set
	 */
	public final void setSpecies_3_loc_y(float species_3_loc_y) {
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
	public final float getSpecies_4_loc_x() {
		return species_4_loc_x;
	}

	/**
	 * @param species_4_loc_x the species_4_loc_x to set
	 */
	public final void setSpecies_4_loc_x(float species_4_loc_x) {
		this.species_4_loc_x = species_4_loc_x;
	}

	/**
	 * @return the species_4_loc_y
	 */
	public final float getSpecies_4_loc_y() {
		return species_4_loc_y;
	}

	/**
	 * @param species_4_loc_y the species_4_loc_y to set
	 */
	public final void setSpecies_4_loc_y(float species_4_loc_y) {
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
	public final float getSpecies_5_loc_x() {
		return species_5_loc_x;
	}

	/**
	 * @param species_5_loc_x the species_5_loc_x to set
	 */
	public final void setSpecies_5_loc_x(float species_5_loc_x) {
		this.species_5_loc_x = species_5_loc_x;
	}

	/**
	 * @return the species_5_loc_y
	 */
	public final float getSpecies_5_loc_y() {
		return species_5_loc_y;
	}

	/**
	 * @param species_5_loc_y the species_5_loc_y to set
	 */
	public final void setSpecies_5_loc_y(float species_5_loc_y) {
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
