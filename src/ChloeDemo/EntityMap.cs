﻿using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChloeDemo
{
    public class UserMapBase<TUser> : EntityTypeBuilder<TUser> where TUser : UserLite
    {
        public UserMapBase()
        {
            this.Ignore(a => a.NotMapped);
            this.Property(a => a.Id).IsAutoIncrement().IsPrimaryKey();
        }
    }
    public class UserMap : UserMapBase<User>
    {
        public UserMap()
        {
            this.MapTo("Users");
            this.HasOne(a => a.City).HasForeignKey("CityId");
            this.Ignore(a => a.NotMapped);
            this.Property(a => a.Gender).HasDbType(DbType.Int32);

            /* Marks the column is timestamp type(sqlserver only) */
            //this.Property(a => a.RowVersion).IsTimestamp();
        }
    }

    public class CityMap : EntityTypeBuilder<City>
    {
        public CityMap()
        {
            this.HasMany(a => a.Users);
            this.HasOne(a => a.Province).HasForeignKey("ProvinceId");
        }
    }

    public class ProvinceMap : EntityTypeBuilder<Province>
    {
        public ProvinceMap()
        {
            this.HasMany(a => a.Cities);
        }
    }
}
