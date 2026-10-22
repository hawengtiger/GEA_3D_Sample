using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("플레이어만 연결")]
    [Tooltip("최대 체력")]
    public int maxHp = 3;
    [Tooltip("현재 오브젝트 체력")]
    public int currentHp;

    [Header("플레이어만 연결")]
    [Tooltip("적 오브젝트에는 이걸 붙이지 않아도 됩니다.")]
    public Slider hpslider;

    public float invincibleTime = 0f;   // 플레이어는 0.5

    public GameObject dropPrefab;     // 적만 연결
    public float dropChance = 0.3f;   // 30%

    public GameObject hitEffect;   // HitEffect Prefab

    float lastHitTime = -999f;


    public static int killCount;    // 모든 Health가 같이 쓰는 값 하나

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (currentHp <= 0) return;
        if (Time.time < lastHitTime + invincibleTime) return;
        lastHitTime = Time.time;

        currentHp -= damage;
        Debug.Log(name + " HP: " + currentHp);
        UpdateBar();

        Vector3 hitPos = transform.position + Vector3.up;
        if (hitEffect != null)
            Instantiate(hitEffect, hitPos, Quaternion.identity);
    

        if (currentHp <= 0) Die();
        else Blink();
    }
    public void Heal(int amount)
    {
        currentHp = Mathf.Min(currentHp + amount, maxHp);
        UpdateBar();
    }



    void UpdateBar()
    {
        if (hpslider != null)
            hpslider.value = (float)currentHp / maxHp;
    }

    void Die()
    {
        if (CompareTag("Player"))
        {
            Debug.Log("게임 오버");
            GameManager.Instance.GameOver();
        }
        else
        {
            killCount++;

            if (dropPrefab != null && Random.value < dropChance)
            {
                Vector3 pos = transform.position + Vector3.up * 0.5f;
                Instantiate(dropPrefab, pos, Quaternion.identity);
            }


            Destroy(gameObject);
        }
    }
    void Blink()
    {
        SetVisible(false);
        Invoke(nameof(ShowAgain), 0.1f);
    }

    void ShowAgain()
    {
        SetVisible(true);
    }

    void SetVisible(bool on)
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = on;
    }

}
