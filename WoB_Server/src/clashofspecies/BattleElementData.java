/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package clashofspecies;

/**
 *
 * @author lev
 * NOTE: this is very likely to be temporary
 */
public class BattleElementData {
    public int speciesID;
    public float x;
    public float y;
    
    public BattleElementData(int _id, float _x, float _y){
        this.speciesID = _id;
        this.x = _x;
        this.y = _y;
    }
}