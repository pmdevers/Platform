using FinSecure.Platform.Common.ValueObjects;

namespace Common.Tests.ValueObjects;

public class PercentageTests
{
    [Fact]
    public void Percentage_Create_Should_Return_Correct_Percentage()
    {
        // Arrange
        decimal value = 50;

        // Act
        var result = Percentage.Create(value);

        // Assert
        result.Should().Be(Percentage.Create(0.5m));
    }

    [Fact]
    public void Percentage_ToString_Should_Return_Correct_String()
    {
        // Arrange
        var percentage = Percentage.Create(0.5m);

        // Act
        var result = percentage.ToString();

        // Assert
        result.Should().Be("50%");
    }

    [Fact]
    public void Percentage_Parse_Should_Return_Correct_Percentage()
    {
        // Arrange
        string value = "50%";

        // Act
        var result = Percentage.Parse(value);

        // Assert
        result.Should().Be(Percentage.Create(0.5m));
    }

    [Fact]
    public void Percentage_TryParse_Should_Return_True_And_Correct_Percentage()
    {
        // Arrange
        string value = "50%";
        bool parseResult;

        // Act
        parseResult = Percentage.TryParse(value, out var result);

        // Assert
        parseResult.Should().BeTrue();
        result.Should().Be(Percentage.Create(0.5m));
    }

    [Fact]
    public void Percentage_TryParse_Should_Return_False_For_Invalid_Input()
    {
        // Arrange
        string value = "invalid";

        // Act
        var parseResult = Percentage.TryParse(value, out var result);

        // Assert
        parseResult.Should().BeFalse();
        result.Should().Be(Percentage.Empty);
    }

    [Fact]
    public void Percentage_Implicit_Operator_Should_Create_Percentage_From_Decimal()
    {
        // Arrange
        decimal value = 0.5m;

        // Act
        Percentage result = value;

        // Assert
        result.Should().Be(0.5m);
    }

    [Fact]
    public void Percentage_Explicit_Operator_Should_Return_Decimal_Value()
    {
        // Arrange
        var percentage = Percentage.Create(0.5m);

        // Act
        decimal result = (decimal)percentage;

        // Assert
        result.Should().Be(0.5m);
    }

    [Fact]
    public void Percentage_Addition_Operator_Should_Add_Two_Percentages()
    {
        // Arrange
        var percentage1 = Percentage.Create(0.3m);
        var percentage2 = Percentage.Create(0.2m);

        // Act
        var result = percentage1 + percentage2;

        // Assert
        result.Should().Be(Percentage.Create(0.5m));
    }

    [Fact]
    public void Percentage_Subtraction_Operator_Should_Subtract_Two_Percentages()
    {
        // Arrange
        var percentage1 = Percentage.Create(0.5m);
        var percentage2 = Percentage.Create(0.2m);

        // Act
        var result = percentage1 - percentage2;

        // Assert
        result.Should().Be(Percentage.Create(0.3m));
    }

    [Fact]
    public void Percentage_Multiplication_Operator_Should_Multiply_Two_Percentages()
    {
        // Arrange
        var percentage1 = Percentage.Create(0.5m);
        var percentage2 = Percentage.Create(0.2m);

        // Act
        var result = percentage1 * percentage2;

        // Assert
        result.Should().Be(Percentage.Create(0.1m));
    }

    [Fact]
    public void Percentage_Division_Operator_Should_Divide_Two_Percentages()
    {
        // Arrange
        var percentage1 = Percentage.Create(0.5m);
        var percentage2 = Percentage.Create(0.2m);

        // Act
        var result = percentage1 / percentage2;

        // Assert
        result.Should().Be(Percentage.Create(2.5m));
    }
}