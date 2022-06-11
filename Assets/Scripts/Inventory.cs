using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite[] numbers;
    [SerializeField] private Sprite isHp, noHp, isBg, noBg, isGg, noGg, isKey;
    [SerializeField] private Image hpImg, bgImg, ggImg, keyImg;
    [SerializeField] private Player player;
    private int hp, bg, gg;
    private void Start()
    {
        if (PlayerPrefs.GetInt("hp") > 0)
        {
            hp = PlayerPrefs.GetInt("hp");
            hpImg.sprite = isHp;
            hpImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
        }
        if (PlayerPrefs.GetInt("bg") > 0)
        {
            bg = PlayerPrefs.GetInt("bg");
            bgImg.sprite = isBg;
            bgImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bg];
        }
        if (PlayerPrefs.GetInt("gg") > 0)
        {
            gg = PlayerPrefs.GetInt("gg");
            ggImg.sprite = isGg;
            ggImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[gg];
        }
    }
    public void Add_hp()
    {
        hp++;
        hpImg.sprite = isHp;
        hpImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
    }
    public void Add_bg()
    {
        bg++;
        bgImg.sprite = isBg;
        bgImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bg];
    }
    public void Add_gg()
    {
        gg++;
        ggImg.sprite = isGg;
        ggImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[gg];
    }
    public void Add_key()
    {
        keyImg.sprite = isKey;
    }
    public void Use_hp()
    {
        if(hp > 0)
        {
            hp--;
            player.RecountHp(1);
            hpImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
            if(hp == 0)
                hpImg.sprite = noHp;
        }
    }
    public void Use_bg()
    {
        if (bg > 0)
        {
            bg--;
            player.BlueGem();
            bgImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bg];
            if (bg == 0)
                bgImg.sprite = noBg;
        }
    }
    public void Use_gg()
    {
        if (gg > 0)
        {
            gg--;
            player.GreenGem();
            ggImg.transform.GetChild(0).GetComponent<Image>().sprite = numbers[gg];
            if (gg == 0)
                ggImg.sprite = noGg;
        }
    }
    public void RecountItems()
    {
        PlayerPrefs.SetInt("hp", hp);
        PlayerPrefs.SetInt("gg", gg);
        PlayerPrefs.SetInt("bg", bg);
    }
}
