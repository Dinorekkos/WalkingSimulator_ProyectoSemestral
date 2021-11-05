using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wave_Effect : MonoBehaviour
{
  [SerializeField] private Transform transform;
  [SerializeField] private float duration;
  [SerializeField] private int vibrato;
  [SerializeField] private float move;

  private void Start()
  {
    DOParhEffect();
  }
  
  void DOParhEffect()
  {
      var tweener = transform.DOShakePosition(duration,move,vibrato).SetLoops(-1);
      if(tweener.IsPlaying()) return;
  }
}
