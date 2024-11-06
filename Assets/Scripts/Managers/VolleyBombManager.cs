using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VolleyBombManager : MonoBehaviour
{

    GameManager _gameManager;

    [SerializeField] private List<GameObject> _bombSpawnList;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private bool _bombDidntSpawn;
    public List<PlayerController> _playersList;
    [SerializeField] private List<GameObject> _groundsList;
    [SerializeField] private List<VolleyBombZone> _zonesList;
    [SerializeField] private List<GameObject> _playersSpawners;

    [SerializeField] private GameObject _actualArena;
    [SerializeField] private List<GameObject> _arenas;
    [SerializeField] private GameObject _map2J;
    [SerializeField] private GameObject _map3J;
    [SerializeField] private GameObject _map4J;

    VolleyBombZone _zone;




    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playersList = _gameManager.GetPlayerManager().GetPlayerList();
        _playersSpawners = new List<GameObject>();
        _zone = FindObjectOfType<VolleyBombZone>();
        _bombDidntSpawn = true;
        Init();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Backspace))
    //    {
    //    }
    //}

    public void Init()
    {
        _playersList = _gameManager.GetPlayerManager().GetPlayerList(); // TODO : replace avec un "GetActifPlayers"
        switch (_playersList.Count)
        {
            case 2:
                _map2J.SetActive(true);
                _map3J.SetActive(false);
                _map4J.SetActive(false);
                break;
            case 3:
                _map2J.SetActive(false);
                _map3J.SetActive(true);
                _map4J.SetActive(false);
                break;
            case 4:
                _map2J.SetActive(false);
                _map3J.SetActive(false);
                _map4J.SetActive(true);
                break;
        }
        
        AssignArena();
        _groundsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ground"));
        var i = 0;
        foreach (VolleyBombZone zone in _zonesList)
        {
            zone.SetOwner(_playersList[i]);
            zone.SetZoneMaterial(_playersList[i].PlayerId);
            zone.SetSpawn(_playersList[i],zone._spawner.transform);
            i++;
        }
        while (_bombDidntSpawn == true)
        {
            int random = Random.Range(0, _bombSpawnList.Count);
            if (_bombSpawnList[random].gameObject.activeInHierarchy == true)
            {
                GameObject spawnerChoosed = _bombSpawnList[random];
                Instantiate(_bomb, spawnerChoosed.transform.position,Quaternion.identity);
                _bombDidntSpawn = false;
                //Debug.Log("Hell yee !");
            }
            else
            {
                _bombDidntSpawn = true;
                //Debug.Log("nope");
            }
        }

    }

    void AssignArena()
    {
        int playerNum = _playersList.Count;
        switch (playerNum)
        {
            case 4:
                _actualArena = _arenas[2];
                break;
            case 3:
                _actualArena = _arenas[1];
                break;
            case 2:
                _actualArena = _arenas[0];
                break;
            case 1:
                _actualArena = _arenas[0];
                break;
        }

        _zonesList = new List<VolleyBombZone>(_actualArena.gameObject.GetComponentsInChildren<VolleyBombZone>());
    }
}
