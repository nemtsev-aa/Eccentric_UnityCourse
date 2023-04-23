using UnityEngine;

public enum ItemType
{
    Empty,
    Ball,
    Barrel,
    Stone,
    Box,
    Dinamit,
    Star
}
public class Item : MonoBehaviour
{
    public ItemType ItemType;
}
