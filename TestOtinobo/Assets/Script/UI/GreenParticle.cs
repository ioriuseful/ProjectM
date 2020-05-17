using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem.MinMaxGradient testColor;
    [SerializeField] private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangeColorFromKind()
    {
        ParticleSystem.MainModule main = particle.main;
        main.startColor = testColor;
        Debug.Log("ChangeColor");
    }
}
