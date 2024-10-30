using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyBombZone : MonoBehaviour
{

    VolleyBombManager m_volleyBombManager;
    PlayerController m_owner;
    Renderer _renderer;

    int m_zoneID;

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void SetZoneMaterial(int playerID)
    {

        Debug.Log("zebi");
        switch (playerID)
        {
            case 0:
                _renderer.material = Resources.Load("Materials/PLAYERS/P1/Player1SecondaryMaterial", typeof(Material)) as Material;
                break;
            case 1:
                _renderer.material = Resources.Load("Materials/PLAYERS/P2/Player2SecondaryMaterial", typeof(Material)) as Material;
                break;                                                  
            case 2:                                                     
                _renderer.material = Resources.Load("Materials/PLAYERS/P3/Player3SecondaryMaterial", typeof(Material)) as Material;
                break;                                                  
            case 3:                                                     
                _renderer.material = Resources.Load("Materials/PLAYERS/P4/Player4SecondaryMaterial", typeof(Material)) as Material;
                break;
        }
    }
    public void SetOwner(PlayerController owner)
    {
        m_owner = owner;
    }
}