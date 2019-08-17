using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public Sprite dmgSprite;
    public int hp = 3;

    private SpriteRenderer spriteRenderer;
    
    public AudioClip chopSound1;
    public AudioClip chopSound2;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        spriteRenderer.sprite = dmgSprite;

        hp -= loss;
        SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
