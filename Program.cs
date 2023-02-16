#region input & calculation of distance
int verticalDistance = 0, horizontalDistance = 0;
string movements;
bool isValid = true;
do
{
    Console.Write("Enter your movements: ");
    movements = Console.ReadLine()!;

    for (int i = 0; i < movements.Length; i++)
    {
        switch (movements[i])
        {
            case '^': verticalDistance++; break;
            case 'V': verticalDistance--; break;
            case '>': horizontalDistance++; break;
            case '<': horizontalDistance--; break;
            default:
                if (Char.IsDigit(movements[i])) { SubstitueNumber(i); i--; }
                else { isValid = false; }
                break;
        }
    }

    if (!isValid) { Console.WriteLine("Try again..."); }
} while (!isValid);
#endregion

#region output
Console.Write("\nThe rover is ");
if (horizontalDistance != 0)
{
    Console.Write($"{Math.Abs(horizontalDistance)}m to the {(horizontalDistance > 0 ? "East" : "West")}");
}
if (verticalDistance != 0)
{
    Console.Write(horizontalDistance == 0 ? " " : " and ");
    Console.Write($"{Math.Abs(verticalDistance)}m to the {(verticalDistance > 0 ? "North" : "South")}");
}
if (horizontalDistance == 0 && verticalDistance == 0) { Console.Write("in the base station"); }

Console.WriteLine();
Console.Write($"Linear distance = {EvaluateDistance(verticalDistance, horizontalDistance, "linear")}m");
Console.WriteLine($", Manhatten distance = {EvaluateDistance(verticalDistance, horizontalDistance, "manhatten")}m");
#endregion

#region Methods
void SubstitueNumber(int index)
{
    int j;
    for (j = index + 1; j < movements.Length; j++) { if (!Char.IsDigit(movements[j])) { break; } }

    string substitution = SubstitueCharacter(movements[index - 1], int.Parse(movements.Substring(index, j - index)) - 1);
    movements = $"{movements.Substring(0, index)}{substitution}{movements.Substring(j)}";
}

string SubstitueCharacter(char movement, int number) => new string(movement, number);

double EvaluateDistance(int verticalDistance, int horizontalDistance, string type)
{
    return type switch
    {
        "manhatten" => Math.Abs(verticalDistance) + Math.Abs(horizontalDistance),
        "linear" => Math.Round(Math.Sqrt(Math.Pow(verticalDistance, 2) + Math.Pow(horizontalDistance, 2)), 2),
        _ => 0
    };
}
#endregion
