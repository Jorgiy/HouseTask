namespace HouseTask
{
    public interface IActionable
    {
        object MakeAction(params object[] @params);
    }
}