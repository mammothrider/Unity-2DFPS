
//grid map
//naive space tree
//graph theory
//graph flow
public class GridMap{

    //basic setting
    public int gridRow;
    public int gridCol;
    public float gridSize;
    
    //grid map, -1 for unreachable, >0 for moving cost
    public List<int> gridMap;
    
    //node graph
    public Dictionary<List<int>> nodeGraph;
    
    //space tree
    struct sSpaceTreeNode {};
    public sSpaceTreeNode rootNode;
}
