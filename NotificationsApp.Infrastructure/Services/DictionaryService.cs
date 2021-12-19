using EfData.Context;
using EfData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.Dictionary;
using NotificationsApp.Domain.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Infrastructure.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly ILogger<DictionaryService> _logger;
        private readonly ApplicationContext _context;

        public DictionaryService(
            ILogger<DictionaryService> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<SystemDto>> GetSystemsAsync(CancellationToken ct)
        {
            try
            {
                var entities = await _context.SystemsDictionary
                     .Include(x => x.Themes)
                     .ToListAsync(ct);

                var result = new List<SystemDto>();

                foreach (var system in entities)
                {
                    var systemDto = new SystemDto(system.Id, system.Name);
                    var themes = new List<ThemeDto>();
                    foreach (var theme in system.Themes)
                        themes.Add(new ThemeDto(theme.Id, theme.Name));
                    systemDto.Themes = themes;
                    result.Add(systemDto);
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }




        public async Task AddSystemAsync(string name, CancellationToken ct)
        {
            try
            {
                var newSystem = new SystemsDictionary
                {
                    Name = name
                };
                await _context.SystemsDictionary.AddAsync(newSystem, ct);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task UpdateSystemAsync(int id, string newName, CancellationToken ct)
        {
            try
            {
                var system = await _context.SystemsDictionary.FirstOrDefaultAsync(x => x.Id == id);
                if (system != null)
                {
                    var allSubsWithSystem = await _context.UserSubscription
                        .Where(x => x.SystemId == system.Id)
                        .ToListAsync(ct);
                    //foreach (var subs in allSubsWithSystem)
                    //{
                    //    subs.System = newName;
                    //    _context.UserSubscription.Update(subs);
                    //    await _context.SaveChangesAsync(ct);
                    //}

                    system.Name = newName;
                    _context.SystemsDictionary.Update(system);
                    await _context.SaveChangesAsync(ct);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task RemoveSystemAsync(int id, CancellationToken ct)
        {
            try
            {
                var system = await _context.SystemsDictionary.FirstOrDefaultAsync(x => x.Id == id);
                _context.SystemsDictionary.Remove(system);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }



        public async Task AddThemeAsync(int systemId, string name, CancellationToken ct)
        {
            try
            {
                var newTheme = new ThemeDictionary
                {
                    Name = name,
                    SystemsDictionaryId = systemId
                };
                await _context.ThemeDictionary.AddAsync(newTheme, ct);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task UpdateThemeAsync(int systemId, int themeId, string newName, CancellationToken ct)
        {
            try
            {
                var theme = await _context.ThemeDictionary.FirstOrDefaultAsync(x => x.Id == themeId && x.SystemsDictionaryId == systemId);
                if (theme != null)
                {
                    var allSubsWithTheme = await _context.UserSubscription
                        .Where(x => x.ThemeId == theme.Id)
                        .ToListAsync(ct);
                    //foreach (var subs in allSubsWithTheme)
                    //{
                    //    subs.Theme = newName;
                    //    _context.UserSubscription.Update(subs);
                    //    await _context.SaveChangesAsync(ct);
                    //}

                    theme.Name = newName;
                    _context.ThemeDictionary.Update(theme);
                    await _context.SaveChangesAsync(ct);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task RemoveThemeAsync(int systemId, int themeId, CancellationToken ct)
        {
            try
            {
                var theme = await _context.ThemeDictionary.FirstOrDefaultAsync(x => x.Id == themeId && x.SystemsDictionaryId == systemId);
                _context.ThemeDictionary.Remove(theme);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }
    }
}
