package db;

// Java Imports
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;


// Other Imports

import model.clashgame.DefenceConfig;
import util.Log;

/**
 * Table(s) Required: defence_config
 *
 *
 * @author Abhijit
 */

public final class DefenceConfigDAO {

        private DefenceConfigDAO() {
        
        }

        
        
        public static DefenceConfig createDefenceConfig(	  int species_1
        												, int species_1_loc_x
        												, int species_1_loc_y
         												, int species_2
         												, int species_2_loc_x
         												, int species_2_loc_y
         												, int species_3
         												, int species_3_loc_x
         												, int species_3_loc_y
         												, int species_4
         												, int species_4_loc_x
         												, int species_4_loc_y
         												, int species_5
         												, int species_5_loc_x
         												, int species_5_loc_y
         												, int player_id
         												, int terrain_id) {
            DefenceConfig DC = null;

            String query = "INSERT INTO `defence_config` "
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
                pstmt.setInt(2, species_1_loc_x);
                pstmt.setInt(3, species_1_loc_y);
                
                pstmt.setInt(4, species_2);
                pstmt.setInt(5, species_2_loc_x);
                pstmt.setInt(6, species_2_loc_y);
                
                pstmt.setInt(7, species_3);
                pstmt.setInt(8, species_3_loc_x);
                pstmt.setInt(9, species_3_loc_y);
                
                pstmt.setInt(10, species_4);
                pstmt.setInt(11, species_4_loc_x);
                pstmt.setInt(12, species_4_loc_y);
                
                pstmt.setInt(13, species_5);
                pstmt.setInt(14, species_5_loc_x);
                pstmt.setInt(15, species_5_loc_y);
                

                pstmt.setInt(16, player_id);
                pstmt.setInt(17, terrain_id);
                
                pstmt.executeUpdate();

                rs = pstmt.getGeneratedKeys();

                if (rs.next()) {
                    int defence_config_id = rs.getInt(1);
                    DC = new DefenceConfig(  defence_config_id
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

    
        
    public static DefenceConfig getConfig(int player_id) {
        
    	DefenceConfig DC = null;

        String query = "SELECT "
        			+ "    defence_config_id "
        			+ " , `species_1`, `species_1_loc_x`, 'species_1_loc_y` "
        			+ " , `species_2`, `species_2_loc_x`, 'species_2_loc_y`"
        			+ " , `species_3`, `species_3_loc_x`, 'species_3_loc_y`"
        			+ " , `species_4`, `species_4_loc_x`, 'species_4_loc_y`"
        			+ " , `species_5`, `species_5_loc_x`, 'species_5_loc_y`"
        			+ " , `player_id`, `terrain_id`"
        			+ "FROM `defence_config` WHERE `player_id` = ? limit 1";

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(query);
            pstmt.setInt(1, player_id);
           
            rs = pstmt.executeQuery();

            if (rs.next()) {
            	
                DC = new DefenceConfig();
                
                DC.setDefence_config_id(rs.getInt("defence_config_id"));
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
