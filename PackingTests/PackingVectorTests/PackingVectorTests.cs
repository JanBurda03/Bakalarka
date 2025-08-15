﻿public class PackingVectorTests
{
    [Fact] 
    public void ImplicitConversionTest()
    {
        var doubleArray = new double[] { 0.1, 0.2, 0.3 };
        var doubleList = new List<double>(doubleArray);

        PackingVector vector1 = new PackingVector(doubleList);
        PackingVector vector2 = new PackingVector(doubleArray);

        double[] doubleArray2 = vector1;
        List<double> doubleList2 = vector2;

        for (int i = 0; i < doubleArray2.Length; i++) 
        {
            Assert.Equal(doubleArray2[i], doubleList2[i], 3);
            Assert.Equal(doubleArray2[i], doubleArray[i], 3);
            Assert.Equal(doubleList2[i], doubleList[i], 3);

        }
    }

    [Fact]
    public void ConversionTestFor1()
    {
        var doubleArray = new double[] { 0.0, 0.99};

        PackingVector vector = new PackingVector(doubleArray);

        Assert.True(vector[1] <= 1.0);
        Assert.True(vector[0] >= 0);

    }

    [Fact]
    public void ExplicitConversionTest1()
    {
        var doubleArray = new double[] { 0.0, 1.1 };

        try
        {
            PackingVector vector = new PackingVector(doubleArray);
            Assert.True(false);
        }

        catch
        {
            Assert.True(true);
        }

    }

    [Fact]
    public void ExplicitConversionTest2()
    {
        var doubleArray = new double[] { 0.0, 1.1 };

        try
        {
            PackingVector vector = (PackingVector)(doubleArray);
            Assert.True(false);
        }

        catch
        {
            Assert.True(true);
        }

    }
}