package db.clashgame;
import db.GameDB;

// Java Imports
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;


// Other Imports

import model.clashgame.DefenseConfig;
import util.Log;

/**
 * Table(s) Required: defence_config
 *
 *
 * @author Abhijit
 */

public final class DefenseConfigDAO {

        private DefenseConfigDAO() {
        
        }

        
        
        public static DefenseConfig createDefenseConfig(	  int species_1
        												, float species_1_loc_x
        												, float species_1_loc_y
         												, int species_2
         												, float species_2_loc_x
         												, float species_2_loc_y
         												, int species_3
         												, float species_3_loc_x
         												, float species_3_loc_y
         												, int species_4
         												, float species_4_loc_x
         												, float species_4_loc_y
         												, int species_5
         												, float species_5_loc_x
         												, float species_5_loc_y
         												, int player_id
         												, int terrain_id) {
            DefenseConfig DC = null;

            String query = "INSERT INTO `clash_defense_config` "
            						+ "(  `species_1`, `species_1_loc_x`, 'species_1_loc_y`"
            						+ " , `species_2`, `species_2_loc_x`, 'species_2_loc_y`"
            						+ " , `species_3`, `species_3_loc_x`, 'species_3_loc_y`"
            						+ " , `species_4`, `species_4_loc_x`, 'species_4_loc_y`"
            						+ " , `species_5`, `species_5_loc_x`, 'species_5_loc_y`"
            						+ " , `player_id`, `terrain_id`) "
            						+ "VALUES "
            						+ "(  ? , ? , ?"
            						+ "  ,? , ? , ?"
            						+ "  ,? , ? , ?"
            						+ "  ,? , ? , ?"
            						+ "  ,? , ? , ?"
            						+ "  ,? ,?)";

            Connection con = null;
            PreparedStatement pstmt = null;
            ResultSet rs = null;

            try {
                con = GameDB.getConnection();
                pstmt = con.prepareStatement(query, Statement.RETURN_GENERATED_KEYS);
                pstmt.setInt(1, species_1);
                pstmt.setFloat(2, species_1_loc_x);
                pstmt.setFloat(3, species_1_loc_y);
                
                pstmt.setInt(4, species_2);
                pstmt.setFloat(5, species_2_loc_x);
                pstmt.setFloat(6, species_2_loc_y);
                
                pstmt.setInt(7, species_3);
                pstmt.setFloat(8, species_3_loc_x);
                pstmt.setFloat(9, species_3_loc_y);
                
                pstmt.setInt(10, species_4);
                pstmt.setFloat(11, species_4_loc_x);
                pstmt.setFloat(12, species_4_loc_y);
                
                pstmt.setInt(13, species_5);
                pstmt.setFloat(14, species_5_loc_x);
                pstmt.setFloat(15, species_5_loc_y);
                

                pstmt.setInt(16, player_id);
                pstmt.setInt(17, terrain_id);
                
                pstmt.executeUpdate();

                rs = pstmt.getGeneratedKeys();

                if (rs.next()) {
                    int defence_config_id = rs.getInt(1);
                    DC = new DefenseConfig(  defence_config_id
                    			, species_1
                    			, species_1_loc_x
                    			, species_1_loc_y
    							, species_2
    							, species_2_loc_x
    							, species_2_loc_y
    							, species_3
    							, species_3_loc_x
    							, species_3_loc_y
    							, species_4
    							, species_4_loc_x
    							, species_4_loc_y
    							, species_5
    							, species_5_loc_x
    							, species_5_loc_y
    							, player_id
    							, terrain_id                		
                    			);
                }
            } catch (SQLException ex) {
                Log.println_e(ex.getMessage());
            } finally {
                GameDB.closeConnection(con, pstmt, rs);
            }

            return DC;
        }

    
        
    public static DefenseConfig getConfig(int player_id) {
        
    	DefenseConfig DC = null;

        String query = "SELECT "
        			+ "    clash_defense_config_id "
        			+ " , `species_1`, `species_1_loc_x`, 'species_1_loc_y` "
        			+ " , `species_2`, `species_2_loc_x`, 'species_2_loc_y`"
        			+ " , `species_3`, `species_3_loc_x`, 'species_3_loc_y`"
        			+ " , `species_4`, `species_4_loc_x`, 'species_4_loc_y`"
        			+ " , `species_5`, `species_5_loc_x`, 'species_5_loc_y`"
        			+ " , `player_id`, `terrain_id`"
        			+ "FROM `clash_defense_config` WHERE `player_id` = ? limit 1";

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(query);
            pstmt.setInt(1, player_id);
           
            rs = pstmt.executeQuery();

            if (rs.next()) {
            	
                DC = new DefenseConfig();
                
                DC.setDefence_config_id(rs.getInt("clash_defense_config_id"));
                DC.setPlayer_id(rs.getInt("player_id"));
                DC.setTerrain_id(rs.getInt("terrain_id"));
                
                DC.setSpecies_1(rs.getInt("species_1"));
                DC.setSpecies_1_loc_x(rs.getInt("species_1_loc_x"));
                DC.setSpecies_1_loc_y(rs.getInt("species_1_loc_y"));
                
                DC.setSpecies_2(rs.getInt("species_2"));
                DC.setSpecies_2_loc_x(rs.getInt("species_2_loc_x"));
                DC.setSpecies_2_loc_y(rs.getInt("species_2_loc_y"));
                
                DC.setSpecies_3(rs.getInt("species_3"));
                DC.setSpecies_3_loc_x(rs.getInt("species_3_loc_x"));
                DC.setSpecies_3_loc_y(rs.getInt("species_3_loc_y"));
                
                DC.setSpecies_4(rs.getInt("species_4"));
                DC.setSpecies_4_loc_x(rs.getInt("species_4_loc_x"));
                DC.setSpecies_4_loc_y(rs.getInt("species_4_loc_y"));
                
                DC.setSpecies_5(rs.getInt("species_5"));
                DC.setSpecies_5_loc_x(rs.getInt("species_5_loc_x"));
                DC.setSpecies_5_loc_y(rs.getInt("species_5_loc_y"));
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }

        return DC;
    }

    } /* Class end*/
