using UnityEngine;
using System.Collections.Generic;
//grid map
//naive space tree
//graph theory
//graph flow
public class GridMap{

    //basic setting
    public int gridRow;
    public int gridCol;
    public float gridSize;
    
    struct sGridNode {
        int x;
        int y;
        int moveCost;
        int space;
    }
    //grid map, -1 for unreachable, >0 for moving cost
    public List<sGridNode> gridMap;
    
    //node graph
    public Dictionary<sGridNode, List<sGridNode>> nodeGraph;
    
    //space tree
    struct sSpaceTreeNode {};
    public sSpaceTreeNode rootNode;
    
    //using cluster method to trans map into graph?
}
