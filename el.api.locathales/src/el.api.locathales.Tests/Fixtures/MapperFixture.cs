﻿using AutoMapper;

namespace el.api.locathales.Tests.Fixtures
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new Application.Mapping.ClienteMap());
            });

            Mapper = config.CreateMapper();
        }
    }
}
