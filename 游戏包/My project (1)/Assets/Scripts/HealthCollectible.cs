using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
   public AudioClip audioClip;
   private void OnTriggerEnter2D(Collider2D collision)
   {
   // Debug.Log("与我们发生碰撞的对象是："+collision);
   RubyController rubyController=collision.GetComponent<RubyController>();
   if(rubyController!=null)
   {
     if (rubyController.currentHealth<rubyController.maxHealth)
     {
rubyController .ChangeHealth(1);
rubyController.PlaySound(audioClip);
   Destroy(gameObject);
     } 
   }
   }
}
