using System;
using Xunit;
using NUnit.Framework;
using Moq;

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
            return BreachType.NORMAL;
        }

        public static void checkAndAlert(string recipient, BatteryParam batteryParam, double temperature)
        {
        }
    }

    public class TypeWiseAlertTestSuite
    {
        private static BreachType mockOutput;
        private static Func<CoolingType, double, BreachType> funcPtrClassifyTemperatureBreach;

        public void Setup()
        {
            funcPtrClassifyTemperatureBreach = TypeWiseAlert.Mock_classifyTemperatureBreach;
        }

        public void TestCheckAndAlert_TO_CONTROLLER_LowBreach()
        {
            mockOutput = funcPtrClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -10);
            var expectedOutput = BreachType.TOO_LOW;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING};
            TypeWiseAlert.checkAndAlert("TO_CONTROLLER", batteryParam, -10);
            Debug.Assert(expectedOutput, mockOutput);
        }

        public void TestCheckAndAlert_TO_CONTROLLER_NormalBreach()
        {
            mockOutput = funcPtrClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 5);
            var expectedOutput = BreachType.NORMAL;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING};
            TypeWiseAlert.checkAndAlert("TO_CONTROLLER", batteryParam, 5);
            Assert.AreEqual(expectedOutput, mockOutput);
        }

        public void TestCheckAndAlert_TO_CONTROLLER_HighBreach()
        {
            mockOutput = funcPtrClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 50);
            var expectedOutput = BreachType.TOO_HIGH;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING};
            TypeWiseAlert.checkAndAlert("TO_CONTROLLER", batteryParam, 50);
            Assert.AreEqual(expectedOutput, mockOutput);
        }

        public void TestCheckAndAlert_TO_EMAIL_LowBreach()
        {
            mockOutput = funcPtrClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, -2);
            var expectedOutput = BreachType.TOO_LOW;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.HI_ACTIVE_COOLING};
            TypeWiseAlert.checkAndAlert("TO_EMAIL", batteryParam, -2);
            Assert.AreEqual(expectedOutput, mockOutput);
        }
        public void TestCheckAndAlert_TO_EMAIL_NormalBreach()
        {
            mockOutput = funcPtrClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 5);
            var expectedOutput = BreachType.NORMAL;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.PASSIVE_COOLING};
            TypeWiseAlert.checkAndAlert("TO_EMAIL", batteryParam, 5);
            Assert.AreEqual(expectedOutput, mockOutput);
        }

        public void TestCheckAndAlert_TO_EMAIL_HighBreach()
        {
            mockOutput = funcPtrClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 50);
            var expectedOutput = BreachType.TOO_HIGH;
            var batteryParam = new BatteryParam { CoolingType = CoolingType.HI_ACTIVE_COOLING};
            TypeWiseAlert.checkAndAlert("TO_EMAIL", batteryParam, 50);
            Assert.AreEqual(expectedOutput, mockOutput);
        }
    }
}
