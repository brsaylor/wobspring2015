package db.clashgame;

// Java Imports

import db.GameDB;
import model.clashgame.Species;
import util.Log;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

/**
 * Table(s) Required: clash_battle
 *
 * The BattleDAO class hold methods that can execute a variety of different
 * queries for very specific purposes.
 *
 * @author Abhijit
 */
public final class SpeciesDAO {

    private static final String LIST_QUERY = "SELECT * FROM `clash_species` "
        + "INNER JOIN `species` ON `clash_species`.`species_id` = `species`.`species_id`";

    private SpeciesDAO() {}

    public static List<Species> getList() {
        List<Species> result = new ArrayList<>();
        try (
          Connection con = GameDB.getConnection();
          PreparedStatement pstmt = con.prepareStatement(LIST_QUERY);
        ) {
            ResultSet rs = pstmt.executeQuery();
            while(rs.next()) {
                Species sp = new Species();
                sp.speciesId = rs.getInt("species_id");
                // NOTE: This is the existing cost from the existing species table.
                sp.price = rs.getInt("cost");
                sp.movementSpeed = rs.getInt("movement_speed");
                sp.attackSpeed = rs.getInt("attack_speed");
                sp.attackPoints = rs.getInt("attack_points");
                sp.hitPoints = rs.getInt("hit_points");
                sp.type = rs.getInt("organism_type");
                sp.description = rs.getString("description");
                result.add(sp);
            }
        } catch (SQLException ex) {
            Log.println_e(ex.getMessage());
        }
        return result;
    }
}
