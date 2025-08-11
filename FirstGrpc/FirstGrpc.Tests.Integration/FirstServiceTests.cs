
using Basics;
using FluentAssertions;

namespace FirstGrpc.Tests.Integration;

public class FirstServiceTests : IClassFixture<MyFactory<Program>>
{
    private readonly MyFactory<Program> _factory;

    public FirstServiceTests(MyFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void GetUnaryMesssage()
    {
        //Arrange
        var client = _factory.CreateGrpcClient();
        var expectedResponse = new Response(){ Message = "message from server localhost"};
        
        //Act
        var actualResponse = client.Unary(new Request() { Content = "message" });
        
        //Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }
}