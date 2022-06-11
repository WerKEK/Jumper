using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Inventory inventory;
    [SerializeField] private SoudEffect soundEffect;
    [SerializeField] public bool inWater;
    [SerializeField] private bool isClimbing;
    [SerializeField] private Main main;
    [SerializeField] private bool key;
    [SerializeField] private GameObject blueGem;
    [SerializeField] private GameObject greenGem;

    private int _curHp;
    private readonly int maxHP = 3;
    private int _coins;
    private int _gemCount;
    private bool _canTp = true;
    private bool _canHit = true;
    private bool _isGrounded;
    private Animator _anim;
    private Rigidbody2D _rb;
    private static readonly int State = Animator.StringToHash("State");
    private static readonly int IsJump = Animator.StringToHash("isJump");

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _curHp = maxHP;
    }
    private void Update()
    {
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            soundEffect.PlayJumpSound();
        }
        Flip();
        if (inWater && !isClimbing)
        {
            _anim.SetInteger(State, 4);
            _isGrounded = false;
            if (Input.GetAxis("Horizontal") != 0)
                Flip();
        }
        else
        {
            CheckGround();
            if (Input.GetAxis("Horizontal") == 0 && (_isGrounded) && !isClimbing)
            {
                _anim.SetInteger(State, 1);
            }
            else
            {
                Flip();
                if (_isGrounded && !isClimbing)
                    _anim.SetInteger(State, 2);
            }
        }
    }
    private void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);

    }
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        _isGrounded = colliders.Length > 1;
        if (!_isGrounded && !isClimbing)
            _anim.SetInteger(State, 3);
    }
    private void Lose()
    {
        main.GetComponent<Main>().Lose();
    }
    public void RecountHp(int deltaHp)
    {
        if (deltaHp < 0 && _canHit)
        {
            _curHp += deltaHp;
            _canHit = false;
            StartCoroutine(OnHit());          
        }
        if (deltaHp > 0 && _curHp != 3)
        {
            _curHp += deltaHp;
        }
        if (_curHp > maxHP)
        {
            _curHp = maxHP;
        }
        if (_curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke(nameof(Lose), 1.5f);
        }
    }
    private IEnumerator OnHit()
    {
        _canHit = false;
        yield return new WaitForSeconds(0.3f);
        _canHit = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            key = true;
            inventory.Add_key();
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            if (collision.gameObject.GetComponent<Door>().isOpen && _canTp)
            {
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                _canTp = false;
                StartCoroutine(TeleportWaiter());
            }
            else if (key)
                collision.gameObject.GetComponent<Door>().Unlock();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {          
            Destroy(collision.gameObject);
            _coins++;
            soundEffect.PlayCoinSound();
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            inventory.Add_hp();
        }
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            Destroy(collision.gameObject);
            RecountHp(-1);
        }
        if (collision.gameObject.CompareTag("GreenGem"))
        {
            Destroy(collision.gameObject);
            inventory.Add_gg();
        }
        if (collision.gameObject.CompareTag("BlueGem"))
        {
            Destroy(collision.gameObject);
            inventory.Add_bg();
        }
    }
    private IEnumerator TeleportWaiter()
    {
        yield return new WaitForSeconds(1f);
        _canTp = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Ladder"))
        {
            isClimbing = true;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            if (Input.GetAxis("Vertical") == 0)
            {
                _anim.SetInteger(State, 5);
            }
            else
            {
                _anim.SetInteger(State, 6);
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime);
            }
        }
        if (collision.gameObject.CompareTag("Icy"))
        {
            if (_rb.gravityScale == 1f)
            {
                _rb.gravityScale = 7f;
                speed *= 0.25f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isClimbing = false;
        if (collision.gameObject.CompareTag("Ladder"))
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (collision.gameObject.CompareTag("Icy"))
        {
            if (_rb.gravityScale == 7f)
            {
                _rb.gravityScale = 1f;
                speed *= 4f;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            StartCoroutine(TrampolineAnim(collision.gameObject.GetComponentInParent<Animator>()));
        }
        if (collision.gameObject.CompareTag("Sand"))
        {
            speed *= 0.25f;
            _rb.mass *= 100f;
        }
    }
    private static IEnumerator TrampolineAnim(Animator animator)
    {
        animator.SetBool(IsJump, true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(IsJump, false);
    }
    private IEnumerator SpeedBonus()
    {
        _gemCount++;
        greenGem.SetActive(true);
        CheckGems(greenGem);
        speed *= 2;
        greenGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(InviseForGem(greenGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        speed /= 2;
        _gemCount--;
        greenGem.SetActive(false);
        CheckGems(blueGem);
    }
    private IEnumerator UnhitBonus()
    {
        _gemCount++;
        blueGem.SetActive(true);
        CheckGems(blueGem);
        _canHit = false;
        blueGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(InviseForGem(blueGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        _canHit = true;
        _gemCount--;
        blueGem.SetActive(false);
        CheckGems(greenGem);
    }
    private void CheckGems(GameObject obj)
    {
        if (_gemCount == 1)
            obj.transform.localPosition = new Vector3(0f, 0.6f, obj.transform.localPosition.z);

        else if (_gemCount == 2)
        {
            blueGem.transform.localPosition = new Vector3(-0.5f, 0.5f, blueGem.transform.localPosition.z);
            greenGem.transform.localPosition = new Vector3(0.5f, 0.5f, greenGem.transform.localPosition.z);
        }
    }
    private IEnumerator InviseForGem(SpriteRenderer spr, float time)
    {
        spr.color = new Color(1f, 1f, 1f, spr.color.a - time * 2);
        yield return new WaitForSeconds(time);
        if (spr.color.a > 0)
        {
            StartCoroutine(InviseForGem(spr, time));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sand"))
        {
            speed *= 4f;
            _rb.mass *= 0.01f;
        }
    }
    public int GetCoins()
    {
        return _coins;
    }
    public int GetHp()
    {
        return _curHp;
    }
    public void BlueGem()
    {
        StartCoroutine(UnhitBonus());
    }
    public void GreenGem()
    {
        StartCoroutine(SpeedBonus());
    }
}













