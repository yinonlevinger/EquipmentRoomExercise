using System;

// <summary>
//Todo: צרו מחלקת חריגים המטפלת במקרים בהם אין מספיק מחשבים זמינים להשאלה.
//שם המחלקה צריך להיות EquipmentUnavailableException.
class EquipmentUnavailableException : Exception
{
    public int Requestedamount { get; }
    public int Missingamount { get; }

    public EquipmentUnavailableException(string message, int requestedamount,int missingamount ) : base(message)
    {
        Requestedamount = requestedamount;
        Missingamount = missingamount;
    }

}



class EquipmentRoom
{
    private int availableComputers;

    public int AvailableComputers
    {
        get
        {
            return availableComputers;
        }
        set
        {
            // TODO: יש לבדוק שמספר המחשבים אינו שלילי.
            // אם כן, יש לזרוק חריגה מתאימה.
            if(value < 0)
            {
                throw new ArgumentOutOfRangeException("num of computers cannt be minus.");
            }
            availableComputers = value;
        }
    }

    public EquipmentRoom(int amount)
    {
        AvailableComputers = amount;
    }

    public void Borrow(int amount)
    {
       
            // TODO: יש לזרוק חריגה מתאימה כאשר אין מספיק מחשבים זמינים.
            if (amount > AvailableComputers)
            {
            int missingAmount = amount - AvailableComputers;
            throw new EquipmentUnavailableException("Not enough computers available."+ amount,missingAmount);
            }

        AvailableComputers -= amount;
        Console.WriteLine($"Borrowed successfully {amount} computers.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        int initialComputers = 10;

        EquipmentRoom room = new EquipmentRoom(initialComputers);

        // יצירת חדר מחשבים עם מספר מחשבים שלילי:
        // לאחר מימוש הבדיקה ב-set, יש לוודא שמצב כזה מטופל כראוי.
        // TODO: יש להוסיף try-catch מתאימים סביב הקריאות שעלולות לזרוק חריגה.
        // int negativeComputers = -5;
        // EquipmentRoom invalidRoom = new EquipmentRoom(negativeComputers);
        try
        {
            int negConputers = -5;
            EquipmentRoom invalidRoom = new EquipmentRoom(negConputers);
            room.Borrow(negConputers);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("computers num cannt be negative"+ex.Message);
        }

        try 
        {
            room.Borrow(4);
            Console.WriteLine("continue on...");

            room.Borrow(8);
        }
        catch (EquipmentUnavailableException ex)
        {
            Console.WriteLine("cannot borrow" + ex.message);
            Console.WriteLine($"want amount: {ex.Requestedamount}, missing amount: {ex.Missingamount}");
        }



        Console.WriteLine("availble computers " + room.AvailableComputers);
    }
}
