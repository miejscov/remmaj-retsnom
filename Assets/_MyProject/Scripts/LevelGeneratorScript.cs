using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour {

    //    public Text debugText;
    //    public Transform plane_green;
    //    public Transform plane_darkgray;
    //
    public float cameraDistanceY = 8;
    public float cameraDistanceZ = 2;
    public float cameraRotationX = 80f;
    public int mapSize = 21;
    public int seed;
    [Tooltip("Number of empty tiles on the map.")]
    public int maxTunnelCount = 64;
    [Tooltip("Minimal tunnel length during generation.")]
    public int minTunnelLength = 3;
    public int cratesAmount = 32;
    public int monstersAmount = 8;
    [Tooltip("Minimum distance between player and monsters on level start.")]
    [Range(1, 5)]
    public int playerSafeDistance = 2;
    [Tooltip("Minimum distance between monsters. DO NOT SET TOO HIGH!")]
    [Range(0, 3)]
    public int _monsterDistance = 1;
    public GameObject MainCamera;
    public GameObject Player;
    public GameObject Wall;
    public GameObject Floor;
    public GameObject Crate;
    public GameObject Monster;
    public GameObject Entrance;
    public GameObject Exit;
    public GameObject Ground;

    private int _playerX;
    private int _playerY;

    private int tunnelCount = 0;
    //    const int TILESIZE = 8;
    const int TILE_ENTRANCE = 3;
    const int TILE_EXIT = 4;
    const int TILE_CRATE = 2;
    const int TILE_WALL = 1;
    const int TILE_TUNNEL = 0;
    const int TILE_MONSTER = 10;
    const int TILE_PLAYER = 1000;

    //const int LASTLOOP = 1024;
    const int DIR_LEFT = 0;
    const int DIR_DOWN = 1;

    //private int[] mapIndexX;
    //private int[] mapIndexY;
    //private int[] mapIndexTile;

    int[,] map;


    public void SetLabirynthParameters(int[] paramArray)
    {
        
        Debug.Log("map size: " + paramArray[0]);
        Debug.Log("map size: " + paramArray[1]);
        Debug.Log("map size: " + paramArray[2]);
        Debug.Log("map size: " + paramArray[3]);
        Debug.Log("map size: " + paramArray[4]);
        
        mapSize = paramArray[0];
        maxTunnelCount = paramArray[1];
        minTunnelLength = paramArray[2];
        cratesAmount = paramArray[3];
        monstersAmount = paramArray[4];
    }
    

    public void GenerateLabirynth()
    {
//        Player.SetActive(false);
        GenerateMap();
        GeneratePlayer();
//        Player.SetActive(true);
        GenerateMonsters(monstersAmount);
        GenerateCrates(cratesAmount);
        GenerateExits();
 

        InstantiateGround();
        InstantiateMonsters();
        InstantiateMap();
        
        //		plane.transform.Translate (1, 1, 1);
    }


    void GenerateMap()
    {
        int startPos = mapSize / 2;
        int maxTunnelLength = mapSize / 2;

        int tunnelLength = Random.Range(1, mapSize - 2);
        int x, y;

        //mapIndexTile = new int[mapSize * mapSize];
        //mapIndexX = new int[mapSize * mapSize];
        //mapIndexY = new int[mapSize * mapSize];

        /*
        for (int i = 0; i < (mapSize * mapSize); i++)
        {
            mapIndexTile[i] = 0;
            mapIndexX[i] = 0;
            mapIndexY[i] = 0;
        }
        */

        //        Random.seed = seed;
        map = new int[mapSize, mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            for (int ii = 0; ii < mapSize; ii++)
            {
                map[i, ii] = TILE_WALL;
            }
        }


        x = 1;
        y = mapSize / 2;
        tunnelLength = mapSize;
        CreateTunnel(x, y, DIR_LEFT, tunnelLength);
        y = 1;
        x = mapSize / 2;
        tunnelLength = mapSize - 2;
        CreateTunnel(x, y, DIR_DOWN, tunnelLength);



        while (tunnelCount < maxTunnelCount)
        {
//            int chosenIndex = Random.Range(0, tunnelCount);
            int dir = Random.Range(0, 2);
            tunnelLength = Random.Range(minTunnelLength, maxTunnelLength);
            //            Debug.Log("Dir = " + dir);
            x = Random.Range(1, mapSize - 3);
            y = Random.Range(1, mapSize - 3);
            //Debug.Log("x = " + x + "   y = " + y);
            if (CheckTunnel(x, y, dir, tunnelLength))
            {
                //Debug.Log("x = " + x + "   y = " + y);
                CreateTunnel(x, y, dir, tunnelLength);
            }
//            CreateTunnel(mapIndexX[chosenIndex], mapIndexY[chosenIndex], dir, tunnelLength);
//            tunnelCount++;
        }

    }

    bool CheckTunnel(int x, int y, int dir, int length)
    {
        bool tunnelOk;
        tunnelOk = false;
        if (dir == DIR_LEFT)
        {
            for (int i = x; i < length + x; i++)
            {
                if (map[y, i] == TILE_TUNNEL) tunnelOk = true;
                if (i > mapSize - 3) break;
            }

        }
        else if (dir == DIR_DOWN)
        {
            for (int i = y; i < length + y; i++)
            {
                if (map[i, x] == TILE_TUNNEL) tunnelOk = true;
                if (i > mapSize - 3) break;
            }

        }
        return tunnelOk;
    }
    void CreateTunnel(int x, int y, int dir, int length)
    {
        if (dir == DIR_LEFT)
        {

            for (int i = x; i < length + x; i++)
            {
                //mapIndexTile[tunnelCount] = TILE_TUNNEL;
                //mapIndexX[tunnelCount] = i;
                //mapIndexY[tunnelCount] = y;
//                OnDrawGizmos();
//                Wait(0.1f);

                if (map[y, i] == TILE_WALL)
                {
                    tunnelCount++;
                    map[y, i] = TILE_TUNNEL;
                }
                if (i > mapSize - 3) return;
            }

        }
        else if (dir == DIR_DOWN)
        {
            for (int i = y; i < length + y; i++)
            {
                //mapIndexTile[tunnelCount] = TILE_TUNNEL;
                //mapIndexX[tunnelCount] = i;
                //mapIndexY[tunnelCount] = y;
                //                OnDrawGizmos();
                //                Wait(0.1f);

                if (map[i, x] == TILE_WALL)
                {
                    tunnelCount++;
                    map[i, x] = TILE_TUNNEL;
                }
                if (i > mapSize - 3) return;
            }

        }
    }

    void GenerateExits()
    {
        map[mapSize / 2, 0] = TILE_ENTRANCE;
        map[mapSize / 2, mapSize - 1] = TILE_EXIT;
    }

    void GenerateCrates(int amount)
    {
        int crates;
        crates = 0;
        int counter;
        counter = 100000;
        while (crates < amount)
        {
            counter--;
            if (counter <= 0)
            {
                Debug.LogError("Could not find suitable Crate position!");
                break;
            }
            int x, y;
            
            x = Random.Range(1, mapSize - 1);
            y = Random.Range(1, mapSize - 1);
            if (map[y, x] != TILE_TUNNEL) continue;
            else if (map[y, x - 1] == TILE_MONSTER && !CheckForAdjacentTunnels(x - 1, y, 2)) continue;
            else if (map[y, x + 1] == TILE_MONSTER && !CheckForAdjacentTunnels(x + 1, y, 2)) continue;
            else if (map[y - 1, x] == TILE_MONSTER && !CheckForAdjacentTunnels(x, y - 1, 2)) continue;
            else if (map[y + 1, x] == TILE_MONSTER && !CheckForAdjacentTunnels(x, y + 1, 2)) continue;

            map[y, x] = TILE_CRATE;
            crates++;
            
        }
    }

    private bool CheckForAdjacentTunnels(int x, int y, int minTunnels)
    {
        int tunnels;
        tunnels = 0;
        if (map[y, x - 1] == TILE_TUNNEL) tunnels++;
        if (map[y, x + 1] == TILE_TUNNEL) tunnels++;
        if (map[y - 1, x] == TILE_TUNNEL) tunnels++;
        if (map[y + 1, x] == TILE_TUNNEL) tunnels++;
        if (tunnels >= minTunnels) return true;
        return false;
    }

    void GenerateMonsters(int amount)
    {
        int monsters;
        monsters = 0;
        int counter;
        counter = 100000;
        while (monsters < amount)
        {
            bool isOk;
            isOk = true;
            int x, y;
            counter--;
            if (counter <= 0)
            {
                Debug.LogError("Could not find suitable Monster start position!");
                return;
            }
            x = Random.Range(1, mapSize - 1);
            y = Random.Range(1, mapSize - 1);
            if (map[y, x] != TILE_TUNNEL) continue;
            else if (!(y < _playerY - playerSafeDistance || y > _playerY + playerSafeDistance || x < _playerX - playerSafeDistance || x > _playerX + playerSafeDistance)) continue;
            else if (!(map[y - 1, x] == TILE_TUNNEL || map[y + 1, x] == TILE_TUNNEL || map[y, x - 1] == TILE_TUNNEL || map[y, x + 1] == TILE_TUNNEL)) continue;

            for (int i = -_monsterDistance; i <= _monsterDistance; i++)
            {
                for (int ii = -_monsterDistance; ii <= _monsterDistance; ii++)
                {
                    if (x + ii > 0 && x + ii < mapSize - 1 && y + i > 0 && y + i < mapSize - 1)
                    {
                        if (map[i + y, ii + x] == TILE_MONSTER)
                        {
                            isOk = false;
                            break;
                        }
                        if (!isOk) break;
                    }
                }
            }
            if (isOk)
            {
                map[y, x] = TILE_MONSTER;
                monsters++;
            }

        }
    }

    void GeneratePlayer()
    {
        //for(int i = 0; i < 100000; i++)
        //{
        //    int x = Random.Range(playerSafeDistance + 1, mapSize - playerSafeDistance - 2);
        //    int y = Random.Range(playerSafeDistance + 1, mapSize - playerSafeDistance - 2);
        //    Debug.Log("x = " + x + "   y = " + y);
        //    if (map[y, x] == TILE_TUNNEL)
        //    {
        //        bool isOk;
        //        isOk = true;
        //        for (i = y - playerSafeDistance; i <= y + playerSafeDistance; i++)
        //        {
        //            for (int ii = x - playerSafeDistance; ii <= x + playerSafeDistance; ii++)
        //            {
        //                if (map[i, ii] == TILE_MONSTER)
        //                {
        //                    isOk = false;
        //                    break;
        //                }
        //            }
        //            if (!isOk) break;
        //        }
        //        if (isOk)
        //        {
        //            map[y, x] = TILE_PLAYER;
        //            _playerX = x;
        //            _playerY = y;
        //            SurroundPlayerWithCrates(y, x);
        //            return;
        //        }
        //    }
        //}
        //Debug.LogError("Could not find suitable player start position!");
        _playerX = 1;
        _playerY = mapSize / 2;
        map[_playerY, _playerX] = TILE_PLAYER;
        SurroundPlayerWithCrates(_playerY, _playerX);
    }

    void SurroundPlayerWithCrates(int y, int x)
    {
        if (map[y - 1,x] == TILE_TUNNEL)
        {
            map[y - 1, x] = TILE_CRATE;
            cratesAmount--;
        }
        if (map[y + 1, x] == TILE_TUNNEL)
        {
            map[y + 1, x] = TILE_CRATE;
            cratesAmount--;
        }
        if (map[y, x - 1] == TILE_TUNNEL)
        {
            map[y, x - 1] = TILE_CRATE;
            cratesAmount--;
        }
        if (map[y, x + 1] == TILE_TUNNEL)
        {
            map[y, x + 1] = TILE_CRATE;
            cratesAmount--;
        }
    }

    void Wait(float waitTime)
    {
        float counter = 0f;
        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            //                       Debug.Log("We have waited for: " + counter + " seconds");
            //Wait for a frame so that Unity doesn't freeze
            //Check if we want to quit this function
            //if (Ap)
            //{
            //    //Quit function
            //    yield break;
            //}
            //                       yield return null;
        }

    }

    private void InstantiateMonsters() 
    {
       for (int i = 0; i < mapSize; i++)
        {
            for (int ii = 0; ii < mapSize; ii++)
            {
                int x = -mapSize / 2 + ii;
                int z = -mapSize / 2 + i;
                if (map[i, ii] == TILE_MONSTER)
                {
                    //GameObject mob = (GameObject)Instantiate(Monster, new Vector3(x, 0.1f, z), Quaternion.identity);
                    //mob.GetComponent<ItemGeneratorScript>().ground = Ground.transform;
                    Instantiate(Monster, new Vector3(x, 0.1f, z), Quaternion.identity);
                }
            }
        }
    }

    void InstantiateMap()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int ii = 0; ii < mapSize; ii++)
            {
                int x = -mapSize / 2 + ii;
                int z = -mapSize / 2 + i;

                switch (map[i, ii])
                {
                    case TILE_WALL:
                        Instantiate(Wall, new Vector3(x, .5f, z), Quaternion.identity);
                        break;
                    case TILE_TUNNEL:
                        Instantiate(Floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TILE_CRATE:
                        Instantiate(Floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        Instantiate(Crate, new Vector3(x, 0.5f, z), Quaternion.identity);
                        break;
                    case TILE_MONSTER:
                        Instantiate(Floor, new Vector3(x, -.5f, z), Quaternion.identity);                      
                        // Instantiate(MainCamera, new Vector3(x, cameraDistanceY, z - cameraDistanceZ), Quaternion.Euler(cameraRotationX, 0f, 0f));
                        break;
                    case TILE_PLAYER:
                        if (!CheckObjectExist("Player"))
                        {
                            Instantiate(Player, new Vector3(x-2, 0f, z), Quaternion.identity);
                        }
                        else
                        {
                            var player = GameObject.Find("Player1(Clone)");
                            player.GetComponent<PlayerRbMoveScript>().SetPlayerPosition(new Vector3(x-2, 0f, z));
                            player.GetComponent<PlayerRbMoveScript>().ResetSpeed();
                            player.GetComponent<PlayerControlScript>().ResetControl();
                        }
                        if (!CheckObjectExist("MainCamera"))
                        {
                            Instantiate(MainCamera, new Vector3(x, cameraDistanceY, z - cameraDistanceZ), Quaternion.Euler(cameraRotationX, 0f, 0f));
                           
                        }
                            Instantiate(Floor, new Vector3(x, -.5f, z), Quaternion.identity);

                        break;
                    case TILE_ENTRANCE:
                        Instantiate(Entrance, new Vector3(x, 0f, z), Quaternion.Euler(0, -90, 0));
                        Instantiate(Floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TILE_EXIT:
                        Instantiate(Exit, new Vector3(x, 0f, z), Quaternion.Euler(0, 90, 0));
                        Instantiate(Floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private static bool CheckObjectExist(string objTag)
    {
        var exist = false;
        exist = GameObject.FindGameObjectWithTag(objTag) != null;
        return exist;
    }


    private void InstantiateGround()
    {
        Vector3 scale;
        GameObject grnd = Instantiate(Ground, new Vector3(0f, -.5f, 0f), Quaternion.identity);
        scale.y = .1f;
        scale.x = mapSize - 2;
        scale.z = mapSize - 2;
        
        grnd.transform.localScale = scale;
        grnd.GetComponent<MeshRenderer>().enabled = false;
//        Ground = grnd;
    }
    /*
    void OnDrawGizmos()
    {




        if (map != null)
        {
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    switch (map[y, x]) {
                        case TILE_TUNNEL:
                            Gizmos.color = Color.gray;
                            break;
                        case TILE_WALL:
                            Gizmos.color = Color.red;
                            break;
                        case TILE_CRATE:
                            Gizmos.color = new Color(.5f, .5f, 0f, 1f);
                            break;
                        case TILE_MONSTER:
                            Gizmos.color = Color.green;
                            break;
                    }
                    Vector3 pos = new Vector3(-mapSize / 2 + x, 0, -mapSize / 2 + y );
                    Gizmos.DrawCube(pos, new Vector3(.8f, .8f, .8f));


                }
            }
        }

    }
    */
}
