using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bombes")]
public class ScriptableBomb : ScriptableObject
{
    public float _dropSpeed;
    public float _timerExplosion;
    [SerializeField] bool _fakeBomb;
    [SerializeField] TypeOfBombs _type;
    [SerializeField] float _radius;

}