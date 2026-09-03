namespace SurveyBasket.Api.Services
{
    public class PollService(ApplicationDbContext context) : IPollService
    {

        private readonly ApplicationDbContext _context = context;
        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default) => 
            await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
        

         public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default)
          =>
           await _context.Polls.FindAsync(id, cancellationToken);

        public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
        {
            await _context.Polls.AddAsync(poll,cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return poll;
        }

        //public bool Update(int id, Poll poll)
        //{
        //    var currentPoll = Get(id);
        //    if (currentPoll is null)
        //        return false;
        //    currentPoll.Title = poll.Title;
        //    currentPoll.Summary = poll.Summary;
        //    return true;

        //}

        //public bool Delete(int id)
        //{
        //    var poll = Get(id);
        //    if (poll is null)
        //        return false;
        //    _polls.Remove(poll);
        //    return true;
        //}
    }
}
