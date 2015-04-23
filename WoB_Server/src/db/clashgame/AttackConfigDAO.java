package db.clashgame;

// Java Imports
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import db.GameDB;
import java.util.ArrayList;
import java.util.Date;

// Other Imports
import model.clashgame.AttackConfig;
import util.Log;

/**
 * Table(s) Required: clash_attack_config
 *
 * The AccountDAO class hold methods that can execute a variety of different
 * queries for very specific purposes.
 *
 * @author Abhijit, Ben
 */

public final class AttackConfigDAO {

    private AttackConfigDAO() {
    }
    
    public static AttackConfig createAttackConfig(int playerId, int terrainId,
            ArrayList<Integer> speciesList) {
        
        AttackConfig AC = null;

        String query = "INSERT INTO `clash_attack_config` "
                + "(player_id, terrain_id, "
                + " species_1, species_2, species_3, species_4, species_5,"
                + " date_created) "
                + "VALUES "
                + "(?, ?, ?, ?, ?, ?, ?, ?, ?)";

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;
        
        Date dateCreated = new java.util.Date();

        try {
            con = GameDB.getConnection();
            
            pstmt = con.prepareStatement(query, Statement.RETURN_GENERATED_KEYS);
            pstmt.setInt(1, playerId);
            pstmt.setInt(2, terrainId);
            
            pstmt.setInt(3, speciesList.get(0));
            pstmt.setInt(4, speciesList.get(1));
            pstmt.setInt(5, speciesList.get(2));
            pstmt.setInt(6, speciesList.get(3));
            pstmt.setInt(7, speciesList.get(4));
            
            pstmt.setDate(8, new java.sql.Date(dateCreated.getTime()));
            
            pstmt.executeUpdate();

            rs = pstmt.getGeneratedKeys();

            if (rs.next()) {
                AC = new AttackConfig();
                AC.attackConfigId = rs.getInt(1);
                AC.dateCreated = dateCreated;
                AC.playerId = playerId;
                AC.terrainId = terrainId;
                AC.speciesList = speciesList;
            }
            
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }

        return AC;
    }

public static AttackConfig getConfig(int playerId) {
        
    	AttackConfig AC = null;

        String query = "SELECT "
        			+ "   `clash_attack_config_id` "
        			+ " , `species_1`, "
        			+ " , `species_2`, "
        			+ " , `species_3`, "
        			+ " , `species_4`, "
        			+ " , `species_5`, "
        			+ " , `player_id`, `terrain_id`, date_created "
        			+ "FROM `clash_attack_config` WHERE `player_id` = ? order by date_created desc limit 1";

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(query);
            pstmt.setInt(1, playerId);
           
            rs = pstmt.executeQuery();

            if (rs.next()) {
            	
                AC = new AttackConfig();
                AC.attackConfigId = rs.getInt("clash_attack_config_id");
                AC.playerId = rs.getInt("player_id");
                AC.terrainId = rs.getInt("terrain_id");
                AC.dateCreated = rs.getDate("date_created");
                
                AC.speciesList = new ArrayList();
                AC.speciesList.add(rs.getInt("species_1"));
                AC.speciesList.add(rs.getInt("species_2"));
                AC.speciesList.add(rs.getInt("species_3"));
                AC.speciesList.add(rs.getInt("species_4"));
                AC.speciesList.add(rs.getInt("species_5"));
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }

        return AC;
    }

}
