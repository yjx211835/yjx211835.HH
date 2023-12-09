using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2d;
    public bool vertical;

    private int direction=1;
    public float changeTime=3;
    private float timer;

    private Animator animator;

    private bool broken;

    public ParticleSystem smokeEffect;

    private AudioSource audioSource;

    public AudioClip fixedSound;

     public AudioClip[] hitSounds;

      public GameObject hitEffectParticle;
    // Start is called before the first frame update
    void Start()
    {
       rigidbody2d=GetComponent<Rigidbody2D>() ;
       timer=changeTime;
       animator=GetComponent<Animator>();
        //animator.SetFloat("MoveX",direction);
        PlayMoveAnimation();
        broken=true;
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

      if(!broken)
      {
        //已修好
        return;
      }
      timer-=Time.deltaTime;
      if(timer<0)
      {
        direction=-direction;
        PlayMoveAnimation();
       // animator.SetFloat("MoveX",direction);
       // animator.SetBool("Vertical",vertical);
       
        timer=changeTime;
      }
      
      Vector2 position=rigidbody2d.position;

      if(vertical)
      {
        position.y=position.y+Time.deltaTime*speed*direction;
      }
      else
      {
         position.x=position.x+Time.deltaTime*speed*direction;
      }
     
      rigidbody2d.MovePosition(position)  ;
    }
    //触发检测
  private void OnCollisionEnter2D(Collision2D collision)
  {
    RubyController rubyController=collision.gameObject.GetComponent<RubyController>();
      
  if(rubyController!=null)
  {
    rubyController.ChangeHealth(-1);
   // rubyController.PlaySound();
  }
  }
  //控制移动动画的方法
  private void PlayMoveAnimation()
  {
if(vertical)
      {
        animator.SetFloat("MoveX",0);
        animator.SetFloat("MoveY",direction);
      }
      else//水平轴向
      {
        animator.SetFloat("MoveX",direction);
        animator.SetFloat("MoveY",0);
      }
  }
  public void Fix()
  {
    Instantiate(hitEffectParticle,transform.position,Quaternion.identity);
    broken=false;
    rigidbody2d.simulated=false;
    animator.SetTrigger("Fixed");
    smokeEffect.Stop();
   //audioSource.PlayOneShot(fixedSound);
    //audioSource.PlayOneShot(fixedSound);
    int randomNum = Random.Range(0,2);
    audioSource.Stop();
        audioSource.volume = 0.5f;
     audioSource.PlayOneShot(hitSounds[randomNum]);
      Invoke("PlayFixedSound",1f);
        UIHealthBar.instance.fixedNum++;
    
  }
  private void PlayFixedSound()
    {
      audioSource.PlayOneShot(fixedSound);
      //audioSource.Stop();
    }
}
