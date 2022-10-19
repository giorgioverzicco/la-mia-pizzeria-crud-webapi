using la_mia_pizzeria_crud_webapi.Models;

namespace la_mia_pizzeria_crud_webapi.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    void Update(Message message);
}