using Drones.Model;
using player;
namespace Test;

[TestClass]
[DoNotParallelize]
public class Testdefense
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
    public void defense_shrink_and_count_when_hit()
    {
        //Arrange
        bone_pile test = new bone_pile(10, 10);

        //Act
        test.hit(false);

        //Assert
        Assert.IsTrue(test.Hitcounter == 1 && test.Height == 65 && test.width == 90);
    }

    [TestMethod]
    public void perdre_score_si_detruit_par_joueur()
    {
        //Arrange
        bone_pile test = new bone_pile(10, 10);
        test.Hitcounter = 4;

        //Act
        test.hit(true);

        Assert.AreEqual(GameSpace.Score, -200);

    }

    [TestMethod]
    public void perdre_pas_score_si_detruit_par_monstre()
    {
        //Arrange
        bone_pile test = new bone_pile(10, 10);
        test.Hitcounter = 4;

        //Act
        test.hit(false);

        Assert.AreEqual(GameSpace.Score, 0);

    }
}
