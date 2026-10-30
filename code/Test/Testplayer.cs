using player.Model;
using player;
using player.Helpers;
using System;

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
        Player gauche = new Player(- 20, Config.limitheight + 20, "test");
        Player droite = new Player(Config.AIRSPACEWIDTH + 20, Config.limitheight + 20, "test");

        //Act
        haut.verifiyborder();
        bas.verifiyborder();
        gauche.verifiyborder();
        droite.verifiyborder();

        //Assert
        Assert.IsTrue(haut.y == (Config.limitheight - haut.Playerheight) && bas.y == (Config.AIRSPACEHEIGHT - bas.Playerheight) && gauche.x == 0 && droite.x == (Config.AIRSPACEWIDTH - droite.Playerheight));
    }
}
