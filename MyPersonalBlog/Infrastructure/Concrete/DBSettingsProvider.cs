﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Infrastructure
{
    public class DBSettingsProvider : ISettingsProvider
    {
        ISettingsRepository _settingsRepository;

        public DBSettingsProvider(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public Settings GetSettings()
        {
            var result = _settingsRepository.Settings.Single();
            return result;
        }

        public void SaveSettings(Settings settings)
        {
            if (settings != null)
            {
                _settingsRepository.Save(settings);
            }
        }
    }
}