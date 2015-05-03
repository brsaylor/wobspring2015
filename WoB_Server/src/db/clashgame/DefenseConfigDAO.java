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
        + " `player_id`, `terrain`, `created_at`) "
        + "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

    private static final String FIND_BY_PLAYER_QUERY = "SELECT * FROM `clash_defense_config`"
        + " WHERE `player_id` = ?"
        + " ORDER BY `created_at` DESC"
        + " LIMIT 1";

    private static final String FIND_BY_DEFENSE_CONFIG_ID_QUERY = "SELECT * FROM `clash_defense_config`"
            + " WHERE `defense_config_id` = ?"
            + " ORDER BY `created_at` DESC"
            + " LIMIT 1";

    private DefenseConfigDAO() {}

    public static DefenseConfig create(DefenseConfig dc) {
        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(INSERT_QUERY, Statement.RETURN_GENERATED_KEYS);

            // Parameter indices 1-15 are occupied by the species layout.
            ArrayList<Integer> keys = new ArrayList(dc.layout.keySet());
            for (int i = 0, j = 1; i < keys.size(); i++, j += 3) {
                pstmt.setInt(j, keys.get(i));

                Vector2<Float> pos = dc.layout.get(keys.get(i));
                pstmt.setFloat(j + 1, pos.getX());
                pstmt.setFloat(j + 2, pos.getY());
            }

            pstmt.setInt(16, dc.playerId);
            pstmt.setString(17, dc.terrain);
            pstmt.setTimestamp(18, new Timestamp(new Date().getTime()));

            pstmt.executeUpdate();

            rs = pstmt.getGeneratedKeys();
            if (rs.next()) {
                dc.id = rs.getInt(1);
            } else {
                throw new SQLException("Failed to create DefenseConfiguration.");
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }
        return dc;
    }

    public static DefenseConfig findByPlayerId(int playerId) {
        DefenseConfig result = new DefenseConfig();

        Connection con = null;
        ResultSet rs = null;
        PreparedStatement pstmt = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(FIND_BY_PLAYER_QUERY);
            pstmt.setInt(1, playerId);

            rs = pstmt.executeQuery();

            if (rs.next()) {
                Timestamp ts = rs.getTimestamp("created_at");
                result.createdAt = new Date(ts.getTime());
                result.id = rs.getInt("clash_defense_config_id");
                result.playerId = rs.getInt("player_id");
                result.terrain = rs.getString("terrain");
                result.layout = new HashMap<Integer, Vector2<Float>>();

                for (int i = 0; i < 5; i++) {
                    String label = "species" + (i + 1);
                    Vector2<Float> pos = new Vector2<Float>();

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
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }
        return result;
    }

    public static DefenseConfig findByDefenseConfigId(int defenseConfigID) {
        DefenseConfig result = new DefenseConfig();

        Connection con = null;
        ResultSet rs = null;
        PreparedStatement pstmt = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(FIND_BY_DEFENSE_CONFIG_ID_QUERY);
            pstmt.setInt(1, defenseConfigID);

            rs = pstmt.executeQuery();

            if (rs.next()) {
                Timestamp ts = rs.getTimestamp("created_at");
                result.createdAt = new Date(ts.getTime());
                result.id = rs.getInt("clash_defense_config_id");
                result.playerId = rs.getInt("player_id");
                result.terrain = rs.getString("terrain");
                result.layout = new HashMap<Integer, Vector2<Float>>();

                for (int i = 0; i < 5; i++) {
                    String label = "species" + (i + 1);
                    Vector2<Float> pos = new Vector2<Float>();

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
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }
        return result;
    }
}

