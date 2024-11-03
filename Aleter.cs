public partial class Alerter{
    public enum AlertTarget { 
        TO_CONTROLLER, 
        TO_EMAIL }

    public static void checkAndAlert(AlertTarget alertTarget, CoolingType coolingType, double temperatureInC)
    {
        BreachType breachType = classifyTemperatureBreach(coolingType, temperatureInC);
        if (alertTarget == AlertTarget.TO_CONTROLLER)
            sendAlertToController(breachType);
        else
            sendAlertToEmail(breachType);
    }

    private static void sendAlertToController(BreachType breachType)
    {
      const ushort header = 0xfeed;
      Console.WriteLine("{} : {}\n", header, breachType);
    }

    private static void sendAlertToEmail(BreachType breachType)
    {
        String recipient = "a.b@c.com";
        if (breachType != BreachType.NORMAL)
            Console.WriteLine("To: {0}\n Hi, the temperature is {1}\n", recipient,
                breachType == BreachType.TOO_LOW ? "too low" : "too high");
    }
}
