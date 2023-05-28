using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BuildingType {
    Barrack,
    Farm,
    TownHall,
    Castle
}

public enum StagesOfConstruction {
    Pit,
    Walls,
    Readiness
}

public class Building : SelectableObject
{
    public StagesOfConstruction CurrentStagesOfConstruction;
    public List<GameObject> Constructions = new List<GameObject>();

    public BuildingType BuildingType;
    public int Price;
    public int Health;
    public int XSize = 3;
    public int ZSize = 3;
    public Renderer Renderer;
    [Tooltip("Полоска жизни")]
    public GameObject HealthBarPrefab;
    public GameObject HealthBarPoint;
    public GameObject CollectionPoint;
    
    public ParticleSystem DamageEffect;
    public ParticleSystem RenovationEffect;
    
    [SerializeField] private AudioClip _buildingconstructionSound;

    public event Action<int, int> OnHealth;

    private Color _startColor;
    private HealthBar _healthBar;
    private int _maxHealth;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _startColor = Renderer.material.color;
        
        _maxHealth = Health;
        Health = Mathf.RoundToInt(_maxHealth * 0.1f);

        CreateHealthBar();
        
    }
    public virtual void CreateHealthBar() {
        GameObject healthBar = Instantiate(HealthBarPrefab);
        healthBar.transform.parent = transform;

        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(HealthBarPoint.transform);
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
        ShowHealth();
        _healthBar.SetHealth(Health, _maxHealth);
        ParticleSystem damageEffect = Instantiate(DamageEffect, transform.position + new Vector3((XSize / 2), 0f, (ZSize / 2)), Quaternion.identity);
        Destroy(damageEffect.gameObject, 1f);
        if (Health <= 0) {
            Die();
        }
    }

    public void TakeHealth(int healthValue) {
        Health += healthValue;
        ShowHealth();
        _healthBar.SetHealth(Health, _maxHealth);
        
        ParticleSystem damageEffect = Instantiate(RenovationEffect, transform.position + new Vector3((XSize / 2), 0f, (ZSize / 2)), Quaternion.identity);
        Destroy(damageEffect.gameObject, 1f);
        
        _audioSource.clip = _buildingconstructionSound;
        _audioSource.Play();

        float percentageOfReadiness = GetHealthProcentage();
        if (percentageOfReadiness < 30f) {
            SetStage(StagesOfConstruction.Pit);
        } else if (percentageOfReadiness > 30f && percentageOfReadiness < 90f) {
            SetStage(StagesOfConstruction.Walls);
        } else if (percentageOfReadiness > 90f) {
            SetStage(StagesOfConstruction.Readiness);
        }      

        if (Health >= _maxHealth) {
            Health = _maxHealth;
        }
    }

    public virtual void SetStage(StagesOfConstruction currentStage) {
        CurrentStagesOfConstruction = currentStage;
        foreach (var item in Constructions) {
            item.SetActive(false);
        }
        switch (currentStage) {
            case StagesOfConstruction.Pit:
                Constructions[0].SetActive(true);
                break;
            case StagesOfConstruction.Walls:
                Constructions[1].SetActive(true);
                break;
            case StagesOfConstruction.Readiness:
                Constructions[2].SetActive(true);
                break;
        }
    }

    public void Die() {
        // !!!Удалить строение из словаря

        Destroy(gameObject);
    }

    public void ShowHealth() {
        if (Health >= 0) {
            OnHealth?.Invoke(Health, _maxHealth);
        } 
    }

    public float GetHealthProcentage() {
        return ((float)Health/_maxHealth) * 100;
    }
}
