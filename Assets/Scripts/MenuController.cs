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
    public GUIText Lives;
    public GUIText LivesPrice;
    public int shootingprice;
    public int lifeprice;

    private int countFasterShooting;
    private bool actMenu;
    private bool closingMenu;

    private void Start()
    {
        actMenu = false;
        closingMenu = false;
		Seconds.text = "";
		WeaponUpgrade.text = "";
		PlayerUpgrade.text = "";
		FasterShooting.text = "";
		FasterShootingPrice.text = "";
        Lives.text = "";
        LivesPrice.text = "";
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
            && countFasterShooting < 6
            )
        {
            PlayerController.instance.fireRate -= 0.2;
            countFasterShooting++;
            GameController.instance.points -= shootingprice;
            shootingprice += 15;

            showText();
        }

        //More Lives
        if (Input.GetKeyDown(KeyCode.L) && actMenu && GameController.instance.points >= lifeprice)
        {
            GameController.instance.playerLives++;
            GameController.instance.points -= lifeprice;
            showText();
        }
    }

    IEnumerator closeMenu()
    {
        closingMenu = true;
        Vector3 newPosition = new Vector3(0, -20, 0);
        transform.position = newPosition;
        disableText();
        Time.timeScale = 0.6F;
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
        GameController.instance.Waves.text = "";
        showText();
    }

    public void showText()
    {
		WeaponUpgrade.text = "Upgrade your weapon:";
		PlayerUpgrade.text = "Upgrade your Player:";
        FasterShooting.text = "Faster Shooting (Press 'F' to buy!)";
        if (countFasterShooting < 6) FasterShootingPrice.text = "" + shootingprice;
        else FasterShootingPrice.text = "max. Level";
        Lives.text = "Buy more lives (Press 'L' to buy!)";
        LivesPrice.text = "" + lifeprice;
        GameController.instance.UpdateScore();
    }

    public void disableText()
    {
		WeaponUpgrade.text = "";
		PlayerUpgrade.text = "";
        FasterShooting.text = "";
        FasterShootingPrice.text = "";
        Lives.text = "";
        LivesPrice.text = "";
    }
}
