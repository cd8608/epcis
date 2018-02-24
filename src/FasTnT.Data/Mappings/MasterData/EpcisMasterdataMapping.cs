﻿using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.MasterData
{
    public class EpcisMasterdataMapping : ClassMap<EpcisMasterdata>
    {
        public EpcisMasterdataMapping()
        {
            Schema("cbv");
            Table("masterdata");

            CompositeId()
                .KeyProperty(x => x.Id, "id")
                .KeyProperty(x => x.Type, "type");

            Map(x => x.CreatedOn).Column("created_on").Not.Nullable();
            Map(x => x.LastUpdatedOn).Column("last_updated_on").Not.Nullable();
        }
    }
}
