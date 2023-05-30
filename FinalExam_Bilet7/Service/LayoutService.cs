using FinalExam_Bilet7.DAL;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_Bilet7.Service
{
    
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string,string>> GetSettingsAsync()
        {
            Dictionary<string, string> settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);
            return settings;
        }

    }
}
