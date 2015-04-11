package clashofspecies;

public interface BattleAction {
  public int playerId;
  public LocalTime when();
  public Object data();
  public String toString();
  public void execute(attackConfig, defenseConfig);
}
