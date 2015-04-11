package clashofspecies;

public class BattleManager {
  protected static final BattleManager instance = new BattleManager();
  protected ArrayList<BattleInstance> battleList = new ArrayList<BattleInstance>();

  protected BattleManager() {}

  public static BattleManager getInstance() {
    return instance;
  }

  public boolean addBattleInstance(BattleInstance b) {
    // Perform checks here to ensure that the player is only involved in
    // a single battle at a time, etc.
    return battleList.add(battleInstance);
  }
}
