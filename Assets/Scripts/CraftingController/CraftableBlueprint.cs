public class CraftableBlueprint
{
    public string craftableName;

    public string req1;
    public string req2;
    public string req3;

    public int req1Amount;
    public int req2Amount;
    public int req3Amount;

    public int numOfRequirements;

    public CraftableBlueprint(string name, int reqNum, string req1, int req1num)
    {
        craftableName = name;
        numOfRequirements = reqNum;

        this.req1 = req1;

        req1Amount = req1num;

    }

    public CraftableBlueprint(string name, int reqNum, string req1, int req1num, string req2, int req2num)
    {
        craftableName = name;
        numOfRequirements = reqNum;

        this.req1 = req1;
        this.req2 = req2;

        req1Amount = req1num;
        req2Amount = req2num;

    }

    public CraftableBlueprint(string name, int reqNum, string req1, int req1num, string req2, int req2num, string req3, int req3num)
    {
        craftableName = name;
        numOfRequirements = reqNum;

        this.req1 = req1;
        this.req2 = req2;
        this.req3 = req3;

        req1Amount = req1num;
        req2Amount = req2num;
        req3Amount = req3num;

    }
}
