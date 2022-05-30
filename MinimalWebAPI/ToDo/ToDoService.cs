using AutoMapper;

namespace MinimalWebAPI.ToDo
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _dbContext;
        private readonly IMapper _mapper;


        public ToDoService(ToDoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Create(ToDo toDo)
        {
            _dbContext.ToDos.Add(_mapper.Map<ToDo>(toDo));
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {


            _dbContext.ToDos.Remove(_dbContext.ToDos.FirstOrDefault(t => t.Id == id));
            _dbContext.SaveChanges();
        }

        public IEnumerable<ToDo> GetAll()
        {
            return _mapper.Map<List<ToDo>>(_dbContext.ToDos.ToList());
        }

        public ToDo GetById(int id)
        {
            

            return _mapper.Map<ToDo>(_dbContext.ToDos.FirstOrDefault(t=>t.Id == id));
        }

        public void Update(ToDo toDo)
        {
            var existingToDo = GetById(toDo.Id);


            _dbContext.ToDos.Update(_dbContext.ToDos.FirstOrDefault(t => t.Id == toDo.Id));
            _dbContext.SaveChanges();
        }
    }
}
