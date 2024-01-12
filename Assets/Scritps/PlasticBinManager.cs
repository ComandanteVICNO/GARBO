using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBinManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public DragAndDrop dragNDrop;
    public GameObject particleEffect;
    public Transform particleSource;

    public Material particleMaterial;
    public Material particleEmissionMaterial;

    public Color particleColor = new Color(231, 204, 39, 255);
    public float emissionIntensity;


    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        dragNDrop = FindAnyObjectByType<DragAndDrop>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DragAndDrop>() == null) return;
        dragNDrop = other.GetComponent<DragAndDrop>();


        if (dragNDrop.isDragging) return;
        else
        {
            if (other.CompareTag("Plastic") || other.CompareTag("Metal"))
            {
                scoreManager.IncreaseScore();
                Object.Destroy(other.gameObject);
                SpawnParticles();
            }
            else
            {
                scoreManager.DecreaseScore();
                Object.Destroy(other.gameObject);
                SpawnSadParticles();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dragNDrop = null;

        
    }

    public void SpawnParticles()
    {

        particleMaterial.color = particleColor;
        particleEmissionMaterial.SetColor("_EmissionColor", particleColor);
        particleEmissionMaterial.color = particleColor;
        GameObject particlesObject = Instantiate(particleEffect, particleSource);

    }
    public void SpawnSadParticles()
    {
        Color sadParticleColor = new Color(0, 0, 0, 255);
        Color sadEmissionColor = new Color(0, 0, 0, 255);

        particleMaterial.color = sadParticleColor;
        particleEmissionMaterial.SetColor("_EmissionColor", sadEmissionColor);
        particleEmissionMaterial.color = sadEmissionColor;

        GameObject particlesObject = Instantiate(particleEffect, particleSource);

    }
}
