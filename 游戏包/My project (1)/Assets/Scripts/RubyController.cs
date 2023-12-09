using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public int maxHealth=5;//最大生命值
    public int currentHealth;//当前生命值
    public int speed=5;
    //Ruby的无敌时间
    public float timeInvincible=2.0f;//无敌时间常量
    private bool isInvincible;
    private float invincibleTimer;

    public int Health{get{return currentHealth;}}

private Vector2 lookDirection = new Vector2(1,0);
private Animator animator;

public AudioSource audioSource;

public GameObject projectilePrefab;
public AudioClip playerHit;

public AudioClip attackSoundClip;

public AudioClip walkSound;
 public AudioSource walkAudioSource;
 //respawnPosition = transform.position;
 private Vector3 respawnPosition;

//public AudioSource walkAudioSource;

 
    //Start is called before the first frame update
     private void Start()
    {
       rigidbody2d=GetComponent<Rigidbody2D>();
       currentHealth=maxHealth;
       animator=GetComponent<Animator>();
       respawnPosition = transform.position;
      //audioSource=GetComponent<AudioSource>();
//int a =GetGubyHealthValue();
//Debug.Log("Ruby当前的血量是："+a);
    }

    // Update is called once per frame
    void Update()
    {
     float horizontal=Input.GetAxis("Horizontal") ;
     float vertical=Input.GetAxis("Vertical") ;
     
     Vector2 move=new Vector2(horizontal,vertical);
     //当前玩家输入的某个轴向值不为0
     if(!Mathf.Approximately(move.x,0)||!Mathf.Approximately(move.y,0))
     {
      lookDirection.Set(move.x,move.y);
       lookDirection.Normalize();
       //audioSource.Play();
      // audioSource.clip=walkSound;
      if(!walkAudioSource.isPlaying)
      {
        walkAudioSource.clip=walkSound;
        walkAudioSource.Play();
      }
     }
     else{
      walkAudioSource.Stop();
     }
     
     animator.SetFloat("Look X",lookDirection.x);
     animator.SetFloat("Look Y",lookDirection.y);
     animator.SetFloat("Speed",move.magnitude);
     
     
     Vector2 position=transform.position;
     //position.x=position.x+speed*horizontal*Time.deltaTime;
     //position.y=position.y+speed*vertical*Time.deltaTime;
     //transform.position=position;

position=position+speed*move*Time.deltaTime;

      rigidbody2d.MovePosition(position); 

      if(isInvincible)
      {
        invincibleTimer=invincibleTimer-Time.deltaTime;
        if(invincibleTimer<=0)
        {
             isInvincible=false;
        }
      } 
      if (Input.GetKeyDown(KeyCode.H))
    {
      Launch();
    }
    //检测与NPC对话
    if(Input.GetKeyDown(KeyCode.T))
    {
      new Vector2(0,1);
RaycastHit2D hit=Physics2D.Raycast(rigidbody2d.position+Vector2.up*0.2f,lookDirection,1.5f,LayerMask.GetMask("NPC"));
if(hit.collider!=null)
{
  //Debug.Log("当前射线检测碰撞到的游戏物体是："+hit.collider.gameObject);
  NPCDialog npcDialog=hit.collider.GetComponent<NPCDialog>();
  if(npcDialog!=null)
  {
    npcDialog.DisplayDialog();
  }
}
    }
    }

   

    public void ChangeHealth(int amount)
    {
      if(amount<0)
      {
        if(isInvincible)
        {
          return;
        }
        isInvincible=true;
        invincibleTimer=timeInvincible;
      animator.SetTrigger("Hit");
        PlaySound(playerHit);
      }
      currentHealth=Mathf.Clamp(currentHealth+amount,0,maxHealth);
      //Debug.Log(currentHealth+"/"+maxHealth);
       if (currentHealth<=0)
        {
            Respawn();
        }
      UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
    }
   
   private void Launch()
   {
    if(!UIHealthBar.instance.hasTask)
    {
      return;
    }
    GameObject projectileObject=Instantiate(projectilePrefab,rigidbody2d.position+Vector2.up*0.5f,Quaternion.identity);
    
    Projectile  projectile=projectileObject.GetComponent<Projectile>();
    projectile.Launch(lookDirection,300);
    animator.SetTrigger("Launch");
    PlaySound(attackSoundClip);
   }

   public void PlaySound(AudioClip audioClip)
   {
      audioSource.PlayOneShot(audioClip);
   }
    private void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition;
    }
}
