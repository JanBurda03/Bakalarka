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

        Assert.True(x.isSubspaceOf(y));
        Assert.True(y.isOverspaceOf(x));
    }

    [Fact]
    public void SubspaceTest2()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(0, 0, 0), new Coordinates(9, 9, 9));

        Assert.True(x.isSubspaceOf(y));
        Assert.True(y.isOverspaceOf(x));
    }


    [Fact]
    public void SubspaceTest3()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(1, 1, 1), new Coordinates(10, 10, 10));

        Assert.True(x.isSubspaceOf(y));
        Assert.True(y.isOverspaceOf(x));
    }

    [Fact]
    public void SubspaceTest4()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));

        Assert.True(x.isSubspaceOf(y));
        Assert.True(y.isOverspaceOf(x));
    }

    [Fact]
    public void SubspaceTest5()
    {
        var y = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var x = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 11));

        Assert.False(x.isSubspaceOf(y));
        Assert.False(y.isOverspaceOf(x));
    }
}

public class EmptyMaximalSpacesTests
{
    EmptyMaximalSpaces helper = new EmptyMaximalSpaces([new Space(new Coordinates(0,0,0), new Coordinates(0, 0, 0))]);

    [Fact]
    public void Test1()
    {
        var original = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 10));
        var occupied = new Space(new Coordinates(0, 0, 1), new Coordinates(5, 5, 5));

        var ex = Assert.Throws<Exception>(() => helper.SplitRemainingSpace(original, occupied));
        Assert.Equal("Newly placed object must always be fully on base", ex.Message);
    }

    [Fact]
    public void Test2()
    {
        var original = new Space(new Coordinates(0, 0, 1), new Coordinates(10, 10, 10));
        var occupied = new Space(new Coordinates(0, 0, 0), new Coordinates(5, 5, 5));

        var ex = Assert.Throws<Exception>(() => helper.SplitRemainingSpace(original, occupied));
        Assert.Equal("Empty maximal space must always be fully on base", ex.Message);
    }

    [Fact]
    public void Test3()
    {
        var original = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 1));
        var occupied = new Space(new Coordinates(3, 3, 0), new Coordinates(7, 7, 1));

        var result = helper.SplitRemainingSpace(original, occupied).ToList();

        Assert.Contains(result, s => s.start.allEqual(new Coordinates(0, 0, 0)) && s.end.allEqual(new Coordinates(3, 10, 0)));
        Assert.Contains(result, s => s.start.allEqual(new Coordinates(0, 0, 0)) && s.end.allEqual(new Coordinates(10, 3, 0)));
        Assert.Contains(result, s => s.start.allEqual(new Coordinates(7, 0, 0)) && s.end.allEqual(new Coordinates(10, 10, 0)));
        Assert.Contains(result, s => s.start.allEqual(new Coordinates(0, 7, 0)) && s.end.allEqual(new Coordinates(10, 10, 0)));

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Test4()
    {
        var original = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 2));
        var occupied = new Space(new Coordinates(3, 3, 0), new Coordinates(7, 7, 1));

        var result = helper.SplitRemainingSpace(original, occupied).ToList();

        Assert.Contains(result, s => s.start.allEqual(new Coordinates(0, 0, 0)) && s.end.allEqual(new Coordinates(3, 10, 0)));
        Assert.Contains(result, s => s.start.allEqual(new Coordinates(0, 0, 0)) && s.end.allEqual(new Coordinates(10, 3, 0)));
        Assert.Contains(result, s => s.start.allEqual(new Coordinates(7, 0, 0)) && s.end.allEqual(new Coordinates(10, 10, 0)));
        Assert.Contains(result, s => s.start.allEqual(new Coordinates(0, 7, 0)) && s.end.allEqual(new Coordinates(10, 10, 0)));
        Assert.Contains(result, s => s.start.allEqual(new Coordinates(3, 3, 1)) && s.end.allEqual(new Coordinates(7, 7, 2)));

        Assert.Equal(5, result.Count);
    }



    [Fact]
    public void Test5()
    {
        var original = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 0));
        var occupied = new Space(new Coordinates(0, 0, 0), new Coordinates(10, 10, 0));

        var result = helper.SplitRemainingSpace(original, occupied).ToList();

        Assert.Empty(result);
    }
}