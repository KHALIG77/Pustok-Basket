using System.Linq;
using Microsoft.EntityFrameworkCore;
using PustokStart.DAL;
using PustokStart.Models;

namespace PustokStart.Services
{
    public class LayoutService
    {
        private readonly PustokContext _context;
        public LayoutService(PustokContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);

        }
        public List<Category> GetCategories()
        {
            return _context.Categories.Include(x=>x.SubCategories).ToList();
        }

    }
}
