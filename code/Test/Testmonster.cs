using player.Model;
using player;
using player.Helpers;
namespace Test;

[TestClass]
[DoNotParallelize]
public class Testmonster
{
    [TestInitialize]
    public void Setup()
    {
        GameSpace.defense.Clear();
    }

    [TestCleanup]
    public void Cleanup()
    {
        GameSpace.defense.Clear();
    }
    [TestMethod]
    public void skeleton_hit_defense_if_next_to_it()
    {
        //Arrange
        List<skeleton> skeletontest = new List<skeleton>();
        skeletontest.Add(new skeleton(10, Config.limitheight));

        GameSpace.defense.Add(new bone_pile(10, Config.limitheight));
        //Act
        foreach (skeleton s in skeletontest)
        {
            s.update(1);
        }

        //Assert
        Assert.AreEqual(1, GameSpace.defense[0].Hitcounter);
    }
    
    [TestMethod]
    public void demon_hit_defense_if_next_to_it()
    {
        //Arrange
        List<demon> demontest = new List<demon>();
        demontest.Add(new demon(10, Config.limitheight));

        GameSpace.defense.Add(new bone_pile(10, Config.limitheight));
        //Act
        foreach (demon d in demontest)
        {
            d.update(1);
        }

        //Assert
        Assert.AreEqual(1, GameSpace.defense[0].Hitcounter);
    }

    [TestMethod]
    public void skeleton_walk_down_then_left()
    {
        //Arrange
        List<skeleton> skeletontest = new List<skeleton>();
        skeletontest.Add(new skeleton(10, 10));

        //Act
        while (skeletontest[0].Down)
        {
            skeletontest[0].update(1);
        }

        //Assert
        Assert.IsTrue(skeletontest[0].Left && skeletontest[0].Lastleft && skeletontest[0].Y == 35 && !skeletontest[0].Down);
    }

    [TestMethod]
    public void demon_walk_down_then_left()
    {
        //Arrange
        List<demon> demontest = new List<demon>();
        demontest.Add(new demon(10, 10));

        //Act
        while (demontest[0].Down)
        {
            demontest[0].update(1);
        }

        //Assert
        Assert.IsTrue(demontest[0].Left && demontest[0].Lastleft && demontest[0].Y == 35 && !demontest[0].Down);
    }

    [TestMethod]
    public void skeleton_walk_right_after_left()
    {
        //Arrange
        List<skeleton> skeletontest = new List<skeleton>();
        skeleton test = new skeleton(10, 10);
        test.Walktime = 25;
        test.Lastleft = true;
        skeletontest.Add(test);

        //Act
        while (test.Lastleft)
        {
            test.update(1);
        }
        while (test.Right)
        {
            test.update(1);
        }

        //Assert
        Assert.IsTrue(!skeletontest[0].Left && !skeletontest[0].Lastleft && skeletontest[0].X == 35 && skeletontest[0].Down && !test.Right);
    }

    [TestMethod]
    public void demon_walk_right_after_left()
    {
        //Arrange
        List<demon> demontest = new List<demon>();
        demon test = new demon(10, 10);
        test.Walktime = 25;
        test.Lastleft = true;
        demontest.Add(test);

        //Act
        while (test.Lastleft)
        {
            test.update(1);
        }
        while (test.Right)
        {
            test.update(1);
        }

        //Assert
        Assert.IsTrue(!demontest[0].Left && !demontest[0].Lastleft && demontest[0].Down && !test.Right && demontest[0].X == 35 );
    }

    [TestMethod]
    public void demon_didnt_die_in_1_hit()
    {
        //Arrange
        GameSpace gamespace = new GameSpace(new Player(10, 10, "test"));
       gamespace.ClearWave();
        gamespace.demonwave.Add(new demon(10, 10));
        gamespace.atk.Add(new PlayerAtk(10, 10));
        //Act
        gamespace.checkhit();
        //Assert
                Assert.IsTrue(gamespace.demondelete.Count == 0);
    }
}
