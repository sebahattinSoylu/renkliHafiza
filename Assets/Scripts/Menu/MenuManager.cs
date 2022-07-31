using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject kafaObje, oyunAdiObje, baslaBtn,nasilOynanirBtn;


    [SerializeField]
    GameObject sayilar;

    [SerializeField]
    GameObject gameObjeler;

    [SerializeField]
    GameObject nasilOynanirObje, digerNesneler;

    int sayiAdet;


    SesManager sesManager;
    GameManager gameManager;

    private void Awake()
    {
        sesManager = Object.FindObjectOfType<SesManager>();
        gameManager = Object.FindObjectOfType<GameManager>();
    }



    private void Start()
    {

        gameManager.pauseBasildimi = true;
        StartCoroutine(SahneElemanlariniEkranaGetirRoutine());
        


    }

    IEnumerator SahneElemanlariniEkranaGetirRoutine()
    {
        kafaObje.GetComponent<CanvasGroup>().DOFade(1, 0.7f);
        kafaObje.GetComponent<RectTransform>().DOLocalMoveX(0, 0.7f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(.2f);

        oyunAdiObje.GetComponent<CanvasGroup>().DOFade(1, 0.7f);
        oyunAdiObje.GetComponent<RectTransform>().DOLocalMoveX(0, 0.7f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(.2f);

        baslaBtn.GetComponent<CanvasGroup>().DOFade(1, 0.7f);
        baslaBtn.GetComponent<RectTransform>().DOLocalMoveY(-490, 0.7f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(.2f);

        nasilOynanirBtn.GetComponent<CanvasGroup>().DOFade(1, 0.7f);
        nasilOynanirBtn.GetComponent<RectTransform>().DOLocalMoveY(-750, 0.7f).SetEase(Ease.OutBack);
    }


    public void OyunuBaslat()
    {
        kafaObje.GetComponent<CanvasGroup>().DOFade(0, 0.7f);
        kafaObje.GetComponent<RectTransform>().DOLocalMoveX(-1400, 0.7f);

        oyunAdiObje.GetComponent<CanvasGroup>().DOFade(0, 0.7f);
        oyunAdiObje.GetComponent<RectTransform>().DOLocalMoveX(1400, 0.7f);

        baslaBtn.GetComponent<CanvasGroup>().DOFade(0, 0.7f);
        baslaBtn.GetComponent<RectTransform>().DOLocalMoveY(-2000, 0.7f).SetEase(Ease.OutBack);


        nasilOynanirBtn.GetComponent<CanvasGroup>().DOFade(0, 0.7f);
        nasilOynanirBtn.GetComponent<RectTransform>().DOLocalMoveY(-2000, 0.7f).SetEase(Ease.OutBack);

        gameObjeler.SetActive(true);
        gameObjeler.GetComponent<CanvasGroup>().DOFade(1, 0.7f);

        StartCoroutine(SayilariAnimasyonluAcRoutine());
    }


    IEnumerator SayilariAnimasyonluAcRoutine()
    {
        sayilar.transform.GetChild(sayiAdet).gameObject.SetActive(true);
        sayilar.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        sayilar.GetComponent<RectTransform>().DOScale(1, 0.5f);


        sesManager.DamlaSesiCikar();
       

        yield return new WaitForSeconds(1f);

        sayilar.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        sayilar.GetComponent<RectTransform>().DOScale(0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        sayilar.transform.GetChild(sayiAdet).gameObject.SetActive(false);


        sayiAdet++;

        if(sayiAdet==3)
        {
            sesManager.OyunaBaslaSesiCikar();
            gameManager.pauseBasildimi = false;

            gameManager.OyunuBaslat();
            
        }

        if(sayiAdet<sayilar.transform.childCount)
        {
            StartCoroutine(SayilariAnimasyonluAcRoutine());
        }




    }


    public void NasilOynanirAc()
    {
        digerNesneler.SetActive(false);


        sesManager.DaireSesiCikar();

        nasilOynanirObje.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        nasilOynanirObje.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }


    public void NasilOynanirKapat()
    {
        digerNesneler.SetActive(true);


        sesManager.DaireSesiCikar();
        nasilOynanirObje.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
        nasilOynanirObje.GetComponent<RectTransform>().DOScale(0, 0.2f);
    }
}
