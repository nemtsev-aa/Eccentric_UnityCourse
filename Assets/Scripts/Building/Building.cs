using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType {
    Barrack,
    Farm,
    TownHall
}

public class Building : SelectableObject
{
    public BuildingType BuildingType;
    public int Price;
    public int Health;
    public int XSize = 3;
    public int ZSize = 3;
    public Renderer Renderer;
    public ParticleSystem DamageEffect;
    public GameObject CollectionPoint;

    private Color _startColor;

    private void Awake() {
        _startColor = Renderer.material.color;
    }

    public void DisplayUnacceptablePosition() {
        Renderer.material.color = Color.red;
    }

    public void DisplayAcceptablePosition() {
        Renderer.material.color = _startColor;
    }

    private void OnDrawGizmos() {

        float cellSize = FindObjectOfType<BuildingPlacer>().CellSize;

        for (int x = 0; x < XSize; x++) {
            for (int z = 0; z < ZSize; z++) {
                Gizmos.DrawWireCube(transform.position + new Vector3(x, 0f, z) * cellSize, new Vector3(1f, 0f, 1f) * cellSize);
            }
        }
    }

    public void TakeDamage(int damageValue) {
        Health -= damageValue;
        ParticleSystem damageEffect = Instantiate(DamageEffect, transform.position + new Vector3((XSize / 2), 0f, (ZSize / 2)), Quaternion.identity);
        Destroy(damageEffect.gameObject, 1f);
        if (Health <= 0) {
            Die();
        }
    }

    public void Die() {
        // !!!Удалить строение из словаря

        Destroy(gameObject);
    }
}
