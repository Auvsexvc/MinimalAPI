namespace MinimalWebAPI.ToDo
{
    public interface IToDoService
    {
        void Create(ToDo toDo);

        void Delete(int id);

        IEnumerable<ToDo> GetAll();

        ToDo GetById(int id);

        void Update(ToDo toDo);
    }
}
