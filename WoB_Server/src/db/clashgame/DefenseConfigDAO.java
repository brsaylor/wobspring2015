package db.clashgame;
import db.GameDB;

// Java Imports
import java.sql.*;
import java.util.*;
import java.util.Date;


// Other Imports

import model.clashgame.DefenseConfig;
import util.Log;
import util.Vector2;

/**
 * Table(s) Required: defence_config
 *
 *
 * @author Abhijit
 */

public final class DefenseConfigDAO {

    private static final String INSERT_QUERY = "INSERT INTO `clash_defense_config`"
        + "(`species1`, `species1_x`, `species1_y`, "
        + " `species2`, `species2_x`, `species2_y`, "
        + " `species3`, `species3_x`, `species3_y`, "
        + " `species4`, `species4_x`, `species4_y`, "
        + " `species5`, `species5_x`, `species5_y`, "
        + " `player_id`, `terrain_id`, `created_at`) "
        + "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

    private static final String FIND_BY_PLAYER_QUERY = "SELECT * FROM `clash_defense_config`"
        + " WHERE `player_id` = ?"
        + " ORDER BY `created_at` DESC"
        + " LIMIT 1";

    private DefenseConfigDAO() {}

    public static DefenseConfig create(DefenseConfig dc) {
        try (
            Connection con = GameDB.getConnection();
            PreparedStatement pstmt = con.prepareStatement(INSERT_QUERY, Statement.RETURN_GENERATED_KEYS);
        ) {

            // Parameter indices 1-15 are occupied by the species layout.
            ArrayList<Integer> keys = new ArrayList(dc.layout.keySet());
            for (int i = 0; i < keys.size(); i++) {
                pstmt.setInt(i + 1, keys.get(i));

                Vector2<Float> pos = dc.layout.get(keys.get(i));
                pstmt.setFloat(i + 2, pos.getX());
                pstmt.setFloat(i + 3, pos.getY());
            }

            pstmt.setInt(16, dc.playerId);
            pstmt.setInt(17, dc.terrainId);
            pstmt.setTimestamp(18, new Timestamp(new Date().getTime()));

            pstmt.executeUpdate();

            try (ResultSet rs = pstmt.getGeneratedKeys();) {
                if (rs.next()) {
                    dc.id = rs.getInt(1);
                } else {
                    throw new SQLException("Failed to create DefenseConfiguration.");
                }
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        }
        return dc;
    }

    public static DefenseConfig findByPlayerId(int playerId) {
        DefenseConfig result = new DefenseConfig();

        try (
            Connection con = GameDB.getConnection();
            PreparedStatement pstmt = con.prepareStatement(FIND_BY_PLAYER_QUERY);
        ) {
            pstmt.setInt(1, playerId);

            ResultSet rs = pstmt.executeQuery();
            if (rs.next()) {
                result.createdAt = rs.getDate("created_at");
                result.id = rs.getInt("clash_defense_config_id");
                result.playerId = rs.getInt("player_id");
                result.terrainId = rs.getInt("terrain_id");
                result.layout = new HashMap<>();

                for (int i = 0; i < 5; i++) {
                    String label = "species" + (i + 1);
                    Vector2<Float> pos = new Vector2<>();

                    int speciesId = rs.getInt(label);
                    pos.setX(rs.getFloat(label + "_x"));
                    pos.setY(rs.getFloat(label + "_y"));
                    result.layout.put(speciesId, pos);
                }
            } else {
                return null;
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        }
        return result;
    }
}

