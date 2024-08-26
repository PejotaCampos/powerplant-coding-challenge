using NSubstitute;
using PowerPlantChallenge.Models;
using PowerPlantChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{    
    public class SwitchPowerplantServiceTest
    {
        [Fact]
        public void TestReturnResponse()
        {
            IPowerplantCostService powerplantCostService = Substitute.For<IPowerplantCostService>();

            SwitchPowerplantService switchPowerplantService = new(powerplantCostService);

            var powerplants = GenerateMocks.GetAllPowerplants();

            foreach (var powerplant in powerplants)
            {
                powerplant.IsOn = true;
                powerplant.PowerLoaded = powerplant.PowerMax;
            }

            var response = switchPowerplantService.GetResponse(powerplants);

            Assert.Equal(6, response.Count());
            Assert.Equal(1332, response.Sum(r => r.Energy));
        }

        [Fact]
        public void TestEnergyLoaded()
        {
            IPowerplantCostService powerplantCostService = Substitute.For<IPowerplantCostService>();

            SwitchPowerplantService switchPowerplantService = new(powerplantCostService);

            var powerplants = GenerateMocks.GetAllPowerplants();

            foreach (var powerplant in powerplants)
            {
                powerplant.IsOn = true;
                powerplant.PowerLoaded = powerplant.PowerMax;
            }

            var energyLoaded = switchPowerplantService.EnergyLoaded(powerplants);

            Assert.Equal(1332, energyLoaded);
        }

        [Fact]
        public void TestTurnOnWinds()
        {
            IPowerplantCostService powerplantCostService = Substitute.For<IPowerplantCostService>();

            SwitchPowerplantService switchPowerplantService = new(powerplantCostService);

            var powerplants = GenerateMocks.GetAllPowerplants();

            var wind = 50.0;

            var fuels = GenerateMocks.FuelsWithWind(wind);

            var windsOn = switchPowerplantService.TurnOnFuelsNoCost(powerplants, wind);

            var energyLoaded = switchPowerplantService.EnergyLoaded(windsOn);

            var onlyTurnedOn = windsOn.Where(w => w.IsOn);

            Assert.Equal(6, windsOn.Count());
            Assert.Equal(2, onlyTurnedOn.Count());
            Assert.Equal(93, energyLoaded);
        }

        [Fact]
        public void TestTurnOnByCost_WhenKerosineIsBetter()
        {
            IPowerplantCostService powerplantCostService = Substitute.For<IPowerplantCostService>();

            SwitchPowerplantService switchPowerplantService = new(powerplantCostService);

            var kerosineIsBetter = GenerateMocks.KerosineBetter();

            powerplantCostService.VerifyBestCost(
                Arg.Any<IEnumerable<Powerplant>>(),
                Arg.Any<Fuels>(),
                Arg.Any<double>())
                .Returns((kerosineIsBetter, 10));

            var powerplants = GenerateMocks.GetGasAndKerosinePowerplants();

            var fuels = GenerateMocks.FuelsNoWind();

            var powerplantsOn = switchPowerplantService.TurnOnByCost(powerplants, fuels, 470);

            var energyLoaded = switchPowerplantService.EnergyLoaded(powerplantsOn);

            Assert.Equal(470, energyLoaded);
        }

        [Fact]
        public void TestTurnOnByCost_WhenGasfiredIsBetter()
        {
            IPowerplantCostService powerplantCostService = Substitute.For<IPowerplantCostService>();

            SwitchPowerplantService switchPowerplantService = new(powerplantCostService);

            var gasfiredIsBetter = GenerateMocks.GasfiredBetter();

            powerplantCostService.VerifyBestCost(
                Arg.Any<IEnumerable<Powerplant>>(),
                Arg.Any<Fuels>(),
                Arg.Any<double>())
                .Returns((gasfiredIsBetter, 40));

            var powerplants = GenerateMocks.GetGasAndKerosinePowerplants();

            var fuels = GenerateMocks.FuelsNoWind();

            var powerplantsOn = switchPowerplantService.TurnOnByCost(powerplants, fuels, 471);

            var energyLoaded = switchPowerplantService.EnergyLoaded(powerplantsOn);

            Assert.Equal(500, energyLoaded);
        }
    }
}
