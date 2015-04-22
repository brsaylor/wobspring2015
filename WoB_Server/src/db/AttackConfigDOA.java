package db;

// Java Imports
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;



// Other Imports
import model.clashgame.AttackConfig;
import util.Log;

/**
 * Table(s) Required: attack_config
 *
 * The AccountDAO class hold methods that can execute a variety of different
 * queries for very specific purposes.
 *
 * @author Abhijit
 */

public final class AttackConfigDOA {

    private AttackConfigDOA() {
    
    }

    
    
    public static AttackConfig createAttackConfig(	  int species_1
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
        AttackConfig AC = null;

        String query = "INSERT INTO `attack_config` "
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
                int attack_config_id = rs.getInt(1);
                AC = new AttackConfig(  attack_config_id
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

        return AC;
    }

    
public static AttackConfig getConfig(int player_id) {
        
    	AttackConfig AC = null;

        String query = "SELECT "
        			+ "   `attack_config_id` "
        			+ " , `species_1`, `species_1_loc_x`, 'species_1_loc_y` "
        			+ " , `species_2`, `species_2_loc_x`, 'species_2_loc_y`"
        			+ " , `species_3`, `species_3_loc_x`, 'species_3_loc_y`"
        			+ " , `species_4`, `species_4_loc_x`, 'species_4_loc_y`"
        			+ " , `species_5`, `species_5_loc_x`, 'species_5_loc_y`"
        			+ " , `player_id`, `terrain_id` "
        			+ "FROM `attack_config` WHERE `player_id` = ? limit 1";

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(query);
            pstmt.setInt(1, player_id);
           
            rs = pstmt.executeQuery();

            if (rs.next()) {
            	
                AC = new AttackConfig();
                
                AC.setAttack_config_id(rs.getInt("attack_config_id"));
                AC.setPlayer_id(rs.getInt("player_id"));
                AC.setTerrain_id(rs.getInt("terrain_id"));
                
                AC.setSpecies_1(rs.getInt("species_1"));
                AC.setSpecies_1_loc_x(rs.getInt("species_1_loc_x"));
                AC.setSpecies_1_loc_y(rs.getInt("species_1_loc_y"));
                
                AC.setSpecies_2(rs.getInt("species_2"));
                AC.setSpecies_2_loc_x(rs.getInt("species_2_loc_x"));
                AC.setSpecies_2_loc_y(rs.getInt("species_2_loc_y"));
                
                AC.setSpecies_3(rs.getInt("species_3"));
                AC.setSpecies_3_loc_x(rs.getInt("species_3_loc_x"));
                AC.setSpecies_3_loc_y(rs.getInt("species_3_loc_y"));
                
                AC.setSpecies_4(rs.getInt("species_4"));
                AC.setSpecies_4_loc_x(rs.getInt("species_4_loc_x"));
                AC.setSpecies_4_loc_y(rs.getInt("species_4_loc_y"));
                
                AC.setSpecies_5(rs.getInt("species_5"));
                AC.setSpecies_5_loc_x(rs.getInt("species_5_loc_x"));
                AC.setSpecies_5_loc_y(rs.getInt("species_5_loc_y"));
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }

        return AC;
    }

}
