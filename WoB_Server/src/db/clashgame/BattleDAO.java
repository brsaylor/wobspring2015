package db.clashgame;

// Java Imports
import java.sql.Connection;
import java.sql.Date;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

// Other Imports
import model.clashgame.Battle;
import util.Log;
import db.GameDB;

/**
 * Table(s) Required: clash_battle
 *
 * The BattleDAO class hold methods that can execute a variety of different
 * queries for very specific purposes.
 *
 * @author Abhijit
 */
public final class BattleDAO {

    private BattleDAO() {
    }

    public static Battle createBattle(	  int AttackConfigId
    									, int DefenceConfigId
    									, Date BattleStart
    									, String BattleOutcome) {
Battle battle = null;

String query = "INSERT INTO `battle` "
			+ "(  `clash_attack_config_id` "
			+ " , `clash_defense_config_id` "
			+ " , `battle_start`"
			+ " , `battle_outcome` )"
			+ "VALUES "
			+ "(  ? , ? , ? , ? )" ;

Connection con = null;
PreparedStatement pstmt = null;
ResultSet rs = null;

try {
con = GameDB.getConnection();
pstmt = con.prepareStatement(query, Statement.RETURN_GENERATED_KEYS);
pstmt.setInt(1, AttackConfigId);
pstmt.setInt(2, DefenceConfigId);
pstmt.setDate(3, BattleStart);
pstmt.setString(4, BattleOutcome);

pstmt.executeUpdate();

rs = pstmt.getGeneratedKeys();

if (rs.next()) {
int BattleId = rs.getInt(1);
battle = new Battle(  BattleId
					, AttackConfigId
					, DefenceConfigId
					, BattleStart
					, BattleOutcome );
}
} catch (SQLException ex) {
Log.println_e(ex.getMessage());
} finally {
GameDB.closeConnection(con, pstmt, rs);
}

return battle;
}

public static Battle getBattle(int battle_id) {
        
    	Battle battle = null;

        String query = "SELECT "
        			+ "	`clash_battle_id`"
        			+ " , `clash_attack_config_id` "
        			+ "	, `clash_defense_config_id`"
        			+ " , `battle_start`"
        			+ " , `battle_outcome"
        			+ "FROM `clash_battle` WHERE `battle_id` = ? limit 1";

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(query);
            pstmt.setInt(1, battle_id);
           
            rs = pstmt.executeQuery();

            if (rs.next()) {
                battle = new Battle();
                battle.setAttack_config_id(rs.getInt("clash_attack_config_id"));
                battle.setDefence_config_id(rs.getInt("clash_defense_config_id"));
                battle.setBattle_id(rs.getInt("clash_battle_id"));
                battle.setBattle_start(rs.getDate("battle_start"));
                battle.setBattle_outcome(rs.getString("battle_outcome"));
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }

        return battle;
    }


}
