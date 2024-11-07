using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VolleyBombManager : MonoBehaviour
{

    GameManager _gameManager;

    [SerializeField] private List<GameObject> _bombSpawnList;
    [SerializeField] private GameObject _bomb;
    public bool _bombDidntSpawn;
    public List<PlayerController> _playersList;
    [SerializeField] private List<GameObject> _groundsList;
    [SerializeField] private List<VolleyBombZone> _zonesList;
    [SerializeField] private List<GameObject> _playersSpawners;

    [SerializeField] private GameObject _actualArena;
    [SerializeField] private List<GameObject> _arenas;
    [SerializeField] private GameObject _map2J;
    [SerializeField] private GameObject _map3J;
    [SerializeField] private GameObject _map4J;

    public float _interTime;
    [SerializeField] private GameObject _intertime;
    [SerializeField] private TMP_Text _intertimeDisplay;
    public float _intertiming;
    VolleyBombZone _zone;




    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playersList = _gameManager.GetPlayerManager().GetPlayerList();
        _playersSpawners = new List<GameObject>();
        _zone = FindObjectOfType<VolleyBombZone>();
        _bombDidntSpawn = true;
        Init();
        _intertiming = _interTime;
    }
    private void Update()
    {
        if(_intertiming > 1)
        {
            _intertiming -= Time.deltaTime;
            _intertimeDisplay.text = ((int)_intertiming).ToString();
        }
        else
        {
            _intertimeDisplay.text = "GO";
        }
    }

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
                StartCoroutine(SpawnBomb(spawnerChoosed));
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
    IEnumerator SpawnBomb(GameObject spawnerChoosed)
    {
        _intertime.gameObject.SetActive(true);
        yield return new WaitForSeconds(_interTime);
        _intertime.gameObject.SetActive(false);
        
        Instantiate(_bomb, spawnerChoosed.transform.position,Quaternion.identity);
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
