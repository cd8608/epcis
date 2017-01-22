﻿using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Epcis.Database.Mappings
{
    public class ObjectEventMap : SubclassMap<ObjectEvent>
    {
        public ObjectEventMap()
        {
            DiscriminatorValue("obj");

            Map(x => x.Action).Column("Action").CustomType<EventAction>().Not.Nullable();
            Map(x => x.Ilmd).Column("Ilmd").CustomType<XDocType>();
            HasManyToMany(x => x.Epcs).Schema(DatabaseConstants.Schemas.Epcis).Table(DatabaseConstants.Tables.EventToEpc).ParentKeyColumn("EventId").ChildKeyColumn("EpcId");
        }
    }
}