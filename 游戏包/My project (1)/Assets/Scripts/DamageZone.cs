﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
   private void OnTriggerStay2D(Collider2D collision)
   {
    RubyController rubyController=collision.GetComponent<RubyController>();
    if(rubyController!=null)
    {
        rubyController.ChangeHealth(-1);
        Debug.Log("Ruby当前的生命值是："+rubyController.Health);
    }
   }

}
