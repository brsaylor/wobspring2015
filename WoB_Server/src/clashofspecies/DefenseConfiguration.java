package clashofspecies;

import java.awt.Point;

public class SpeciesPoint {
  public Point location;
  public int biomass;

  public SpeciesPoint(Point location, int biomass) {
    this.location = location;
    this.biomass = biomass;
  }
}

public class DefenseConfiguration {

  private static final MAX_BIOMASS = 2500;

  protected int playerId;
  protected Map<int, SpeciesPoint> speciesLayout;

  public DefenseConfiguration(int playerId) {
    this.playerId = playerId;
    this.speciesLayout = new Map<int, SpeciesPoint>();
  }

  public boolean addSpecies(int speciesId, int biomass, Point position) {
    Iterator it = speciesLayout.entrySet().iterator();
    int totalBiomass = biomass;
    int totalSpecies = speciesLayout.size();

    while (it.hasNext()) {
      Map.Entry pair = (Map.Entry)it.next();
      totalBiomass += pair.getValue().biomass;
    }

    if (totalBiomass > MAX_BIOMASS) {
      return false;
    }

    if (!speciesLayout.containsKey(speciesId) && totalSpecies == MAX_SPECIES) {
      return false;
    }

    if (speciesLayout.containsKey(speciesId)) {
      SpeciesPoint p = speciesLayout.get(speciesId);
      p.biomass += biomass;
      p.position = position;
      speciesLayout.put(speciesId, speciesPoint);
    } else {
      speciesLayout.put(speciesId, new SpeciesPoint(biomass, position));
    }
    return true;
  }
}
