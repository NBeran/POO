using player;
using player.Helpers;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test;

[TestClass]
public class Testplayer
{
    [TestMethod]
    public void check_if_player_movement_work()
    {
        //Arrange
        Player haut = new Player(30, Config.limitheight + 20, "test");
        Player bas = new Player(30, Config.limitheight + 20, "test");
        Player gauche = new Player(30, Config.limitheight + 20, "test");
        Player droite = new Player(30, Config.limitheight + 20, "test");

        //Act
        haut.Update(1, true, false, false, false);
        bas.Update(1, false, true, false, false);
        gauche.Update(1, false, false, true, false);
        droite.Update(1, false, false, false, true);

        //Assert
        Assert.IsTrue(haut.y == Config.limitheight && bas.y == Config.limitheight + 40 && gauche.x == 10 && droite.x == 50);
    }

    [TestMethod]
    public void verify_border_is_working()
    {
        // Arrange
        Player haut = new Player(30, Config.limitheight - 100, "test");
        Player bas = new Player(30, Config.AIRSPACEHEIGHT + 20, "test");
        Player gauche = new Player(-20, Config.limitheight + 20, "test");
        Player droite = new Player(Config.AIRSPACEWIDTH + 20, Config.limitheight + 20, "test");

        //Act
        haut.verifiyborder();
        bas.verifiyborder();
        gauche.verifiyborder();
        droite.verifiyborder();

        //Assert
        Assert.IsTrue(haut.y == (Config.limitheight - haut.Playerheight) && bas.y == (Config.AIRSPACEHEIGHT - bas.Playerheight) && gauche.x == 0 && droite.x == (Config.AIRSPACEWIDTH - droite.Playerheight));
    }

    [TestMethod]
    public void test_that_dash_cooldown_get_bigger_if_is_lower_than_100()
    {
        //Arrange
        Player little_cooldown = new Player(30, 30, "test");
        Player big_cooldown = new Player(30, 30, "test");
        big_cooldown.Dashcooldown = 100;

        //Act
        little_cooldown.Update(1, false, false, false, false);
        big_cooldown.Update(1, false, false, false, false);

        //Assert 
        Assert.IsTrue(little_cooldown.Dashcooldown == 1 && big_cooldown.Dashcooldown == 100);
    }

    [TestMethod]
    public void test_that_player_can_dash_if_cooldown_is_over_100_and_cant_if_lower()
    {
        //Arrange
        Player cant_dash = new Player(30, 30, "test");
        Player can_dash = new Player(30, 30, "test");
        can_dash.Dashcooldown = 100;

        //Act
        cant_dash.dash();
        can_dash.dash();

        //Assert
        Assert.IsTrue(cant_dash.speed == 20 && can_dash.speed == 100 && can_dash.Dashduration == 3);

    }


    [TestMethod]
    public void speed_become_normal_after_3_frames()
    {
        //Arrange
        Player dash = new Player(30, 30, "test");
        dash.Dashcooldown = 100;
        dash.Dashduration = 1;

        //Act
        dash.Update(1, false, false, false, false);

        //Assert
        Assert.IsTrue(dash.Dashduration == 0 && dash.speed == 20);
    }



}
