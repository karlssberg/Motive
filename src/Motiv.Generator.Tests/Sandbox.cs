namespace Test;


public class MyBuildTarget
{
    [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
    public MyBuildTarget(
        int number,
        string text,
        Guid id)
    {
        Number = number;
        Text = text;
        Id = id;
    }

    public int Number { get; set; }

    public string Text { get; set; }

    public Guid Id { get; set; }
}





// public class MyBuildTarget
// {
//     [Motiv.Generator.Attributes.GenerateFluentBuilder("Test.Factory")]
//     public MyBuildTarget(
//         int number,
//         string text)
//     {
//         Number = number;
//         Text = text;
//     }
//
//     public int Number { get; set; }
//
//     public string Text { get; set; }
// }
