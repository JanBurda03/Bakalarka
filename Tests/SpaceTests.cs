using Xunit;


public class IntersectionTests
{
    [Fact]
    public void IntersectionTest1()
    {
        var space1 = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var space2 = new Space(new Coordinates(5, 5, 5), new Coordinates(15, 15, 15));

        Assert.True(space1.IntersectsWith(space2));
        Assert.True(space2.IntersectsWith(space1));
    }


    [Fact]
    public void IntersectionTest2()
    {
        var space1 = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var space2 = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));

        Assert.True(space1.IntersectsWith(space2));
        Assert.True(space2.IntersectsWith(space1));
    }


    [Fact]
    public void IntersectionTest3()
    {
        var space1 = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var space2 = new Space(new Coordinates(2, 2, 2), new Coordinates(8, 8, 8));

        Assert.True(space1.IntersectsWith(space2));
        Assert.True(space2.IntersectsWith(space1));
    }



    [Fact]
    public void IntersectionTest4()
    {
        var space1 = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var space2 = new Space(new Coordinates(0, 0, 0), new Coordinates(1, 1, 1));

        Assert.True(space1.IntersectsWith(space2));
        Assert.True(space2.IntersectsWith(space1));
    }



    [Fact]
    public void IntersectionTest5()
    {
        var space1 = new Space(new Coordinates(5, 5, 5), new Coordinates(10, 10, 10));
        var space2 = new Space(new Coordinates(0, 0, 0), new Coordinates(5, 5, 5));

        Assert.False(space1.IntersectsWith(space2));
        Assert.False(space2.IntersectsWith(space1));
    }


    [Fact]
    public void IntersectionTest6()
    {
        var space1 = new Space(new Coordinates(0, 0, 1), new Coordinates(10, 10, 10));
        var space2 = new Space(new Coordinates(5, 5, 0), new Coordinates(15, 15, 1));

        Assert.False(space1.IntersectsWith(space2));
        Assert.False(space2.IntersectsWith(space1));
    }



    [Fact]
    public void IntersectionTest7()
    {
        var space1 = new Space(new Coordinates(2, 2, 2), new Coordinates(10, 10, 10));
        var space2 = new Space(new Coordinates(1, 2, 3), new Coordinates(2, 2, 4));

        Assert.False(space1.IntersectsWith(space2));
        Assert.False(space2.IntersectsWith(space1));
    }

}

public class SubspaceTests
{
    [Fact]
    public void SubspaceTest1()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(1, 1, 1), new Coordinates(9, 9, 9));

        Assert.True(x.IsSubspaceOf(y));
        Assert.True(y.IsOverspaceOf(x));
    }

    [Fact]
    public void SubspaceTest2()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(0, 0, 0), new Coordinates(9, 9, 9));

        Assert.True(x.IsSubspaceOf(y));
        Assert.True(y.IsOverspaceOf(x));
    }


    [Fact]
    public void SubspaceTest3()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(1, 1, 1), new Coordinates(10, 10, 10));

        Assert.True(x.IsSubspaceOf(y));
        Assert.True(y.IsOverspaceOf(x));
    }

    [Fact]
    public void SubspaceTest4()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));

        Assert.True(x.IsSubspaceOf(y));
        Assert.True(y.IsOverspaceOf(x));
    }

    [Fact]
    public void SubspaceTest5()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 11));

        Assert.False(x.IsSubspaceOf(y));
        Assert.False(y.IsOverspaceOf(x));
    }

    [Fact]
    public void SubspaceTest6()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 5));
        var x = new Space(new Coordinates(2, 2, 0), new Coordinates(5, 5, 4));

        Assert.True(x.IsSubspaceOf(y));
        Assert.True(y.IsOverspaceOf(x));
    }


}

public class TripletTest
{
    [Fact]
    public void TripletTest1()
    {
        Coordinates x = new Coordinates(0, 0, 0);
        Coordinates y = new Coordinates(2, 2, 0);

        Assert.True(x.AllLessOrEqualThan(y));
    }

    [Fact]
    public void TripletTest2()
    {
        Coordinates x = new Coordinates(10, 10, 5);
        Coordinates y = new Coordinates(5, 5, 4);

        Assert.True(x.AllGreaterOrEqualThan(y));
    }
}
