
namespace Global.Common.Azure.Tests.Cases
{
    public class AzODataTableResponseErrorTest
    {
        [Fact]
        public void TryCreate_Test1()
        {
            // Arrange
            var oDataInput = ODataHelper.OdataErrorTestInput();
            var expectedMessageValue = ODataHelper.tableAlreadyExistMessageValue.Split("\n")[0];

            // Act
            var actualResult = AzODataErrorTestResult<AzODataTableResponseError>.Create(
                status: AzODataTableResponseError.TryCreate(oDataInput.OData, out AzODataTableResponseError? azODataError),
                azODataError);

            // Assert

            Assert.True(actualResult.Status);

            Assert.NotNull(actualResult.AzODataError);
            Assert.NotNull(actualResult.AzODataError.Message);

            Assert.Equal(oDataInput.ErrorCode, actualResult.AzODataError.ErrorCode);
            Assert.Equal(expectedMessageValue, actualResult.AzODataError.Message);
        }
    }
}
