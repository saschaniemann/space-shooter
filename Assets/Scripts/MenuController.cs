using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GUIText Seconds;
    public GUIText WeaponUpgrade;
    public GUIText FasterShooting;
	public GUIText FasterShootingPrice;
	public GUIText PlayerUpgrade;
    private bool actMenu;
    private bool closingMenu;

    public int shootingprice;
    private int countFasterShooting;

    private void Start()
    {
        actMenu = false;
        closingMenu = false;
		Seconds.text = "";
		WeaponUpgrade.text = "";
		PlayerUpgrade.text = "";
		FasterShooting.text = "";
		FasterShootingPrice.text = "";
        countFasterShooting = 0;
    }

    void Update ()
    {
        //Menu öffnen
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!closingMenu)
            {
                actMenu = !actMenu;
                if (actMenu) Menu();
                if (!actMenu) StartCoroutine(closeMenu());
            }
        }

        //Faster Shooting
        if (
               Input.GetKeyDown(KeyCode.F)
            && GameController.instance.points >= shootingprice
            && actMenu 
            && countFasterShooting < 4
            )
        {
            PlayerController.instance.fireRate -= 0.3;
            countFasterShooting++;

            GameController.instance.points -= shootingprice;
            showText();
        }
    }

    IEnumerator closeMenu()
    {
        closingMenu = true;
        Vector3 newPosition = new Vector3(0, -20, 0);
        transform.position = newPosition;
        disableText();
        Time.timeScale = 0.3F;
        Seconds.text = "3";
        yield return new WaitForSecondsRealtime(1);
        Seconds.text = "2";
        yield return new WaitForSecondsRealtime(1);
        Seconds.text = "1";
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1.0F;
        Seconds.text = "";
        closingMenu = false;
    }

    public void Menu()
    {
        Time.timeScale = 0F;
        Vector3 newPosition = new Vector3(0, 5, 0);
        transform.position = newPosition;
        showText();
    }

    public void showText()
    {
		WeaponUpgrade.text = "Upgrade your weapon:";
		PlayerUpgrade.text = "Upgrade your Player:";
        FasterShooting.text = "Faster Shooting (Press 'F' to buy!)";
        if (countFasterShooting < 5) FasterShootingPrice.text = "" + shootingprice;
        else FasterShootingPrice.text = "max. Level";
        GameController.instance.UpdateScore();
    }

    public void disableText()
    {
		WeaponUpgrade.text = "";
		PlayerUpgrade.text = "";
        FasterShooting.text = "";
        FasterShootingPrice.text = "";
    }
}
