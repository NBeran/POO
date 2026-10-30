using player;
using player.Helpers;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using player.Model;
namespace Test;

[TestClass]
[DoNotParallelize]
public class Testgamespace
{
    [TestInitialize]
    public void Setup()
    {
        GameSpace.defense.Clear();
        GameSpace.Score = 0;
    }

    [TestCleanup]
    public void Cleanup()
    {
        GameSpace.defense.Clear();
        GameSpace.Score = 0;
    }
    [TestMethod]
    public void wavecooldawn_and_coolldawn_do_plus_1_when_no_monster()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));

        //Act
        gamespace.ClearWave();
        gamespace.Update(1);

        //Assert
        Assert.IsTrue(gamespace.Wavecooldawn == 1 && gamespace.Cooldawn == 1);
    }

    [TestMethod]
    public void monstercooldawn_get_up_when_ther_is_monster()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));

        //Act
        gamespace.Update(1);

        //Assert
        Assert.IsTrue(gamespace.Demoncooldown == 5 && gamespace.Skeletoncooldawn == 5);
    }

    [TestMethod]
    public void monster_shoot_when_cooldawn_is_ready()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
        gamespace.Skeletoncooldawn = 25;
        gamespace.Demoncooldown = 30;
        //Act
        gamespace.Update(1);

        //Assert
        Assert.IsTrue(gamespace.Skeletoncooldawn == 0 && gamespace.Demoncooldown == 0 && gamespace.demonatks.Count == 1 && gamespace.skeletonatks.Count == 1);
    }
    [TestMethod]
    public void shoot_when_cooldawn_is_ready()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
        gamespace.Cooldawn = 6;
        //Act
        gamespace.shoot();
        //Assert
        Assert.IsTrue(gamespace.Cooldawn == 0 && gamespace.atk.Count == 1);
    }

    [TestMethod]
    public void bullet_dispawn_when_not_in_terrain()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
        gamespace.atk.Add(new PlayerAtk(10, -100));
        gamespace.demonatks.Add(new demonatk(10, Config.AIRSPACEHEIGHT + demonatk.Height));
        gamespace.skeletonatks.Add(new skeletonatk(10, Config.AIRSPACEHEIGHT + skeletonatk.Height));
        //Act
        gamespace.checkhit();
        //Assert
        Assert.IsTrue(gamespace.demonatkdelete.Count == 1 && gamespace.skeletonatksdelete.Count == 1 && gamespace.atkdelete.Count == 1);
    }

    [TestMethod]
    public void player_atk_hit_everything()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
        gamespace.ClearWave();
        GameSpace.defense.Clear();
        gamespace.atk.Add(new PlayerAtk(10, 100));
        gamespace.atk.Add(new PlayerAtk(10, 200));
        gamespace.atk.Add(new PlayerAtk(10, 300));
        gamespace.atk.Add(new PlayerAtk(10, 400));
        gamespace.atk.Add(new PlayerAtk(10, 500));

        gamespace.skeletonwave.Add(new skeleton(10, 100));
        gamespace.demonwave.Add(new demon(10, 200));
        gamespace.demonwave[0].Hp = 1;
        gamespace.skeletonatks.Add(new skeletonatk(10, 300));
        gamespace.demonatks.Add(new demonatk(10, 400));
        GameSpace.defense.Add(new bone_pile(10, 500));
        GameSpace.defense[0].Hitcounter = 4;
        GameSpace.Score = 0;

        //Act
        gamespace.checkhit();

        //Assert
        Assert.IsTrue(gamespace.atkdelete.Count == 5 && gamespace.skeletondelete.Count == 1 && gamespace.demondelete.Count == 1 && gamespace.skeletonatksdelete.Count == 1 && gamespace.demonatkdelete.Count == 1 && gamespace.defenseadelete.Count == 1 && GameSpace.Score == 250);


    }

    [TestMethod]
    public void skeleton_atk_hit_everything()
    {
        //Arrange
        Player player = new Player(10, 300, "test");
        player.live = 3;
        GameSpace gamespace = new GameSpace(player);
        gamespace.ClearWave();
        GameSpace.defense.Clear();
        gamespace.skeletonatks.Add(new skeletonatk(10, 100));
        gamespace.skeletonatks.Add(new skeletonatk(10, 200));
        player.x = 10;
        player.y = 100;
        GameSpace.defense.Clear();
        GameSpace.defense.Add(new bone_pile(10, 200));
        GameSpace.defense[0].Hitcounter = 4;

        //Act
        gamespace.checkhit();

        //Assert
        Assert.IsTrue(player.live == 2 && gamespace.skeletonatksdelete.Count == 2 && gamespace.defenseadelete.Count == 1);

    }

    [TestMethod]
    public void demon_atk_hit_everything()
    {
        //Arrange
        Player player = new Player(10, 300, "test");
        player.live = 3;
        GameSpace gamespace = new GameSpace(player);
        gamespace.ClearWave();
        GameSpace.defense.Clear();
        gamespace.demonatks.Add(new demonatk(10, 100));
        gamespace.demonatks.Add(new demonatk(10, 200));
        player.x = 10;
        player.y = 100;
        GameSpace.defense.Clear();
        GameSpace.defense.Add(new bone_pile(10, 200));
        GameSpace.defense[0].Hitcounter = 4;

        //Act
        gamespace.checkhit();

        //Assert
        Assert.IsTrue(player.live == 2 && gamespace.demonatkdelete.Count == 2 && gamespace.defenseadelete.Count == 1);

    }

    [TestMethod]
    public void remove_obj_actually_remove_everything()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
        gamespace.ClearWave();
        GameSpace.defense.Clear();
        gamespace.atk.Add(new PlayerAtk(10, 10));
        gamespace.atkdelete.Add(gamespace.atk[0]);

        gamespace.skeletonwave.Add(new skeleton(10, 10));
        gamespace.skeletondelete.Add(gamespace.skeletonwave[0]);

        gamespace.skeletonatks.Add(new skeletonatk(10, 10));
        gamespace.skeletonatksdelete.Add(gamespace.skeletonatks[0]);

        gamespace.demonwave.Add(new demon(10, 10));
        gamespace.demondelete.Add(gamespace.demonwave[0]);

        GameSpace.defense.Add(new bone_pile(10, 10));
        gamespace.defenseadelete.Add(GameSpace.defense[0]);

        //Act
        gamespace.removeobj();

        //Assert
        Assert.IsTrue(gamespace.atk.Count == 0 && gamespace.skeletonwave.Count == 0 && gamespace.skeletonatks.Count == 0 && gamespace.demonwave.Count == 0 && GameSpace.defense.Count == 0);
    }

    [TestMethod]
    public void newwave_create_a_harder_wave()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
        gamespace.ClearWave();
        gamespace.Demoninterval = 350;

        //Act
        gamespace.newwave();

        //Assert
        Assert.IsTrue(gamespace.Demoninterval == 300 && gamespace.demonwave.Count == 5 && gamespace.skeletonwave.Count == 5);
    }

    [TestMethod]
    public void ClearWave_clear_everything()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));

        //Act 
        gamespace.ClearWave();

        //Assert
        Assert.IsTrue(gamespace.demonwave.Count == 0 && gamespace.skeletonwave.Count == 0);
    }

    [TestMethod]
    public void isinsreen_check_everything()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
        gamespace.ClearWave();
        GameSpace.defense.Clear();
        gamespace.skeletonwave.Add(new skeleton(-100, 10));
        gamespace.skeletonwave.Add(new skeleton(Config.AIRSPACEWIDTH + 100, 10));
        gamespace.skeletonwave.Add(new skeleton(100, -100));
        gamespace.skeletonwave.Add(new skeleton(100, Config.AIRSPACEHEIGHT + 100));

        gamespace.demonwave.Add(new demon(-100, 10));
        gamespace.demonwave.Add(new demon(Config.AIRSPACEWIDTH + 100, 10));
        gamespace.demonwave.Add(new demon(100, -100));
        gamespace.demonwave.Add(new demon(100, Config.AIRSPACEHEIGHT + 100));

        gamespace.demonatks.Add(new demonatk(-100, 10));
        gamespace.demonatks.Add(new demonatk(Config.AIRSPACEWIDTH + 100, 10));
        gamespace.demonatks.Add(new demonatk(100, -100));
        gamespace.demonatks.Add(new demonatk(100, Config.AIRSPACEHEIGHT + 100));

        gamespace.skeletonatks.Add(new skeletonatk(-100, 10));
        gamespace.skeletonatks.Add(new skeletonatk(Config.AIRSPACEWIDTH + 100, 10));
        gamespace.skeletonatks.Add(new skeletonatk(100, -100));
        gamespace.skeletonatks.Add(new skeletonatk(100, Config.AIRSPACEHEIGHT + 100));

        gamespace.atk.Add(new PlayerAtk(-100, 10));
        gamespace.atk.Add(new PlayerAtk(Config.AIRSPACEWIDTH + 100, 10));
        gamespace.atk.Add(new PlayerAtk(100, -100));
        gamespace.atk.Add(new PlayerAtk(100, Config.AIRSPACEHEIGHT + 100));

        //Act
        gamespace.isinscreen();

        //Assert
        Assert.IsTrue(gamespace.demonwave.Count == 0 && gamespace.skeletonwave.Count == 0 && gamespace.demonatks.Count == 0 && gamespace.skeletonatks.Count == 0 && gamespace.atk.Count == 0);
    }

}
