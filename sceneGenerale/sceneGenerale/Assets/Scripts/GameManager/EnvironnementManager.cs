using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironnementManager : MonoBehaviour
{
    public static EnvironnementManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    [SerializeField]
    float _qualiteAir;
    [SerializeField]
    float _qualiteEau;
    [SerializeField]
    float _qualiteSol;

    public float qualiteAir
    {
        get => _qualiteAir;
        set => _qualiteAir = Mathf.Clamp(value, 0, 100f);
    }
    public float qualiteEau
    {
        get => _qualiteEau;
        set => _qualiteEau = Mathf.Clamp(value, 0, 100f);
    }
    public float qualiteSol
    {
        get => _qualiteSol;
        set => _qualiteSol = Mathf.Clamp(value, 0, 100f);
    }

    private void OnValidate()
    {
        qualiteAir = _qualiteAir;
        qualiteEau = _qualiteEau;
        qualiteSol = _qualiteSol;
    }

    public float maxQE = 70f;

    private void Start()
    {
        qualiteAir = 70f;
        qualiteEau = maxQE;
        qualiteSol = 70f;
    }

    public void MajEnviro()
    {

    }
}
