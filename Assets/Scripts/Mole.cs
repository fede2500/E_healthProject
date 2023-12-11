using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mole : MonoBehaviour
{
    
    public ScoreManagerMole scoreManager;
    public Sprite mole;
    public Sprite moleHit;
    public Sprite bomb;
    
    public enum MoleType { Mole,  Bomb };
    private MoleType moleType;
    
    private Vector2 start = new Vector2(0f, -2.56f);
    private Vector2 end = Vector2.zero;
    public float showDuration = 0.5f;
    private float duration = 1f;
    private float bombRate = 0.35f;
    private int moleIndex = 0;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;
    private bool hittable = true;
    

    private void Start()
    {
        SetLevel(0);
        CreateNext();
        StartCoroutine(ShowHide(start, end));
    }
    
    
    private void Awake() {
        // Get references to the components we'll need.
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        // Work out collider values.
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -start.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);
    }
    
    public void Activate(int level)
    {
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(start, end));
    }
    
    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {

        // Make sure we start at the start.
        transform.localPosition = start;

        // Show the mole.
        float elapsed = 0f;
        while (elapsed < showDuration) {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
            
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Make sure we're exactly at the end.
        transform.localPosition = end;
        boxCollider2D.offset = boxOffset;
        boxCollider2D.size = boxSize;

        // Wait for duration to pass.
        yield return new WaitForSeconds(duration);

        // Hide the mole.
        elapsed = 0f;
        while (elapsed < showDuration) {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
           
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Make sure we're exactly back at the start position.
        transform.localPosition = start;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
   
    }
    
    private void OnMouseDown() {
        if (hittable)
        {
            switch (moleType)
            {
                case MoleType.Mole:
                    spriteRenderer.sprite = moleHit;
                    scoreManager.AddScore(moleIndex);
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    hittable = false;
                    break;
                case MoleType.Bomb:
                    scoreManager.RemoveScore(moleIndex);
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    hittable = false;
                    break;
            }
        }
    }
    
    public IEnumerator QuickHide() {
        yield return new WaitForSeconds(0.25f);
        
        if (!hittable) {
            transform.localPosition = start;
            boxCollider2D.offset = boxOffsetHidden;
            boxCollider2D.size = boxSizeHidden;
        }
        
    }
    
    private void CreateNext() {
        float random = Random.Range(0f, 1f);
        if (random < bombRate) {
            // Make a bomb.
            moleType = MoleType.Bomb;
            spriteRenderer.sprite = bomb;
            
        } else {
            moleType = MoleType.Mole;
            spriteRenderer.sprite = mole;
            
        }
        // Mark as hittable so we can register an onclick event.
        hittable = true;
    }
    
    private void SetLevel(int level) {
        // As level increases increse the bomb rate to 0.25 at level 10.
        bombRate = Mathf.Min(level * 0.3f, 0.50f);
        
    }

    public void SetIndex(int index)
    {
        moleIndex = index;
    }
    
    public void restartGame()
    {
        SceneManager.LoadScene("Profiling_anx _pix");
    }
    
   
}
