package db.clashgame;

import model.clashgame.Player;

import java.util.ArrayList;
import db.GameDB;
import util.Log;

import java.sql.*;
import java.util.List;

/**
 * Created by dkush_000 on 4/23/2015.
 */
public class ClashPlayerDAO {
    private static final String FIND_ELIGIBLE_QUERY = "SELECT `player`.* FROM `player`"
        + " INNER JOIN `clash_defense_config`"
        + " GROUP BY `player`.`player_id`";

    private static final String FIND_BY_ID = "SELECT `player`.* FROM `player`"
        + " WHERE `player`.`player_id` = ?"
        + " LIMIT 1";

    private ClashPlayerDAO() {}

    public static List<Player> findEligiblePlayers() {
        List<Player> result = new ArrayList<Player>();

        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(FIND_ELIGIBLE_QUERY);
            rs = pstmt.executeQuery();
            while (rs.next()) {
                Player pl = new Player();
                pl.id = rs.getInt("player_id");
                pl.name = rs.getString("name");
                pl.level = rs.getInt("level");
                result.add(pl);
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }
        return result;
    }

    public static Player findById(int playerId) {
        Player result = null;
        Connection con = null;
        PreparedStatement pstmt = null;
        ResultSet rs = null;

        try {
            con = GameDB.getConnection();
            pstmt = con.prepareStatement(FIND_BY_ID);
            pstmt.setInt(1, playerId);

            rs = pstmt.executeQuery();

            if (rs.next()) {
                result = new Player();
                result.id = rs.getInt("player_id");
                result.name = rs.getString("name");
                result.level = rs.getInt("level");
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        } finally {
            GameDB.closeConnection(con, pstmt, rs);
        }
        return result;
    }
}
