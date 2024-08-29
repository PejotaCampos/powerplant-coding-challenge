using PowerPlantChallenge.Services;

namespace Services.Tests
{
    public class PowerplantResponseServiceTest
    {
        [Fact]
        public void TestReturnResponse()
        {
            PowerplantResponseService powerplantResponseService = new();

            var powerplants = GenerateMocks.GetAllPowerplants();

            foreach (var powerplant in powerplants)
            {
                powerplant.IsOn = true;
                powerplant.PowerLoaded = powerplant.PowerMax;
            }

            var response = powerplantResponseService.GetResponse(powerplants);

            Assert.Equal(6, response.Count());
            Assert.Equal(1332, response.Sum(r => r.Energy));
        }
    }
}
