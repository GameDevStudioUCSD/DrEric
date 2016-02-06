using UnityEngine;
using System.Collections;

public class LevelInfo  {

    private World world;
    private Level level;

    public LevelInfo( World w, Level l)
    {
        world = w;
        level = l;
    }

    public Level GetLevel() { return level;  }
    public World GetWorld() { return world;  }

}
