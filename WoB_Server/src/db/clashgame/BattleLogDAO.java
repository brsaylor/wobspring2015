package db.clashgame;

// Java Imports
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

// Other Imports
import model.clashgame.BattleLog;
import util.Log;
import db.GameDB;

/**
 * Table(s) Required: battle_log
 *
 * The AccountDAO class hold methods that can execute a variety of different
 * queries for very specific purposes.
 *
 * @author Abhijit
 */
public final class BattleLogDAO {

    private BattleLogDAO() {
    }

    public static BattleLog createBattle(	  int BattleId
    									, int SpeciesId
    									, String TeamFlag) {
BattleLog battle_log = null;

String query = "INSERT INTO `clash_battle_log` "
			+ "(  `clash_battle_id` "
			+ " , `clash_species_id` "
			+ " , `team_flag` )"
			+ "VALUES "
			+ "(  ? , ? , ? )" ;

Connection con = null;
PreparedStatement pstmt = null;
ResultSet rs = null;

try {
con = GameDB.getConnection();
pstmt = con.prepareStatement(query, Statement.RETURN_GENERATED_KEYS);
pstmt.setInt(1, BattleId);
pstmt.setInt(2, SpeciesId);
pstmt.setString(3, TeamFlag);

pstmt.executeUpdate();

rs = pstmt.getGeneratedKeys();

if (rs.next()) {
battle_log = new BattleLog(   BattleId
							, SpeciesId
							, TeamFlag );
}
} catch (SQLException ex) {
Log.println_e(ex.getMessage());
} finally {
GameDB.closeConnection(con, pstmt, rs);
}

return battle_log;
}

public static BattleLog getBattleLog(int battle_id) {
        
    	BattleLog battle_log = null;

        String query = "SELECT "
        			 + "   `clash_battle_id`"
        			 + " , `clash_species_id` "
        			 + " , `team_flag`"
        			 + "FROM `clash_battle_log` WHERE `battle_id` = ? limit 1";

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(query);
            pstmt.setInt(1, battle_id);
           
            rs = pstmt.executeQuery();

            if (rs.next()) {
                battle_log = new BattleLog();
                battle_log.setBattle_id(rs.getInt("clash_battle_id"));
                battle_log.setSpecies_id(rs.getInt("clash_species_id"));
                battle_log.setTeam_flag(rs.getString("team_flag"));
               
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }

        return battle_log;
    }


}
