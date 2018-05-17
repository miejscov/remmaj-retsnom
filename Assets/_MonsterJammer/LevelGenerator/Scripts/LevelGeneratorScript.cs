using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour {

    private const float CameraDistanceY = 8;
    private const float CameraDistanceZ = 2;
    private const float CameraRotationX = 80f;
    private int _mapSize;// = 21;
    private int _seed;
    [Tooltip("Number of empty tiles on the map.")]
    private int _maxTunnelCount;// = 64;
    [Tooltip("Minimal tunnel length during generation.")]
    private int _minTunnelLength;// = 3;
    private int _cratesAmount;// = 32;
    private int _monstersAmountA;// = 8;
    private int _monstersAmountB;// = 1;
    private int _monstersAmountC;// = 1;
    [Tooltip("Minimum distance between player and monsters on level start.")]
    [Range(1, 5)]
    private int _playerSafeDistance = 2;
    [Tooltip("Minimum distance between monsters. DO NOT SET TOO HIGH!")]
    [Range(0, 3)]
    private int _monsterDistance = 1;
    

    private int _playerX, _playerY;
    private int _tunnelCount = 0;
    private const int LessCratesDistance = 4;
    private const int LessCratesDistanceProbability = 50; // % of probability to spawn a crate in this area
    private const int TileEntrance = 3;
    private const int TileExit = 4;
    private const int TileDebugCrate = 5;
    private const int TileCrate = 2;
    private const int TileWall = 1;
    private const int TileTunnel = 0;
    private const int TileMonsterA = 10;
    private const int TileMonsterB = 11;
    private const int TileMonsterC = 12;
    private const int TilePlayer = 1000;
    private const int DirLeft = 0;
    private const int DirDown = 1;
    private int[,] _map;
    
    private SettingsControlScript _settingsControl;
    private GameObject[] _setting;
    private GameObject _mainCamera, _player, _wall, _floor, _crate, _monsterA, _monsterB, _monsterC, _entrance, _exit, _ground, _debugCrate;
    private void GetSetting()
    {
        _settingsControl = GameObject.Find("LevelControl").GetComponentInChildren<SettingsControlScript>();
        _setting = _settingsControl.GetSetting();
        
        _mainCamera = _setting[0];
        _player = _setting[1];
        _wall = _setting[2];
        _floor = _setting[3];
        _crate = _setting[4];
        _monsterA = _setting[5];
        _monsterB = _setting[6];
        _monsterC = _setting[7];
        _entrance = _setting[8];
        _exit = _setting[9];
        _ground = _setting[10];
    }

    public void SetLabirynthParameters(int[] paramArray)
    {
        _mapSize = paramArray[0];
        _maxTunnelCount = paramArray[1];
        _minTunnelLength = paramArray[2];
        _cratesAmount = paramArray[3];
        _monstersAmountA = paramArray[4];
        _monstersAmountB = paramArray[5];
        _monstersAmountC = paramArray[6];
    }

    public void GenerateLabirynth()
    {
        GetSetting();
        GenerateMap();
        GeneratePlayer();
        GenerateMonstersA(_monstersAmountA);
        GenerateMonstersB(_monstersAmountB);
        GenerateMonstersC(_monstersAmountC);
        GenerateCrates(_cratesAmount);
        GenerateExits();
        InstantiateGround();
        InstantiateMonsters();
        InstantiateMap();
    }


    private void GenerateMap()
    {
        var startPos = _mapSize / 2;
        var maxTunnelLength = _mapSize / 2;

        _map = new int[_mapSize, _mapSize];
        for (var i = 0; i < _mapSize; i++)
        {
            for (var ii = 0; ii < _mapSize; ii++)
            {
                _map[i, ii] = TileWall;
            }
        }

        var x = 1;
        var y = _mapSize / 2;
        var tunnelLength = _mapSize;
        CreateTunnel(x, y, DirLeft, tunnelLength);
        y = 1;
        x = _mapSize / 2;
        tunnelLength = _mapSize - 2;
        CreateTunnel(x, y, DirDown, tunnelLength);

        while (_tunnelCount < _maxTunnelCount)
        {
            var dir = Random.Range(0, 2);
            tunnelLength = Random.Range(_minTunnelLength, maxTunnelLength);
            x = Random.Range(1, _mapSize - 3);
            y = Random.Range(1, _mapSize - 3);
            if (CheckTunnel(x, y, dir, tunnelLength))
            {
                CreateTunnel(x, y, dir, tunnelLength);
            }
        }
    }

    private bool CheckTunnel(int x, int y, int dir, int length)
    {
        var tunnelOk = false;
        switch (dir)
        {
            case DirLeft:
                for (var i = x; i < length + x; i++)
                {
                    if (_map[y, i] == TileTunnel) tunnelOk = true;
                    if (i > _mapSize - 3) break;
                }
                break;
            case DirDown:
                for (var i = y; i < length + y; i++)
                {
                    if (_map[i, x] == TileTunnel) tunnelOk = true;
                    if (i > _mapSize - 3) break;
                }
                break;
        }
        return tunnelOk;
    }

    private void CreateTunnel(int x, int y, int dir, int length)
    {
        if (dir == DirLeft)
        {
            for (var i = x; i < length + x; i++)
            {
                if (_map[y, i] == TileWall)
                {
                    _tunnelCount++;
                    _map[y, i] = TileTunnel;
                }
                if (i > _mapSize - 3) return;
            }

        }
        else if (dir == DirDown)
        {
            for (var i = y; i < length + y; i++)
            {
                if (_map[i, x] == TileWall)
                {
                    _tunnelCount++;
                    _map[i, x] = TileTunnel;
                }
                if (i > _mapSize - 3) return;
            }

        }
    }

    private void GenerateExits()
    {
        _map[_mapSize / 2, 0] = TileEntrance;
        _map[_mapSize / 2, _mapSize - 1] = TileExit;
    }

    private void GenerateCrates(int amount)
    {
        var crates = 0;
        var counter = 100000;
        while (crates < amount)
        {
            counter--;
            if (counter <= 0)
            {
                Debug.LogError("Could not find suitable Crate position!");
                break;
            }
            var x = Random.Range(1, _mapSize - 1);
            var y = Random.Range(1, _mapSize - 1);
            var lessCratesRandomRange = Random.Range(1, 100);
            
            if (_map[y, x] != TileTunnel) continue;
            else if ((_map[y, x - 1] == TileMonsterA || _map[y, x - 1] == TileMonsterB || _map[y, x - 1] == TileMonsterC) && !CheckForAdjacentTunnels(x - 1, y, 2)) continue;
            else if ((_map[y, x + 1] == TileMonsterA || _map[y, x + 1] == TileMonsterB || _map[y, x + 1] == TileMonsterC) && !CheckForAdjacentTunnels(x + 1, y, 2)) continue;
            else if ((_map[y - 1, x] == TileMonsterA || _map[y - 1, x] == TileMonsterB || _map[y - 1, x] == TileMonsterC) && !CheckForAdjacentTunnels(x, y - 1, 2)) continue;
            else if ((_map[y + 1, x] == TileMonsterA || _map[y + 1, x] == TileMonsterB || _map[y + 1, x] == TileMonsterC) && !CheckForAdjacentTunnels(x, y + 1, 2)) continue;
            else if (y == _mapSize / 2 && (x <= LessCratesDistance || x >= _mapSize - LessCratesDistance - 1))
            {
                Debug.Log("LessCratesRandomRange = " + lessCratesRandomRange +" of Probability = "+LessCratesDistanceProbability);

                //_map[y, x] = TileDebugCrate;
                if (lessCratesRandomRange < LessCratesDistanceProbability) continue;
            }
            _map[y, x] = TileCrate;
            crates++;
        }
    }

    private bool CheckForAdjacentTunnels(int x, int y, int minTunnels)
    {
        var tunnels = 0;
        if (_map[y, x - 1] == TileTunnel) tunnels++;
        if (_map[y, x + 1] == TileTunnel) tunnels++;
        if (_map[y - 1, x] == TileTunnel) tunnels++;
        if (_map[y + 1, x] == TileTunnel) tunnels++;
        return tunnels >= minTunnels;
    }

    private void GenerateMonstersA(int amount)
    {
        var monstersA = 0;
        var counter = 100000;
        while (monstersA < amount)
        {
            var isOk = true;
            counter--;
            if (counter <= 0)
            {
                Debug.LogError("Could not find suitable Monster-A start position!");
                return;
            }
            var x = Random.Range(1, _mapSize - 1);
            var y = Random.Range(1, _mapSize - 1);
            if (_map[y, x] != TileTunnel) continue;
            if (!(y < _playerY - _playerSafeDistance || y > _playerY + _playerSafeDistance || x < _playerX - _playerSafeDistance || x > _playerX + _playerSafeDistance)) continue;
            if (!(_map[y - 1, x] == TileTunnel || _map[y + 1, x] == TileTunnel || _map[y, x - 1] == TileTunnel || _map[y, x + 1] == TileTunnel)) continue;

            for (var i = -_monsterDistance; i <= _monsterDistance; i++)
            {
                for (var ii = -_monsterDistance; ii <= _monsterDistance; ii++)
                {
                    if (x + ii <= 0 || x + ii >= _mapSize - 1 || y + i <= 0 || y + i >= _mapSize - 1) continue;
                    if (_map[i + y, ii + x] == TileMonsterA || _map[i + y, ii + x] == TileMonsterB)
                    {
                        isOk = false;
                        break;
                    }
                    if (!isOk) break;
                }
            }
            if (!isOk) continue;
            _map[y, x] = TileMonsterA;
            monstersA++;
        }
    }

    private void GenerateMonstersB(int amount)
    {
        var monstersB = 0;
        var counter = 100000;
        while (monstersB < amount)
        {
            var isOk = true;
            counter--;
            if (counter <= 0)
            {
                Debug.LogError("Could not find suitable Monster-B start position!");
                return;
            }
            var x = Random.Range(1, _mapSize - 1);
            var y = Random.Range(1, _mapSize - 1);
            if (_map[y, x] != TileTunnel) continue;
            else if (!(y < _playerY - _playerSafeDistance || y > _playerY + _playerSafeDistance || x < _playerX - _playerSafeDistance || x > _playerX + _playerSafeDistance)) continue;
            else if (!(_map[y - 1, x] == TileTunnel || _map[y + 1, x] == TileTunnel || _map[y, x - 1] == TileTunnel || _map[y, x + 1] == TileTunnel)) continue;

            for (var i = -_monsterDistance; i <= _monsterDistance; i++)
            {
                for (var ii = -_monsterDistance; ii <= _monsterDistance; ii++)
                {
                    if (x + ii <= 0 || x + ii >= _mapSize - 1 || y + i <= 0 || y + i >= _mapSize - 1) continue;
                    if (_map[i + y, ii + x] == TileMonsterA || _map[i + y, ii + x] == TileMonsterB)
                    {
                        isOk = false;
                        break;
                    }
                    if (!isOk) break;
                }
            }
            if (!isOk) continue;
            _map[y, x] = TileMonsterB;
            monstersB++;
        }
    }

    private void GenerateMonstersC(int amount)
    {
        var monstersC = 0;
        var counter = 100000;
        while (monstersC < amount)
        {
            var isOk = true;
            counter--;
            if (counter <= 0)
            {
                Debug.LogError("Could not find suitable Monster-C start position!");
                return;
            }
            var x = Random.Range(1, _mapSize - 1);
            var y = Random.Range(1, _mapSize - 1);
            if (_map[y, x] != TileTunnel) continue;
            else if (!(y < _playerY - _playerSafeDistance || y > _playerY + _playerSafeDistance || x < _playerX - _playerSafeDistance || x > _playerX + _playerSafeDistance)) continue;
            else if (!(_map[y - 1, x] == TileTunnel || _map[y + 1, x] == TileTunnel || _map[y, x - 1] == TileTunnel || _map[y, x + 1] == TileTunnel)) continue;

            for (var i = -_monsterDistance; i <= _monsterDistance; i++)
            {
                for (var ii = -_monsterDistance; ii <= _monsterDistance; ii++)
                {
                    if (x + ii <= 0 || x + ii >= _mapSize - 1 || y + i <= 0 || y + i >= _mapSize - 1) continue;
                    if (_map[i + y, ii + x] == TileMonsterA || _map[i + y, ii + x] == TileMonsterB || _map[i + y, ii + x] == TileMonsterB)
                    {
                        isOk = false;
                        break;
                    }
                    if (!isOk) break;
                }
            }
            if (!isOk) continue;
            _map[y, x] = TileMonsterC;
            monstersC++;
        }
    }

    private void GeneratePlayer()
    {
        _playerX = 1;
        _playerY = _mapSize / 2;
        _map[_playerY, _playerX] = TilePlayer;
        SurroundPlayerWithCrates(_playerY, _playerX);
    }

    private void SurroundPlayerWithCrates(int y, int x)
    {
        if (_map[y - 1,x] == TileTunnel)
        {
            _map[y - 1, x] = TileCrate;
            _cratesAmount--;
        }
        if (_map[y + 1, x] == TileTunnel)
        {
            _map[y + 1, x] = TileCrate;
            _cratesAmount--;
        }
        if (_map[y, x - 1] == TileTunnel)
        {
            _map[y, x - 1] = TileCrate;
            _cratesAmount--;
        }
        if (_map[y, x + 1] != TileTunnel) return;
        _map[y, x + 1] = TileCrate;
        _cratesAmount--;
    }

    private void InstantiateMonsters() 
    {
       for (var i = 0; i < _mapSize; i++)
        {
            for (var ii = 0; ii < _mapSize; ii++)
            {
                var x = -_mapSize / 2 + ii;
                var z = -_mapSize / 2 + i;
                if (_map[i, ii] == TileMonsterA)
                    Instantiate(_monsterA, new Vector3(x, 0.1f, z), Quaternion.identity);
                else if (_map[i, ii] == TileMonsterB)
                {
                    Instantiate(_monsterB, new Vector3(x, 0.1f, z), Quaternion.identity);
                }
                else if (_map[i, ii] == TileMonsterC)
                {
                    Instantiate(_monsterC, new Vector3(x, 0.1f, z), Quaternion.identity);
                }
            }
        }
    }

    private void InstantiateMap()
    {
        for (var i = 0; i < _mapSize; i++)
        {
            for (var ii = 0; ii < _mapSize; ii++)
            {
                var x = -_mapSize / 2 + ii;
                var z = -_mapSize / 2 + i;

                switch (_map[i, ii])
                {
                    case TileWall:
                        Instantiate(_wall, new Vector3(x, .5f, z), Quaternion.identity);
                        break;
                    case TileTunnel:
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TileCrate:
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        Instantiate(_crate, new Vector3(x, 0.5f, z), Quaternion.identity);
                        break;
                    case TileDebugCrate:
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        Instantiate(_crate, new Vector3(x, 0.05f, z), Quaternion.identity);
                        break;
                    case TileMonsterA:
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TileMonsterB:
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TileMonsterC:
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TilePlayer:
                        if (!CheckObjectExist("Player"))
                        {
                            Instantiate(_player, new Vector3(x - 2, 0f, z), Quaternion.identity);
                        }
                        else
                        {
                            var player = GameObject.Find("Player1(Clone)");
                            player.GetComponent<PlayerRbMoveScript>().SetPlayerPosition(new Vector3(x - 2, 0f, z));
                            player.GetComponent<PlayerRbMoveScript>().ResetSpeed();
                            player.GetComponent<PlayerControlScript>().ResetControl();
                        }
                        if (!CheckObjectExist("MainCamera"))
                        {
                            Instantiate(_mainCamera, new Vector3(x, CameraDistanceY, z - CameraDistanceZ),
                                Quaternion.Euler(CameraRotationX, 0f, 0f));
                        }
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TileEntrance:
                        Instantiate(_entrance, new Vector3(x, 0f, z), Quaternion.Euler(0, -90, 0));
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
                        break;
                    case TileExit:
                        Instantiate(_exit, new Vector3(x, 0f, z), Quaternion.Euler(0, 90, 0));
                        Instantiate(_floor, new Vector3(x, -.5f, z), Quaternion.identity);
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
        var grnd = Instantiate(_ground, new Vector3(0f, -.5f, 0f), Quaternion.identity);
        scale.y = .1f;
        scale.x = _mapSize - 2;
        scale.z = _mapSize - 2;
        
        grnd.transform.localScale = scale;
        grnd.GetComponent<MeshRenderer>().enabled = false;
    }
}
