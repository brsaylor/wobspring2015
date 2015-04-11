package clashofspecies;

/**
 * This class represents a single attack configuration set up by a player
 * before launching an attack on an enemy plot. 
 */
public class AttackConfiguration {

  private static final MAX_BIOMASS = 2500;
  private static final MAX_SPECIES = 5;

  protected int playerId; 
  protected Map<int, int> speciesMass;

  public AttackConfiguration(int playerId) {
    this.playerId = playerId;
    this.speciesMass = new Map<int, int>();
  }

  public boolean addSpecies(int speciesId, int biomass) {
    Iterator it = speciesMass.entrySet().iterator();
    int totalBiomass = biomass;

    while (it.hasNext()) {
      Map.Entry pair = (Map.Entry)it.next();
      totalBiomass += pair.getValue();
    }

    // Prevent the player from adding excessive biomass.
    if (totalBiomass > MAX_BIOMASS) {
      return false;
    } 

    // Prevent the player from adding excess species.
    if (!speciesMass.containsKey(speciesId) && speciesMass.size() == MAX_SPECIES) {
      return false;
    }

    // If present in the map, add the biomass, otherwise create a new entry.
    if (speciesMass.containsKey(speciesId)) {
      speciesMass.put(speciesId, speciesMass.get(speciesId) + biomass);
    } else { 
      speciesMass.put(speciesId, biomass);
    }

    return true;
  }
}
