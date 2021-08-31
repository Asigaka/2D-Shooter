using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform begin;
    [SerializeField] private Transform end;

    public Transform Begin { get => begin; private set => begin = value; }
    public Transform End { get => end; private set => end = value; }
}
