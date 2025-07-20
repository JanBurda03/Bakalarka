public class EmptyMaximalRegionsTests
{

    [Fact]
    public void Test2()
    {
        var initial = new Region(new Coordinates(0, 0, 0), new Coordinates(100, 100, 100));
        var ems = new EmptyMaximalRegions(initial);

        var box1 = new Region(new Coordinates(2, 2, 0), new Coordinates(5, 5, 4));
        var box2 = new Region(new Coordinates(0, 0, 0), new Coordinates(2, 2, 4));
        ems.UpdateEMR(box1);
        ems.UpdateEMR(box2);

        var updatedSpaces = ems.GetEMR().ToList();



        Assert.Contains(new Region(new Coordinates(0, 2, 0), new Coordinates(2, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(2, 0, 0), new Coordinates(10, 2, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(5, 0, 0), new Coordinates(10, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 5, 0), new Coordinates(10, 10, 5)), updatedSpaces);

        Assert.Contains(new Region(new Coordinates(2, 2, 4), new Coordinates(5, 5, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 0, 4), new Coordinates(2, 2, 5)), updatedSpaces);

        Assert.Equal(updatedSpaces.Count, 6);



    }

    [Fact]
    public void Test3()
    {
        var initial = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 5));
        var ems = new EmptyMaximalRegions(initial);

        var box1 = new Region(new Coordinates(2, 2, 0), new Coordinates(5, 5, 4));
        var box2 = new Region(new Coordinates(0, 0, 0), new Coordinates(2, 2, 4));
        ems.UpdateEMR(box1);
        ems.UpdateEMR(box2);

        var updatedSpaces = ems.GetEMR().ToList();



        Assert.Contains(new Region(new Coordinates(0, 2, 0), new Coordinates(2, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(2, 0, 0), new Coordinates(10, 2, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(5, 0, 0), new Coordinates(10, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 5, 0), new Coordinates(10, 10, 5)), updatedSpaces);

        Assert.Contains(new Region(new Coordinates(2, 2, 4), new Coordinates(5, 5, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 0, 4), new Coordinates(2, 2, 5)), updatedSpaces);

        Assert.Equal(updatedSpaces.Count, 6);



    }

    /*

    [Fact]
    public void Test6()
    {
        var baseSpace = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 5));
        var smaller1 = new Region(new Coordinates(1, 1, 0), new Coordinates(3, 3, 5));
        var smaller2 = new Region(new Coordinates(0, 0, 0), new Coordinates(5, 5, 5));

        var ems = new EmptyMaximalSpaces(baseSpace);

        var cleaned = ems.deleteSubspaces(new List<Region> { baseSpace, smaller1, smaller2 }, new List<Region>());

        Assert.DoesNotContain(smaller1, cleaned);
        Assert.DoesNotContain(smaller2, cleaned);
        Assert.Contains(baseSpace, cleaned);
    }
    

    [Fact]
    public void Test4()
    {
        var initial = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 5));
        var ems = new EmptyMaximalSpaces(initial);

        var box1 = new Region(new Coordinates(2, 2, 0), new Coordinates(5, 5, 4));
        var box2 = new Region(new Coordinates(0, 0, 0), new Coordinates(2, 2, 4));

        ems.updateEmptyMaximalSpaces(box1);
        ems.updateEmptyMaximalSpaces(box2);

        var box3 = new Region(new Coordinates(3, 0, 0), new Coordinates(5, 2, 3));

        ems.updateEmptyMaximalSpaces(box3);

        var updatedSpaces = ems.getEmptyMaximalSpaces(new Sizes(0, 0, 0)).ToList();



        Assert.Contains(new Region(new Coordinates(0, 2, 0), new Coordinates(2, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(2, 0, 0), new Coordinates(3, 2, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(5, 0, 0), new Coordinates(10, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 5, 0), new Coordinates(10, 10, 5)), updatedSpaces);

        Assert.Contains(new Region(new Coordinates(2, 2, 4), new Coordinates(5, 5, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 0, 4), new Coordinates(2, 2, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(3, 0, 3), new Coordinates(5, 2, 5)), updatedSpaces);

        Assert.Equal(updatedSpaces.Count, 7);



    }

    [Fact]
    public void Test5()
    {
        var initial = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 5));
        var ems = new EmptyMaximalSpaces(initial);

        var box1 = new Region(new Coordinates(2, 2, 0), new Coordinates(5, 5, 4));
        var box2 = new Region(new Coordinates(0, 0, 0), new Coordinates(2, 2, 4));

        ems.updateEmptyMaximalSpaces(box1);
        ems.updateEmptyMaximalSpaces(box2);

        var box3 = new Region(new Coordinates(3, 0, 0), new Coordinates(6, 2, 3));

        ems.updateEmptyMaximalSpaces(box3);

        var updatedSpaces = ems.getEmptyMaximalSpaces(new Sizes(0, 0, 0)).ToList();



        Assert.Contains(new Region(new Coordinates(0, 2, 0), new Coordinates(2, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(2, 0, 0), new Coordinates(3, 2, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(5, 2, 0), new Coordinates(10, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 5, 0), new Coordinates(10, 10, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(6, 0, 0), new Coordinates(10, 10, 5)), updatedSpaces);

        Assert.Contains(new Region(new Coordinates(2, 2, 4), new Coordinates(5, 5, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(0, 0, 4), new Coordinates(2, 2, 5)), updatedSpaces);
        Assert.Contains(new Region(new Coordinates(3, 0, 3), new Coordinates(6, 2, 5)), updatedSpaces);

        Assert.Equal(updatedSpaces.Count, 8);



    }
    */

    

}


public class EMRNoTopNoBottomSpliterTest
{
    

    [Fact]
    public void Test1()
    {
        var original = new Region(new Coordinates(30, 10, 0), new Coordinates(35, 30, 20));
        var occupied = new Region(new Coordinates(20, 20, 0), new Coordinates(40, 30, 10));

        EMRNoTopNoBottomSpliter spliter = new EMRNoTopNoBottomSpliter();

        var updatedSpaces = spliter.SplitRegion(original, occupied).ToList();



        Assert.Contains(new Region(new Coordinates(30, 10, 0), new Coordinates(35, 20, 20)), updatedSpaces);
        Assert.Equal(updatedSpaces.Count, 1);
    }

    /*

    [Fact]
    public void Test2()
    {
        var original = new Region(new Coordinates(0, 0, 1), new Coordinates(10, 10, 10));
        var occupied = new Region(new Coordinates(0, 0, 0), new Coordinates(5, 5, 5));

        var ex = Assert.Throws<Exception>(() => helper.SplitSpace(original, occupied));
        Assert.Equal("Empty maximal space must always be fully on base", ex.Message);
    }

    [Fact]
    public void Test3()
    {
        var original = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 1));
        var occupied = new Region(new Coordinates(3, 3, 0), new Coordinates(7, 7, 1));

        var result = helper.SplitSpace(original, occupied).ToList();

        Assert.Contains(result, s => s.Start.Equals(new Coordinates(0, 0, 0)) && s.End.Equals(new Coordinates(3, 10, 1)));
        Assert.Contains(result, s => s.Start.Equals(new Coordinates(0, 0, 0)) && s.End.Equals(new Coordinates(10, 3, 1)));
        Assert.Contains(result, s => s.Start.Equals(new Coordinates(7, 0, 0)) && s.End.Equals(new Coordinates(10, 10, 1)));
        Assert.Contains(result, s => s.Start.Equals(new Coordinates(0, 7, 0)) && s.End.Equals(new Coordinates(10, 10, 1)));

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Test4()
    {
        var original = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 2));
        var occupied = new Region(new Coordinates(3, 3, 0), new Coordinates(7, 7, 1));

        var result = helper.SplitSpace(original, occupied).ToList();

        Assert.Contains(result, s => s.Start.Equals(new Coordinates(0, 0, 0)) && s.End.Equals(new Coordinates(3, 10, 2)));
        Assert.Contains(result, s => s.Start.Equals(new Coordinates(0, 0, 0)) && s.End.Equals(new Coordinates(10, 3, 2)));
        Assert.Contains(result, s => s.Start.Equals(new Coordinates(7, 0, 0)) && s.End.Equals(new Coordinates(10, 10, 2)));
        Assert.Contains(result, s => s.Start.Equals(new Coordinates(0, 7, 0)) && s.End.Equals(new Coordinates(10, 10, 2)));

        Assert.Equal(4, result.Count);
    }



    [Fact]
    public void Test5()
    {
        var original = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 0));
        var occupied = new Region(new Coordinates(0, 0, 0), new Coordinates(10, 10, 0));

        var result = helper.SplitSpace(original, occupied).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void Test6()
    {
        var original = new Region(new Coordinates(3, 3, 3), new Coordinates(10, 10, 10));
        var occupied = new Region(new Coordinates(0, 0, 3), new Coordinates(7, 7, 7));

        var result = helper.SplitSpace(original, occupied).ToList();

        Assert.Contains(result, s => s.Start.Equals(new Coordinates(7, 3, 3)) && s.End.Equals(new Coordinates(10, 10, 10)));
        Assert.Contains(result, s => s.Start.Equals(new Coordinates(3, 7, 3)) && s.End.Equals(new Coordinates(10, 10, 10)));

        Assert.Equal(2, result.Count);
    }
    */
}
