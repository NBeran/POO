using Drones.Helpers;
using Drones.Model;
namespace Test;


    [TestClass]
    public sealed class TestRandomhelper
    {
    [TestMethod]
    public void rndskeleton_actualy_give_you_a_skeleton_in_the_liste() {

        {
            // Arrange
            List<skeleton> skeletontest = new List<skeleton>();
            for (int i = 0; i < 10; i++)
            {
                skeleton s = new skeleton(i, i);
                skeletontest.Add(s);
            }

            // Act
            skeleton result = randomhelper.rndskeleton(skeletontest);

            // Assert
            Assert.IsTrue(skeletontest.Contains(result));
        }
    }
        [TestMethod]
        public void rnddemon_actualy_give_you_a_skeleton_in_the_liste()
        {

            {
                // Arrange
                List<demon> demontest = new List<demon>();
                for (int i = 0; i < 10; i++)
                {
                    demon d = new demon(i, i);
                    demontest.Add(d);
                }

                // Act
                demon result = randomhelper.rnddemon(demontest);    
                // Assert
                Assert.IsTrue(demontest.Contains(result));
            }
        }
    }


