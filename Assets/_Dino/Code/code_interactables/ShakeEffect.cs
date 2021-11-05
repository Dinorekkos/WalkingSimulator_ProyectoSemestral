using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private Transform transform;
    [Header("Shake")]
    public float Sduration;
    public Vector3 Sstrenght;
    [Header("Rotate")] 
    public Vector3 Rvector;
    public float Rduration;



    public void DOShake()
    {
        var tweener = transform.DOShakePosition(Sduration, Sstrenght);
        if(tweener.IsPlaying()) return;
    }

    public void DORotate()
    {
        var tweener = transform.DORotate(Rvector, Rduration);
    }
    
    
}
