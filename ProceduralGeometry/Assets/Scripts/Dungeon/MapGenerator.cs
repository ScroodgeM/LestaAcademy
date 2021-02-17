using UnityEngine;

public static class MapGenerator
{
    public static bool[,] Create2DMap(Vector2Int size, int steps, int seed)
    {
        bool[,] result = new bool[size.x, size.y];

        Vector2Int cursor = size / 2;

        result[cursor.x, cursor.y] = true;

        Random.InitState(seed);

        for (int s = 0; s < steps; s++)
        {
            Vector2Int direction = Vector2Int.zero;
            int distanceLeft = 0;

            switch (Random.Range(0, 4))
            {
                case 0: direction = Vector2Int.up; distanceLeft = Random.Range(0, size.y); break;
                case 1: direction = Vector2Int.down; distanceLeft = Random.Range(0, size.y); break;
                case 2: direction = Vector2Int.left; distanceLeft = Random.Range(0, size.x); break;
                case 3: direction = Vector2Int.right; distanceLeft = Random.Range(0, size.x); break;
            }

            while (true)
            {
                Vector2Int newCursor = cursor + direction;

                if (newCursor.x < 0 || newCursor.x >= size.x || newCursor.y < 0 || newCursor.y >= size.y)
                {
                    // out of randge, stop moving and go to next step, starting from center
                    cursor = size / 2;
                    break;
                }

                cursor = newCursor;

                if (result[cursor.x, cursor.y] == false)
                {
                    // make this cell part of dungeon and go to next step
                    result[cursor.x, cursor.y] = true;
                    break;
                }

                if (--distanceLeft <= 0)
                {
                    // not enough mana to move further
                    break;
                }

                // this cell is already part of dungeon, continue moving
            }
        }

        return result;
    }
}
