namespace Incremental;

public class Upgrade
{
    public double cost;
    public double multiplier;

    public Upgrade(double cost, double multi)
    {
        this.cost = cost;
        this.multiplier = multi;
        //TODO effect types with enum maybe and dont take multi but just like value/change
    }
}