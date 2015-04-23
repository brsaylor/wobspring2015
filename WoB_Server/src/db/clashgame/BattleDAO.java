package db.clashgame;

// Java Imports
import java.sql.*;

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

    private static final String INSERT_QUERY = "INSERT INTO `clash_battle`"
        + "(`clash_attack_config_id`, `clash_defense_config_id`, `time_started`)"
        + "VALUES (?, ?, ?)";

    private BattleDAO() {}

    public static Battle create(Battle battle) {

        try (
            Connection con = GameDB.getConnection();
            PreparedStatement pstmt = con.prepareStatement(INSERT_QUERY, Statement.RETURN_GENERATED_KEYS);
        ) {
            pstmt.setInt(1, battle.attackConfigId);
            pstmt.setInt(2, battle.defenseConfigId);
            pstmt.setDate(3, battle.battleStart);

            pstmt.executeUpdate();

            try (ResultSet rs = pstmt.getGeneratedKeys();) {
                if (rs.next()) {
                    battle.id = rs.getInt(1);
                } else {
                    throw new SQLException("Failed to create Battle.");
                }
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        }

        return battle;
    }
}
