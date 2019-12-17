﻿//    Copyright 2014 Productivity Apex Inc.
//        http://www.productivityapex.com/
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System.Linq;
using PAI.FRATIS.SFL.Common.Infrastructure.Data;
using PAI.FRATIS.SFL.Domain.Geography;
using PAI.FRATIS.SFL.Services.Core;
using PAI.FRATIS.SFL.Services.Core.Caching;

namespace PAI.FRATIS.SFL.Services.Geography
{
    public interface ILocationOptionsService : IEntitySubscriberServiceBase<LocationOptions>
    {
        IQueryable<Location> GetCustomHandlingLocations(int subscriberId);

        IQueryable<Location> GetDriverSelectableLocations(int subscriberId);
    }

    public class LocationOptionsService : EntitySubscriberServiceBase<LocationOptions>, ILocationOptionsService
    {
        public LocationOptionsService(IRepository<LocationOptions> repository, ICacheManager cacheManager) : base(repository, cacheManager)
        {
        }

        public override IQueryable<LocationOptions> SelectWithAll()
        {
            return _repository.SelectWith("Location");
        }

        public IQueryable<Location> GetCustomHandlingLocations(int subscriberId)
        {
            return SelectWithAll()
                .Where(p => p.SubscriberId == subscriberId 
                    && p.IsCustomHandling)
                .Select(p => p.Location);
        }

        public IQueryable<Location> GetDriverSelectableLocations(int subscriberId)
        {
            return SelectWithAll()
                .Where(p => p.SubscriberId == subscriberId 
                    && p.IsDriverSelectable)
                .Select(p => p.Location);
        }
    }
}