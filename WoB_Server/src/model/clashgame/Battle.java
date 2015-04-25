package model.clashgame;

import java.util.Date;

/**
 *
 * @author Abhijit
 */
public class Battle {

    public static enum Outcome {

        WIN(0),
        LOSE(1),
        DRAW(2);

        private final int value;

        private Outcome(int value) {
            this.value = value;
        }

        public int getValue() {
            return this.value;
        }
    }

    public Integer id;
    public Integer attackConfigId;
    public Integer defenseConfigId;
    public Date timeStarted;
    public Date timeEnded;
    public Outcome outcome;
}
