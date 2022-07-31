using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesManager : MonoBehaviour
{
    [SerializeField]
    AudioSource oyunaBasla_FX, dogru_FX, yanlis_FX, bonus_FX, oyunuBitir_FX,damla_FX,daireDonus_FX;



    public void OyunaBaslaSesiCikar()
    {
        oyunaBasla_FX.Play();
    }


    public void DogruSesiCikar()
    {
        dogru_FX.Play();
    }

    public void YanlisSesiCikar()
    {
        yanlis_FX.Play();
    }

    public void BonusSesiCikar()
    {
        bonus_FX.Play();
    }

    public void DamlaSesiCikar()
    {
        damla_FX.Play();
    }

    public void DaireSesiCikar()
    {
        daireDonus_FX.Play();
    }


    public void OyunuBitirSesiCikar()
    {
        oyunuBitir_FX.Play();
    }



   
}
