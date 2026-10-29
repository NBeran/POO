namespace Test;
using player.Model;
using Drones.Model;


[TestClass]
public class Testatk
{
    [TestMethod]
    public void skeletonatk_update()
    {
        //Arrange
        skeletonatk testskeletonatk = new skeletonatk(100, 100);

        //Act
        testskeletonatk.update(1);

        //Assert
        Assert.AreEqual(125, testskeletonatk.Y);
    }

    [TestMethod]
    public void demonatk_update()
    {
        //Arrange
        demonatk testdemonatk = new demonatk(100, 100);

        //Act
        testdemonatk.update(1);

        //Assert
        Assert.AreEqual(125, testdemonatk.Y);
    }

    [TestMethod]
    public void playeratk_update()
    {
        //Arrange
        PlayerAtk testplayeratk = new PlayerAtk(100, 100);

        //Act
        testplayeratk.update(1);

        //Assert
        Assert.AreEqual(65, testplayeratk.Y);
    }
}
