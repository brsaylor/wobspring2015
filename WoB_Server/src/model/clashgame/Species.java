package model.clashgame;

/**
 * Created by dkushner on 4/23/2015.
 */
public class Species {
    public static enum Type {
        PLANT(0),
        CARNIVORE(1),
        HERBIVORE(2),
        OMNIVORE(3);

        private final int value;
        private Type(int value) {
            this.value = value;
        }
        public int getValue() {
            return this.value;
        }
    }

    public int speciesId;
    public String name;
    public int price;
    public Type type;
    public String description;
    public int attackPoints;
    public int hitPoints;
    public int movementSpeed;
    public int attackSpeed;
}

