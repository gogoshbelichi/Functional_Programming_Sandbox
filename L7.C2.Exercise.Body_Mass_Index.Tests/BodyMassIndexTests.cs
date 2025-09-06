namespace L7.C2.Exercise.Body_Mass_Index.Tests;

public class BodyMassIndexTests
{
    private readonly BodyMassIndexApp app = new();
    
    
    [Theory]
    [InlineData(46.8, 1.15, 35.39)] //setup expected
    [InlineData(65d, 1.7d, 22.49)]
    [InlineData(400d, 2.03f, 97.07)]
    [InlineData(400f, 2.03d, 97.07)]
    public void MassIndexTests(double mass, double height, double expected)
    {
        var result = app.MassIndex(mass, height);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(10.02, nameof(BodyCategory.Underweight))]
    [InlineData(18.50, nameof(BodyCategory.Healthy))]
    [InlineData(20.5, nameof(BodyCategory.Healthy))]
    [InlineData(25, nameof(BodyCategory.Overweight))]
    [InlineData(28.6, nameof(BodyCategory.Overweight))]
    [InlineData(30.00, nameof(BodyCategory.Obesity))]
    [InlineData(41.24, nameof(BodyCategory.Obesity))]
    public void MassIndexCategoryTests(double index, string expected)
    {
        Assert.Equal(expected, app.MassIndexCategory(index));
    }
    
    [Theory]
    [InlineData(173.3, 1.72)]
    [InlineData(83.3,1.93)]
    [InlineData(53.3,1.44)]
    [InlineData(43.3,1.7)]
    [InlineData(33.3,0.9)]
    [InlineData(23.3,1.2)]
    [InlineData(13.3, 1.1)]
    public void WriteTests(double mass, double height)
    {
        var body = new Body(mass, height);
        var result = app.Write(body);
        Assert.True(result);
    }
}