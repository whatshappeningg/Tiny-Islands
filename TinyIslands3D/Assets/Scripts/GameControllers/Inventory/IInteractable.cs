using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public ObjectType Type { get; }
    public List<InteractionOption> Options { get; }
}

[System.Serializable]
public class InteractionOption
{
    public KeyCode Key;
    public InteractionType Action;
}

[System.Serializable]
public enum InteractionType
{
    collect,
    fire,
    repair,
    dash,
    embark,
    disembark,
}

[System.Serializable]
public enum ObjectType
{
    wood,
    pearl,
    cannonball,
    cannon,
    raft,
}

