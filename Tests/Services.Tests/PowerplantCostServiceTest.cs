using PowerPlantChallenge.Models;
using PowerPlantChallenge.Services;

namespace Services.Tests
{
    public class PowerplantCostServiceTest
    {

        private readonly PowerplantCostService powerplantCostService = new();

        [Fact]
        public void Given10load_Then_ReturnKerosine()
        {
            var powerplants = GenerateMocks.GetGasAndKerosinePowerplants();
            var fuels = GenerateMocks.FuelsNoWind();

            var bestPowerplantLoad = powerplantCostService.VerifyBestCost(powerplants, fuels, 10);

            var bestPowerplant = bestPowerplantLoad.powerplant;
            var bestLoad = bestPowerplantLoad.load;

            Assert.Equal(10, bestLoad);
            Assert.Equal("tj1", bestPowerplant.Name);
        }

        [Fact]
        public void Given15load_Then_ReturnKerosine()
        {
            var powerplants = GenerateMocks.GetGasAndKerosinePowerplants();
            var fuels = GenerateMocks.FuelsNoWind();

            var bestPowerplantLoad = powerplantCostService.VerifyBestCost(powerplants, fuels, 15);

            var bestPowerplant = bestPowerplantLoad.powerplant;
            var bestLoad = bestPowerplantLoad.load;

            Assert.Equal(15, bestLoad);
            Assert.Equal("tj1", bestPowerplant.Name);
        }

        [Fact]
        public void Given16load_Then_ReturnSmallgasfired()
        {
            var powerplants = GenerateMocks.GetAllPowerplantsCO2();
            var fuels = GenerateMocks.FuelsNoWind();

            var bestPowerplantLoad = powerplantCostService.VerifyBestCost(powerplants, fuels, 16);

            var bestPowerplant = bestPowerplantLoad.powerplant;
            var bestLoad = bestPowerplantLoad.load;

            Assert.Equal(40, bestLoad);
            Assert.Equal("gasfiredsomewhatsmaller", bestPowerplant.Name);
        }
    }

    public static class GenerateMocks
    {
        public static IEnumerable<Powerplant> GetAllPowerplants()
        {

            List<Powerplant> powerplants = new()
            {
                new()
                {
                    Name = "gasfiredbig1",
                    Type = "gasfired",
                    Efficiency = 0.53,
                    PowerMin = 100,
                    PowerMax = 460
                },
                new()
                {
                    Name = "gasfiredbig2",
                    Type = "gasfired",
                    Efficiency = 0.53,
                    PowerMin = 100,
                    PowerMax = 460
                },
                new()
                {
                    Name = "gasfiredsomewhatsmaller",
                    Type = "gasfired",
                    Efficiency = 0.37,
                    PowerMin = 40,
                    PowerMax = 210
                },
                new()
                {
                    Name = "tj1",
                    Type = "turbojet",
                    Efficiency = 0.3,
                    PowerMin = 0,
                    PowerMax = 16
                },
                new()
                {
                    Name = "windpark1",
                    Type = "windturbine",
                    Efficiency = 1,
                    PowerMin = 0,
                    PowerMax = 150
                },
                new()
                {
                    Name = "windpark2",
                    Type = "windturbine",
                    Efficiency = 1,
                    PowerMin = 0,
                    PowerMax = 36
                }
            };

            return powerplants;
        }

        public static IEnumerable<Powerplant> GetGasAndKerosinePowerplants()
        {
            var powerplants = GetAllPowerplants().Where(p=> p.Type != "windturbine") ;

            return powerplants;
            
        }

        public static Fuels FuelsWithWind(double windRate) => new() { Co2EuroTon = 20, GasEuroMWh = 13.4, KerosineEuroMWh = 50.8, Wind = windRate };

        public static Fuels FuelsNoWind() => new() { Co2EuroTon = 20, GasEuroMWh = 13.4, KerosineEuroMWh = 50.8, Wind = 0 };

        public static Powerplant KerosineBetter() => new() {
            Name = "tj1",
            Type = "turbojet",
            Efficiency = 0.3,
            PowerMin = 0,
            PowerMax = 16
        };

        public static Powerplant GasfiredBetter() => new()
        {
            Name = "gasfiredsomewhatsmaller",
            Type = "gasfired",
            Efficiency = 0.37,
            PowerMin = 40,
            PowerMax = 210
        };

        public static IEnumerable<Powerplant> GetAllPowerplantsCO2()
        {

            List<Powerplant> powerplants = new()
            {
                new()
                {
                    Name = "gasfiredbig1",
                    Type = "gasfired",
                    Efficiency = 0.53,
                    PowerMin = 100,
                    PowerMax = 460
                },
                new()
                {
                    Name = "gasfiredbig2",
                    Type = "gasfired",
                    Efficiency = 0.53,
                    PowerMin = 100,
                    PowerMax = 460
                },
                new()
                {
                    Name = "gasfiredsomewhatsmaller",
                    Type = "gasfired",
                    Efficiency = 0.37,
                    PowerMin = 40,
                    PowerMax = 210
                },
                new()
                {
                    Name = "tj1",
                    Type = "turbojet",
                    Efficiency = 0.3,
                    PowerMin = 0,
                    PowerMax = 30
                }
            };

            return powerplants;
        }
    }
}