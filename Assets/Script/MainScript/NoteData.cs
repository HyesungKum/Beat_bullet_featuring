using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/NoteData", fileName = "NoteData", order = int.MaxValue)]
public class NoteData : ScriptableObject
{
    [SerializeField] public AudioClip checkClip;

    public float noteSpeed = 5f;
}
