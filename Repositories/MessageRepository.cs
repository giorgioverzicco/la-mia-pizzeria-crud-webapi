using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using la_mia_pizzeria_crud_webapi.Models;

namespace la_mia_pizzeria_crud_webapi.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    private readonly ApplicationDbContext _db;
    
    public MessageRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Message message)
    {
        _db.Messages.Update(message);
    }
}