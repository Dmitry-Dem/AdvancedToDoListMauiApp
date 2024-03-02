using SQLite;

namespace AdvancedToDoListMauiApp.Models;

public class BaseEntity
{
    [PrimaryKey, AutoIncrement, Column("Id")]
    public int Id { get; set; }
}
