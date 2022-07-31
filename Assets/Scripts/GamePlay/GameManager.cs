using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    RenkItem[] renkItem;

    [SerializeField]
    TMP_Text anlamTxt, renkTxt;

    [SerializeField]
    TMP_Text toplamPuanTxt,sureTxt;

    [SerializeField]
    GameObject bonusYazi;

    [SerializeField]
    GameObject pausePanel,sonucPanel,ustPanel,butonlarPanel,ortaPanel;

    [SerializeField]
    TMP_Text dogruAdetTxt, yanlisAdetTxt;


    [SerializeField]
    TMP_Text dogruSonucTxt, yanlisSonucTxt, sonucToplamPuanTxt;

    int rastgeleDogruAnlam;
    int rastgeleYanlisAnlam;

    Color32 yukariRenk;

    Color32 dogruRenk;

    bool sonucDogrumu;

    int dogruAdet;
    int yanlisAdet;

    int bonusAdet;
    bool bonusVarmi;

    int puan=10;
    int toplamPuan;

    int kalanSure;

    public bool pauseBasildimi;

    SesManager sesManager;
   

    private void Awake()
    {
        sesManager = Object.FindObjectOfType<SesManager>();
        
    }

    void Start()
    {
        pauseBasildimi = false;

        kalanSure = 90;

        
    }


    public void OyunuBaslat()
    {
        StartCoroutine(GeriSayRoutine());
        RastgeleRenkUret();
    }
   
    void RastgeleRenkUret()
    {
      int rastgeleSayi = Random.Range(0, 100);
      

        if(rastgeleSayi<40)
        {
            //dogru ise yapýlacak iþlemler

            rastgeleDogruAnlam = Random.Range(0, renkItem.Length);

            anlamTxt.text = renkItem[rastgeleDogruAnlam].renkAdi;

            yukariRenk = renkItem[Random.Range(0, renkItem.Length)].renk;
            anlamTxt.color = new Color32(yukariRenk.r, yukariRenk.g, yukariRenk.b, 255);

            dogruRenk = renkItem[rastgeleDogruAnlam].renk;

            renkTxt.color = new Color32(dogruRenk.r, dogruRenk.g, dogruRenk.b, 255);
            renkTxt.text = renkItem[Random.Range(0, renkItem.Length)].renkAdi;

            sonucDogrumu = true;

        } else
        {
            //yanlýþ ise yapýlacak iþlemler;
            rastgeleYanlisAnlam = Random.Range(0, renkItem.Length);

            if(rastgeleYanlisAnlam!=rastgeleDogruAnlam)
            {
                anlamTxt.text = renkItem[rastgeleYanlisAnlam].renkAdi;


                yukariRenk = renkItem[Random.Range(0, renkItem.Length)].renk;
                anlamTxt.color = new Color32(yukariRenk.r, yukariRenk.g, yukariRenk.b, 255);

                dogruRenk = renkItem[rastgeleDogruAnlam].renk;

                renkTxt.color= new Color32(dogruRenk.r, dogruRenk.g, dogruRenk.b, 255);
                renkTxt.text = renkItem[Random.Range(0, renkItem.Length)].renkAdi;

                sonucDogrumu = false;
            } else
            {
                RastgeleRenkUret();
            }
        }

       
        
        anlamTxt.GetComponent<CanvasGroup>().DOFade(1, 0.002f);
        renkTxt.GetComponent<CanvasGroup>().DOFade(1, 0.002f);


    }


    public void DogruyaBasildi()
    {
        if (pauseBasildimi)
            return;


        if(sonucDogrumu)
        {
           //dogru ise yapýlacak iþlemler
            dogruAdet++;

            sesManager.DogruSesiCikar();

            PuanArtir();
            


        } else
        {
            //yanlýþ ise yapýlacak iþlemler
            sesManager.YanlisSesiCikar();
            yanlisAdet++;
            PuaniAzalt();
            
        }

        StartCoroutine(YeniRenkUretRouitine());
    }


    public void YanlisaBasildi()
    {
        if (pauseBasildimi)
            return;

        if (sonucDogrumu )
        {
            //yanlýþ ise yapýlacak iþlemler
            sesManager.YanlisSesiCikar();
            yanlisAdet++;
            PuaniAzalt();

        }
        else
        {
            //dogru iken yapýlacak iþlemler
            sesManager.DogruSesiCikar();
            dogruAdet++;
            PuanArtir();
           
        }

        StartCoroutine(YeniRenkUretRouitine());

    }

    IEnumerator YeniRenkUretRouitine()
    {
        anlamTxt.GetComponent<CanvasGroup>().DOFade(0, 0.002f);
        renkTxt.GetComponent<CanvasGroup>().DOFade(0, 0.002f);

        yield return new WaitForSeconds(0.2f);

        RastgeleRenkUret();
    }


    void PuanArtir()
    {
        bonusAdet++;

        if(bonusAdet==5)
        {
            bonusVarmi = true;
        }

        if(bonusVarmi)
        {
            if(bonusAdet>=5 && bonusAdet<8)
            {
                sesManager.BonusSesiCikar();
                bonusYazi.GetComponent<CanvasGroup>().DOFade(1, .4f);
                bonusYazi.GetComponent<RectTransform>().DOScale(1, .4f).SetEase(Ease.OutBounce);
                
                
                puan = bonusAdet * 10;



            }
        }

        if(bonusAdet==8)
        {
            bonusAdet = 0;
            puan = 10;
            bonusVarmi = false;

            bonusYazi.GetComponent<CanvasGroup>().DOFade(0, .2f);
            bonusYazi.GetComponent<RectTransform>().DOScale(0, .2f).SetEase(Ease.InBounce);

        }

        toplamPuan += puan;
        toplamPuanTxt.text = toplamPuan.ToString();
    }

    void PuaniAzalt()
    {

        bonusAdet = 0;
        puan = 10;
        bonusVarmi = false;

        bonusYazi.GetComponent<CanvasGroup>().DOFade(0, .2f);
        bonusYazi.GetComponent<RectTransform>().DOScale(0, .2f).SetEase(Ease.InBounce);


        toplamPuan -= 10;
        if(toplamPuan<=0)
        {
            toplamPuan = 0;
        }

        toplamPuanTxt.text = toplamPuan.ToString();

    }

    IEnumerator GeriSayRoutine()
    {
        yield return new WaitForSeconds(1f);

        kalanSure--;

       

        if(kalanSure<10)
        {
            sureTxt.text = "0" + kalanSure.ToString();
        } else
        {
            sureTxt.text = kalanSure.ToString();

        }



        StartCoroutine(GeriSayRoutine());

        if(kalanSure<=0)
        {
            StopAllCoroutines();




            StartCoroutine(OyunuBitirRoutine());
        }
    }


   public void OyunuDurdur()
    {
        if(!pauseBasildimi)
        {
            pauseBasildimi = true;
            StopAllCoroutines();

            dogruAdetTxt.text = dogruAdet.ToString();
            yanlisAdetTxt.text = yanlisAdet.ToString();

            pausePanel.GetComponent<CanvasGroup>().DOFade(1, .5f);
            pausePanel.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);
        }
    }

    public void OyunaDon()
    {
        if(pauseBasildimi)
        {
            pauseBasildimi = false;

            StartCoroutine(GeriSayRoutine());

            pausePanel.GetComponent<CanvasGroup>().DOFade(0, .5f);
            pausePanel.GetComponent<RectTransform>().DOScale(0, .5f).SetEase(Ease.InBack);
        }
    }




    IEnumerator OyunuBitirRoutine()
    {
        butonlarPanel.SetActive(false);
        ustPanel.SetActive(false);
        ortaPanel.SetActive(false);
        bonusYazi.SetActive(false);

        SonucuYazdir();

        sesManager.OyunuBitirSesiCikar();

        yield return new WaitForSeconds(.2f);

        pauseBasildimi = true;

        sonucPanel.GetComponent<CanvasGroup>().DOFade(1, .5f);
        sonucPanel.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);


    }

    void SonucuYazdir()
    {
        dogruSonucTxt.text = dogruAdet.ToString() + " Adet";
        yanlisSonucTxt.text = yanlisAdet.ToString() + " Adet";

        sonucToplamPuanTxt.text = toplamPuan.ToString() + " Puan";

    }

    public void AnaMenu()
    {
        SceneManager.LoadScene("GamePlay");
    }


    public void OyundanCik()
    {
        Application.Quit();
    }
    

}
