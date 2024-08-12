using SQLite;

namespace MathGame.Models;
[Table("Game")]
public class Game
{
    [PrimaryKey, AutoIncrement, Column("ID")]
    public int ID { get; set; }
    public int Score {  get; set; }
    public GameOperation Type {  get; set; }
    public DateTime Date { get; set; }


}

public enum GameOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
