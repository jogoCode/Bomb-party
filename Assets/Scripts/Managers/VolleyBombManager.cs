using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VolleyBombManager : MonoBehaviour
{

    GameManager _gameManager;

    [SerializeField] private List<GameObject> _bombSpawnList;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private List<PlayerController> _playersList;
    [SerializeField] private List<GameObject> _groundsList;
    [SerializeField] private List<VolleyBombZone> _zonesList;

    [SerializeField] private GameObject _actualArena;
    [SerializeField] private List<GameObject> _arenas;
    [SerializeField] private GameObject _map2J;
    [SerializeField] private GameObject _map3J;
    [SerializeField] private GameObject _map4J;




    private void Start()
    {

        _gameManager = GameManager.Instance;
        _playersList = _gameManager.GetPlayerManager().GetPlayerList();
        _groundsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ground"));
 

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Init();
        }
    }


    private void Init()
    {
        _playersList = _gameManager.GetPlayerManager().GetPlayerList(); // TODO : replace avec un "GetActifPlayers"
        switch (_playersList.Count)
        {
            case 1:
                _map2J.SetActive(true);
                break;
            case 3:
                _map3J.SetActive(true);
                break;
            case 4:
                _map4J.SetActive(true);
                break;
        }
        AssignArena();
        var i = 0;
        foreach (VolleyBombZone zone in _zonesList)
        {
            zone.SetOwner(_playersList[0]);
            zone.SetZoneMaterial(_playersList[0].PlayerId);
            _playersList.RemoveAt(0);
            
        }
        
    }

    void AssignArena()
    {
        int playerNum = _playersList.Count;
        switch (playerNum)
        {
            case 4:
                _actualArena = _arenas[0];
                break;
            case 3:
                _actualArena = _arenas[1]; // TODO : a modifier
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
