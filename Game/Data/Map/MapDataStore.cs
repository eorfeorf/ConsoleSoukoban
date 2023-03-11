﻿namespace Souko.Game.Data.Map;

public class MapDataStore : IMapDataStore
{
    public int[] Load(int mapId)
    {
        return new[]
        {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 3, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 2, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 2, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 1, 0, 0, 1, 1, 1, 0, 1,
            1, 0, 1, 4, 0, 4, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        };
    }
}