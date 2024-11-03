using System;
using Xunit;

public class TypeWiseAlertTests
{
    public enum BreachType
    {
        TOO_LOW,
        TOO_HIGH,
        NORMAL
    }

    public enum CoolingType
    {
        PASSIVE_COOLING,
        HI_ACTIVE_COOLING
    }

    public class BatteryParam
    {
        public CoolingType CoolingType { get; set; }
    }

    public static class TypeWiseAlert
    {
        public static BreachType Mock_classifyTemperatureBreach(CoolingType coolingType, double temperatureInC)
        {
            if (temperatureInC < 0) return BreachType.TOO_LOW;
            if (temperatureInC > 45) return BreachType.TOO_HIGH;
            return BreachType.NORMAL;
        }
    }

    [Collection("TypeWiseAlertTests")]
    public class TypeWiseAlertTestSuite
    {
        [Fact]
        public void TestCheckAndAlert_TO_CONTROLLER_LowBreach()
        {
            var expectedOutput = BreachType.TOO_LOW;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING };
            var mockOutput = TypeWiseAlert.Mock_classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -10);
            TypeWiseAlert.checkAndAlert("TO_CONTROLLER", batteryParam, -10);
            Assert.Equal(expectedOutput, mockOutput);
        }

        [Fact]
        public void TestCheckAndAlert_TO_CONTROLLER_NormalBreach()
        {
            var expectedOutput = BreachType.NORMAL;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING };
            var mockOutput = TypeWiseAlert.Mock_classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 5);
            TypeWiseAlert.checkAndAlert("TO_CONTROLLER", batteryParam, 5);
            Assert.Equal(expectedOutput, mockOutput);
        }

        [Fact]
        public void TestCheckAndAlert_TO_CONTROLLER_HighBreach()
        {
            var expectedOutput = BreachType.TOO_HIGH;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING };
            var mockOutput = TypeWiseAlert.Mock_classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 50);
            TypeWiseAlert.checkAndAlert("TO_CONTROLLER", batteryParam, 50);
            Assert.Equal(expectedOutput, mockOutput);
        }

        [Fact]
        public void TestCheckAndAlert_TO_EMAIL_LowBreach()
        {
            var expectedOutput = BreachType.TOO_LOW;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.HI_ACTIVE_COOLING };
            var mockOutput = TypeWiseAlert.Mock_classifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, -2);
            TypeWiseAlert.checkAndAlert("TO_EMAIL", batteryParam, -2);
            Assert.Equal(expectedOutput, mockOutput);
        }

        [Fact]
        public void TestCheckAndAlert_TO_EMAIL_NormalBreach()
        {
            var expectedOutput = BreachType.NORMAL;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING };
            var mockOutput = TypeWiseAlert.Mock_classifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 5);
            TypeWiseAlert.checkAndAlert("TO_EMAIL", batteryParam, 5);
            Assert.Equal(expectedOutput, mockOutput);
        }

        [Fact]
        public void TestCheckAndAlert_TO_EMAIL_HighBreach()
        {
            var expectedOutput = BreachType.TOO_HIGH;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.HI_ACTIVE_COOLING };
            var mockOutput = TypeWiseAlert.Mock_classifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 50);
            TypeWiseAlert.checkAndAlert("TO_EMAIL", batteryParam, 50);
            Assert.Equal(expectedOutput, mockOutput);
        }
    }
}
