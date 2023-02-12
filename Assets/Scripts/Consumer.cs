using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [Tooltip("Âğåìÿ ñóùåñòâîâàíèÿ")]
    [SerializeField] public float LifeTime;
    [Tooltip("Ñïğàéò")]
    [SerializeField] private GameObject canvas;
    [Tooltip("Ñèñòåìà ÷àñòèö - âûçîâ äîñòàâùèêà")]
    [SerializeField] private ParticleSystem _ñallingDeliverer;
    [Tooltip("Ñèñòåìà ÷àñòèö - äîâîëüíûé êëèåíò")]
    [SerializeField] private ParticleSystem _satisfiedÑustomer;
    [Tooltip("Ñèñòåìà ÷àñòèö - çëîé êëèåíò")]
    [SerializeField] private ParticleSystem _angryÑustomer;
    
    [SerializeField] private float _time;
    // Start is called before the first frame update
    void Start()
    {

        //_particle = GetComponent<ParticleSystem>();
        //_particle.Stop();
        //var particleDuration = _particle.main;
        //particleDuration.duration = 1f;

        DeactivateParticleSystem(_satisfiedÑustomer, true);
        DeactivateParticleSystem(_angryÑustomer, true);

        //_satisfiedÑustomer.Pause();
        //_angryÑustomer.Pause();
        _ñallingDeliverer.Play();
        
        _time = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _time = LifeTime;
            FailedDelivery();
        }

        //else
        //{
        //    float proc = _time / LifeTime;
        //    var particleDuration = _particle.main;
        //    particleDuration.duration = 1f - proc;
        //    Debug.Log("particleDuration" + particleDuration.duration);
        //}   
    }

    /// Ïîäïèñêà íà ñîáûòèÿ 
    private void OnEnable()
    {
        PlayerControllerX.OnSuccessfulDelivery += OnSuccessfulDelivery;
    }

    /// Îòïèñêà îò ñîáûòèÿ 
    private void OnDisable()
    {
        PlayerControllerX.OnSuccessfulDelivery -= OnSuccessfulDelivery;
    }

    ///Îáğàáîò÷èê ñîáûòèÿ "Óäà÷íàÿ äîñòàâêà"
    private void OnSuccessfulDelivery(PlayerControllerX player)
    {
        SuccessfulDelivery();
    }
    private void SuccessfulDelivery()
    {
        canvas.SetActive(false);
        this.GetComponent<SphereCollider>().enabled = false;

        //DeactivateParticleSystem(_ñallingDeliverer, false);
        //DeactivateParticleSystem(_satisfiedÑustomer, true);
        //_satisfiedÑustomer.Play(); 
    }

    private void FailedDelivery()
    {
        canvas.SetActive(false);

        DeactivateParticleSystem(_ñallingDeliverer, true);
        _ñallingDeliverer.Stop();

        DeactivateParticleSystem(_angryÑustomer, false);
        _angryÑustomer.Play();
        Invoke(nameof(Destroy), 2f);
    }

    private void DeactivateParticleSystem(ParticleSystem particleSystem, bool status)
    {
        CFX_AutoDestructShuriken autoDestruct = particleSystem.GetComponent<CFX_AutoDestructShuriken>();
        autoDestruct.OnlyDeactivate = status;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    
}
