package clashofspecies;

public class BattleInstance {

  // Enum representing the end state of this battle from the perspective of the
  // attacking player.
  public enum EndState = {
    VICTORY, DEFEAT, FORFEIT
  }

  protected AttackConfiguration attackConfig;
  protected DefenseConfiguration defenseConfig;
  protected ArrayList<BattleAction> battleActions;
  protected LocalTime started;
  protected LocalTime ended;

  public BattleInstance(AttackConfiguration a, DefenseConfiguration d) {
    this.attackConfig = a;
    this.defenseConfig = d;
  }

  public void addAction(BattleAction ba) {
    battleActions.add(ba);
  }
}
